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
	/// 系统用户控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class Sys_UserController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public Sys_UserController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult AddSysUser([FromBody]Sys_UserDto model)
		{
			try
			{
				PwdTransition pwdTransition = new PwdTransition();
				var Salt = Guid.NewGuid().ToString();
				var Hashpwd = pwdTransition.ToHash("123456", Salt);
				var sysUserInfo = new Sys_User()
				{
					Account = model.Account,
					Password = Hashpwd,
					Salt = Salt,
					CreateDate = DateTime.Now,
					Phone = model.Phone,
					Logo = model.Logo,
					UpDate = DateTime.Now,
					Name = model.Name,
					IsDel = 0,
					RoleId = model.RoleId,
					RoleName = model.RoleName,
				};
				_context.Sys_Users.Add(sysUserInfo);
				_context.SaveChanges();
				return new JsonResult("添加成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult ModifySysUser([FromBody]Sys_UserDto model)
		{
			try
			{
				var ModifyObj = _context.Sys_Users.Where(i => i.Id == model.Id).FirstOrDefault();

				ModifyObj.Account = model.Account;
				ModifyObj.Phone = model.Phone;
				ModifyObj.UpDate = DateTime.Now;
				ModifyObj.Name = model.Name;
				ModifyObj.Logo = model.Logo;
				ModifyObj.RoleId = model.RoleId;
				ModifyObj.RoleName = model.RoleName;
				_context.SaveChanges();
				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取系统用户列表
		/// </summary>
		/// <param name="pageDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult GetSysUser([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<Sys_UserDto>> returnData = new GetPageDto<List<Sys_UserDto>>();
				returnData.TotalCount = _context.Sys_Users.Where(i => i.Name.Contains(pageDto.SearchContent) && i.Id != 1 && i.IsDel == 0).Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var SysUserList = (from a in _context.Sys_Users
									   where a.Name.Contains(pageDto.SearchContent) && a.Id != 1 && a.IsDel == 0
									   select new Sys_UserDto()
									   {
										   Id = a.Id,
										   Name = a.Name,
										   Phone = a.Phone,
										   Logo = a.Logo,
										   UpDate = a.UpDate.ToString("yyyy-MM-dd HH:mm:ss"),
										   CreateDate = a.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
										   Account = a.Account,
										   RoleId = a.RoleId,
										   RoleName = a.RoleName,
									   }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
					returnData.BigField = SysUserList;
				}
				return new ObjectResult(returnData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 删除系统用户
		/// </summary>
		/// <param name="id">系统用户id</param>
		/// <returns>返回结果</returns>
		[HttpDelete]
		public IActionResult DeleteSysUser(long id)
		{
			try
			{
				var Info = _context.Sys_Users.Where(d => d.Id == id).FirstOrDefault();
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
		/// 获取所有角色
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetRoles()
		{
			try
			{
				var rolelist = _context.Roles.Where(x => x.IsDel == 0).ToList();
				return Ok(rolelist);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取所有小程序
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetApplets()
		{
			try
			{
				var rolelist = _context.Applet.ToList();
				return Ok(rolelist);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// 系统登录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult SysLogin([FromBody]Sys_UserDto model)
		{
			try
			{
				var sysuserinfo = _context.Sys_Users.Where(x => x.Account == model.Account && x.IsDel == 0).FirstOrDefault();
				if (sysuserinfo == null)
				{
					return BadRequest("不存在此账号！");
				}
				PwdTransition pwdTransition = new PwdTransition();
				var Hashpwd = pwdTransition.ToHash(model.Password, sysuserinfo.Salt);
				if (sysuserinfo.Password != Hashpwd)
				{
					return BadRequest("密码不正确！请重新输入");
				}

				string validateNum = HttpContext.Session.GetString("Code_ValidateNum");
				HttpContext.Session.Remove("Code_ValidateNum");
				if (validateNum.ToUpper() != model.Code.ToUpper())
				{
					return BadRequest("输入验证码有误!");
				}
				var token = Guid.NewGuid().ToString();
				sysuserinfo.Token = token;
				//sysuserinfo.LoginIp = HttpContext.Connection.RemoteIpAddress.ToString();
				sysuserinfo.UpDate = DateTime.Now;
				_context.SaveChanges();
				//var loginInfo = sysuserinfo.SysUserId + "_" + token;
				return new JsonResult(sysuserinfo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult InitSysPassword([FromBody]Sys_UserDto model)
		{
			try
			{
				var ModifyObj = _context.Sys_Users.Where(i => i.Id == model.Id).FirstOrDefault();
				PwdTransition pwdTransition = new PwdTransition();
				var Salt = Guid.NewGuid().ToString();
				var Hashpwd = pwdTransition.ToHash(model.Password, Salt);
				ModifyObj.Salt = Salt;
				ModifyObj.Password = Hashpwd;
				ModifyObj.UpDate = DateTime.Now;

				_context.SaveChanges();
				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 检查密码正确与否
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult CheckPassword([FromBody]Sys_UserDto model)     
		{
			try
			{
				PwdTransition pwdTransition = new PwdTransition();
				var SysUser = _context.Sys_Users.Where(i => i.Id == model.Id).FirstOrDefault();
				var Hashpwd = pwdTransition.ToHash(model.Password, SysUser.Salt);
				var CheckedObj = _context.Sys_Users.Where(i => i.Password == Hashpwd).FirstOrDefault();
				if (CheckedObj == null)
				{
					return BadRequest("密码输入错误");
				}
				else
				{
					return new JsonResult("下一步");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 初始化系统用户密码
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult InitPwd(long id)
		{
			try
			{
				PwdTransition pwdTransition = new PwdTransition();
				var SysUser = _context.Sys_Users.Where(i => i.Id == id).FirstOrDefault();
				var Hashpwd = pwdTransition.ToHash("123456", SysUser.Salt);
				SysUser.Password = Hashpwd;
				_context.SaveChanges();
				return new JsonResult("初始化密码成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
