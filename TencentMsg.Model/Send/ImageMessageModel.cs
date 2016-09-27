using System;
using System.Xml.Serialization;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 基础消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class ImageMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ImageMessageModel()
        {
            this.msgtype = "image";
            this.image = new ImageModel();
        }
        /// <summary>
        /// 图片消息
        /// </summary>
        public ImageModel image { get; set; }

    }
    /// <summary>
    /// 发送图片消息
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// 图片的媒体ID
        /// </summary>
        public string media_id { get; set; }
    }
}
