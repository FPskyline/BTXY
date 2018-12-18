using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋轮播图
	/// </summary>
    public class HouseBanner
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
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
}
