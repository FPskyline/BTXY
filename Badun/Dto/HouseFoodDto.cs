using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 周边模型
    /// </summary>
    public class HouseFoodDto
    {
        /// <summary>
		/// id
		/// </summary>
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
        /// 权重值
        /// </summary>
        public string WeightValue { get; set; }
        /// <summary>
        /// 房屋id
        /// </summary>
        public long HouseId { get; set; }
        
    }
}
