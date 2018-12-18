using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Models
{
    public class SmsCode
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
    }
}
