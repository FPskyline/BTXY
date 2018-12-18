using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋退订规则
	/// </summary>
    public class HouseUnsub
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
        /// <summary>
		/// 入住前几天
		/// </summary>
		public int  Days { get; set; }
        /// <summary>
        /// 退款比例
        /// </summary>
        public int Proportion { get; set; }
    }
}
