using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Utility
{
    public class AzureBlobSetings
    {
        CloudBlobClient BlobClient;
        public AzureBlobSetings(IConfiguration Configuration)
        {
            //解析配置中的连接字符串
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.GetConnectionString("StorageConnectionString"));// "DefaultEndpointsProtocol=https;AccountName=restaurantblob;AccountKey=0132Y3/WP4ZJab40nxm5bchGBnK4U0AZHFA045Ocvokwn7iwo7lRygPXkD6NSSrW30dGFj9XX0hdUEgKkU7qvg==;EndpointSuffix=core.chinacloudapi.cn");
            //创建 Blob 服务客户端
            BlobClient = storageAccount.CreateCloudBlobClient();
        }

        public CloudBlockBlob GetContainer(string mycontainer, string fileName)
        {
            //获取容器的引用
            CloudBlobContainer container = BlobClient.GetContainerReference(mycontainer);
            //获取块 Blob 引用
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            return blob;
        }
        public async Task<string> UploadToBlob(string fileName, string mycontainer, string ContentType, Stream fileStream)
        {
            ////获取容器的引用
            CloudBlobContainer container = BlobClient.GetContainerReference(mycontainer);
			//创建一个容器（如果该容器不存在）
			await container.CreateIfNotExistsAsync();
			//设置该容器为公共容器，也就是说网络上能访问容器中的文件，但不能修改、删除
			await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //将Blob（文件）上载到容器中，如果已存在同名Blob，则覆盖它
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);//获取块 Blob 引用
            blockBlob.Properties.ContentType = ContentType;                                                                //文件路径

            await blockBlob.UploadFromStreamAsync(fileStream);

            return blockBlob.Uri.ToString();
        }
    }
}
