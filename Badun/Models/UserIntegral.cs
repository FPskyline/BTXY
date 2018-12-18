using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 用户积分表
	/// </summary>
    public class UserIntegral
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 积分
		/// </summary>
		public int Integral { get; set; }
		/// <summary>
		/// 积分类型  1-签到 2-其他
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
    }
}
