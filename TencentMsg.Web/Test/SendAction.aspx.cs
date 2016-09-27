using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TencentMsg.Model;
using TencentMsg.Common.CommService;
using TencentMsg.Model.Send;
using TencentMsg.BLL.Send;
using TencentMsg.BLL.MsgData;
using System.Data;
using TencentMsg.DAL;
using System.IO;

public partial class SendAction : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    public void InitPage()
    {
        MsgDataBLL BLL = new MsgDataBLL();
       // DBHelper.ExecuteSql("insert into TMsgInfo (TMIOpenId) values ('openid测试')");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        NewsMessageModel nmsg = new NewsMessageModel();
        nmsg.touser = "@all1";
        List<NewsArticles> list = new List<NewsArticles>();
        NewsArticles articles = new NewsArticles();
        articles.description = "图文描述";
        articles.picurl = "图片Url";
        articles.title = "图片标题";
        articles.url = "跳转Url";
        list.Add(articles);
        NewsArticles articles1 = new NewsArticles();
        articles1.description = "图文描述1";
        articles1.picurl = "图片Url1";
        articles1.title = "图片标题1";
        articles1.url = "跳转Url1";
        list.Add(articles1);
        nmsg.news.articles = list;

        string json = JsonHelper.JsonSerializer(nmsg);
        string url = "http://10.1.40.136:8040/Service/SaveService.ashx";
        HttpRequestHelper.HttpPostWebRequest(url, 10000, json);
        //string msg = SendMessageApi.CommSendMessageTest(json);//接手post请求测试
        //MusicMessageModel msModel = new MusicMessageModel();
        //msModel.touser = "@all";
        //msModel.music.description = "音乐描述";
        //msModel.music.hqmusicurl = "高品质链接";
        //msModel.music.thumb_media_id = "缩略ID";
        //msModel.music.title = "主题";
        //msModel.music.musicurl = "音乐链接";
        //string json = JsonHelper.JsonSerializer(msModel);
        //msg = string.Format("<script>jsonF('{0}')</script>", msg);
        //div_show.InnerHtml = msg;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        object ss = CommMsgParam.ReadMemoryCachel("1234");
        CommMsgParam.RemoveMemoryCache("1234");
        ss = CommMsgParam.ReadMemoryCachel("1234");
        //CommMsgParam.InertMemoryCache("1234","你好",1000);
        object qwe = CommMsgParam.ReadMemoryCachel(CommMsgParam.tokenKey);
        CommMsgParam.RemoveMemoryCache(CommMsgParam.tokenKey);
        qwe = CommMsgParam.ReadMemoryCachel(CommMsgParam.tokenKey);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string path1 = CommonFun.PhysicalFilePath + "imgMsg/2014-12-22/20141222111426.bmp";
        FileInfo fi = new FileInfo(path1);
        if (fi.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename="+fi.Name);
            Response.AddHeader("Content-Length", fi.Length.ToString());
            Response.ContentType = "application/octet-stream;charset=gb2321";
            Response.WriteFile(fi.FullName);
            Response.Flush();
            Response.Close();
            ApplicationInstance.CompleteRequest();
        }
    }
}