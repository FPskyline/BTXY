using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WePay
{
	internal class HttpService
	{
		public static async Task<string> Post(string xml, string url)
		{
			using (var client = new HttpClient())
			{
				var httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");
				var response = await client.PostAsync(url, httpContent);
				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}

