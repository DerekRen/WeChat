using System;
using System.Xml.Serialization;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 视频消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class VideoMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public VideoMessageModel()
        {
            this.msgtype = "video";
            this.video = new VideoModel();
        }
        /// <summary>
        /// 视频消息
        /// </summary>
        public VideoModel video { get; set; }

    }
    /// <summary>
    /// 发送视频消息
    /// </summary>
    public class VideoModel
    {
        /// <summary>
        /// 发送的视频的媒体ID
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string description { get; set; }
    }
}
