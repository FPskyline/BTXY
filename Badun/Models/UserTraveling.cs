using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    /// <summary>
    /// 旅游收藏
    /// </summary>
    public class UserTraveling
    {
        /// <summary>
        /// 房屋
        /// </summary>
        [Key, Column(Order = 1)]
        public long UserId { get; set; }
        public User Users { set; get; }
        /// <summary>
        /// 设备
        /// </summary>
        [Key, Column(Order = 2)]
        public long TravelingId { get; set; }
        public Traveling Travelings { set; get; }
    }
}
