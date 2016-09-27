<%@ WebHandler Language="C#" Class="Service" %>

using System;
using System.Web;
using System.Text;
using System.IO;
using System.Xml;
using TencentMsg.Common.CommService;
using System.Web.Security;
using TencentMsg.Model;
using System.Collections.Generic;
using System.Data;
using TencentMsg.BLL.MsgData;
using TencentMsg.Common.CommonEnum;

public class Service : BaseHandler
{
    MsgDataBLL miBLL = new MsgDataBLL();
    public override void ProcessRequest(HttpContext context)
    {
        base.ProcessRequest(context);
        if (strActionName.Equals("GetMessageList"))//获取发送者信息列表
        {
            GetMessageList(context);
        }
        else if (strActionName.Equals("GetnewsType"))//获取图文描述
        {
            GetnewsType(context);
        }
        else if (strActionName.Equals("DownLoadContent"))//获取图文描述
        {
            DownLoadContent(context);
        }
        else
        {
            ResponseAjaxContent("AjaxCall请求为空");
        }
    }
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="context"></param>
    public void DownLoadContent(HttpContext context)
    {
        string mediaId = ConvertHelper.ToString(context.Request["mediaId"]);
        string voicePath = CommMsgParam.GetMultimedia(mediaId, false);
        ResponseAjaxContent(voicePath);
        //System.IO.Directory.GetFiles(CommonFun.ApplicationFilePath+"DownloadFile/2014-12-26/1.mp3", "*.mp3", System.IO.SearchOption.AllDirectories);
        //string path = CommonFun.PhysicalFilePath + voicePath;
        //string path1 = CommonFun.PhysicalFilePath + "imgMsg/2014-12-22/20141222111426.bmp";
        //FileInfo fi = new FileInfo(path1);
        //if (fi.Exists)
        //{
        //    context.Response.Clear();
        //    context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fi.Name);
        //    context.Response.AddHeader("Content-Length", fi.Length.ToString());
        //    context.Response.ContentType = "application/octet-stream;charset=gb2321";
        //    context.Response.WriteFile(fi.FullName);
        //    context.Response.Flush();
        //    context.Response.Close();
        //    context.ApplicationInstance.CompleteRequest();
        //}
    }
    /// <summary>
    /// 获取图文描述
    /// </summary>
    /// <returns></returns>
    public void GetnewsType(HttpContext context)
    {
        int num = ConvertHelper.ToInt(context.Request["num"]);
        StringBuilder strBr = new StringBuilder();
        strBr.AppendFormat("<tr title=\"newsType{0}\"><td class=\"leftBox\">标题：</td>", num);
        strBr.Append("<td class=\"rightBox\">");
        strBr.AppendFormat("<input type=\"text\" id=\"newsTitle{0}\" name=\"newsTitle{0}\" class=\"form_text\" maxlength=\"100\" style=\"width:500px;\">", num);
        strBr.AppendFormat("<input type=\"button\" id=\"newsAdd{0}\" class=\"form_bto_only\" style=\"margin-top:5px;\" value=\"+\" onclick=\"AddNews({1})\">", num, num + 1);
        strBr.AppendFormat("<input type=\"button\" id=\"newsDel{0}\" class=\"form_bto_only\" style=\"margin-top:5px;\" value=\"-\" onclick=\"DelNews({0})\">", num);
        strBr.Append("</td></tr>");
        strBr.AppendFormat("<tr title=\"newsType{0}\"><td class=\"leftBox\">点击链接：</td>", num);
        strBr.Append("<td class=\"rightBox\" colspan=\"3\">");
        strBr.AppendFormat("<input type=\"text\" id=\"newsUrl{0}\" class=\"form_text\" maxlength=\"100\" style=\"width:500px;\">", num);
        strBr.Append("</td></tr>");
        strBr.AppendFormat("<tr title=\"newsType{0}\"><td class=\"leftBox\">图片链接：</td>", num);
        strBr.Append("<td class=\"rightBox\" colspan=\"3\">");
        strBr.AppendFormat("<input type=\"text\" id=\"newsPicUrl{0}\" class=\"form_text\" maxlength=\"100\" style=\"width:500px;\">", num);
        strBr.Append("</td></tr>");
        strBr.AppendFormat("<tr title=\"newsType{0}\"><td class=\"leftBox\">图片描述：</td>", num);
        strBr.Append("<td class=\"rightBox\" colspan=\"3\">");
        strBr.AppendFormat("<textarea  id=\"newsDes{0}\" name=\"newsDes{0}\" class=\"form_text\" style=\"min-width:600px; max-width: 600px; min-height: 100px;max-height: 200px;\"></textarea>", num);
        strBr.Append("</td></tr>");
        ResponseAjaxContent(strBr.ToString());
    }
    /// <summary>
    /// 获取消息列表
    /// </summary>
    /// <param name="?"></param>
    public void GetMessageList(HttpContext context)
    {

        int pageIndex = ConvertHelper.ToInt(context.Request["currentPage"]);
        string CategoryName = context.Request["CategoryName"];//消息类型
        string txtBegin = context.Request["txtBegin"].Trim();//开始时间
        string txtEnd = context.Request["txtEnd"].Trim();//结束时间        
        int totalCount = 0;
        int pageSize = 10;
        List<string> tableTH = new List<string>();
        StringBuilder sqlStr = new StringBuilder();
        if (!string.IsNullOrEmpty(CategoryName))
        {
            sqlStr.AppendFormat(" AND TMIMsgType='{0}'", CategoryName);
        }
        if (!string.IsNullOrEmpty(txtBegin))
        {
            sqlStr.AppendFormat(" AND TMIAddTime >=#{0}#", txtBegin + " 00:00:00");
        }
        if (!string.IsNullOrEmpty(txtEnd))
        {
            sqlStr.AppendFormat(" AND TMIAddTime <=#{0}#", txtEnd + " 23:59:59");
        }
        sqlStr.AppendFormat(" AND TMISource=0 ");
        tableTH.Add("width:10%,发送人ID");
        tableTH.Add("width:10%,消息类型");
        tableTH.Add("width:10%,消息来源");
        tableTH.Add("width:40%,消息内容");
        tableTH.Add("width:15%,发送时间");
        tableTH.Add("width:15%,操作");
        StringBuilder strBr = new StringBuilder();
        DataTable dt = miBLL.ExecuteDataTable(" * ", ConvertHelper.ToString(sqlStr), pageIndex, pageSize, ref totalCount);
        if (ConvertHelper.checkData(dt))
        {
            foreach (DataRow dr in dt.Rows)
            {
                strBr.Append("<tr>");
                strBr.AppendFormat("<td>{0}</td>", dr["TMIOpenId"]);
                strBr.AppendFormat("<td>{0}</td>", dr["TMIMsgType"]);
                strBr.AppendFormat("<td>{0}</td>", CommonEnum.GetValueByEnumName(typeof(CommonEnum.enumMsgSource), ConvertHelper.ToInt(dr["TMISource"])));
                strBr.AppendFormat("<td>{0}</td>", dr["TMIContent"]);
                strBr.AppendFormat("<td>{0}</td>", dr["TMIAddTime"]);
                strBr.Append("<td>");
                strBr.AppendFormat("<input type=\"button\" class=\"form_bto_only\" value=\"回复\" onclick=\"ReplyMsg({0})\">", dr["TMIId"]);
                strBr.AppendFormat("<input type=\"button\" class=\"form_bto_only\" value=\"展开回复\" onclick=\"SelectReplyMsg(this,{0},'{1}')\">", dr["TMIId"], dr["TMIMsgRelative"]);
                strBr.AppendFormat(BtoType(ConvertHelper.ToString(dr["TMIMsgType"]), dr["TMIContent"], null));
                strBr.Append("</tr>");
                DataTable dte = ReplyData(dr["TMIMsgRelative"]);
                if (ConvertHelper.checkData(dte))
                {
                    string emsgType = string.Empty; ;//判断是否是文图类型
                    foreach (DataRow dre in dte.Rows)
                    {
                        strBr.AppendFormat("<tr class=\"hid{0}\" style=\"display:none;background:#f7d1f4;\">", dr["TMIId"]);
                        strBr.AppendFormat("<td>{0}</td>", dre["TMIOpenId"]);
                        strBr.AppendFormat("<td>{0}</td>", dre["TMIMsgType"]);
                        strBr.AppendFormat("<td>{0}</td>", CommonEnum.GetValueByEnumName(typeof(CommonEnum.enumMsgSource), ConvertHelper.ToInt(dre["TMISource"])));
                        strBr.AppendFormat("<td>{0}</td>", dre["TMIContent"]);
                        strBr.AppendFormat("<td>{0}</td>", dre["TMIAddTime"]);
                        strBr.Append("<td>");
                        strBr.AppendFormat(BtoType(ConvertHelper.ToString(dre["TMIMsgType"]), null, dre["TMIId"]));
                        strBr.Append("</tr>");
                    }
                }
                else
                {
                    strBr.AppendFormat("<tr class=\"hid{1}\" style=\"display:none;background:#f7d1f4;\"><td colspan=\"{0}\" style=\"text-align:center;\">暂无回复</td></tr>", tableTH.Count, dr["TMIId"]);//数据为空时
                }
            }
        }
        else
        {
            strBr.AppendFormat("<tr id=\"Line\"><td colspan=\"{0}\" style=\"text-align:center;\">暂无数据</td></tr>", tableTH.Count);//数据为空时
        }
        ResponseAjaxContent(new TencentMsg.Common.PageList().BindQueryListNewPage(tableTH, strBr.ToString(), pageIndex, totalCount, pageSize));
    }

    /// <summary>
    /// 获取回复消息
    /// </summary>
    /// <param name="TMIMsgRelative"></param>
    /// <returns></returns>
    public DataTable ReplyData(object TMIMsgRelative)
    {
        StringBuilder sqlStr = new StringBuilder();
        sqlStr.AppendFormat(" AND TMIMsgRelative='{0}'", TMIMsgRelative);
        sqlStr.AppendFormat(" AND TMISource=1 ");
        return miBLL.ExecuteDataTable(" * ", sqlStr.ToString());
    }
    /// <summary>
    /// 根据不同的消息类型返回不同的按钮
    /// </summary>
    /// <param name="msgType"></param>
    /// <returns></returns>
    public string BtoType(string msgType, object content, object id)
    {
        string btoType = string.Empty;//按钮类型
        if (msgType.Equals(CommonEnum.enumMsgType.text.ToString()))//文本内容无查看按钮
        { }
        else if (msgType == CommonEnum.enumMsgType.voice.ToString())//下载按钮
        {
            btoType = string.Format("<input type=\"button\" class=\"form_bto_only\" value=\"下载内容\" onclick=\"DownLoadContent({0})\">", content);
        }
        else//查看按钮
        {
            btoType = string.Format("<input type=\"button\" class=\"form_bto_only\" value=\"查看详情\" onclick=\"MsgDetails({0})\">", id);
        }
        return btoType;
    }
}