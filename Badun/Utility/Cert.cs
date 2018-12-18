using Badun.Dto;
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
	public class RespData
	{
		/// <summary>
		/// 
		/// </summary>
		public bool charge { get; set; }
		/// <summary>
		/// 返回码
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int remain { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string msg { get; set; }

        /// <summary>
		/// 
		/// </summary>
		public CertInfoDto result { get; set; }
    }

	/// <summary>
	/// 三网认证接口
	/// </summary>
	public class Cert
	{
		private string CertKey;
		public Cert(IConfiguration Configuration)
		{
            //解析配置中的连接字符串
            CertKey = Configuration.GetConnectionString("CertKey");
		}
		/// <summary>
		/// 短息接口
		/// </summary>
		/// <returns></returns>
		public RespData SendCert(string phone, string name, string idCard)
		{
			string appKey = CertKey; 
            var url = string.Format("https://way.jd.com/fegine/phoneCheck?idCard={0}&mobile={1}&name={2}&appkey={3}", idCard, phone, name, appKey);
			var client = new HttpClient();
			var result = client.GetAsync(url).Result;
            RespData repData = JsonConvert.DeserializeObject<RespData>(result.Content.ReadAsStringAsync().Result);
			return repData;
		}
	}

}
