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
	/// 收藏控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class UserCollectController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public UserCollectController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 增加/取消收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]UserCollectDto model)
        {
            try
            {
				var collectinfo = _context.UserCollects.Where(x => x.HouseId == model.HouseId && x.UserId == model.UserId).FirstOrDefault();
				if(collectinfo == null)
				{
					var info = new UserCollect()
					{
						CreateDate = DateTime.Now,
						HouseId = model.HouseId,
						HouseName = model.HouseName,
						UpDate = DateTime.Now,
						UserId = model.UserId,
						UserName = model.UserName,
					};
					_context.UserCollects.Add(info);
					_context.SaveChanges();
					return new JsonResult("收藏成功");
				}
				else
				{
					_context.UserCollects.Remove(collectinfo);
					_context.SaveChanges();
					return new JsonResult("取消收藏");
				}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		/// <summary>
		/// 查看房屋是否被某人收藏
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public IActionResult GetUserCollect([FromBody]UserCollectDto model)
		{
			try
			{
				var collectinfo = _context.UserCollects.Where(x => x.HouseId == model.HouseId && x.UserId == model.UserId).FirstOrDefault();
				if(collectinfo == null)
				{
					return new JsonResult("未收藏");
				}
				else
				{
					return new JsonResult("已收藏");
				}
				
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
		public IActionResult GetUserCollects([FromBody] PageDto pageDto)
		{
			try
			{
                GetPageDto<List<UserCollectDto>> returnData = new GetPageDto<List<UserCollectDto>>();
                var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
                returnData.TotalCount = _context.UserCollects.Where(a=>a.UserName.Contains(searchstr)).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var MemberList = (from a in _context.UserCollects
                                      from b in _context.Houses
                                      from c in _context.Users
                                      where a.UserName.Contains(searchstr)
                                      where b.Id ==a.HouseId
                                      where c.Id == a.UserId
                                      select new UserCollectDto()
                                      {
                                          Id = a.Id,
                                          UserId = a.UserId,
                                          UserName = WebUtility.UrlDecode(c.We_Name),
                                          HouseId =a.HouseId,
                                          HouseName = b.Name,
                                          CreateDate = a.CreateDate,
                                          UpDate = a.UpDate
                                      }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = MemberList;
                }
                return new ObjectResult(returnData);
            }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
        /// <summary>
        /// 获取我的收藏列表
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult GetUserCollectsByUserID([FromBody] PageDto pageDto)
        {
            try
            {
                GetPageDto<List<UserCollectByUserID>> returnData = new GetPageDto<List<UserCollectByUserID>>();
                returnData.TotalCount = _context.UserCollects.Where(a => a.UserId==pageDto.UserId).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var collectList = (from a in _context.UserCollects
                                      from  b in _context.Houses
                                      where a.UserId == pageDto.UserId && b.Id == a.HouseId
                                      select new UserCollectByUserID()
                                      {
                                          UserId = a.UserId,
                                          Area =b.Area,
                                          BedInfo = b.BedInfo,
                                          HowArea= b.HowArea,
                                          HowBed = b.HowBed,
                                          HowCook = b.HowCook,
                                          HowHall = b.HowHall,
                                          HowRoom = b.HowRoom,
                                          HowWC = b.HowWC,
                                          Logo = b.Logo,
                                          Name = b.Name,
                                          Price = b.Price,
                                          PeopleNum = b.PeopleNum,
                                          HouseId = a.HouseId,
                                      }).OrderByDescending(d => d.HouseId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = collectList;
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
