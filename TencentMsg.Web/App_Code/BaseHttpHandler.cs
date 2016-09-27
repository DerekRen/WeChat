using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BaseHandler 的摘要说明
/// </summary>
public class BaseHandler : IHttpHandler
{
    /// <summary>
    /// 请求的Action名称
    /// </summary>
    public static string strActionName = string.Empty;
    /// <summary>
    /// 网站路径：/虚拟目录名称
    /// </summary>
    public virtual void ProcessRequest(HttpContext context)
    {
        strActionName = context.Request["actionName"] ?? "";
        if (strActionName == "")
        {
            strActionName = context.Request["action"] ?? "";
            if (strActionName == "")
            {
                ResponseAjaxContent("action请求不能为空");
            }
            return;
        }
        context.Response.Clear();
    }
    /// <summary>
    /// 返回Ajax请求内容
    /// </summary>
    /// <param name="content"></param>
    public void ResponseAjaxContent(string content)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "text/html";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        HttpContext.Current.Response.Write(content);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 获得网站虚拟路径名称 格式是：/虚拟目录名称 
    /// </summary>
    public string ApplicationPath
    {
        get
        {
            return HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath + "";
        }
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}