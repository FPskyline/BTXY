﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 房屋表
	/// </summary>
	public class House
    {
		/// <summary>
		/// id
		/// </summary>
		[Key]
		public long Id { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public long CityId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 一天价格
        /// </summary>
        public int Price { get; set; }
		/// <summary>
		/// 权重值
		/// </summary>
		public int WeightValue { get; set; }
		/// <summary>
		/// 是否为热点
		/// </summary>
		public int IsHot { get; set; }
        /// <summary>
        /// 是否上架
        /// </summary>
        public int IsSeal { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string Introduce { get; set; }
		/// <summary>
		/// 周边介绍
		/// </summary>
		public string Introduce_rim { get; set; }
		/// <summary>
		/// 可住人数
		/// </summary>
		public int PeopleNum { get; set; }
		/// <summary>
		/// 平米数
		/// </summary>
		public int HowArea { get; set; }
		/// <summary>
		/// 室
		/// </summary>
		public int HowRoom { get; set; }
		/// <summary>
		/// 厅
		/// </summary>
		public int HowHall { get; set; }
		/// <summary>
		/// 卫生间
		/// </summary>
		public int HowWC { get; set; }
		/// <summary>
		/// 厨房
		/// </summary>
		public int HowCook { get; set; }
		/// <summary>
		/// 床
		/// </summary>
		public int HowBed { get; set; }
		/// <summary>
		/// 床位信息
		/// </summary>
		public string BedInfo { get; set; }
		/// <summary>
		/// Logo
		/// </summary>
		public string Logo { get; set; }
		/// <summary>
		/// 客人须知
		/// </summary>
		public string Notice { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatDate { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpDate { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 接待可客人数量
        /// </summary>
        public int ReceptionNum { get; set; }
        /// <summary>
        /// 评价数量
        /// </summary>
        public int EvaluateNum { get; set; }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int CollectNum { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeNum { get; set; }
		/// <summary>
		/// 地图生成路径
		/// </summary>
		public string MapUrl { get; set; }
    }
}
