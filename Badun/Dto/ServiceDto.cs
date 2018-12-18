using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 服务对象
    /// </summary>
    public class ServiceDto
    {
        /// <summary>
		/// id
		/// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string Name { get; set; }
		public string label { get; set; }
		public string value { get; set; }
		public bool @checked { get; set; }
	}
	public class House_ServiceDto
	{
		public long HouseId { get; set; }
		public List<ServiceDto> serviceDto { get; set; }
	}
}
