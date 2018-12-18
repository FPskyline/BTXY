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
using Microsoft.Extensions.Configuration;

namespace Badun.Controllers
{

	/// <summary>
	/// 用户控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class UserController : Controller
	{
		private MyDbContext _context;
        private IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserController(MyDbContext context, IConfiguration Configuration)
		{
			_context = context;
            _configuration = Configuration;
        }
		/// <summary>
		/// 新增/修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Creat([FromBody]UserDto model)
		{
			try
			{
				var userinfo = _context.Users.Where(x => x.We_OpenId == model.We_OpenId).FirstOrDefault();
				var token = Guid.NewGuid().ToString();
				if (userinfo == null && model.We_OpenId != null)
				{
					var info = new User()
					{
						We_AvtUrl = model.We_AvtUrl,
						We_Name = WebUtility.UrlEncode(model.We_Name),
						We_OpenId = model.We_OpenId,
						CreateDate = DateTime.Now,
						UpDate = DateTime.Now,
						Token = token,
						Integral = 0
					};
					_context.Users.Add(info);
					_context.SaveChanges();
					return new ObjectResult(info);
				}
				else if (userinfo != null && model.We_OpenId != null)
				{
					userinfo.We_AvtUrl = model.We_AvtUrl;
					userinfo.We_Name = WebUtility.UrlEncode(model.We_Name);
					userinfo.UpDate = DateTime.Now;
					userinfo.Token = token;
					_context.SaveChanges();
					return new ObjectResult(userinfo);
				}
				else
				{
					return BadRequest("未找到openid");
				}

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        /// <summary>
        /// openID获取用户信息
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetUserInfo(string OpenID)
        {
            try
            {
                var user = _context.Users.Where(x=>x.We_OpenId == OpenID).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("没有此用户");
                }
                return new ObjectResult(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
		public IActionResult GetMember([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<UserDto>> returnData = new GetPageDto<List<UserDto>>();
				var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
				returnData.TotalCount = _context.Users.Where(a => a.We_Name.Contains(searchstr)).Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var MemberList = (from a in _context.Users
									  where a.We_Name.Contains(searchstr)
									  select new UserDto()
									  {
										  Id = a.Id,
										  We_Name = WebUtility.UrlDecode(a.We_Name),
										  We_AvtUrl = a.We_AvtUrl,
										  CreateDate = a.CreateDate,
										  UpDate = a.UpDate,
                                          IDNum = a.IDNum,
                                          IsCert = a.IsCert,
                                          IsReal = a.IsReal,
                                          PhoneNum = a.PhoneNum,
                                          UserName = a.UserName,
                                          Integral =a .Integral
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
        /// 用户实名认证
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult UserCert([FromBody] CertDto model)
        {
            try
            {
                var user = _context.Users.Where(x => x.Id == model.Id).FirstOrDefault();
                if (user == null)
                {
                    return BadRequest("没有此用户");
                }
                //实名认证过程 身份证 手机号 
                var codeInfo = _context.SmsCodes.Where(x => x.Id == model.CodeId).FirstOrDefault();
                if (codeInfo == null)
                {
                    return BadRequest("此验证码不存在");
                }
                if (codeInfo.Code != model.PhoneCode)
                {
                    return BadRequest("此验证码不对");
                }
                user.IsCert = 1; //手机号验证成功
                _context.SmsCodes.Remove(codeInfo);
                _context.SaveChanges();
                //------------------------------------
                Cert sendcert = new Cert(_configuration);
                var rep = sendcert.SendCert(model.PhoneNum, model.UserName, model.IDNum);
                if (rep.code !=10000)
                {
                    return BadRequest("实名认证接口调用失败");
                }
                var infos = new CertInfo
                {
                    addrCode = rep.result.addrCode,
                    area = rep.result.area,
                    birthday = rep.result.birthday,
                    city = rep.result.city,
                    idCard = rep.result.idCard,
                    lastCode = rep.result.lastCode,
                    status = rep.result.status,
                    sex = rep.result.sex,
                    mobile = rep.result.mobile,
                    mobileType = rep.result.mobileType,
                    msg = rep.result.msg,
                    name = rep.result.name,
                    prefecture = rep.result.prefecture,
                    province = rep.result.province,
                };
                _context.CertInfos.Add(infos);
                if (rep.result.status != "01")
                {
                    return BadRequest("实名认证不通过");
                }
                user.IDNum = model.IDNum;
                user.IsReal = 1;
                user.PhoneNum = model.PhoneNum;
                user.UserName = model.UserName;
                user.IsCert = 1;
                _context.SaveChanges();
                return new ObjectResult(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SendSms(string Phone)
        {
            try
            {
                Random rm = new Random();
                var s = Convert.ToString(rm.Next(1000, 9999));
                Sms sendSms = new Sms(_configuration);
                var rep = sendSms.SendSms(Phone, "您的验证码为：" + s + ",为了保护您的账户安全，验证码请勿转发他人【巴顿行辕】");
                var info = new SmsCode()
                {
                    Code = s.ToString(),
                };
                if (rep.success ==true)
                {
                    _context.SmsCodes.Add(info);
                    _context.SaveChanges();
                }
                return new JsonResult(info.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 实名认证
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="name"></param>
        /// <param name="idNo"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SendCert(string Phone, string name, string idNo)
        {
            try
            {
                Random rm = new Random();
                var s = Convert.ToString(rm.Next(1000, 9999));
                Cert sendcert = new Cert(_configuration);
                HttpContext.Session.SetString("CodeString", s);
                var rep = sendcert.SendCert(Phone, name, idNo);
                return new ObjectResult(rep);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
