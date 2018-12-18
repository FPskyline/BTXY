using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Badun.Utility
{
	//[Consumes("application/json", "application/json-patch-json", "multipart/form-data")]
	public class WxMap
    {
		private IConfiguration _configuration;
		public WxMap(IConfiguration Configuration)
		{
			_configuration = Configuration;
		}

		public async Task<string> CreatMapAsync(string lat, string log)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{

					MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
					client.DefaultRequestHeaders.Accept.Add(contentType);
					var zoom = "&zoom=14";
					var markers = "&markers=size:large|" + lat + "," + log;
					var key = "&key=DXLBZ-FQYEO-63EW4-SK6CI-W3LDZ-TBFMX";
					var size = "&size=500*200";
					HttpResponseMessage response = client.GetAsync("https://apis.map.qq.com/ws/staticmap/v2/?center=" + lat + "," + log + zoom + markers + key + size).Result;
					var stream = response.Content.ReadAsStreamAsync().Result;
					//存入blob
					AzureBlobSetings AzureBlob = new AzureBlobSetings(_configuration);
					var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
					fileName = await AzureBlob.UploadToBlob(fileName, "wxhousemap", "image/png", stream);
					return fileName;
				}

			}
			catch 
			{
				return "";
			}

		}
    }
}
