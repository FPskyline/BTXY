using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 广告对象
    /// </summary>
    public class AdBannerDto
    {
        /// <summary>
        /// id
        /// </summary>
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
