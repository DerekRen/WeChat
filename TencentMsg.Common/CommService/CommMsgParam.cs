using System;
using System.Net;
using System.Web;
using System.Web.Caching;
using TencentMsg.Model;
using System.Configuration;
using System.IO;

namespace TencentMsg.Common.CommService
{
    /// <summary>
    /// 微信的一些公共参数
    /// </summary>
    public class CommMsgParam
    {


        /// <summary>
        /// access_token的缓存key
        /// </summary>
        public static string tokenKey = "BC9E-CD28-47A2-8AK7-6A21P8SDA78";

        #region 获取微信凭证
        public static string GetAccessToken()
        {
            //获取微信凭证access_token的接口
            string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            string accessToken = ConvertHelper.ToString(ReadMemoryCachel(tokenKey));//读取缓存
            if (string.IsNullOrEmpty(accessToken))
            {
                string respText = string.Empty;
                //获取在web_config中配置的appid和appsercret
                string wechat_appid = ConfigurationManager.AppSettings["appid"];
                string wechat_appsecret = ConfigurationManager.AppSettings["appsecret"];
                //获取josn数据
                string url = string.Format(getAccessTokenUrl, wechat_appid, wechat_appsecret);
                respText = HttpRequestHelper.HttpGetWebRequest(url, 10000);
                TockenModel token = JsonHelper.JsonDeserialize<TockenModel>(respText);//反序列化
                if (token != null)
                {
                    if (!string.IsNullOrEmpty(token.errmsg))
                    {
                        Log4NetHelper.WriteExceptionLog("获取token异常" + token.errmsg);
                        return null;
                    }
                    accessToken = token.access_token;
                }
                InertMemoryCache(tokenKey, accessToken, 7000);//微信accessToken过期时间是7200s，这里设置7000s。
            }
            return accessToken;
        }
        #endregion

        #region 上传多媒体
        /// <summary>
        /// 上传多媒体
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string UploadMultimedia(string filePath, string Type)
        {
            if (string.IsNullOrEmpty(filePath))
            { return null; }
            filePath = CommonFun.PhysicalFilePath + filePath;
            string url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + GetAccessToken() + "&type=" + Type;
            string result = HttpRequestHelper.HttpPostWebRequest(url, 10000, filePath);
            UploadModel model = JsonHelper.JsonDeserialize<UploadModel>(result);
            if (!string.IsNullOrEmpty(model.media_id))
            {
                return model.media_id;
            }
            return null;
        }
        #endregion
        #region 多媒体下载
        /// <SUMMARY> 
        /// 下载保存多媒体文件,返回多媒体保存路径 (微信不支持视频下载)
        /// </SUMMARY> 
        /// <PARAM name="ACCESS_TOKEN"></PARAM> 
        /// <PARAM name="MEDIA_ID"></PARAM> 
        /// <RETURNS></RETURNS> 
        public static string GetMultimedia(string MEDIA_ID, bool IsImage)
        {
            string ext = ".jpg";
            if (!IsImage)
            {
                ext = ".mp3";
            }
            string ACCESS_TOKEN = CommMsgParam.GetAccessToken();
            string fileName = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();
                Log4NetHelper.WriteExceptionLog("接收类别://" + myResponse.ContentType);
                WebClient mywebclient = new WebClient();
                fileName = "DownloadFile/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                savepath = CommonFun.PhysicalFilePath + fileName;
                Log4NetHelper.WriteExceptionLog("路径://" + savepath);
                try
                {
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }
                    savepath += fileName;
                    fileName += DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ext;
                    mywebclient.DownloadFile(strpath, savepath);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.WriteExceptionLog("下载异常：" + ex.Message);
                    return null;
                }
            }
            return fileName;
        }
        #endregion
        #region 内存缓存
        /// <summary>
        /// 向内存写入数据缓存
        /// </summary>
        /// <param name="cachekey">缓存标识关键字</param>
        /// <param name="cacheresult">需要存放的数据</param>
        /// <param name="cachetime">单位秒</param>
        public static void InertMemoryCache(string cachekey, object cacheresult, int cachetime)
        {
            if (cacheresult != null)
            {
                try
                {
                    HttpRuntime.Cache.Insert(cachekey, cacheresult, null
                    , Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(cachetime));
                }
                catch (Exception ex)
                {
                    Log4NetHelper.WriteExceptionLog("写入缓存异常", ex);
                }

            }
        }

        /// <summary>
        /// 移除内存中指定的数据缓存
        /// </summary>
        /// <param name="cachekey">缓存标识关键字</param>
        public static void RemoveMemoryCache(string cachekey)
        {
            try
            {
                HttpRuntime.Cache.Remove(cachekey);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("写入缓存异常", ex);
            }
        }

        /// <summary>
        /// 根据缓存标识读取内存缓存信息
        /// </summary>
        /// <param name="cachekey">缓存标识关键字</param>
        /// <returns>内存缓存数据</returns>
        public static object ReadMemoryCachel(string cachekey)
        {
            object obj = null;
            try
            {
                obj = HttpRuntime.Cache.Get(cachekey);
                return obj;
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("写入缓存异常", ex);
            }
            return null;
        }
        #endregion
    }
}
