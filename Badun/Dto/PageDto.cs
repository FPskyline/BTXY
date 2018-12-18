using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
	/// <summary>
	/// 分页器视图模型
	/// </summary>
	public class GetPageDto<T>
	{
		/// <summary>
		/// 大字段列表
		/// </summary>
		public T BigField { get; set; }
		/// <summary>
		/// 列表总数
		/// </summary>
		public int TotalCount { get; set; }
		/// <summary>
		/// 总金额
		/// </summary>
		public double TotalMoney { get; set; }
		/// <summary>
		/// 是否显示全部结算
		/// </summary>
		public bool IsAllPay { get; set; }
	}
	/// <summary>
	/// 分页获取视图模型
	/// </summary>
	public class PageDto
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
        /// 订单状态
        /// </summary>
        public int State { get; set; }
        
        /// <summary>
        /// 会员Id
        /// </summary>
        public long UserId { set; get; }
		/// <summary>
		/// 系统用户id
		/// </summary>
		public long SysUserId { get; set; }
		/// <summary>
		/// 厂家id
		/// </summary>
		public long FactoryId { get; set; }
		/// <summary>
		/// 商家id
		/// </summary>
		public long MerchantId { get; set; }
		/// <summary>
		/// 搜索内容
		/// </summary>
		public string SearchContent { set; get; }
		/// <summary>
		/// 商户名
		/// </summary>
		public string MerName { get; set; }
		/// <summary>
		/// 搜索开始日期
		/// </summary>
		public DateTime BeginDate { get; set; }
		/// <summary>
		/// 搜索结束日期
		/// </summary>
		public DateTime EndDate { get; set; }
		/// <summary>
		/// 小程序会员登录token
		/// </summary>
		public string Token { get; set; }
		/// <summary>
		/// 类id
		/// </summary>
		public int Sum { get; set; }
		/// <summary>
		/// 价格排序
		/// </summary>
		public int Num { get; set; }
		/// <summary>
		/// 销量排序
		/// </summary>
		public int Res { get; set; }
		/// <summary>
		/// 小程序id
		/// </summary>
		public long AppletId { get; set; }
		/// <summary>
		/// 房屋id
		/// </summary>
		public long HouseId { get; set; }

	}
}
