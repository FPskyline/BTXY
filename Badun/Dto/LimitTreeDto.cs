using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 权限dto
	/// </summary>
    public class LimitTreeDto
    {
		public string title { get; set; }
		public List<ChildrenDto> children { get; set; }

	}
	public class ChildrenDto
	{
		public string title { get; set; }

		public string key { get; set; }
		public bool isLeaf { get; set; }
		public bool @checked { get; set; }
	}
}
