using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 系统用户权限多对多
	/// </summary>
	public class Role_Limit
    {
		/// <summary>
		/// 角色
		/// </summary>
		[Key, Column(Order = 1)]
		public long RoleId { get; set; }
		public Role Roles { set; get; }
		/// <summary>
		/// 权限
		/// </summary>
		[Key, Column(Order = 2)]
		public long LimitId { get; set; }
		public Limit limits { set; get; }

	}
}
