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
	/// 须知控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class NoticeController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public NoticeController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]NoticeDto model)
        {
            try
            {
                var info = new Notice()
                {
                    Content = model.Content,
                    Title = model.Title,
                };
                _context.Notices.Add(info);
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
        public IActionResult Modify([FromBody]NoticeDto model)
        {
            try
            {
                var Notice = _context.Notices.Where(a => a.Id == model.Id).FirstOrDefault();
                if (Notice == null)
                {
                    return BadRequest("查无此数据");
                }
                Notice.Title = model.Title;
                Notice.Content = model.Content;
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
                var Notice = _context.Notices.Where(a => a.Id == Id).FirstOrDefault();
                if (Notice == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Notices.Remove(Notice);
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
		public IActionResult GetNotices()
		{
			try
			{
                var notices = _context.Notices.ToList();
                return new ObjectResult(notices);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
