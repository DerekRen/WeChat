using System;
using System.Xml.Serialization;

namespace TencentMsg.Model.Send
{
    /// <summary>
    /// 语音消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class VoiceMessageModel:MsgBaseModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public VoiceMessageModel()
        {
            this.msgtype = "voice";
            this.voice = new VoiceModel();
        }
        /// <summary>
        /// 语音消息
        /// </summary>
        public VoiceModel voice { get; set; }

    }
    /// <summary>
    /// 发送语音消息
    /// </summary>
    public class VoiceModel
    {
        /// <summary>
        /// 语音的媒体ID
        /// </summary>
        public string media_id { get; set; }
    }
}
