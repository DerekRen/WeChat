
namespace TencentMsg.Model
{
    /// <summary>
    /// token实体
    /// </summary>
    public class TockenModel:ErrModel
    {
        /// <summary>
        /// token值
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string expires_in { get; set; }

    }
}
