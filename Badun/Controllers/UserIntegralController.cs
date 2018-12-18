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
	/// 积分控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class UserIntegralController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public UserIntegralController(MyDbContext context)
		{
			_context = context;
		}
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]UserIntegralDto model)
        {
            try
            {
                var integral = _context.UserIntegrals.Where(x => x.CreateDate.ToShortDateString() == DateTime.Now.ToShortDateString()).FirstOrDefault();
                if (integral != null ) {
                    return BadRequest("今日签到已经记录");
                }
                var info = new UserIntegral()
                {
                   CreateDate = DateTime.Now,
                   Integral = 1,
                   Type = 1,
                   UpDate = DateTime.Now,
                   UserId = model.UserId,
                   UserName = model.UserName,
                };
                _context.UserIntegrals.Add(info);
                _context.SaveChanges();
                var user = _context.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
                user.Integral = user.Integral+1;
                _context.SaveChanges();
                return new JsonResult("新增成功");
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
		public IActionResult GetUserIntegrals([FromBody] PageDto pageDto)
		{
			try
			{
                GetPageDto<List<UserIntegralDto>> returnData = new GetPageDto<List<UserIntegralDto>>();
                var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
                returnData.TotalCount = _context.UserIntegrals.Where(a=>a.UserName.Contains(searchstr)).Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var MemberList = (from a in _context.UserIntegrals
                                      where a.UserName.Contains(searchstr)
                                      select new UserIntegralDto()
                                      {
                                          Id = a.Id,
                                          Type = a.Type,
                                          Integral = a.Integral,
                                          UserId = a.UserId,
                                          UserName = a.UserName,
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


	}
}
