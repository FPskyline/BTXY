using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    /// <summary>
    /// 房屋周边美食
    /// </summary>
    public class HouseFood
    {
        /// <summary>
		/// id
		/// </summary>
		[Key]
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 房屋id
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 权重值
        /// </summary>
        public string WeightValue { get; set; }
    }
}
