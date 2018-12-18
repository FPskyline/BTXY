using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 系统用户dto
	/// </summary>
    public class Sys_UserDto
    {
		/// <summary>
		/// 系统用户id
		/// </summary>
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
		/// 公告
		/// </summary>
		public string Notice { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public string CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public string UpDate { get; set; }
		/// <summary>
		/// 是否假删
		/// </summary>
		public bool IsDel { get; set; }
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
		/// 验证码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 角色id
		/// </summary>
		public long RoleId { get; set; }
		/// <summary>
		/// 角色名
		/// </summary>
		public string RoleName { get; set; }
	}
}
