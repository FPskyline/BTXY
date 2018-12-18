using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 房屋订单-日期表
	/// </summary>
	public class OrderDateDto
    {
		/// <summary>
		/// 主键id
		/// </summary>
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
