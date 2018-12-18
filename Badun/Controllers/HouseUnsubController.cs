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
using System.Net;

namespace Badun.Controllers
{

	/// <summary>
	/// 退订控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class HouseUnsubController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public HouseUnsubController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]HouseUnsubDto model)
        {
            try
            {
                var info = new HouseUnsub()
                {
                    Name = model.Name,
                    Days = model.Days,
                    Logo = model.Logo,
                    Proportion = model.Proportion
                };
                _context.HouseUnsubs.Add(info);
                _context.SaveChanges();
                return new JsonResult("新增成功");
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
        public IActionResult Modify([FromBody]HouseUnsubDto model)
        {
            try
            {
                var Service = _context.HouseUnsubs.Where(a => a.Id == model.Id).FirstOrDefault();
                if (Service == null)
                {
                    return BadRequest("查无此数据");
                }
                Service.Name = model.Name;
                Service.Proportion = model.Proportion;
                Service.Days = model.Days;
                Service.Logo = model.Logo;
                _context.SaveChanges();
                return new JsonResult("修改成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
		public IActionResult GetHouseUnsubs()
		{
			try
			{
                var services = _context.HouseUnsubs.ToList();
                return new ObjectResult(services);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
