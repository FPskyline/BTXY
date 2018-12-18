using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 角色表
	/// </summary>
    public class Role
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
		/// 假删
		/// </summary>
		public int IsDel { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 权限多对多映射表
		/// </summary>
		public virtual ICollection<Role_Limit> Role_Limits { get; set; }

	}
}
