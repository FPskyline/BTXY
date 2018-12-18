using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋-需知表
	/// </summary>
    public class Notice
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 须知标题
		/// </summary>
		public string Title { get; set; }
        /// <summary>
        /// 须知内容
        /// </summary>
        public string Content { get; set; }
    }
}
