using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 设备对象
    /// </summary>
    public class DeviceDto
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
		/// 描述
		/// </summary>
		public string Content { get; set; }
		public string label { get; set; }
		public string value { get; set; }
		public int @checked { get; set; }
	}
    public class House_DeviceDto
    {
        public long HouseId { get; set; }
        public List<DeviceDto> deviceDto { get; set; }
    }
    public class HouseDeviceInfoDto
    {
        /// <summary>
        /// 房屋
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public long DeviceId { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string Content { get; set; }
    }
    public class DeviceInfoDto
    {
        /// <summary>
        /// 房屋
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public long DeviceId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string Content { get; set; }
    }
}
