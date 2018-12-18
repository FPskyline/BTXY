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
	/// 设施控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class DeviceController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public DeviceController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]DeviceDto model)
        {
            try
            {
                var info = new Device()
                {
                    Content = model.Content,
                    Logo = model.Logo,
                    Name = model.Name,
                };
                _context.Devices.Add(info);
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
        public IActionResult Modify([FromBody]DeviceDto model)
        {
            try
            {
                var device = _context.Devices.Where(a => a.Id == model.Id).FirstOrDefault();
                if (device == null)
                {
                    return BadRequest("查无此数据");
                }
                device.Logo = model.Logo;
                device.Name = model.Name;
                device.Content = model.Content;
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
                var device = _context.Devices.Where(a => a.Id == Id).FirstOrDefault();
                if (device == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Devices.Remove(device);
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
		public IActionResult GetDevices()
		{
			try
			{
                var devices = _context.Devices.ToList();
                return new ObjectResult(devices);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
