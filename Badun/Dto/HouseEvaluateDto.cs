using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 评价管理模型
    /// </summary>
    public class HouseEvaluateDto
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 房屋id
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 房屋名称
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否显示  1-显示  2-不显示
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatDateStr { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpDate { get; set; }
		public UserDto userDto { get; set; }
    }
}
