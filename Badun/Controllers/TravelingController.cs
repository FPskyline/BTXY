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
	/// 旅游控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class TravelingController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public TravelingController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]TravelingDto model)
        {
            try
            {
                var info = new Traveling()
                {
                    Banner = model.Banner,
                    CityId = model.CityId,
                    Content = model.Content,
                    CreatDate = DateTime.Now,
                    IsHot = model.IsHot?1:0,
                    Logo = model.Logo,
                    Title = model.Title,
                    UpDate = DateTime.Now,
                    Video = model.Video,
                    //ClickNum = model.ClickNum,
                    //ZanNum = model.ZanNum,
                    WeightValue = model.WeightValue,
                };
                _context.Travelings.Add(info);
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
        public IActionResult Modify([FromBody]TravelingDto model)
        {
            try
            {
                var traveling = _context.Travelings.Where(a => a.Id == model.Id).FirstOrDefault();
                if (traveling == null)
                {
                    return BadRequest("查无此数据");
                }
                traveling.Banner = model.Banner;
                traveling.CityId = model.CityId;
                traveling.ClickNum = model.ClickNum;
                traveling.ZanNum = model.ZanNum;
                traveling.CityId = model.CityId;
                traveling.Content = model.Content;
                traveling.IsHot = model.IsHot ? 1 : 0;
                traveling.Logo = model.Logo;
                traveling.Title = model.Title;
                traveling.Video = model.Video;
                traveling.UpDate = DateTime.Now;
                traveling.WeightValue = model.WeightValue;
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
                var traveling = _context.Travelings.Where(a => a.Id == Id).FirstOrDefault();
                if (traveling == null)
                {
                    return BadRequest("查无此数据");
                }
                _context.Travelings.Remove(traveling);
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
		public IActionResult GetTravelings([FromBody] PageDto pageDto)
		{
			try
			{
                GetPageDto<List<TravelingDto>> returnData = new GetPageDto<List<TravelingDto>>();
                var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
                returnData.TotalCount = _context.Travelings.Where(a => a.Title.Contains(searchstr)).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var TravelingList = (from a in _context.Travelings
                                         from b in _context.Citys
                                         where a.Title.Contains(searchstr) && b.Id == a.CityId
                                      select new TravelingDto()
                                      {
                                          Id = a.Id,
                                          Title = WebUtility.UrlDecode(a.Title),
                                          Banner = a.Banner,
                                          CityId = a.CityId,
                                          CityName = b.Name,
                                          ClickNum = a.ClickNum,
                                          Content = a.Content,
                                          CreatDate = a.CreatDate,
                                          CreatDateStr = a.CreatDate.ToString("yyyy-MM-dd"),
                                          IsHot = changeInt(a.IsHot),
                                          Logo = a.Logo,
                                          Video = a.Video,
                                          ZanNum = a.ZanNum,
                                          WeightValue = a.WeightValue,
                                          UpDate = a.UpDate
                                      }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = TravelingList;
                }
                return new ObjectResult(returnData);
            }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
        /// <summary>
        /// 通过Id获取对应数据
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetTravelingById(long Id)
        {
            try
            {

                var travelings = (from a in _context.Travelings
                                     from b in _context.Citys
                                     where a.Id == Id && b.Id == a.CityId
                                     select new TravelingDto()
                                     {
                                         Id = a.Id,
                                         Title = WebUtility.UrlDecode(a.Title),
                                         Banner = a.Banner,
                                         CityId = a.CityId,
                                         CityName = b.Name,
                                         ClickNum = a.ClickNum,
                                         Content = a.Content,
                                         CreatDate = a.CreatDate,
                                         CreatDateStr = a.CreatDate.ToString("yyyy-MM-dd"),
                                         IsHot = changeInt(a.IsHot),
                                         Logo = a.Logo,
                                         Video = a.Video,
                                         ZanNum = a.ZanNum,
                                         WeightValue = a.WeightValue,
                                         UpDate = a.UpDate
                                     }).FirstOrDefault();
                return new ObjectResult(travelings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 通过userId获取对应数据
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult GetUserTravelingList([FromBody] PageDto pageDto)
        {
            try
            {
                GetPageDto<List<TravelingDto>> returnData = new GetPageDto<List<TravelingDto>>();
                var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
                returnData.TotalCount = _context.UserTravelings.Where(a => a.UserId ==pageDto.UserId ).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var TravelingList = (from a in _context.Travelings
                                         from b in _context.UserTravelings
                                         from c in _context.Citys
                                         where b.UserId == pageDto.UserId
                                         where a.Id == b.TravelingId
                                         where c.Id == a.CityId

                                         select new TravelingDto()
                                         {
                                             Id = a.Id,
                                             Title = WebUtility.UrlDecode(a.Title),
                                             Banner = a.Banner,
                                             CityId = a.CityId,
                                             CityName = c.Name,
                                             ClickNum = a.ClickNum,
                                             Content = a.Content,
                                             CreatDate = a.CreatDate,
                                             CreatDateStr = a.CreatDate.ToString("yyyy-MM-dd"),
                                             IsHot = changeInt(a.IsHot),
                                             Logo = a.Logo,
                                             Video = a.Video,
                                             ZanNum = a.ZanNum,
                                             WeightValue = a.WeightValue,
                                             UpDate = a.UpDate
                                         }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = TravelingList;
                }
                return new ObjectResult(returnData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public bool changeInt(int i)
        {
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 点击阅读量
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult TravelingClick(long Id)
        {
            try
            {

                var travelings = _context.Travelings.Where(x=>x.Id == Id).FirstOrDefault();
                travelings.ClickNum = travelings.ClickNum + 1;
                _context.SaveChanges();
                return new JsonResult("点击成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 赞数量
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult TravelingCheck(long Id, long UserId)
        {
            try
            {
                var usertraveling = _context.UserTravelings.Where(x => x.TravelingId == Id && x.UserId == UserId).FirstOrDefault();
                if (usertraveling == null)
                {              
                    return new JsonResult("nocollected");
                }
                else
                {
                    return new JsonResult("collected");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 赞数量
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult TravelingZan(long Id,long UserId)
        {
            try
            {
                var usertraveling = _context.UserTravelings.Where(x => x.TravelingId == Id && x.UserId == UserId).FirstOrDefault();
                if (usertraveling == null)
                {
                    var info = new UserTraveling
                    {
                        UserId = UserId,
                        TravelingId = Id,
                    };
                    _context.UserTravelings.Add(info);
                    var travelings = _context.Travelings.Where(x => x.Id == Id).FirstOrDefault();
                    travelings.ZanNum = travelings.ZanNum + 1;
                    _context.SaveChanges();
                    return new JsonResult("喜欢成功");
                }
                else
                {
                    _context.UserTravelings.Remove(usertraveling);
                    var travelings = _context.Travelings.Where(x => x.Id == Id).FirstOrDefault();
                    travelings.ZanNum = travelings.ZanNum - 1;
                    _context.SaveChanges();
                    return new JsonResult("取消喜欢");
                }
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
