using System;
using System.Xml.Serialization;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 音乐消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class MusicMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public MusicMessageModel()
        {
            this.msgtype = "music";
            this.music = new MusicModel();
        }
        /// <summary>
        /// 音乐消息
        /// </summary>
        public MusicModel music { get; set; }

    }
    /// <summary>
    /// 发送音乐消息
    /// </summary>
    public class MusicModel
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 音乐描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl { get; set; }

        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl { get; set; }

        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id { get; set; }
    }
}
