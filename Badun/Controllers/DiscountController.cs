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
	public class DiscountController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public DiscountController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]DiscountDto model)
        {
            try
            {
                var info = new Discount()
                {
                   Days = model.Days,
                   DisValue = model.DisValue,
                };
                _context.Discounts.Add(info);
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
        public IActionResult Modify([FromBody]DiscountDto model)
        {
            try
            {
                var discount = _context.Discounts.Where(a => a.Id == model.Id).FirstOrDefault();
                if (discount == null)
                {
                    return BadRequest("查无此数据");
                }
                discount.DisValue = model.DisValue;
                discount.Days = model.Days;
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
                var count = _context.Discounts.Count();
                if (count <=1 )
                {
                    return BadRequest("必须得有一条数据");
                }
                var discount = _context.Discounts.Where(a => a.Id == Id).FirstOrDefault();
                if (discount == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Discounts.Remove(discount);
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
		public IActionResult GetDiscounts()
		{
			try
			{
                var discounts = _context.Discounts.ToList();
                return new ObjectResult(discounts);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
