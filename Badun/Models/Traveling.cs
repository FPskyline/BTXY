using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 旅游推荐表
	/// </summary>
    public class Traveling
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		/// 轮播图
		/// </summary>
		public string Banner { get; set; }
        /// <summary>
        /// 内容 富文本
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 视频
        /// </summary>
        public string Video { get; set; }
		/// <summary>
		/// 点击量
		/// </summary>
		public int ClickNum { get; set; }
		/// <summary>
		/// 点赞数
		/// </summary>
		public int ZanNum { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatDate { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 所属城市
		/// </summary>
		public long CityId { get; set; }
		/// <summary>
		/// 是否为热点
		/// </summary>
		public int IsHot { get; set; }
		/// <summary>
		/// 权重值
		/// </summary>
		public int WeightValue { get; set; }
	}
}
