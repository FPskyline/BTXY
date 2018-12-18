using Badun.Models;
using Badun.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Badun.Models.InitModels;

namespace Badun.Data
{
	public static class DbInitializer
	{
		public static void EnsureSeedDataForContext(this MyDbContext _context)
		{
			if (!_context.Limits.Any())
			{
				//权限
				var limitinfo = new List<Limit>()
				{
					new Limit {Name ="参数",Type = 1,TypeName = "小程序管理"},
					new Limit {Name ="系统用户",Type = 2,TypeName = "系统管理"},
					new Limit {Name ="系统角色",Type = 2,TypeName = "系统管理"},
					new Limit {Name ="房主",Type = 3,TypeName = "房源管理"},
					new Limit {Name ="客房",Type = 3,TypeName = "房源管理"},
					new Limit {Name ="订单",Type = 4,TypeName = "订单管理"},
					new Limit {Name ="会员列表",Type = 5,TypeName = "会员管理"},
				};
				_context.Limits.AddRange(limitinfo);
				_context.SaveChanges();
				//初始角色
				var role = new Role { Name = "超级管理员", IsDel = 0 };
				_context.Roles.Add(role);
				_context.SaveChanges();
				//系统用户
				PwdTransition pwdTransition = new PwdTransition();
				var Salt = Guid.NewGuid().ToString();
				var Hashpwd = pwdTransition.ToHash("111111", Salt);
				var sysuser = new Sys_User { Account = "admin", Password = Hashpwd, Salt = Salt, Name = "超管", CreateDate = DateTime.Now, UpDate = DateTime.Now, RoleId = 1,RoleName= "超级管理员",IsDel = 0, };
				_context.Sys_Users.Add(sysuser);
				_context.SaveChanges();
				var Role_Limit = new List<Role_Limit>();
				foreach(var item in limitinfo)
				{
					Role_Limit.Add(new Role_Limit { RoleId = role.Id, Roles = role, LimitId = item.Id, limits = item });
				}
				_context.Role_Limits.AddRange(Role_Limit);
				_context.SaveChanges();
			}
			if (!_context.Applet.Any())
			{
				//初始小程序参数
				var applet = new Applet()
				{
					Name = "巴顿小程序",
					CreateDate = DateTime.Now,
					UpDate = DateTime.Now
				};
				_context.Applet.Add(applet);
				_context.SaveChanges();
			}


		}
	}
}
