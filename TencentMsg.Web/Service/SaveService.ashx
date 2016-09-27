<%@ WebHandler Language="C#" Class="Service" %>

using System;
using System.Web;
using System.Text;
using System.IO;
using System.Xml;
using TencentMsg.Common.CommService;
using System.Web.Security;
using TencentMsg.Model;
using TencentMsg.Model.Send;
using TencentMsg.Common.CommonEnum;
using TencentMsg.Model.MsgData;
using TencentMsg.BLL.MsgData;
using System.Collections.Generic;

/// <summary>
/// 发送被动响应消息
/// </summary>
public class Service : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;//接收请求
        if (request.HttpMethod.ToUpper() == "POST")
        {
            StreamReader sr = new StreamReader(request.InputStream, Encoding.UTF8);
            string data = sr.ReadToEnd();//读取内存中流
            if (!string.IsNullOrEmpty(data))
            {
                SavePostData(data);
            }
        }
        else
        {
            AuthTest(); //微信接入的测试
        }
    }
    /// <summary>
    /// 保存推送过来的数据
    /// </summary>
    /// <param name="data"></param>
    public void SavePostData(string data)
    {
        bool Istrue = true;
        MsgDataBLL miBLL = new MsgDataBLL();
        MsgBaseModel typeModel = JsonHelper.JsonDeserialize<MsgBaseModel>(data);//反序列化获取消息类型
        MsgInfoModel miModel = new MsgInfoModel();
        MsgExtendModel meModel = new MsgExtendModel();
        //公用的字段统一赋值
        miModel.TMIAddTime = DateTime.Now;
        miModel.TMIMsgType = typeModel.msgtype;
        miModel.TMIOpenId = typeModel.touser;
        miModel.TMIMsgRelative = typeModel.msgrelative;
        miModel.TMISource = typeModel.msgsource;
        if (typeModel.msgtype == CommonEnum.enumMsgType.text.ToString())//文本消息保存
        {
            var model = JsonHelper.JsonDeserialize<TextMessageModel>(data);
            miModel.TMIContent = model.text.content;
            Istrue=miBLL.AddMsgInfo(miModel);
        }
        else if (typeModel.msgtype == CommonEnum.enumMsgType.image.ToString())//图片消息保存
        {
            var model = JsonHelper.JsonDeserialize<ImageMessageModel>(data);
            miModel.TMIContent = model.image.media_id;
            Istrue = miBLL.AddMsgInfo(miModel);
        }
        else if (typeModel.msgtype == CommonEnum.enumMsgType.voice.ToString())//语音消息保存
        {
            var model = JsonHelper.JsonDeserialize<VoiceMessageModel>(data);
            miModel.TMIContent = model.voice.media_id;
            Istrue = miBLL.AddMsgInfo(miModel);
        }
        else if (typeModel.msgtype == CommonEnum.enumMsgType.video.ToString())//视频消息保存
        {
            var model = JsonHelper.JsonDeserialize<VideoMessageModel>(data);
            miModel.TMIContent = model.video.media_id;
            int miId = miBLL.AddMsgInfoBackId(miModel);//保存消息主表
            meModel.TMEDes = model.video.description;
            meModel.TMEMainId = model.video.media_id;
            meModel.TMESubId = model.video.thumb_media_id;
            meModel.TMETIId = miId;
            meModel.TMETitle = model.video.title;
            Istrue = miBLL.AddMsgExtend(meModel);//保存消息扩展表
        }
        else if (typeModel.msgtype == CommonEnum.enumMsgType.music.ToString())//音乐消息保存
        {
            var model = JsonHelper.JsonDeserialize<MusicMessageModel>(data);
            miModel.TMIContent = model.music.thumb_media_id;
            int miId = miBLL.AddMsgInfoBackId(miModel);//保存消息主表
            meModel.TMEDes = model.music.description;
            meModel.TMEMainId = model.music.musicurl;
            meModel.TMESubId = model.music.hqmusicurl;
            meModel.TMETIId = miId;
            meModel.TMETitle = model.music.title;
            meModel.TMEmusicid = model.music.thumb_media_id;
            Istrue = miBLL.AddMsgExtend(meModel);//保存消息扩展表
        }
        else if (typeModel.msgtype == CommonEnum.enumMsgType.news.ToString())//图文消息保存
        {
            var model = JsonHelper.JsonDeserialize<NewsMessageModel>(data);
            miModel.TMIContent = "无";
            int miId = miBLL.AddMsgInfoBackId(miModel);
            if (miId > 0)
            {
                List<MsgExtendModel> ModelList = new List<MsgExtendModel>();
                foreach (NewsArticles articles in model.news.articles)
                {
                    meModel.TMEDes = articles.description;
                    meModel.TMEMainId = articles.url;
                    meModel.TMESubId = articles.picurl;
                    meModel.TMETitle = articles.title;
                    meModel.TMETIId = miId;
                    ModelList.Add(meModel);
                }
                Istrue = miBLL.AddMsgExtend(ModelList);
            }
        }
        ResponseContent(Istrue.ToString());
    }
    /// <summary>
    /// 成为开发者的第一步，验证并相应服务器的数据
    /// </summary>
    private void AuthTest()
    {
        string token = CommMsgParam.GetAccessToken();//从配置文件获取Token
        if (string.IsNullOrEmpty(token))
        {
            Log4NetHelper.WriteExceptionLog(string.Format("WeixinToken 配置项没有配置！"));
        }

        string echoString = HttpContext.Current.Request.QueryString["echoStr"];
        string signature = HttpContext.Current.Request.QueryString["signature"];
        string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
        string nonce = HttpContext.Current.Request.QueryString["nonce"];

        if (CheckSignature(token, signature, timestamp, nonce))
        {
            if (!string.IsNullOrEmpty(echoString))
            {
                HttpContext.Current.Response.Write(echoString);
                HttpContext.Current.Response.End();
            }
        }
    }

    /// <summary>
    /// 验证微信签名
    /// </summary>
    private bool CheckSignature(string token, string signature, string timestamp, string nonce)
    {
        string[] ArrTmp = { token, timestamp, nonce };

        Array.Sort(ArrTmp);
        string tmpStr = string.Join("", ArrTmp);

        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();

        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 返回Ajax请求内容
    /// </summary>
    /// <param name="content">返回内容</param>
    /// <param name="contentType">返回类型</param>
    public void ResponseContent(string content)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "text/xml";//"text/html";"text/xml";
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        HttpContext.Current.Response.Write(content);
        HttpContext.Current.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}