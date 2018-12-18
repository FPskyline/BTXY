using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    /// <summary>
    /// 身份验证
    /// </summary>
    public class CertInfo
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 验证通过	提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 四川省乐山市夹江县	身份证所在你
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 1995-11-11	生日
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 乐山市	城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 夹江县	区县
        /// </summary>
        public string prefecture { get; set; }
        /// <summary>
        /// 511126199511111111	身份证号码
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 男	性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 18000000000	电话
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 联通	运营商
        /// </summary>
        public string mobileType { get; set; }
        /// <summary>
        /// 511126	地区代码
        /// </summary>
        public string addrCode { get; set; }
        /// <summary>
        /// 四川省	省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 张三	姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 1	校验码 
        /// </summary>
        public string lastCode { get; set; }
        /// <summary>
        ///  01	状态码:01 通过；02 不通过 ；
        /// </summary>
        public string status { get; set; }
    }
}
