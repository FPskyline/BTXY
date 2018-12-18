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
	/// 评价控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class HouseEvaluateController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public HouseEvaluateController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]HouseEvaluateDto model)
        {
            try
            {
                var Evalate = _context.HouseEvaluates.Where(x => x.OrderId == model.OrderId).FirstOrDefault();
                if (Evalate !=null)
                {
                    return BadRequest("此订单已评价");
                }
                var info = new HouseEvaluate()
                {
                    UserName = model.UserName,
                    UserId = model.UserId,
                    IsShow = 1,
                    Content = model.Content,
                    CreatDate = DateTime.Now,
                    HouseName = model.HouseName,
                    HouseId = model.HouseId,
                    UpDate = DateTime.Now,
                    OrderId = model.OrderId
                };
                _context.HouseEvaluates.Add(info);

				var order = _context.Orders.Where(x => x.Id == model.OrderId).FirstOrDefault();
				if (order == null)
				{
					return BadRequest("未找到该订单");
				}
				order.State = 4;

				_context.SaveChanges();
                return new JsonResult("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// 修改显示
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long Id)
        {
            try
            {
                var data = _context.HouseEvaluates.Where(a => a.Id == Id).FirstOrDefault();
                if (data== null)
                {
                    return BadRequest("没有此条评价");
                }
                data.IsShow = 2;
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
        [HttpPost]
		public IActionResult GetHouseEvaluates([FromBody] PageDto pageDto)
		{
			try
			{
                GetPageDto<List<HouseEvaluateDto>> returnData = new GetPageDto<List<HouseEvaluateDto>>();
                var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
                returnData.TotalCount = _context.HouseEvaluates.Where(a => a.HouseName.Contains(searchstr)).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var HouseList = (from a in _context.HouseEvaluates
                                     where a.HouseName.Contains(searchstr)
                                     select new HouseEvaluateDto()
                                     {
                                         Id = a.Id,
                                         Content = a.Content,
                                         HouseId = a.HouseId,
                                         HouseName = a.HouseName,
                                         IsShow = a.IsShow,
                                         OrderId = a.OrderId,
                                         UserId = a.UserId,
                                         UserName = a.UserName,
                                         CreatDate = a.CreatDate,
                                         UpDate = a.UpDate
                                     }).OrderByDescending(d => d.UpDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = HouseList;
                }
                return new ObjectResult(returnData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
        /// <summary>
        /// 用户获取评价列表
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult GetHouseEvaluatesByUser([FromBody] PageDto pageDto)
        {
            try
            {
                GetPageDto<List<HouseEvaluateDto>> returnData = new GetPageDto<List<HouseEvaluateDto>>();
                returnData.TotalCount = _context.HouseEvaluates.Where(a => a.UserId== pageDto.UserId ).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var HouseList = (from a in _context.HouseEvaluates
                                     where a.UserId == pageDto.UserId
                                     select new HouseEvaluateDto()
                                     {
                                         Id = a.Id,
                                         Content = a.Content,
                                         HouseId = a.HouseId,
                                         HouseName = a.HouseName,
                                         IsShow = a.IsShow,
                                         UserId = a.UserId,
                                         UserName = a.UserName,
                                         OrderId = a.OrderId,
                                         //CreatDate = a.CreatDate,
                                         CreatDateStr = a.CreatDate.ToString("yyyy-MM-dd"),
                                         UpDate = a.UpDate
                                     }).OrderByDescending(d => d.UpDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = HouseList;
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
