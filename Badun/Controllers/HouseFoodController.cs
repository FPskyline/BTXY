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
	/// 周边控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class HouseFoodController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public HouseFoodController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]HouseFoodDto model)
        {
            try
            {
                var info = new HouseFood()
                {
                    Content = model.Content,
                    PicPath = model.PicPath,
                    HouseId = model.HouseId,
                    Name = model.Name,
                    WeightValue = model.WeightValue,
                };
                _context.HouseFoods.Add(info);
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
        public IActionResult Modify([FromBody]HouseFoodDto model)
        {
            try
            {
                var houseFood = _context.HouseFoods.Where(a => a.Id == model.Id).FirstOrDefault();
                if (houseFood == null)
                {
                    return BadRequest("查无此数据");
                }
                houseFood.Content = model.Content;
                houseFood.PicPath = model.PicPath;
                houseFood.WeightValue = model.WeightValue;
                houseFood.Name = model.Name;
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
                var houseFood = _context.HouseFoods.Where(a => a.Id == Id).FirstOrDefault();
                if (houseFood == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.HouseFoods.Remove(houseFood);
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
		public IActionResult GetHouseFoods(long houseId)
		{
			try
			{
                var houseFoods = _context.HouseFoods.Where(x=>x.HouseId == houseId).ToList();
                return new ObjectResult(houseFoods);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


	}
}
