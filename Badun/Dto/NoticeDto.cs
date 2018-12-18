using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 须知对象
    /// </summary>
    public class NoticeDto
    {
        /// <summary>
		/// id
		/// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 须知标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 须知内容
        /// </summary>
        public string Content { get; set; }
		public string label { get; set; }
		public string value { get; set; }
		public bool @checked { get; set; }
	}
	public class House_NoticeDto
	{
		public long HouseId { get; set; }
		public List<ServiceDto> noticeDto { get; set; }
	}
}
