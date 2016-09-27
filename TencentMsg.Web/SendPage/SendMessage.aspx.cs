using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using TencentMsg.BLL.MsgData;
using TencentMsg.BLL.Send;
using TencentMsg.Common.CommonEnum;
using TencentMsg.Common.CommService;
using TencentMsg.Model.MsgData;
using TencentMsg.Model.Send;

public partial class SendAction : BasePage
{
    int id = 0;
    MsgDataBLL miBLL = new MsgDataBLL();
    MsgInfoModel getmodel = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = ConvertHelper.ToInt(Request["id"]);
        getmodel = getModel(id);//根据消息主表Id获取相关信息
    }
    #region 回复消息
    /// <summary>
    /// 回复消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string data = string.Empty;
        SendMessageApi sendApi = new SendMessageApi();
        #region 文本消息
        if (hidMsgType.Value == CommonEnum.enumMsgType.text.ToString())//文本消息
        {
            TextMessageModel txtModel = new TextMessageModel();
            txtModel.msgtype = hidMsgType.Value;//消息类型
            txtModel.touser = getmodel.TMIOpenId;//接收人
            txtModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            txtModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            txtModel.text.content = txtContent.Text.Trim();
            if (!sendApi.SendTextMessage(txtModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(txtModel);
        }
        #endregion

        #region 图片消息
        else if (hidMsgType.Value == CommonEnum.enumMsgType.image.ToString())//图片消息
        {
            ImageMessageModel imgModel = new ImageMessageModel();
            string fileName = GetFileName(Fileimg.PostedFile, "imgMsg");
            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<script>alert('上传文件为空')</script>");
                return;
            }
            imgModel.msgtype = hidMsgType.Value;
            imgModel.touser = getmodel.TMIOpenId;
            imgModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            imgModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            imgModel.image.media_id = CommMsgParam.UploadMultimedia(fileName, CommonEnum.enumMsgType.image.ToString());//上传多媒体返回media_Id
            if (!sendApi.SendImageMessage(imgModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(imgModel);
        }
        #endregion

        #region 语音消息
        else if (hidMsgType.Value == CommonEnum.enumMsgType.voice.ToString())//语音消息
        {
            VoiceMessageModel vocModel = new VoiceMessageModel();
            string fileName = GetFileName(Filevoice.PostedFile, "voiceMsg");
            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<script>alert('上传文件为空')</script>");
                return;
            }
            vocModel.msgtype = hidMsgType.Value;
            vocModel.touser = getmodel.TMIOpenId;//接收人
            vocModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            vocModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            vocModel.voice.media_id = CommMsgParam.UploadMultimedia(fileName, CommonEnum.enumMsgType.voice.ToString());//上传多媒体返回media_Id
            if (!sendApi.SendVoiceMessage(vocModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(vocModel);
        }
        #endregion

        #region 视频消息
        else if (hidMsgType.Value == CommonEnum.enumMsgType.video.ToString())//视频消息
        {
            VideoMessageModel vdoModel = new VideoMessageModel();
            string fileName = GetFileName(FileVideo.PostedFile, "videoMsg");//上传返回名称
            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<script>alert('上传文件为空')</script>");
                return;
            }
            vdoModel.msgtype = hidMsgType.Value;
            vdoModel.touser = getmodel.TMIOpenId;//接收人
            vdoModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            vdoModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            vdoModel.video.media_id = CommMsgParam.UploadMultimedia(fileName, CommonEnum.enumMsgType.video.ToString());//上传多媒体返回media_Id
            vdoModel.video.thumb_media_id = CommMsgParam.UploadMultimedia(fileName, CommonEnum.enumMsgType.thumb.ToString());//上传多媒体返回media_Id
            vdoModel.video.title = title.Text;
            vdoModel.video.description = description.Text;
            if (!sendApi.SendVideoMessage(vdoModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(vdoModel);
        }
        #endregion

        #region 音乐消息
        else if (hidMsgType.Value == CommonEnum.enumMsgType.music.ToString())//音乐消息
        {
            MusicMessageModel musModel = new MusicMessageModel();
            string fileName = GetFileName(FileMusic.PostedFile, "musicMsg");
            if (string.IsNullOrEmpty(fileName))
            {
                Response.Write("<script>alert('上传文件为空')</script>");
                return;
            }
            musModel.msgtype = hidMsgType.Value;
            musModel.touser = getmodel.TMIOpenId;//接收人
            musModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            musModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            musModel.music.musicurl = musicUrl.Text.Trim();
            musModel.music.hqmusicurl = hqMusicUrl.Text;
            musModel.music.title = title.Text;
            musModel.music.description = description.Text;
            musModel.music.thumb_media_id = CommMsgParam.UploadMultimedia(fileName, CommonEnum.enumMsgType.thumb.ToString());//上传多媒体返回media_Id;
            if (!sendApi.SendMusicMessage(musModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(musModel);
        }
        #endregion

        #region 图文消息
        else if (hidMsgType.Value == CommonEnum.enumMsgType.news.ToString())//图文消息
        {
            NewsMessageModel newsModel = new NewsMessageModel();
            newsModel.msgtype = hidMsgType.Value;
            newsModel.touser = getmodel.TMIOpenId;//接收人
            newsModel.msgrelative = getmodel.TMIMsgRelative;//消息唯一标识
            newsModel.msgsource = CommonEnum.enumMsgSource.send.GetHashCode();//消息来源
            List<NewsArticles> list = new List<NewsArticles>();
            string newslist = hidnewsType.Value.TrimEnd('|');
            string[] newsListArr = newslist.Split('|');
            string picUrls = string.Empty;//获取所有的图片链接
            for (int i = 0; i < newsListArr.Length; i++)
            {
                NewsArticles articles = new NewsArticles();
                string[] listArr = newsListArr[i].Split(',');
                articles.title = listArr[0];//图文标题
                articles.url = listArr[1];//点击跳转的链接
                articles.picurl = listArr[2];//图文消息的链接
                articles.description = listArr[3];//描述
                list.Add(articles);
                picUrls += list[0] + ",";
            }
            newsModel.news.articles = list;
            if (!sendApi.SendNewsMessage(newsModel))
            {
                Response.Write("<script>alert('发送失败')</script>");
                return;
            }
            data = JsonHelper.JsonSerializer(newsModel);
        }
        #endregion

        SaveMessage(data);//保存消息
    }
    #endregion

    #region 上传文件
    /// <summary>
    /// 获取上传文件名称
    /// </summary>
    /// <param name="inputFile"></param>
    /// <param name="saveRootFolder"></param>
    /// <returns></returns>
    public string GetFileName(HttpPostedFile inputFile, string saveRootFolder)
    {
        if (inputFile.ContentLength > 0)
        {
            string msg = string.Empty;
            string fileName = string.Empty;
            CommonFun.FileUpload(inputFile, saveRootFolder, ref msg, ref fileName);
            if (!string.IsNullOrEmpty(msg))
            {
                Alert("上传出错");
                Log4NetHelper.WriteExceptionLog("上传报错:" + msg);
                return null;
            }
            return fileName;
        }
        Alert("上传文件不存在");
        return null;
    }
    #endregion

    #region 保存消息
    /// <summary>
    /// 保存消息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="url"></param>
    private void SaveMessage(string data)
    {
        try
        {
            //保存消息
            string SaveUrl = "http://" + Request.Url.Authority + "/Service/SaveService.ashx";
            string SaveBack = HttpRequestHelper.HttpPostWebRequest(SaveUrl, 100000000, data);
            if (SaveBack.ToLower() == "true")
            {
                AlertFun("保存成功", "window.opener.QueryDataList(1);window.close();");
            }
            else
            {
                Alert("保存失败");
            }
        }
        catch (Exception ex)
        {
            Log4NetHelper.WriteExceptionLog("发送消息异常:" + ex.Message);
            return;//异常不保存消息
        }
    }
    #endregion

    #region 获取消息主表
    /// <summary>
    /// 获取消息主表的OpenId和消息的发送接收之间的唯一标识
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MsgInfoModel getModel(int id)
    {
        MsgInfoModel model = new MsgInfoModel();
        string show = "TMIOpenId,TMIMsgRelative";
        string where = " AND TMIId=" + id;
        DataTable dt = miBLL.ExecuteDataTable(show, where);
        if (ConvertHelper.checkData(dt))
        {
            model.TMIOpenId = ConvertHelper.ToString(dt.Rows[0]["TMIOpenId"]);
            model.TMIMsgRelative = ConvertHelper.ToString(dt.Rows[0]["TMIMsgRelative"]);
        }
        return model;
    }
    #endregion
}