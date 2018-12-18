using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Badun.Utility
{
	/// <summary>
	/// 返回数据对象
	/// </summary>
	public class RepData
	{
		/// <summary>
		/// 随机数
		/// </summary>
		public string bacthseq { get; set; }
		/// <summary>
		/// 返回码
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		public string msg { get; set; }
		/// <summary>
		/// 状态码
		/// </summary>
		public bool success { get; set; }
	}

	/// <summary>
	/// 短信接口
	/// </summary>
	public class Sms
	{
		private string uidString;
		private string pwdString;
		public Sms(IConfiguration Configuration)
		{
			//解析配置中的连接字符串
			uidString = Configuration.GetConnectionString("SmsUid");
			pwdString = Configuration.GetConnectionString("SmsPasswd");
		}
		/// <summary>
		/// 短息接口
		/// </summary>
		/// <param name="uid"></param>
		/// <param name="passwd"></param>
		/// <param name="phonelist"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public RepData SendSms(string phonelist, string content)
		{
			string uid = uidString;
			string passwd = pwdString;
			content = HttpUtility.UrlEncode(content, Encoding.UTF8);
			var url = string.Format("http://sms.bamikeji.com:8890/mtPort/mt/normal/send?uid={0}&passwd={1}&phonelist={2}&content={3}", uid, passwd, phonelist, content);
			var client = new HttpClient();
			var result = client.GetAsync(url).Result;
			RepData repData = JsonConvert.DeserializeObject<RepData>(result.Content.ReadAsStringAsync().Result);
			return repData;
		}
	}

}
