using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 订单表
	/// </summary>
    public class Order
    {
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		public long Id { set; get; }
		/// <summary>
		/// 客房Id
		/// </summary>
		public long HouseId { set; get; }
		/// <summary>
		/// 用户Id
		/// </summary>
		public long UserId { set; get; }
		/// <summary>
		/// 订单状态 1-已支付 2-未支付 3-已完成 4-已评论 9-已取消
		/// </summary>
		public int State { set; get; }
        /// <summary>
        /// 入住几晚
        /// </summary>
        public int Days { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpDate { get; set; }
		/// <summary>
		/// 订单金额
		/// </summary>
		public double Amount { set; get; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 退房时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public string BeginDatestr { get; set; }
        /// <summary>
        /// 退房时间
        /// </summary>
        public string EndDatestr { get; set; }
		/// <summary>
		/// 入住人数
		/// </summary>
		public int PeopleNum { get; set; }
		/// <summary>
		/// 留言
		/// </summary>
		public string Msg { get; set; }
        /// <summary> 
		/// 订单来源  0-小程序 1-其他平台
		/// </summary>
		public int OrderSource { get; set; }

    }
}
