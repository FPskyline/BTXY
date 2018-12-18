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
	/// 订单控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class OrderController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public OrderController(MyDbContext context)
		{
			_context = context;
		}
		
		/// <summary>
		/// 新增订单
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult CreatOrder([FromBody]OrderDto orderDto)
		{
			try
			{
				//判断是否已经被预定/租出
				var info = _context.OrderDates.Where(x => x.HouseId == orderDto.HouseId && x.Date > DateTime.Now.AddDays(-1)).ToList();
				if(info != null)
				{
					foreach(var p in info)
					{
						if(p.Date >= orderDto.BeginDate && p.Date <= orderDto.EndDate)
						{
							return BadRequest("包含已组日期，请重新选择日期");
						}
					}
				};
				var outTradeNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999));
				var addOrder = new Order()
				{
					OrderNo = outTradeNo,
					State = 2,
					Amount = orderDto.Amount,
					UserId = orderDto.UserId,
					HouseId  = orderDto.HouseId,
					Days = orderDto.Days,
					BeginDate = orderDto.BeginDate,
					EndDate = orderDto.EndDate,
					PeopleNum  = orderDto.PeopleNum,
					Msg = orderDto.Msg,
                    OrderSource =0,
                    UpDate = DateTime.Now,
					CreateDate = DateTime.Now,
				};
				_context.Orders.Add(addOrder);
				_context.SaveChanges();
				return new JsonResult(addOrder);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        /// <summary>
		/// 平台导入订单
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
        public IActionResult CreateOrderBySysUser([FromBody]OrderDto orderDto)
        {
            try
            {
                var outTradeNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(999));
                var addOrder = new Order()
                {
                    OrderNo = outTradeNo,
                    State = 1,
                    Amount = orderDto.Amount,
                    HouseId = orderDto.HouseId,
                    Days = orderDto.Days,
                    BeginDate = orderDto.BeginDate,
                    EndDate = orderDto.EndDate,
                    PeopleNum = orderDto.PeopleNum,
                    Msg = orderDto.Msg,
                    UpDate = DateTime.Now,
                    CreateDate = DateTime.Now,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                    OrderSource = 1,
                };
                _context.Orders.Add(addOrder);
                _context.SaveChanges();
                var orderinfo = _context.Orders.Where(x => x.OrderNo == outTradeNo).FirstOrDefault();
                var bg_date = orderinfo.BeginDate;
                var end_date = orderinfo.EndDate;
                List<DateTime> listDays = new List<DateTime>();
                DateTime dtDay = new DateTime();
                for (dtDay = bg_date; dtDay.CompareTo(end_date) < 0; dtDay = dtDay.AddDays(1))
                {
                    listDays.Add(dtDay);
                }
                foreach (var date in listDays)
                {
                    var info = new OrderDate()
                    {
                        OrderId = orderinfo.Id,
                        HouseId = orderinfo.HouseId,
                        Date = date
                    };
                    _context.OrderDates.Add(info);
                    _context.SaveChanges();
                }
                return new JsonResult("新增成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 新增订单-日期 （支付时）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
		public IActionResult CreatOrderDate([FromBody]OrderDto orderDto)
		{
			try
			{
				var orderinfo = _context.Orders.Where(x => x.Id == orderDto.Id).FirstOrDefault();
				var bg_date = orderinfo.BeginDate;
				var end_date = orderinfo.EndDate;
				List<DateTime> listDays = new List<DateTime>();
				DateTime dtDay = new DateTime();
				for (dtDay = bg_date; dtDay.CompareTo(end_date) < 0; dtDay = dtDay.AddDays(1))
				{
					listDays.Add(dtDay);
				}
				foreach(var date in listDays)
				{
					var info = new OrderDate()
					{
						OrderId = orderinfo.Id,
						HouseId = orderinfo.HouseId,
						Date = date
					};
					_context.OrderDates.Add(info);
					_context.SaveChanges();
				}
				return new JsonResult("新增成功");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	
		


        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult GetOrderList([FromBody] PageDto pageDto)
        {
            try
            {
                pageDto.EndDate = pageDto.EndDate.AddHours(8);
                pageDto.BeginDate = pageDto.BeginDate.AddHours(8);
                GetPageDto<List<OrderDto>> returnData = new GetPageDto<List<OrderDto>>();
                if (pageDto.State == 0)
                {
                    returnData.TotalCount = _context.Orders.Where(a=>a.CreateDate >= pageDto.BeginDate && a.CreateDate <= pageDto.EndDate).Count();
                    if (pageDto.Page >= 1 && pageDto.Number > 0)
                    {
                        var OrderList = (from a in _context.Orders
                                         from b in _context.Houses
                                         where b.Id == a.HouseId
                                         where a.CreateDate >= pageDto.BeginDate && a.CreateDate <= pageDto.EndDate
                                         select new OrderDto()
                                         {
                                             Id = a.Id,
                                             Amount = a.Amount,
                                             BeginDate = a.BeginDate,
                                             BeginDatestr = a.BeginDatestr,
                                             CreateDate = a.CreateDate,
                                             Days = a.Days,
                                             EndDate = a.EndDate,
                                             EndDatestr = a.EndDatestr,
                                             Msg = a.Msg,
                                             OrderNo = a.OrderNo,
                                             PeopleNum = a.PeopleNum,
                                             State = a.State,
                                             HouseId = a.HouseId,
                                             HouseName = b.Name,
                                             UserId = a.UserId,
                                             OrderSource = a.OrderSource,
                                             UpDate = a.UpDate
                                         }).OrderByDescending(d => d.CreateDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                        returnData.BigField = OrderList;
                    }
                }
                else
                {
                    returnData.TotalCount = _context.Orders.Where(a=> a.State == pageDto.State && a.CreateDate >= pageDto.BeginDate && a.CreateDate <= pageDto.EndDate).Count();
                    if (pageDto.Page >= 1 && pageDto.Number > 0)
                    {
                        var OrderList = (from a in _context.Orders
                                         from b in _context.Houses
                                         where b.Id == a.HouseId
                                         where a.State == pageDto.State && a.CreateDate >= pageDto.BeginDate && a.CreateDate <= pageDto.EndDate
                                         select new OrderDto()
                                         {
                                             Id = a.Id,
                                             Amount = a.Amount,
                                             BeginDate = a.BeginDate,
                                             BeginDatestr = a.BeginDatestr,
                                             CreateDate = a.CreateDate,
                                             Days = a.Days,
                                             EndDate = a.EndDate,
                                             EndDatestr = a.EndDatestr,
                                             Msg = a.Msg,
                                             OrderNo = a.OrderNo,
                                             PeopleNum = a.PeopleNum,
                                             State = a.State,
                                             HouseId = a.HouseId,
                                             OrderSource = a.OrderSource,
                                             HouseName = b.Name,
                                             UserId = a.UserId,
                                             UpDate = a.UpDate
                                         }).OrderByDescending(d => d.CreateDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                        returnData.BigField = OrderList;
                    }
                }
                return new ObjectResult(returnData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
		/// <summary>
		/// 小程序分类获取订单列表
		/// </summary>
		/// <returns>返回结果</returns>
		[HttpGet]
		public IActionResult GetOrders(long Id)
		{
			try
			{
				var Orderlist_1 = (from a in _context.Orders
								   where a.UserId == Id && a.State == 1 && a.OrderSource ==0
								   select new OrderDto()
								   {
									   Id = a.Id,
									   State = a.State,
									   Amount = a.Amount,
									   OrderNo = a.OrderNo,
									   PeopleNum = a.PeopleNum,
									   Msg = a.Msg,
									   Days = a.Days,
									   houseDto = (from b in _context.Houses
												   where b.Id == a.HouseId
												   select new HouseDto()
												   {
													   Id = b.Id,
													   Name = b.Name,
													   Logo = b.Logo,
													   HowArea = b.HowArea,
													   HowCook = b.HowCook,
													   HowHall = b.HowHall,
													   HowRoom = b.HowRoom,
													   HowWC = b.HowWC,
													   PeopleNum = b.PeopleNum
												   }).FirstOrDefault(),
									   userDto = (from c in _context.Users
												  where c.Id == a.UserId
												  select new UserDto()
												  {
													  Id = c.Id,
													  UserName = c.UserName,
													  PhoneNum = c.PhoneNum,
												  }).FirstOrDefault(),
									   BeginDatestr = a.BeginDate.ToString("yyyy-MM-dd"),
									   EndDatestr = a.EndDate.ToString("yyyy-MM-dd"),
								   }).OrderByDescending(d => d.Id).AsNoTracking().ToList();
				var Orderlist_2 = (from a in _context.Orders
								   where a.UserId == Id && a.State == 2 && a.OrderSource == 0
                                   select new OrderDto()
								   {
									   Id = a.Id,
									   State = a.State,
									   Amount = a.Amount,
									   OrderNo = a.OrderNo,
									   PeopleNum = a.PeopleNum,
									   Msg = a.Msg,
									   Days = a.Days,
									   houseDto = (from b in _context.Houses
												   where b.Id == a.HouseId
												   select new HouseDto()
												   {
													   Id = b.Id,
													   Name = b.Name,
													   Logo = b.Logo,
													   HowArea = b.HowArea,
													   HowCook = b.HowCook,
													   HowHall = b.HowHall,
													   HowRoom = b.HowRoom,
													   HowWC = b.HowWC,
													   PeopleNum = b.PeopleNum
												   }).FirstOrDefault(),
									   userDto = (from c in _context.Users
												  where c.Id == a.UserId
												  select new UserDto()
												  {
													  Id = c.Id,
													  UserName = c.UserName,
													  PhoneNum = c.PhoneNum,
												  }).FirstOrDefault(),
									   BeginDatestr = a.BeginDate.ToString("yyyy-MM-dd"),
									   EndDatestr = a.EndDate.ToString("yyyy-MM-dd"),
								   }).OrderByDescending(d => d.Id).AsNoTracking().ToList();				
				var Orders = new OrderInfoDto()
				{
					orderList_1 = Orderlist_1,
					orderList_2 = Orderlist_2,
				};
				return new ObjectResult(Orders);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 小程序端获取(已完成)订单列表
		/// </summary>
		/// <param name="pageDto"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult GetOverOrders([FromBody] PageDto pageDto)
		{
			try
			{
				GetPageDto<List<OrderDto>> returnData = new GetPageDto<List<OrderDto>>();
				if (pageDto.Page >= 1 && pageDto.Number > 0)
				{
					var OrderList = (from a in _context.Orders
									 where a.UserId == pageDto.UserId && (a.State == 3 || a.State == 4) && a.OrderSource == 0
                                     select new OrderDto()
									 {
										 Id = a.Id,
										 State = a.State,
										 Amount = a.Amount,
										 OrderNo = a.OrderNo,
										 PeopleNum = a.PeopleNum,
										 Msg = a.Msg,
										 Days = a.Days,
										 houseDto = (from b in _context.Houses
													 where b.Id == a.HouseId
													 select new HouseDto()
													 {
														 Id = b.Id,
														 Name = b.Name,
														 Logo = b.Logo,
														 HowArea = b.HowArea,
														 HowCook = b.HowCook,
														 HowHall = b.HowHall,
														 HowRoom = b.HowRoom,
														 HowWC = b.HowWC,
														 PeopleNum = b.PeopleNum
													 }).FirstOrDefault(),
										 userDto = (from c in _context.Users
													where c.Id == a.UserId
													select new UserDto()
													{
														Id = c.Id,
														UserName = c.UserName,
														PhoneNum = c.PhoneNum,
													}).FirstOrDefault(),
										 BeginDatestr = a.BeginDate.ToString("yyyy-MM-dd"),
										 EndDatestr = a.EndDate.ToString("yyyy-MM-dd"),
									 }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
					returnData.BigField = OrderList;

				}

				return new ObjectResult(returnData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 取消订单 -》 9
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult EditOderto9(long Id)
		{
			var order = _context.Orders.Where(x => x.Id == Id).FirstOrDefault();
			if (order == null)
			{
				return BadRequest("未找到该订单");
			}
			order.State = 9;
			_context.SaveChanges();
			return new JsonResult("修改成功");
		}
		/// <summary>
		/// 已支付2 -》 已完成3
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult EditOderto3(long Id)
		{
			var order = _context.Orders.Where(x => x.Id == Id).FirstOrDefault();
			if (order == null)
			{
				return BadRequest("未找到该订单");
			}
			order.State = 3;
			_context.SaveChanges();
			return new JsonResult("修改成功");
		}
        /// <summary>
        /// 每天调用一编跑批  完成订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RunOrderFinish()
        {
            var endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var order = _context.Orders.Where(x => x.EndDate.ToString("yyyy-MM-dd") == endDate && x.State ==1).ToList();
            if (order.Count == 0)
            {
                return BadRequest("无跑批订单");
            }
            foreach (var p in order)
            {
                p.State = 3;
            }
            _context.SaveChanges();
            return new JsonResult("跑批成功");
        }
    }
}
