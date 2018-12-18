using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋服务对照表
	/// </summary>
    public class HouseService
    {
        /// <summary>
        /// 房屋
        /// </summary>
        [Key, Column(Order = 1)]
        public long HouseId { get; set; }
        public House Houses { set; get; }
        /// <summary>
        /// 服务
        /// </summary>
        [Key, Column(Order = 2)]
        public long ServiceId { get; set; }
        public Service Services { set; get; }
    }
}
