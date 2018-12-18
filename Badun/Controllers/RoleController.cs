using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Badun.Models.InitModels;
using Badun.Dto;
using Badun.Utility;
using Microsoft.AspNetCore.Http;
using Badun.Models;
using Microsoft.EntityFrameworkCore;

namespace Badun.Controllers
{

	/// <summary>
	/// 系统角色控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class RoleController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public RoleController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 获取角色列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpPost]
		public IActionResult GetRoles([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<Role>> returnData = new GetPageDto<List<Role>>();
				
					returnData.TotalCount = _context.Roles.Where(i => i.Name.Contains(pageDto.SearchContent) && i.Id != 1 && i.IsDel == 0).Count();
					if (pageDto.Page >= 1 && pageDto.Number > 0)
					{
						var RoleList = _context.Roles.Where(i => i.Name.Contains(pageDto.SearchContent) && i.Id != 1 && i.IsDel == 0).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
						returnData.BigField = RoleList;
					}
				
			
			

				return new ObjectResult(returnData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 新增角色
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpPost]
		public IActionResult CreateRole([FromBody]RoleDto roleDto)
		{
			try
			{
				var role = _context.Roles.Where(x => x.Name == roleDto.Name).FirstOrDefault();
				if (role != null)
				{
					return BadRequest("角色名重复");
				}
				//添加角色
				var RoleInfo = new Role()
				{
					Name = roleDto.Name,
					IsDel = 0,
					CreatDate = DateTime.Now,
					UpDate = DateTime.Now
				};
				_context.Roles.Add(RoleInfo);
				//添加多对多表
				foreach (var p in roleDto.CheckedArray)
				{
					var role_limit = new Role_Limit()
					{
						RoleId = RoleInfo.Id,
						LimitId = Convert.ToInt64(p.key)
					};
					_context.Role_Limits.Add(role_limit);

				}
				_context.SaveChanges();

				return new JsonResult("新增成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 修改角色
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpPost]
		public IActionResult EditRole([FromBody]RoleDto roleDto)
		{
			try
			{
				var role = _context.Roles.Where(x => x.Id == roleDto.Id).FirstOrDefault();
				if (role == null)
				{
					return BadRequest("该角色不存在");
				}
				role.Name = roleDto.Name;
				role.UpDate = DateTime.Now;
				if (roleDto.CheckedArray != null)
				{
					//此角色具有的权限
					var HaveRoleFunc = _context.Role_Limits.Where(x => x.RoleId == roleDto.Id).Select(c => c.LimitId).ToList();
					List<long> ChgRoleFunc = new List<long>();
					//此角色修改后有的权限
					foreach (var p in roleDto.CheckedArray)
					{
						ChgRoleFunc.Add(Convert.ToInt64(p.key));
					}
					//此角色修改后的权限与原权限重复的部分
					var sameRoleFunc = ChgRoleFunc.Intersect(HaveRoleFunc).ToList();
					//应该增加的权限
					var addFunc = ChgRoleFunc.Except(sameRoleFunc).ToList();
					foreach (var p in addFunc)
					{
						var role_limit = new Role_Limit
						{
							RoleId = roleDto.Id,
							LimitId = p
						};
						_context.Role_Limits.Add(role_limit);
					}
					//应该删除的权限
					var delFunc = HaveRoleFunc.Except(sameRoleFunc).ToList();
					foreach (var p in delFunc)
					{
						var role_auth = _context.Role_Limits.Where(x => x.RoleId == roleDto.Id && x.LimitId == p).FirstOrDefault();
						_context.Role_Limits.Remove(role_auth);
					}

				}
				_context.SaveChanges();

				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 删除角色
		/// </summary>
		/// <param name="id">角色id</param>
		/// <returns>返回结果</returns>
		[HttpDelete]
		public IActionResult DeleteRole(long id)
		{
			try
			{
				//获取需要删除的角色数据
				var Info = _context.Roles.Where(d => d.Id == id).FirstOrDefault();
				if (Info == null)
				{
					return BadRequest("没有该条数据！");
				}
				Info.IsDel = 1;
				_context.SaveChanges();
				return new JsonResult("删除成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取某一角色所有权限
		/// </summary>
		/// <param name="id">角色id</param>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetOneRoleLimit(long id)
		{
			try
			{
				var role_limits = _context.Role_Limits.Where(x => x.RoleId == id).ToList();
				var AuthArray = (from b in _context.Limits
								 select new CheckedArrayDto()
								 {
									 Id = b.Id,
									 IsHave = (from c in role_limits
											   where c.LimitId == b.Id
											   select b
										 ).Any()
								 }).ToList();
				return Ok(AuthArray);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
