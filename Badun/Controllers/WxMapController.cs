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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace Badun.Controllers
{

	/// <summary>
	/// 小程序地图控制器
	/// </summary>
	[Route("api/[controller]/[action]")]
	//[Consumes("application/json", "application/json-patch-json", "multipart/form-data")]
	public class WxMapController : Controller
	{
		private MyDbContext _context;
		private IConfiguration _configuration;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public WxMapController(MyDbContext context,IConfiguration Configuration)
		{
			_context = context;
			_configuration = Configuration;
		}
		/// <summary>
		///  通过经纬度，获取map png，带markers
		/// </summary>
		/// <returns></returns>
		[HttpGet]

		public async Task<IActionResult> GetMap(string lat,string log)
		{

			try
			{
				using (HttpClient client = new HttpClient())
				{
					WxMap wxMap = new WxMap(_configuration);
					var fileName = await wxMap.CreatMapAsync(lat, log);
					return new JsonResult(fileName);
				}

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
