using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    /// <summary>
    /// 设备表
    /// </summary>
    public class Device
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
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
    }
}
