using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Badun.Models.InitModels;
using Badun.Dto;
using Badun.Utility;
using Microsoft.AspNetCore.Http;
using Badun.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using WePay;
using System.IO;

namespace Badun.Controllers
{
	/// <summary>
	/// wechat
	/// </summary>
	[Route("api/[controller]/[action]")]
	public class WeChatController : Controller
	{
		private MyDbContext _context;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public WeChatController(MyDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// 微信支付数据传输对象
		/// </summary>
		public class WePayDto
		{
			public string timeStamp { get; set; }
			public string package { get; set; }
			public string PaySign { get; set; }
			public string NonceStr { get; set; }
			public string signType { get; set; }
		}
		/// <summary>
		///  微信支付请求数据传输对象
		/// </summary>
		public class WePayRequestDto
		{
			/// <summary>
			/// 用户id
			/// </summary>
			public long UserId { get; set; }
			/// <summary>
			/// 总金额
			/// </summary>
			public int total_fee { get; set; }
			/// <summary>
			/// 商品详情描述
			/// </summary>
			public string body { get; set; }
			/// <summary>
			/// 订单编号
			/// </summary>
			public string OrderNo { get; set; }
		}
		public class WeChatDto
		{
			public long id { get; set; }
			public string appid { get; set; }
			public string js_code { get; set; }

		}
		public class WeChatInfo
		{
			public string OpenId { get; set; }
			public string Session_key { get; set; }
			public long MerId { get; set; }
		}
		/// <summary>
		/// 发送code 解析openid
		/// </summary>
		/// <returns></returns>
		[HttpPost]

		public IActionResult HttpPost([FromBody]WeChatDto model)
		{

			try
			{
				using (HttpClient client = new HttpClient())
				{
					WeChatInfo weChatInfo = new WeChatInfo();
					MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
					client.DefaultRequestHeaders.Accept.Add(contentType);
					HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/sns/jscode2session?appid=" + "wx3a42c30d316ede91" + "&secret=" + "0554d40e2cea40ea521acd4a40ba63bc" + "&js_code=" + model.js_code + "&grant_type=authorization_code").Result;
					string stringData = response.Content.ReadAsStringAsync().Result;

					var jObject = JObject.Parse(stringData);
					weChatInfo.OpenId = jObject["openid"].ToString();
					weChatInfo.Session_key = jObject["session_key"].ToString();
					var token = Guid.NewGuid().ToString();
					return Ok(weChatInfo);
				}

			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 支付（小程序）
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Pay([FromBody]WePayRequestDto wePayRequestDto)
		{
			try
			{
				var User = _context.Users.Where(x => x.Id == wePayRequestDto.UserId).FirstOrDefault();
				if (User == null)
				{
					return BadRequest("无此用户");
				}
				var Order = _context.Orders.Where(x => x.OrderNo == wePayRequestDto.OrderNo && x.UserId == User.Id).FirstOrDefault();
				if (Order == null)
				{
					return BadRequest("无此订单");
				}
				//var ran = new Random();
				//var outTradeNo = string.Format("{0}{1}{2}", "1492059872", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
				TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
				var timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();



				var data = new WxPayData();
				data.SetValue("appid", "wx998cfc2cc3850f9c");//微信分配的公众账号ID
				data.SetValue("body", wePayRequestDto.body);//商品描述
				data.SetValue("mch_id", "1492059872");//微信支付分配的商户号
				data.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串		
				data.SetValue("notify_url", "https://badun.chinacloudsites.cn/api/WeChat/PayNotify");//接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数http://www.weixin.qq.com/wxpay/pay.php
																										//data.SetValue("openid", "oJM_05YO1aKaL2XugzCKiuwgSEsE");
				data.SetValue("out_trade_no", wePayRequestDto.OrderNo);//商户订单号
				data.SetValue("spbill_create_ip", "8.8.8.8");//APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP
				data.SetValue("sub_appid", "wx3a42c30d316ede91");//当前调起支付的小程序APPID

				data.SetValue("sub_mch_id", "1515156771");//微信支付分配的子商户号
				data.SetValue("sub_openid", User.We_OpenId);
				data.SetValue("total_fee", (int)Order.Amount*100);//总金额
				//data.SetValue("total_fee", 1);//总金额
				data.SetValue("trade_type", "JSAPI");//支付类型
													 //data.SetValue("spbill_create_ip", Request.HttpContext.Connection.RemoteIpAddress);//用户端实际ip	

				data.SetValue("sign", data.MakeSign());//签名
				string xml = data.ToXml();
				string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
				string response = await HttpService.Post(xml, url);
				WxPayData result = new WxPayData();
				result.FromXml(response);
				//小程序二次签名
				var newdata = new WxPayData();
				newdata.SetValue("appId", "wx3a42c30d316ede91");//微信分配的小程序ID，服务商模式下应为当前调起支付小程序的appid
				newdata.SetValue("nonceStr", result.GetValue("nonce_str").ToString());//随机字符串
				newdata.SetValue("package", "prepay_id=" + result.GetValue("prepay_id").ToString());//	统一下单接口返回的 prepay_id 参数值，提交格式如：prepay_id=*
				newdata.SetValue("signType", "MD5");//	签名类型，默认为MD5，支持HMAC-SHA256和MD5。注意此处需与统一下单的签名类型一致
				newdata.SetValue("timeStamp", timestamp);//时间戳


				var wePayDto = new WePayDto()
				{
					package = "prepay_id=" + result.GetValue("prepay_id").ToString(),
					NonceStr = result.GetValue("nonce_str").ToString(),
					PaySign = newdata.MakeSign(),
					timeStamp = timestamp
				};

				return Ok(wePayDto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/// <summary>
		/// 微信支付结果通知
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public IActionResult PayNotify()
		{
			//接收从微信后台POST过来的数据
			string postStr = "";
			using (var ms = new MemoryStream())
			{
				Request.Body.CopyTo(ms);
				var myByteArray = ms.ToArray();
				postStr = System.Text.Encoding.UTF8.GetString(myByteArray);
			}
			//转换数据格式并验证签名
			WxPayData notifyData = new WxPayData();
			try
			{
				notifyData.FromXml(postStr);
			}
			catch (Exception ex)
			{
				//若签名错误，则立即返回结果给微信支付后台
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "FAIL");
				res.SetValue("return_msg", ex.Message);
				return BadRequest(res.ToXml());
			}
			//检查支付结果中transaction_id 微信支付订单号 是否存在
			if (!notifyData.IsSet("out_trade_no"))
			{
				//若transaction_id不存在，则立即返回结果给微信支付后台
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "FAIL");
				res.SetValue("return_msg", "支付结果中微信订单号不存在");
				return BadRequest(res.ToXml());
			}
			string out_trade_no = notifyData.GetValue("out_trade_no").ToString();
			var orderinfo = _context.Orders.Where(x => x.OrderNo == out_trade_no).FirstOrDefault();
			//查询订单，判断订单真实性
			if (orderinfo == null)
			{
				//若订单查询失败，则立即返回结果给微信支付后台
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "FAIL");
				res.SetValue("return_msg", "订单查询失败");
				return BadRequest(res.ToXml());
			}
			//查询订单成功
			else
			{
				//修改数据库订单状态
				orderinfo.State = 1;
				_context.SaveChanges();
				//上报微信服务器xml
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "SUCCESS");
				res.SetValue("return_msg", "OK");
				return Ok(res.ToXml());
			}
		}
	}
}
