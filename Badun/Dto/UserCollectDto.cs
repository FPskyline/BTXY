using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 收藏模型
    /// </summary>
    public class UserCollectDto
    {
        /// <summary>
		/// id
		/// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDate { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 房屋主键
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 房屋名称
        /// </summary>
        public string HouseName { get; set; }
    }

    /// <summary>
    /// 用户获取收藏列表模型
    /// </summary>

    public class UserCollectByUserID
    {
        /// <summary>
        /// 房屋主键
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 一天价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 可住人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 平米数
        /// </summary>
        public int HowArea { get; set; }
        /// <summary>
        /// 室
        /// </summary>
        public int HowRoom { get; set; }
        /// <summary>
        /// 厅
        /// </summary>
        public int HowHall { get; set; }
        /// <summary>
        /// 卫生间
        /// </summary>
        public int HowWC { get; set; }
        /// <summary>
        /// 厨房
        /// </summary>
        public int HowCook { get; set; }
        /// <summary>
        /// 床
        /// </summary>
        public int HowBed { get; set; }
        /// <summary>
        /// 床位信息
        /// </summary>
        public string BedInfo { get; set; }
        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }
    }
}
