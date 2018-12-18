using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 小程序参数表
	/// </summary>
    public class Applet
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		/// 微信小程序appid
		/// </summary>
		public string appid { get; set; }
		/// <summary>
		/// 微信小程序app secret
		/// </summary>
		public string secret { get; set; }
		/// <summary>
		/// 子商户号
		/// </summary>
		public string sub_mch_id { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
	}
}
