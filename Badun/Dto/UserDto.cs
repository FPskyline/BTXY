using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 用户dto
	/// </summary>
    public class UserDto
    {
		/// <summary>
		/// id
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 微信openid
		/// </summary>
		public string We_OpenId { get; set; }
		/// <summary>
		/// 微信昵称
		/// </summary>
		public string We_Name { get; set; }
		/// <summary>
		/// 微信头像
		/// </summary>
		public string We_AvtUrl { set; get; }
		/// <summary>
		/// 积分
		/// </summary>
		public int Integral { get; set; }
		/// <summary>
		/// 微信登录后授权token
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 手机号号
		/// </summary>
		public string PhoneNum { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		public string UserName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNum { get; set; }
        /// <summary>
        /// 是否实名 1-已经实名  2-未实名 3-实名失败
        /// </summary>
        public int IsReal { get; set; }

        /// <summary>
        /// 是否手机认证 1-已认证  2-未认证
        /// </summary>
        public int IsCert { get; set; }
    }
}
