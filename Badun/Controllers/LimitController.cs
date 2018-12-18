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
	/// 权限控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class LimitController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public LimitController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 获取权限树
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetLimitTree()
		{
			try
			{	
				var BigTreeinfo = (from a in _context.Limits
								   group a by new { a.Type, a.TypeName } into g
								   select new LimitTreeDto()
								   {
									   title = g.Key.TypeName,
									   children = (from b in g
												   select new ChildrenDto()
												   {
													   title = b.Name,
													   key = b.Id.ToString(),
													   isLeaf = true,
													   @checked = false
												   }).ToList()
								   }).ToList();
				return new ObjectResult(BigTreeinfo);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 获取某一角色权限树
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetOneLimitTree(long id)
		{
			try
			{
				var role_auths = _context.Role_Limits.Where(x => x.RoleId == id).ToList();
				var BigTreeinfo = (from a in _context.Limits
								   group a by new { a.Type, a.TypeName } into g
								   select new LimitTreeDto()
								   {
									   title = g.Key.TypeName,
									   children = (from b in g												  
												   select new ChildrenDto()
												   {
													   title = b.Name,
													   key = b.Id.ToString(),
													   isLeaf = true,
													   @checked = (from c in role_auths
																   where c.LimitId == b.Id
																   select c
																   ).Any()
												   }).ToList()
								   }).Distinct().ToList();
				return new ObjectResult(BigTreeinfo);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
