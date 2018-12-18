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
	/// 房屋控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class HouseController : Controller
	{
		private MyDbContext _context;
		private IConfiguration _configuration;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public HouseController(MyDbContext context, IConfiguration Configuration)
		{
			_context = context;
			_configuration = Configuration;
		}
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody]HouseDto model)
		{
			try
			{
				WxMap wxMap = new WxMap(_configuration);
				var fileName = await wxMap.CreatMapAsync(model.Latitude, model.Longitude);

				var info = new House()
				{
					Address = model.Address,
					Area = model.Area,
					HowArea = model.HowArea,
					BedInfo = model.BedInfo,
					CityId = model.CityId,
					CreatDate = DateTime.Now,
					HowBed = model.HowBed,
					HowCook = model.HowCook,
					HowHall = model.HowHall,
					HowRoom = model.HowRoom,
					HowWC = model.HowWC,
					Introduce = model.Introduce,
					Introduce_rim = model.Introduce_rim,
					IsHot = model.IsHot ? 1 : 0,
					IsSeal = model.IsSeal ? 1 : 0,
					Latitude = model.Latitude,
					Longitude = model.Longitude,
					PeopleNum = model.PeopleNum,
					Price = model.Price,
					ReceptionNum = model.ReceptionNum,
					UpDate = DateTime.Now,
					Name = model.Name,
					Logo = model.Logo,
					WeightValue = model.WeightValue,
					MapUrl = fileName
				};
				_context.Houses.Add(info);
				_context.SaveChanges();
				return new JsonResult("新增成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 查询设施、服务、须知
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult HouseFind([FromBody]HouseFindDto models)
		{
			try
			{
				if (models.Type == 1)
				{
					var list = _context.HouseDevices.Where(x => x.HouseId == models.HouseId).ToList();
					return new ObjectResult(list);
				}
				else if (models.Type == 2)
				{
					var list = _context.HouseServices.Where(x => x.HouseId == models.HouseId).ToList();
					return new ObjectResult(list);
				}
				else if (models.Type == 3)
				{
					var list = _context.HouseNotices.Where(x => x.HouseId == models.HouseId).ToList();
					return new ObjectResult(list);
				}
				else
					return BadRequest("查询类型不对");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 客房新增设施
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult HouseAddDevice([FromBody]List<DeviceArrayDto> models)
		{
			try
			{
				var list = _context.HouseDevices.Where(x => x.HouseId == models[0].HouseId).ToList();
				_context.HouseDevices.RemoveRange(list);
				_context.SaveChanges();
				//添加设施多对多表
				foreach (var p in models)
				{
					var housedevice = new HouseDevice()
					{
						HouseId = p.HouseId,
						DeviceId = p.Id
					};
					_context.HouseDevices.Add(housedevice);
				}
				_context.SaveChanges();
				return new JsonResult("新增成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 客房新增服务
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult HouseAddService([FromBody]List<ServiceArrayDto> models)
		{
			try
			{
				var list = _context.HouseServices.Where(x => x.HouseId == models[0].HouseId).ToList();
				_context.HouseServices.RemoveRange(list);
				_context.SaveChanges();
				//添加服务多对多表
				foreach (var p in models)
				{
					var houseservice = new HouseService()
					{
						HouseId = p.HouseId,
						ServiceId = p.Id
					};
					_context.HouseServices.Add(houseservice);
				}
				_context.SaveChanges();
				return new JsonResult("新增成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 客房新增须知
		/// </summary>
		/// <param name="models"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult HouseAddNotice([FromBody]List<NoticeArrayDto> models)
		{
			try
			{
				var list = _context.HouseNotices.Where(x => x.HouseId == models[0].HouseId).ToList();
				_context.HouseNotices.RemoveRange(list);
				_context.SaveChanges();
				//添加须知多对多表
				foreach (var p in models)
				{
					var housenotice = new HouseNotice()
					{
						HouseId = p.HouseId,
						NoticeId = p.Id
					};
					_context.HouseNotices.Add(housenotice);
				}
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
		public async Task<IActionResult> Modify([FromBody]HouseDto model)
		{
			try
			{
				var house = _context.Houses.Where(a => a.Id == model.Id).FirstOrDefault();
				if (house == null)
				{
					return BadRequest("查无此数据");
				}
				WxMap wxMap = new WxMap(_configuration);
				var fileName = await wxMap.CreatMapAsync(model.Latitude, model.Longitude);


				house.UpDate = DateTime.Now;
				house.Address = model.Address;
				house.Area = model.Area;
				house.HowArea = model.HowArea;
				house.BedInfo = model.BedInfo;
				house.CityId = model.CityId;
				house.HowBed = model.HowBed;
				house.HowCook = model.HowCook;
				house.HowHall = model.HowHall;
				house.HowRoom = model.HowRoom;
				house.HowWC = model.HowWC;
				house.Introduce = model.Introduce;
				house.Introduce_rim = model.Introduce_rim;
				house.IsHot = model.IsHot ? 1 : 0;
				house.IsSeal = model.IsSeal ? 1 : 0;
				house.Latitude = model.Latitude;
				house.Longitude = model.Longitude;
				house.PeopleNum = model.PeopleNum;
				house.Price = model.Price;
				house.ReceptionNum = model.ReceptionNum;
				house.Name = model.Name;
				house.Logo = model.Logo;
				house.WeightValue = model.WeightValue;
				house.MapUrl = fileName;
				_context.SaveChanges();
				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 上架
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Added(long Id)
		{
			try
			{
				var house = _context.Houses.Where(a => a.Id == Id).FirstOrDefault();
				if (house == null)
				{
					return BadRequest("查无此数据");
				}
				house.IsSeal = 1;
				_context.SaveChanges();
				return new JsonResult("上架成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 下架
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Bottom(long Id)
		{
			try
			{
				var house = _context.Houses.Where(a => a.Id == Id).FirstOrDefault();
				if (house == null)
				{
					return BadRequest("查无此数据");
				}
				house.IsSeal = 0;
				_context.SaveChanges();
				return new JsonResult("下架成功");
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
		public IActionResult GetHouses([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<HouseDto>> returnData = new GetPageDto<List<HouseDto>>();
				var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
				returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchstr)).Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var HouseList = (from a in _context.Houses
									 from b in _context.Citys
									 where a.Name.Contains(searchstr) && a.CityId == b.Id
									 select new HouseDto()
									 {
										 Id = a.Id,
										 Name = a.Name,
										 Address = a.Address,
										 CityName = b.Name,
										 CreatDate = a.CreatDate,
										 HowArea = a.HowArea,
										 HowBed = a.HowBed,
										 CollectNum = a.CollectNum,
										 CityId = a.CityId,
										 EvaluateNum = a.EvaluateNum,
										 HowCook = a.HowCook,
										 HowHall = a.HowHall,
										 HowRoom = a.HowRoom,
										 HowWC = a.HowWC,
										 Introduce = a.Introduce,
										 Introduce_rim = a.Introduce_rim,
										 IsHot = changeInt(a.IsHot) ,
										 IsSeal = changeInt(a.IsSeal),
										 Latitude = a.Latitude,
										 LikeNum = a.LikeNum,
										 Logo = a.Logo,
										 Longitude = a.Longitude,
										 PeopleNum = a.PeopleNum,
										 Price = a.Price,
										 ReceptionNum = a.ReceptionNum,
										 WeightValue = a.WeightValue,
										 Area = a.Area,
										 BedInfo = a.BedInfo,
										 UpDate = a.UpDate,
										 MapUrl = a.MapUrl,										
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
        public bool changeInt(int i)
        {
            if (i == 1) {
                return true;
            }
            else
            {
                return false;
            }
                 
        }
		/// <summary>
		/// 获取推荐房源列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpPost]
		public IActionResult GetRecHouses([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<HouseDto>> returnData = new GetPageDto<List<HouseDto>>();
				var searchstr = WebUtility.UrlEncode(pageDto.SearchContent);
				returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchstr) && a.IsHot == 1 && a.IsSeal == 1).Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var HouseList = (from a in _context.Houses
									 from b in _context.Citys
									 where a.Name.Contains(searchstr) && a.IsHot == 1 && a.IsSeal == 1
									 where a.CityId == b.Id
									 select new HouseDto()
									 {
										 Id = a.Id,
										 Name = a.Name,
										 HowBed = a.HowBed,
										 CityId = a.CityId,
										 CityName = b.Name,
										 HowCook = a.HowCook,
										 HowHall = a.HowHall,
										 HowRoom = a.HowRoom,
										 HowWC = a.HowWC,
										 Logo = a.Logo,
										 PeopleNum = a.PeopleNum,
										 Price = a.Price,
										 ReceptionNum = a.ReceptionNum,
										 WeightValue = a.WeightValue,
										 Area = a.Area,
										 BedInfo = a.BedInfo,
									 }).OrderByDescending(a => a.WeightValue).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
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
		/// 获取房源信息
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetHouseInfoById(long HouseId)
		{
			try
			{

				var house = (from a in _context.Houses
							 from b in _context.Citys
							 where a.Id == HouseId
							 where a.CityId == b.Id
							 select new HouseDto()
							 {
								 Id = a.Id,
								 Name = a.Name,
								 Address = a.Address,
								 CityName = b.Name,
								 CreatDate = a.CreatDate,
								 HowArea = a.HowArea,
								 HowBed = a.HowBed,
								 CollectNum = a.CollectNum,
								 CityId = a.CityId,
								 EvaluateNum = a.EvaluateNum,
								 HowCook = a.HowCook,
								 HowHall = a.HowHall,
								 HowRoom = a.HowRoom,
								 HowWC = a.HowWC,
								 Introduce = a.Introduce,
								 Introduce_rim = a.Introduce_rim,
                                 IsHot = changeInt(a.IsHot),
                                 IsSeal = changeInt(a.IsSeal),
                                 Latitude = a.Latitude,
								 LikeNum = a.LikeNum,
								 Logo = a.Logo,
								 Longitude = a.Longitude,
								 PeopleNum = a.PeopleNum,
								 Price = a.Price,
								 ReceptionNum = a.ReceptionNum,
								 WeightValue = a.WeightValue,
								 Area = a.Area,
								 BedInfo = a.BedInfo,
								 UpDate = a.UpDate,
								 MapUrl = a.MapUrl

							 }).FirstOrDefault();
				if (house == null)
				{
					return BadRequest("没有此房屋信息");
				}
				var devices = (from a in _context.HouseDevices
							   from b in _context.Devices
							   where a.HouseId == HouseId && b.Id == a.DeviceId
							   select new DeviceArrayDto
							   {
								   Content = b.Content,
                                   HouseContent = a.Content,
                                   Logo = b.Logo,
								   Name = b.Name,
							   }).ToList();
				var services = (from a in _context.HouseServices
								from b in _context.Services
								where a.HouseId == HouseId && b.Id == a.ServiceId
								select new ServiceArrayDto
								{
									Name = b.Name,
								}).ToList();
				var notices = (from a in _context.HouseNotices
							   from b in _context.Notices
							   where a.HouseId == HouseId && b.Id == a.NoticeId
							   select new NoticeArrayDto
							   {
								   Title = b.Title,
								   Content = b.Content,
							   }).ToList();
				var banners = _context.HouseBanners.Where(x => x.HouseId == HouseId).ToList();
				var unsub = (from a in _context.HouseUnsubs
							 select new HouseUnsubDto()
							 {
								 Name = a.Name,
								 Proportion = a.Proportion
							 }).FirstOrDefault();
				house.NoticeArray = notices;
				house.ServiceArray = services;
				house.DeviceArray = devices;
				house.BinnerArray = banners;
				house.Unsub = unsub;
				return new ObjectResult(house);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 获取房屋已租日期数组(大于当前日期)
		/// </summary>
		/// <param name="houseId"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetHouseIsRent(long houseId)
		{
			try
			{
				List<string> DateStrList = new List<string>();

				var infolist = (from a in _context.OrderDates
								where a.HouseId == houseId && a.Date > DateTime.Now.AddDays(-1)
								select a.Date.ToString("yyyy-MM-dd")).ToList();
				if (infolist != null)
				{
					DateStrList = infolist;
				}
				return Ok(DateStrList);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}
		/// <summary>
		/// 获取房屋已租日期数组(大于当前日期) date
		/// </summary>
		/// <param name="houseId"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetHouseIsRentDate(long houseId)
		{
			try
			{
				List<long> DateStrList = new List<long>();

				DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

				var infolist = (from a in _context.OrderDates
								where a.HouseId == houseId && a.Date > DateTime.Now.AddDays(-1)
								select Convert.ToInt64((a.Date.ToUniversalTime() - epoch).TotalSeconds)*1000).ToList();
				if (infolist != null)
				{
					DateStrList = infolist;
				}
				return Ok(DateStrList);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}
		/// <summary>
		/// 获取房屋评论列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpPost]
		public IActionResult GetHouseCommentList([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<HouseEvaluateDto>> returnData = new GetPageDto<List<HouseEvaluateDto>>();
				returnData.TotalCount = _context.HouseEvaluates.Where(a => a.HouseId == pageDto.HouseId && a.IsShow == 1).Count();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var CommentList = (from a in _context.HouseEvaluates

									   where a.HouseId == pageDto.HouseId
									   select new HouseEvaluateDto()
									   {
										   Id = a.Id,
										   Content = a.Content,
										   CreatDateStr = a.CreatDate.ToString("yyy-MM-dd"),
										   userDto = (from b in _context.Users
													  where b.Id == a.UserId
													  select new UserDto()
													  {
														  Id = b.Id,
														  We_Name = WebUtility.UrlDecode(b.We_Name),
														  We_AvtUrl = b.We_AvtUrl
													  }).FirstOrDefault()
									   }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
					returnData.BigField = CommentList;
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
