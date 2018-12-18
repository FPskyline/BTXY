using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋设施对照表
	/// </summary>
    public class HouseDevice
    {
        /// <summary>
        /// 房屋
        /// </summary>
        [Key, Column(Order = 1)]
        public long HouseId { get; set; }
        public House Houses { set; get; }
        /// <summary>
        /// 设备
        /// </summary>
        [Key, Column(Order = 2)]
        public long DeviceId { get; set; }
        public Device Devices { set; get; }

        public string Content { get; set; }
    }
}
