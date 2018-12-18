using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 退订对象
    /// </summary>
    public class HouseUnsubDto
    {
        /// <summary>
		/// id
		/// </summary>
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
		public int Days { get; set; }
        /// <summary>
        /// 退款比例
        /// </summary>
        public int Proportion { get; set; }
    }
}
