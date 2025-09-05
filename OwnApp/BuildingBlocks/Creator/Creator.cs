using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Creator
{
    public static class Creator
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// 生成唯一订单号，例如：202509051230451234
        /// 格式：yyyyMMddHHmmss + 4位随机数
        /// </summary>
        public static string CreateOrderNumber()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            int randomSuffix = _random.Next(1000, 9999);
            return $"{timestamp}{randomSuffix}";
        }

        /// <summary>
        /// 生成临时用户名，例如：用户1234
        /// </summary>
        public static string CreateTempUserName()
        {
            int suffix = _random.Next(1000, 9999);
            return $"用户{suffix}";
        }

        /// <summary>
        /// 生成 GUID 字符串（32位无分隔符）
        /// </summary>
        public static string CreateGuidString()
        {
            return Guid.NewGuid().ToString("N"); // N 表示 32 位无分隔符
        }
    }
}
