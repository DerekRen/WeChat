//-----------------------------------------------------------------------
// <copyright company="同程网" file="BasePage.cs">
//    Copyright (c)  V1.0   
//    作者：韦震
//    功能：BasePage
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Threading;
using System.Data;

    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        #region 属性区域

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationPath = "";

        #endregion

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitApplication();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitApplication()
        {
            #region 定义ApplicationPage
            this.ApplicationPath = HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath + "";;
            #endregion
        }

        #region 弹出对话框
        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void Alert(string msg)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            ExecStartupScript(msg);
        }

        /// <summary>
        /// /弹出消息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="fun">fun</param>
        public void AlertFun(string msg, string fun)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            string info = "alert('" + msg + "');" + fun;
            ExecStartupScript(info);
        }

        public void AlertExce(string msg)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            string info = "alert('" + msg + "');";
            ExecStartupScript(info);
        }

        /// <summary>
        ///  弹框完成保存的时候弹出消息
        /// </summary>
        /// <param name="Msg"></param>
        public void AlertColorboxParent(string Msg)
        {
            AlertFun(Msg, "parent.$.colorbox.close();");
        }

        /// <summary>
        ///  弹框完成保存的时候弹出消息-刷新父页面查询
        /// </summary>
        /// <param name="Msg"></param>
        public void AlertColorboxClose(string Msg)
        {
            AlertFun(Msg, "parent.QueryDataList();parent.$.colorbox.close();");
        }
        /// <summary>
        ///  弹框完成保存的时候弹出消息-执行方法
        /// </summary>
        /// <param name="Msg"></param>
        public void AlertColorboxCloseFun(string Msg, string Fun)
        {
            AlertFun(Msg, Fun + "$.colorbox.close();");
        }
        /// <summary>
        /// 页面加载完成后执行客户端脚本
        /// </summary>
        /// <param name="script">要执行的函数Refresh();...</param>
        public static void ExecStartupScript(string script)
        {
            ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page, typeof(Page), Guid.NewGuid().ToString(), script, true);
        }
        #endregion
    }
