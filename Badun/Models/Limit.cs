using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 权限表
	/// </summary>
    public class Limit
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
		/// 类别 1.小程序管理 2.系统管理 3.房主管理 4.客房管理
		/// </summary>
		public int Type { get; set; }
		/// <summary>
		/// 类别名称
		/// </summary>
		public string TypeName { get; set; }
		/// <summary>
		/// 权限多对多映射表
		/// </summary>
		public virtual ICollection<Role_Limit> Role_Limits { get; set; }
	}
}
