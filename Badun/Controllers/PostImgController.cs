using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Badun.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Badun.Controllers
{
	/// <summary>
	/// 图片上传控制器
	/// </summary>
    [Route("api/[controller]/[action]")]
    [Consumes("application/json", "application/json-patch-json","multipart/form-data")]
    public class PostImgController : Controller
    {
        private IConfiguration _configuration;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Configuration"></param>
        public PostImgController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostProfilePicture(string path)
        {
            var fileName = "";
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            AzureBlobSetings AzureBlob = new AzureBlobSetings(_configuration);
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string suffix = fileName.Split('.')[1];
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + suffix;
                fileName = await AzureBlob.UploadToBlob(fileName, path, file.ContentType.ToString(), stream);
            }
            return new JsonResult(fileName);
        }
    }
}
