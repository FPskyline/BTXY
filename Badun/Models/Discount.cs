using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    /// <summary>
    /// 折扣表
    /// </summary>
    public class Discount
    {
        /// <summary>
		/// id
		/// </summary>
		[Key]
        public long Id { get; set; }
        /// <summary>
        /// 预定满天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 折扣值 0-100  如：九折=90
        /// </summary>
        public int DisValue { get; set; }
    }
}
