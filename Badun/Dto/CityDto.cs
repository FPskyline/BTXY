using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 城市对象
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 城市Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 是否为热点
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 权重值
        /// </summary>
        public int WeightValue { get; set; }
    }
	public class CityTreeDto
	{
		public string title { get; set; }
		public List<CityItemDto> item { get; set; }
	}
	public class CityItemDto
	{
		public long id { get; set; }
		public string name { get; set; }
		public string key { get; set; }
	}
}
