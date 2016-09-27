using System;
using TencentMsg.Common.CommService;
using TencentMsg.IBLL;
using TencentMsg.Model.Send;
using TencentMsg.Model;

namespace TencentMsg.BLL.Send
{
    /// <summary>
    /// 发送消息服务
    /// </summary>
    public class SendMessageApi : ISendMessage
    {

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendTextMessage(TextMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendImageMessage(ImageMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendVoiceMessage(VoiceMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendVideoMessage(VideoMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendMusicMessage(MusicMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendNewsMessage(NewsMessageModel model)
        {
            string data = JsonHelper.JsonSerializer(model);//转化json数据
            return CommSendMessage(data);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private  bool CommSendMessage(string data)
        {
            string accessToken = "accessToken";
            string result = string.Empty;
            try
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", accessToken);
                result = HttpRequestHelper.HttpPostWebRequest(url, 100000, data);//post发送数据
                ErrModel err = JsonHelper.JsonDeserialize<ErrModel>(result);
                if (!string.IsNullOrEmpty(err.errcode))
                {
                    Log4NetHelper.WriteExceptionLog("发送消息失败;失败原因"+err.errmsg);//失败日志
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("发送消息异常", ex, data, result);//记录异常
                return false;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CommSendMessageTest(string data)
        {
            string accessToken = CommMsgParam.GetAccessToken();
            string result = string.Empty;
            try
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", accessToken);
                return HttpRequestHelper.HttpPostWebRequest(url, 100000, data);//post发送数据
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("发送消息异常", ex, data, result);//记录异常
                return "异常";
            }
        }
    }
}
