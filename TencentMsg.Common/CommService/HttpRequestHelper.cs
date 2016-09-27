using System.IO;
using System.Net;
using System.Text;

namespace TencentMsg.Common.CommService
{
    /// <summary>
    /// url请求处理
    /// </summary>
    public class HttpRequestHelper
    {
        /// <summary>
        /// 获取GET请求结果
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回请求结果</returns>
        public static string HttpGetWebRequest(string requestUrl, int timeout)
        {
            string response = string.Empty;
            if (!string.IsNullOrEmpty(requestUrl))
            {
                HttpWebRequest myRequest = WebRequest.Create(requestUrl) as HttpWebRequest;
                if (myRequest != null)
                {
                    myRequest.Method = "GET";
                    myRequest.Timeout = timeout;
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        response = sr.ReadToEnd();
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// 获取POST请求结果(默认utf-8方式解码)
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="requestXML">请求xml内容</param>
        /// <returns></returns>
        public static string HttpPostWebRequest(string requestUrl, int timeout, string requestXML)
        {
            return HttpPostWebRequest(requestUrl, timeout, requestXML, "utf-8");
        }

        /// <summary>
        /// 获取POST请求结果
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="requestXML">请求xml内容</param>
        /// <param name="encoding">解码方式</param>
        /// <returns>返回请求结果</returns>
        public static string HttpPostWebRequest(string requestUrl, int timeout, string requestXML, string encoding)
        {
            string response = string.Empty;
            if (!string.IsNullOrEmpty(requestUrl) && !string.IsNullOrEmpty(requestXML))
            {
                try
                {
                    HttpWebRequest myRequest = WebRequest.Create(requestUrl) as HttpWebRequest;
                    if (myRequest != null)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(requestXML);
                        myRequest.ServicePoint.Expect100Continue = false;
                        myRequest.ServicePoint.UseNagleAlgorithm = false;
                        myRequest.Headers.Clear();  //清除http请求头信息
                        myRequest.Timeout = timeout;   //超时时间
                        myRequest.Method = "POST";  //POST方式提交
                        myRequest.ContentType = "text/xml;charset=utf-8"; //编码格式utf-8
                        myRequest.ContentLength = data.Length;
                        using (Stream datasteam = myRequest.GetRequestStream())
                        {
                            datasteam.Write(data, 0, data.Length);
                            datasteam.Close();
                        }

                        HttpWebResponse myResponse = myRequest.GetResponse() as HttpWebResponse;
                        using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding(encoding)))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    return "";
                }
            }
            return response;
        }
    }
}
