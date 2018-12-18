using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 实名认证模型
    /// </summary>
    public class CertDto
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 验证码ID
        /// </summary>
        public long CodeId { get; set; }        
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNum { get; set; }
        /// <summary>
        /// 是否实名 1-已经实名  2-未实名 3-实名失败
        /// </summary>
        public int IsReal { get; set; }
        /// <summary>
        /// 手机号号
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否手机认证 1-已认证  2-未认证
        /// </summary>
        public int IsCert { get; set; }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public string PhoneCode { get; set; }

    }
}
