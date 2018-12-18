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
	/// 搜索控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class SearchController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public SearchController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 搜索房源
		/// </summary>
		/// <param name="searchDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Search([FromBody] SearchDto searchDto)
		{
			try
			{
				GetSearchDto<List<HouseDto>> returnData = new GetSearchDto<List<HouseDto>>();
				if (searchDto.BeginDate == "" && searchDto.EndDate == "") //没有时间
				{
					if (searchDto.CityId == 0) //没有时间 没有城市
					{
						if (searchDto.Searchtype == 2) // 没有时间 没有城市 价格 低到高
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderBy(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else if (searchDto.Searchtype == 3)//没有时间 没有城市 价格 高到低
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderByDescending(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else//没有时间 没有城市 价格 默认
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderByDescending(d => d.Id).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
					}
					else  //没有时间 有城市
					{
						if (searchDto.Searchtype == 2)  // 没有时间 有城市 价格 低到高
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderBy(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else if (searchDto.Searchtype == 3) //没有时间 有城市 价格 高到低
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderByDescending(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else//没有时间 有城市 价格 默认
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1 && a.CityId == searchDto.CityId).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().OrderByDescending(d => d.Id).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
					}
				}
				else //有时间
				{
					DateTime bgDate = DateTime.Parse(searchDto.BeginDate);
					DateTime endDate = DateTime.Parse(searchDto.EndDate);
					if (searchDto.CityId == 0) //有时间 没有城市
					{
						//没有城市  获取搜索时间内，已经租出的房屋
						var hasDateHouse = (from a in _context.OrderDates
											from b in _context.Houses
											where a.HouseId == b.Id && (a.Date >= bgDate && a.Date < endDate)
											select b.Id).Distinct();

						if (searchDto.Searchtype == 2) //有时间 没有城市 价格 低到高
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 from b in _context.OrderDates
												 where b.HouseId == a.Id && ((b.Date > bgDate && b.Date >= endDate) || (b.Date < bgDate && b.Date < endDate))
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderBy(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else if (searchDto.Searchtype == 3)//有时间 没有城市 价格 高到低
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 from b in _context.OrderDates
												 where b.HouseId == a.Id && ((b.Date > bgDate && b.Date >= endDate) || (b.Date < bgDate && b.Date < endDate))
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderByDescending(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else//有时间 没有城市 价格 默认
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 from b in _context.OrderDates
												 where b.HouseId == a.Id && ((b.Date > bgDate && b.Date >= endDate) || (b.Date < bgDate && b.Date < endDate))
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderByDescending(d => d.Id).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
					}
					else  //有时间 有城市
					{
						//有城市  获取搜索时间内，已经租出的房屋
						var hasDateHouse = (from a in _context.OrderDates
											from b in _context.Houses
											where b.CityId == searchDto.CityId && a.HouseId == b.Id && (a.Date >= bgDate && a.Date < endDate)
											select b.Id).Distinct();
						if (searchDto.Searchtype == 2)  //有时间 有城市 价格 低到高
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 from b in _context.OrderDates
												 where b.HouseId == a.Id && ((b.Date > bgDate && b.Date >= endDate) || (b.Date < bgDate && b.Date < endDate))
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderBy(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else if (searchDto.Searchtype == 3) //有时间 有城市 价格 高到低
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 from b in _context.OrderDates
												 where b.HouseId == a.Id && ((b.Date > bgDate && b.Date >= endDate) || (b.Date < bgDate && b.Date < endDate))

												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderByDescending(d => d.Price).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
						else//有时间 有城市 价格 默认
						{
							returnData.TotalCount = _context.Houses.Where(a => a.Name.Contains(searchDto.SearchContent) && a.IsSeal == 1 && a.CityId == searchDto.CityId).Count();
							if (searchDto.Page >= 1 && searchDto.Number > 0)
							{
								var HouseList = (from a in _context.Houses
												 from b in _context.OrderDates
												 where a.Name.Contains(searchDto.SearchContent) && a.CityId == searchDto.CityId && a.IsSeal == 1
												 from c in _context.Citys
												 where c.Id == a.CityId
												 select new HouseDto()
												 {
													 Id = a.Id,
													 Name = a.Name,
													 Logo = a.Logo,
													 CityId = a.CityId,
													 CityName = c.Name,
													 HowArea = a.HowArea,
													 HowRoom = a.HowRoom,
													 HowHall = a.HowHall,
													 HowCook = a.HowCook,
													 Price = a.Price,
													 HowWC = a.HowWC
												 }).Distinct().Where(p => !hasDateHouse.Any(p2 => p2 == p.Id)).OrderByDescending(d => d.Id).Skip((searchDto.Page - 1) * searchDto.Number).Take(searchDto.Number).AsNoTracking().ToList();
								returnData.BigField = HouseList;
							}
						}
					}
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
