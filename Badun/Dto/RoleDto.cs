using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 角色dto
	/// </summary>
    public class RoleDto
    {
		/// <summary>
		/// id
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 假删
		/// </summary>
		public bool IsDel { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 权限树
		/// </summary>
		public List<CheckedArrayDto> CheckedArray { get; set; }
	}
	/// <summary>
	/// 权限树
	/// </summary>
	public class CheckedArrayDto
	{
		public string key { get; set; }
		public long Id { get; set; }
		public bool IsHave { get; set; }
	}

}
