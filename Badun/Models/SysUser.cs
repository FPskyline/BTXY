using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 系统用户表
	/// </summary>
    public class Sys_User
    {
		/// <summary>
		/// 系统用户id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 账号
		/// </summary>
		public string Account { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// 盐
		/// </summary>
		public string Salt { get; set; }
		/// <summary>
		/// Token
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// 系统用户名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 是否假删
		/// </summary>
		public int IsDel { get; set; }
		/// <summary>
		/// 角色id
		/// </summary>
		public long RoleId { get; set; }
		/// <summary>
		/// 角色name
		/// </summary>
		public string RoleName { get; set; }
	}
}
