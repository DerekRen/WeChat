
namespace TencentMsg.Model.MsgData
{
    public class MsgExtendModel
    {
        private int _TMEId = 0;
        private int _TMETIId = 0;
        private string _TMETitle = string.Empty;
        private string _TMEDes = string.Empty;
        private string _TMEMainId = string.Empty;
        private string _TMESubId = string.Empty;
        private string _TMEmusicid = string.Empty;
        /// <summary>
        /// 主键Id
        /// </summary>
        public int TMEId { get { return _TMEId; } set { _TMEId = value; } }

        /// <summary>
        /// 消息主键Id
        /// </summary>
        public int TMETIId { get { return _TMETIId; } set { _TMETIId = value; } }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string TMETitle { get { return _TMETitle; } set { _TMETitle = value; } }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string TMEDes { get { return _TMEDes; } set { _TMEDes = value; } }

        /// <summary>
        /// 消息主标识（主url或主id）
        /// </summary>
        public string TMEMainId { get { return _TMEMainId; } set { _TMEMainId = value; } }

        /// <summary>
        /// 消息副标识（副url或副id）
        /// </summary>
        public string TMESubId { get { return _TMESubId; } set { _TMESubId = value; } }

        /// <summary>
        ///音乐的id（音乐类型专属）
        /// </summary>
        public string TMEmusicid { get { return _TMEmusicid; } set { _TMEmusicid = value; } }
    }
}
