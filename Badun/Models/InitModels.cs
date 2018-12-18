using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
	/// <summary>
	/// 初始化数据实例
	/// </summary>
	public class InitModels
	{
		/// <summary>
		/// 定义数据库上下文
		/// </summary>
		public class MyDbContext : DbContext
		{
			/// <summary>
			/// 重载
			/// </summary>
			/// <param name="options"></param>
			public MyDbContext(DbContextOptions<MyDbContext> options)
				: base(options)
			{
			}
			/// <summary>
			/// 多表
			/// </summary>
			/// <param name="modelBuilder"></param>
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				base.OnModelCreating(modelBuilder);
				modelBuilder.Entity<Role_Limit>().HasKey(x => new { x.RoleId, x.LimitId });
                modelBuilder.Entity<HouseNotice>().HasKey(x => new { x.HouseId, x.NoticeId });
                modelBuilder.Entity<HouseDevice>().HasKey(x => new { x.HouseId, x.DeviceId });
                modelBuilder.Entity<HouseService>().HasKey(x => new { x.HouseId, x.ServiceId });
                modelBuilder.Entity<UserTraveling>().HasKey(x => new { x.UserId, x.TravelingId });
            }
			/// <summary>
			/// 系统用户表
			/// </summary>
			public DbSet<Sys_User> Sys_Users { get; set; }
			/// <summary>
			/// 角色表
			/// </summary>
			public DbSet<Role> Roles { get; set; }
			/// <summary>
			/// 权限表
			/// </summary>
			public DbSet<Limit> Limits { get; set; }
			/// <summary>
			/// 角色-权限多对多
			/// </summary>
			public DbSet<Role_Limit> Role_Limits { get; set; }
			/// <summary>
			/// 房屋表
			/// </summary>
			public DbSet<House> Houses { get; set; }
            /// <summary>
            /// 房屋评价表
            /// </summary>
            public DbSet<HouseEvaluate> HouseEvaluates { get; set; }
            /// <summary>
            /// 房屋轮播图
            /// </summary>
            public DbSet<HouseBanner> HouseBanners { get; set; }
			/// <summary>
			/// 房屋设施
			/// </summary>
			public DbSet<HouseDevice> HouseDevices { get; set; }
            /// <summary>
			/// 用户旅游
			/// </summary>
			public DbSet<UserTraveling> UserTravelings { get; set; }
            
            /// <summary>
            /// 房屋-需知表
            /// </summary>
            public DbSet<HouseNotice> HouseNotices { get; set; }
			/// <summary>
			/// 房屋服务
			/// </summary>
			public DbSet<HouseService> HouseServices { get; set; }
			/// <summary>
			/// 房屋退订规则
			/// </summary>
			public DbSet<HouseUnsub> HouseUnsubs { get; set; }
			/// <summary>
			/// 小程序表
			/// </summary>
			public DbSet<Applet> Applet { get; set; }
			/// <summary>
			/// 用户表
			/// </summary>
			public DbSet<User> Users { get; set; }
			/// <summary>
			/// 订单表
			/// </summary>
			public DbSet<Order> Orders { get; set; }
			/// <summary>
			/// 城市表
			/// </summary>
			public DbSet<City> Citys { get; set; }
			/// <summary>
			/// 旅游推荐表
			/// </summary>
			public DbSet<Traveling> Travelings { get; set; }
            /// <summary>
            /// 须知表
            /// </summary>
            public DbSet<Notice> Notices { get; set; }
            /// <summary>
            /// 设施表
            /// </summary>
            public DbSet<Device> Devices { get; set; }
            /// <summary>
            /// 折扣表
            /// </summary>
            public DbSet<Discount> Discounts { get; set; }
            /// <summary>
            /// 设服务表
            /// </summary>
            public DbSet<Service> Services { get; set; }
            /// <summary>
            /// 认证记录信息表
            /// </summary>
            public DbSet<CertInfo> CertInfos { get; set; }
            
            /// <summary>
            /// 用户收藏表
            /// </summary>
            public DbSet<UserCollect> UserCollects { get; set; }
            /// <summary>
            /// 用户积分表
            /// </summary>
            public DbSet<UserIntegral> UserIntegrals { get; set; }
            /// <summary>
            /// 广告管理表
            /// </summary>
            public DbSet<AdBanner> AdBanners { get; set; }
            /// <summary>
            /// 房屋周边美食表
            /// </summary>
            public DbSet<HouseFood> HouseFoods { get; set; }
            /// <summary>
            /// 短信验证码临时数据
            /// </summary>
            public DbSet<SmsCode> SmsCodes { get; set; }
			/// <summary>
			/// 房屋订单-日期表
			/// </summary>
			public DbSet<OrderDate> OrderDates { get; set; }
		}
	}
}
