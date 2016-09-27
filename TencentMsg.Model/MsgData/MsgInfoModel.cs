using System;

namespace TencentMsg.Model.MsgData
{
    public class MsgInfoModel
    {
        private int _TMIId = 0;
        private string _TMIOpenId = string.Empty;
        private string _TMIMsgType = string.Empty;
        private string _TMIMsgRelative = string.Empty;
        private int _TMISource = 0;
        private string _TMIContent = string.Empty;
        private DateTime _TMIAddTime = DateTime.Now;
        /// <summary>
        /// 主键Id
        /// </summary>
        public int TMIId { get { return _TMIId; } set { _TMIId = value; } }

        /// <summary>
        /// 关注人Id
        /// </summary>
        public string TMIOpenId { get { return _TMIOpenId; } set { _TMIOpenId = value; } }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string TMIMsgType { get { return _TMIMsgType; } set { _TMIMsgType = value; } }

        /// <summary>
        /// 消息接收回复的唯一标识
        /// </summary>
        public string TMIMsgRelative { get { return _TMIMsgRelative; } set { _TMIMsgRelative = value; } }
        /// <summary>
        /// 消息来源（1:发送;0:接收）
        /// </summary>
        public int TMISource { get { return _TMISource; } set { _TMISource = value; } }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string TMIContent { get { return _TMIContent; } set { _TMIContent = value; } }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime TMIAddTime { get { return _TMIAddTime; } set { _TMIAddTime = value; } }
    }
}
