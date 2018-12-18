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
	/// 广告控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class AdBannerController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public AdBannerController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]AdBannerDto model)
        {
            try
            {
                var info = new AdBanner()
                {
                    PicContent = model.PicContent,
                    PicPath = model.PicPath,
                    WeightValue = model.WeightValue,
                };
                _context.AdBanners.Add(info);
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
        public IActionResult Modify([FromBody]AdBannerDto model)
        {
            try
            {
                var AdBanner = _context.AdBanners.Where(a => a.Id == model.Id).FirstOrDefault();
                if (AdBanner == null)
                {
                    return BadRequest("查无此数据");
                }
                AdBanner.PicContent = model.PicContent;
                AdBanner.PicPath = model.PicPath;
                AdBanner.WeightValue = model.WeightValue;
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
                var AdBanner = _context.AdBanners.Where(a => a.Id == Id).FirstOrDefault();
                if (AdBanner == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.AdBanners.Remove(AdBanner);
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
		public IActionResult GetAdBanners()
		{
			try
			{
                var adBanners = _context.AdBanners.ToList();
                return new ObjectResult(adBanners);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
