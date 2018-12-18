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
	/// 服务控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class ServiceController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public ServiceController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]ServiceDto model)
        {
            try
            {
                var info = new Service()
                {
                    Name = model.Name,
                };
                _context.Services.Add(info);
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
        public IActionResult Modify([FromBody]ServiceDto model)
        {
            try
            {
                var Service = _context.Services.Where(a => a.Id == model.Id).FirstOrDefault();
                if (Service == null)
                {
                    return BadRequest("查无此数据");
                }
                Service.Name = model.Name;
                _context.SaveChanges();
                return new JsonResult("修改成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long Id)
        {
            try
            {
                var Service = _context.Services.Where(a => a.Id == Id).FirstOrDefault();
                if (Service == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Services.Remove(Service);
                _context.SaveChanges();
                return new JsonResult("删除成功");
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
		public IActionResult GetServices()
		{
			try
			{
                var services = _context.Services.ToList();
                return new ObjectResult(services);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
