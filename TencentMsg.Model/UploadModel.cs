
using System;
namespace TencentMsg.Model
{
    /// <summary>
    /// 上传实体
    /// </summary>
    public class UploadModel : ErrModel
    {
        public UploadModel()
        {
            this.UploadTime = GetTime(this.created_at);//时间戳转为DateTime时间
        }
        /// <summary>
        ///媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图
        ///（thumb，主要用于视频与音乐格式的缩略图）
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识
        /// 媒体文件在后台保存时间为3天，即3天后media_id失效。对于需要重复使用的多媒体文件，可以每3天循环上传一次，更新media_id。
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public string created_at { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 时间戳转为DateTime时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }
}
