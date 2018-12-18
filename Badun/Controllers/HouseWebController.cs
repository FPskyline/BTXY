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

namespace Badun.Controllers
{

	/// <summary>
	/// huose  web端控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class HouseWebController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public HouseWebController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 获取全部设施列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetAllDevice(long Id)
		{
			try
			{
				var housedevice = _context.HouseDevices.Where(x => x.HouseId == Id).FirstOrDefault();
				var info = new List<DeviceDto>();
				if (housedevice == null)
				{
					info = (from a in _context.Devices
							select new DeviceDto()
							{
								Id = a.Id,
								Logo = a.Logo,
								Name = a.Name,
								value = a.Name,
								label = a.Name,
								@checked = 0
							}).ToList();
				}
				else
				{
					info = (from a in _context.Devices
							select new DeviceDto()
							{
								Id = a.Id,
								Logo = a.Logo,
								Name = a.Name,
								value = a.Name,
								label = a.Name,
								@checked = (from b in _context.HouseDevices
											where b.HouseId == Id && b.DeviceId == a.Id
											select b 
											).Count()
							}).ToList();
				}

				return new ObjectResult(info);


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
        /// <summary>
		/// 修改房子-设施信息  多对多
		/// </summary>
		/// <param name="roleDto"></param>
		/// <returns></returns>
		[HttpPost]
        public IActionResult EditDeviceInfo([FromBody]List<HouseDeviceInfoDto> house_InfoDto)
        {
            try
            {
                if (house_InfoDto.Count != 0)
                {                   
                    foreach (var p in house_InfoDto)
                    {
                        var house_deviceinfo = _context.HouseDevices.Where(x => x.HouseId == p.HouseId && x.DeviceId == p.DeviceId).FirstOrDefault();
                        house_deviceinfo.Content = p.Content;
                        _context.SaveChanges();
                    }
                }
                return new JsonResult("修改成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 修改房子-设施信息  多对多
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDeviceInfo(long Id)
        {
            try
            {
                var infoList = (from a in _context.HouseDevices
                                from b in _context.Devices
                                where a.HouseId == Id && b.Id == a.DeviceId
                                select new DeviceInfoDto()
                                 {
                                     HouseId = a.HouseId,
                                     Name = b.Name,
                                    DeviceId = a.DeviceId,
                                    Content = a.Content
                                 }).ToList();
                return new ObjectResult(infoList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 修改房子-设施  多对多
        /// </summary>
        /// <param name="roleDto"></param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult EditDevice([FromBody]House_DeviceDto house_DeviceDto)
		{
			try
			{
				if (house_DeviceDto.deviceDto != null)
				{
					//此房子具有的设备
					var HaveFunc = _context.HouseDevices.Where(x => x.HouseId == house_DeviceDto.HouseId).Select(c => c.DeviceId).ToList();
					if(HaveFunc == null) 
					{
						//新增
						//添加多对多表
						foreach (var p in house_DeviceDto.deviceDto)
						{
							var house_device = new HouseDevice()
							{
								HouseId = house_DeviceDto.HouseId,
								DeviceId = Convert.ToInt64(p.Id)
							};
							_context.HouseDevices.Add(house_device);

						}
					}
					else 
					{
						//修改
						List<long> ChgFunc = new List<long>();
						//此房子修改后具有的设备
						foreach (var p in house_DeviceDto.deviceDto)
						{
							ChgFunc.Add(Convert.ToInt64(p.Id));
						}
						//此房子修改后的设备与原设备重复的部分
						var sameFunc = ChgFunc.Intersect(HaveFunc).ToList();
						//应该增加的设备
						var addFunc = ChgFunc.Except(sameFunc).ToList();
						foreach (var p in addFunc)
						{
							var housedevice = new HouseDevice
							{
								HouseId = house_DeviceDto.HouseId,
								DeviceId = p
							};
							_context.HouseDevices.Add(housedevice);
						}
						//应该删除的设备
						var delFunc = HaveFunc.Except(sameFunc).ToList();
						foreach (var p in delFunc)
						{
							var house_device = _context.HouseDevices.Where(x => x.HouseId == house_DeviceDto.HouseId && x.DeviceId == p).FirstOrDefault();
							_context.HouseDevices.Remove(house_device);
						}
					}
				

				}
				_context.SaveChanges();

				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取全部服务列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetAllService(long Id)
		{
			try
			{
				var houseservice = _context.HouseServices.Where(x => x.HouseId == Id).FirstOrDefault();
				var info = new List<ServiceDto>();
				if (houseservice == null)
				{
					info = (from a in _context.Services
							select new ServiceDto()
							{
								Id = a.Id,
								Name = a.Name,
								value = a.Name,
								label = a.Name,
								@checked = false
							}).ToList();
				}
				else
				{
					info = (from a in _context.Services
							select new ServiceDto()
							{
								Id = a.Id,
								Name = a.Name,
								value = a.Name,
								label = a.Name,
								@checked = (from b in _context.HouseServices
											where b.HouseId == Id && b.ServiceId == a.Id
											select b
											).Any()
							}).ToList();
				}

				return new ObjectResult(info);


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 修改房子-服务  多对多
		/// </summary>
		/// <param name="roleDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult EditService([FromBody]House_ServiceDto house_ServiceDto)
		{
			try
			{
				if (house_ServiceDto.serviceDto != null)
				{
					//此房子具有的服务
					var HaveFunc = _context.HouseServices.Where(x => x.HouseId == house_ServiceDto.HouseId).Select(c => c.ServiceId).ToList();
					if (HaveFunc == null)
					{
						//新增
						//添加多对多表
						foreach (var p in house_ServiceDto.serviceDto)
						{
							var house_Service = new HouseService()
							{
								HouseId = house_ServiceDto.HouseId,
								ServiceId = Convert.ToInt64(p.Id)
							};
							_context.HouseServices.Add(house_Service);

						}
					}
					else
					{
						//修改
						List<long> ChgFunc = new List<long>();
						//此房子修改后具有的服务
						foreach (var p in house_ServiceDto.serviceDto)
						{
							ChgFunc.Add(Convert.ToInt64(p.Id));
						}
						//此房子修改后的服务与原服务重复的部分
						var sameFunc = ChgFunc.Intersect(HaveFunc).ToList();
						//应该增加的服务
						var addFunc = ChgFunc.Except(sameFunc).ToList();
						foreach (var p in addFunc)
						{
							var houseService = new HouseService
							{
								HouseId = house_ServiceDto.HouseId,
								ServiceId = p
							};
							_context.HouseServices.Add(houseService);
						}
						//应该删除的服务
						var delFunc = HaveFunc.Except(sameFunc).ToList();
						foreach (var p in delFunc)
						{
							var house_Service = _context.HouseServices.Where(x => x.HouseId == house_ServiceDto.HouseId && x.ServiceId == p).FirstOrDefault();
							_context.HouseServices.Remove(house_Service);
						}
					}


				}
				_context.SaveChanges();

				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取全部须知列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetAllNotice(long Id)
		{
			try
			{
				var houseNotice = _context.HouseNotices.Where(x => x.HouseId == Id).FirstOrDefault();
				var info = new List<NoticeDto>();
				if (houseNotice == null)
				{
					info = (from a in _context.Notices
							select new NoticeDto()
							{
								Id = a.Id,
								Title = a.Title,
								value = a.Title,
								label = a.Title,
								@checked = false
							}).ToList();
				}
				else
				{
					info = (from a in _context.Notices
							select new NoticeDto()
							{
								Id = a.Id,
								Title = a.Title,
								value = a.Title,
								label = a.Title,
								@checked = (from b in _context.HouseNotices
											where b.HouseId == Id && b.NoticeId == a.Id
											select b
											).Any()
							}).ToList();
				}

				return new ObjectResult(info);


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 修改房子-须知  多对多
		/// </summary>
		/// <param name="roleDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult EditNotice([FromBody]House_NoticeDto house_NoticeDto)
		{
			try
			{
				if (house_NoticeDto.noticeDto != null)
				{
					//此房子具有的须知
					var HaveFunc = _context.HouseNotices.Where(x => x.HouseId == house_NoticeDto.HouseId).Select(c => c.NoticeId).ToList();
					if (HaveFunc == null)
					{
						//新增
						//添加多对多表
						foreach (var p in house_NoticeDto.noticeDto)
						{
							var house_Notice = new HouseNotice()
							{
								HouseId = house_NoticeDto.HouseId,
								NoticeId = Convert.ToInt64(p.Id)
							};
							_context.HouseNotices.Add(house_Notice);

						}
					}
					else
					{
						//修改
						List<long> ChgFunc = new List<long>();
						//此房子修改后具有的须知
						foreach (var p in house_NoticeDto.noticeDto)
						{
							ChgFunc.Add(Convert.ToInt64(p.Id));
						}
						//此房子修改后的须知与原须知重复的部分
						var sameFunc = ChgFunc.Intersect(HaveFunc).ToList();
						//应该增加的须知
						var addFunc = ChgFunc.Except(sameFunc).ToList();
						foreach (var p in addFunc)
						{
							var houseNotice = new HouseNotice
							{
								HouseId = house_NoticeDto.HouseId,
								NoticeId = p
							};
							_context.HouseNotices.Add(houseNotice);
						}
						//应该删除的须知
						var delFunc = HaveFunc.Except(sameFunc).ToList();
						foreach (var p in delFunc)
						{
							var house_Notice = _context.HouseNotices.Where(x => x.HouseId == house_NoticeDto.HouseId && x.NoticeId == p).FirstOrDefault();
							_context.HouseNotices.Remove(house_Notice);
						}
					}


				}
				_context.SaveChanges();

				return new JsonResult("修改成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		/// <summary>
		/// 获取全部轮播图
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetAllBanner(long Id)
		{
			try
			{
				var BannerList = (from a in _context.HouseBanners
								  where a.HouseId == Id
								  select new House_BannerDto()
								  {
									  uid = a.Id,
									  url = a.PicPath,
									  response = a.PicPath
								  }).ToList();

				return new ObjectResult(BannerList);


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 修改全部轮播图
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult AddBanner([FromBody] List<HouseBannerDto> model)
		{
			try
			{
				if(model.Count != 0)
				{
					var housebanner = _context.HouseBanners.Where(x => x.HouseId == model[0].HouseId).ToList();
					if(housebanner.Count != 0)
					{
						_context.HouseBanners.RemoveRange(housebanner);

					}
					foreach (var p in model)
					{
						var info = new HouseBanner()
						{
							PicPath = p.PicPath,
							HouseId = p.HouseId
						};
						_context.HouseBanners.Add(info);
						_context.SaveChanges();
					}					
				}
				return new JsonResult("修改成功");


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
