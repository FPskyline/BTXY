using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 城市表
	/// </summary>
    public class City
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
        /// <summary>
        /// 城市Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string Name { get; set; }
		/// <summary>
		/// 是否为热点
		/// </summary>
		public int IsHot { get; set; }
		/// <summary>
		/// 权重值
		/// </summary>
		public int WeightValue { get; set; }
	}
}
