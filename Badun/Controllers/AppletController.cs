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
	/// 小程序参数控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class AppletController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public AppletController(MyDbContext context)
		{
			_context = context;
		}
		
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult EditApplet([FromBody]AppletDto model)
		{
			try
			{
				var ModifyObj = _context.Applet.Where(i => i.Id == model.Id).FirstOrDefault();

				ModifyObj.UpDate = DateTime.Now;
				ModifyObj.Name = model.Name;
				ModifyObj.Logo = model.Logo;
				ModifyObj.appid = model.appid;
				ModifyObj.secret = model.secret;
				ModifyObj.sub_mch_id = model.sub_mch_id;
				_context.SaveChanges();
				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取小程序列表
		/// </summary>
		/// <param name="pageDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult GetApplets([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<AppletDto>> returnData = new GetPageDto<List<AppletDto>>();
				returnData.TotalCount = _context.Applet.Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var AppletList = (from a in _context.Applet
									  select new AppletDto()
									  {
										  Id = a.Id,
										  Name = a.Name,
										  Logo = a.Logo,
										  appid = a.appid,
										  secret = a.secret,
										  sub_mch_id = a.sub_mch_id,
										  UpDate = a.UpDate.ToString("yyyy-MM-dd HH:mm:ss"),
										  CreateDate = a.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
									  }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
					returnData.BigField = AppletList;
				}
				return new ObjectResult(returnData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
