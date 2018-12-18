using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// search分页器dto
	/// </summary>
	public class GetSearchDto<T>
	{
		/// <summary>
		/// 大字段列表
		/// </summary>
		public T BigField { get; set; }
		/// <summary>
		/// 列表总数
		/// </summary>
		public int TotalCount { get; set; }
	}
	/// <summary>
	/// 搜索dto
	/// </summary>
	public class SearchDto
    {
		/// <summary>
		/// 页数
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// 当前条数
		/// </summary>
		public int Number { get; set; }
		/// <summary>
		/// 搜索内容
		/// </summary>
		public string SearchContent { set; get; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public string BeginDate { get; set; }
		/// <summary>
		/// 离开日期
		/// </summary>
		public string EndDate { get; set; }
		/// <summary>
		/// 城市id
		/// </summary>
		public long CityId { get; set;}
		/// <summary>
		/// 排序类型  1-默认 2-低价优先 3-高价优先 
		/// </summary>

		public int Searchtype { get; set; }
	}
}
