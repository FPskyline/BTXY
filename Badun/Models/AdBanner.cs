using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 广告管理表
	/// </summary>
    public class AdBanner
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
		/// <summary>
		/// 轮播图路径
		/// </summary>
		public string PicPath { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string PicContent { get; set; }
        /// <summary>
        /// 权重值
        /// </summary>
        public int WeightValue { get; set; }
    }
}
