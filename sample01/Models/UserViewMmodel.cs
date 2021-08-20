using System;

namespace sample01.Models
{
    /// <summary>
    ///用户基本信息
    /// </summary>
    public class UserViewMmodel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; } = "hanbing";

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; } = "老卢聊技术";

        /// <summary>
        /// qq号码
        /// </summary>
        public string qq { get; set; } = "81868164";

        /// <summary>
        ///微信号码
        /// </summary>
        public string wxid { get; set; } = "hanbing_81868164";

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creation_time { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public string version { get; set; }
    }
}
