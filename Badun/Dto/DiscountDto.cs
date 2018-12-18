using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 折扣模型
    /// </summary>
    public class DiscountDto
    {
        /// <summary>
        /// id
        /// </summary>
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
