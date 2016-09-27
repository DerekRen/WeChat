using TencentMsg.Model.Send;

namespace TencentMsg.IBLL
{
    /// <summary>
    /// 发送消息接口
    /// </summary>
    public interface ISendMessage
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendTextMessage(TextMessageModel model);

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendImageMessage(ImageMessageModel model);

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendVoiceMessage(VoiceMessageModel model);

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendVideoMessage(VideoMessageModel model);

        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendMusicMessage(MusicMessageModel model);

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendNewsMessage(NewsMessageModel model);
    }
}
