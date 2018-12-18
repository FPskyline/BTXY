using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 服务表
	/// </summary>
    public class Service
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 服务名
		/// </summary>
		public string Name { get; set; }
	}
}
