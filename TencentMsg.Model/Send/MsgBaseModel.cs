using System;
using System.Xml.Serialization;

namespace TencentMsg.Model
{
    /// <summary>
    /// 基础消息内容
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class MsgBaseModel
    {
        private int _msgsource = 0;
        private string _msgrelative = DateTime.Now.ToString("yyyyMMddHHmmssfff");//消息接收回复标识默认值
        private string _touser = string.Empty;
        private string _msgtype = string.Empty;
        /// <summary>
        /// UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// </summary>
        public string touser { get { return _touser; } set { _touser = value; } }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get { return _msgtype; } set { _msgtype = value; } }

        /// <summary>
        /// 消息回复标识
        /// </summary>
        public string msgrelative { get { return _msgrelative; } set { _msgrelative = value; } }

        /// <summary>
        /// 消息来源（0:接收，1：发送）
        /// </summary>
        public int msgsource { get { return _msgsource; } set { _msgsource = value; } }
    }
}
