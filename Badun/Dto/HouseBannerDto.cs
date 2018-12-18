using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 房屋轮播图dto
	/// </summary>
	public class HouseBannerDto
	{
		/// <summary>
		/// id
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 轮播图路径
		/// </summary>
		public string PicPath { get; set; }
		/// <summary>
		/// 房屋id
		/// </summary>
		public long HouseId { get; set; }
	}
	public class House_BannerDto
	{
		public long uid { get; set; }
		public string url { get; set; }
		public string response { get; set; }
	}
}
