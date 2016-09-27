using System;
using System.Xml.Serialization;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 基础消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class TextMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public TextMessageModel()
        {
            this.msgtype = "text";
            this.text = new TextModel();
        }
        /// <summary>
        /// 文本消息
        /// </summary>
        public TextModel text { get; set; }

    }
    /// <summary>
    /// 发送文本消息
    /// </summary>
    public class TextModel
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string content { get; set; }
    }
}
