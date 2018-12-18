using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋订单-日期表
	/// </summary>
    public class OrderDate
    {
		/// <summary>
		/// 主键id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 房屋id
		/// </summary>
		public long HouseId { get; set; }
		/// <summary>
		/// 订单id
		/// </summary>
		public long OrderId { get; set; }
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime Date { get; set; }
    }
}
