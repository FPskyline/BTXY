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
	/// 城市控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class CityController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public CityController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]CityDto model)
        {
            try
            {
                var info = new City()
                {
                    Name = model.Name,
                    Key = model.Key,
                    IsHot = model.IsHot?1:0,
                    WeightValue = model.WeightValue
                };
                _context.Citys.Add(info);
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
        public IActionResult Modify([FromBody]CityDto model)
        {
            try
            {
                var city = _context.Citys.Where(a => a.Id == model.Id).FirstOrDefault();
                if (city == null)
                {
                    return BadRequest("查无此数据");
                }
                city.Name = model.Name;
                city.Key = model.Key;
                city.IsHot = model.IsHot ? 1 : 0;
                city.WeightValue = model.WeightValue;
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
                var city = _context.Citys.Where(a => a.Id == Id).FirstOrDefault();
                if (city == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Citys.Remove(city);
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
		public IActionResult GetCitys()
		{
			try
			{
                var citys = _context.Citys.ToList();
                return new ObjectResult(citys);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		/// <summary>
		/// 获取城市树-小程序
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetCityTree()
		{
			try
			{
				var list = new List<CityTreeDto>();
				var hotinfo = new CityTreeDto()
				{
					title = "热门城市",
					item = (from a in _context.Citys
							where a.IsHot == 1
							select new CityItemDto()
							{
								id = a.Id,
								name = a.Name,
								key = "热门"
							}).ToList()
				};
				list.Add(hotinfo);
				var BigTreeinfo = (from a in _context.Citys
								   group a by new { a.Key } into g
								   select new CityTreeDto()
								   {
									   title = g.Key.Key,
									   item = (from b in g
												   select new CityItemDto()
												   {
													   id = b.Id,
													   name = b.Name,
													   key = b.Key
												   }).ToList()
								   }).Distinct().ToList();
				var result = list.Concat(BigTreeinfo).ToList();
				return new ObjectResult(result);

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

	}
}
