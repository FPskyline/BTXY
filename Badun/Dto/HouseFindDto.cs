using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Badun.Dto
{
    /// <summary>
    /// 查询对象
    /// </summary>
    public class HouseFindDto
    {
        /// <summary>
        /// 房屋Id
        /// </summary>
        public long HouseId { get; set; }
        /// <summary>
        /// 服务类型  1-设施 2-服务 3-须知
        /// </summary>
        public int Type { get; set; }
    }
}
