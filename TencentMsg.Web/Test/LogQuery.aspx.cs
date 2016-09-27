using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SceneryJobConfiguration_LogQuery : BasePage
{
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">e</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTree();
        }

        if (this.hifAdress.Value != "")
        {
            ExecStartupScript("document.getElementById('iframequery').src='" + this.hifAdress.Value.Substring(1) + "?date=" + DateTime.Now + "';");
        }
    }

    /// <summary>
    /// 绑定树
    /// </summary>
    private void BindTree()
    {
        string path = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogFile"]) + @"\UploadFile\Log"; //+ @"\Log\Exception"   //Server.MapPath(". ") + @"\UploadFile\Log";
        TreeNode tr = new TreeNode("Web日志", "A1");
        string url = HttpContext.Current.Request.Url.ToString();
        string no_http = url.Substring(url.IndexOf("//") + 2);
        string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1);
        Hidhttphost.Value = url.Substring(0, url.IndexOf("Test"));
        DirectoryInfo di = new DirectoryInfo(path);
        DirectoryInfo[] dis = di.GetDirectories();
        foreach (DirectoryInfo diss in dis)
        {
            TreeNode td = new TreeNode();
            td.Text = diss.Name;
            td.Value = "B1" + diss.Name;
            td.CollapseAll();
            DirectoryInfo[] disOK = diss.GetDirectories();
            foreach (DirectoryInfo disk in disOK)
            {
                FileInfo[] fs = disk.GetFiles();
                if (fs.Length > 0)
                {
                    if (fs.Length == 1)
                    {
                        if (fs[0].Length > 0)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Text = diss.Name + @"/" + disk.Name;
                            tn.Value = "A" + Hidhttphost.Value + @"UploadFile/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[0].Name;
                            td.ChildNodes.AddAt(0, tn);
                            if (DateTime.Parse(disk.Name) >= DateTime.Now.Date)
                            {
                                tn.ExpandAll();
                                tn.Parent.Expand();
                            }
                        }
                    }
                    else
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = diss.Name + @"/" + disk.Name;
                        tn.Value = "B1" + Hidhttphost.Value + @"UploadFile/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[0].Name;
                        tn.Collapse();
                        for (int i = 0; i < fs.Length; i++)
                        {
                            if (fs[i].Length > 0)
                            {
                                TreeNode tn1 = new TreeNode();
                                tn1.Text = fs[i].Name.Replace(@"Exception@", "");
                                tn1.Value = "A" + Hidhttphost.Value + @"UploadFile/Log/" + disk.Parent.ToString() + @"/" + disk.Name + @"/" + fs[i].Name;
                                tn.ChildNodes.AddAt(0, tn1);
                            }
                        }
                        td.ChildNodes.AddAt(0, tn);
                        if (DateTime.Parse(disk.Name) >= DateTime.Now.Date)
                        {
                            tn.ExpandAll();
                            tn.Parent.Expand();
                        }
                    }
                }
            }
            tr.ChildNodes.AddAt(0, td);
        }
        this.TreeView1.Nodes.AddAt(0, tr);
    }

    /// <summary>
    /// 树节点选择改变事件
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">e</param>
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.TreeView1.SelectedNode.Value != "A1" && this.TreeView1.SelectedNode.Value != "B1" && this.TreeView1.SelectedNode.Value.Substring(0, 2) != "B1")
        {
            string address = this.TreeView1.SelectedNode.Value.Substring(1);
            this.hifAdress.Value = this.TreeView1.SelectedNode.Value;
            ExecStartupScript("document.getElementById('iframequery').src='" + address + "?date=" + DateTime.Now + "';");

        }
        else
        {
            this.hifAdress.Value = "";
        }
    }

    /// <summary>
    /// 下载文件按钮的点击
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="e">e</param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string adress = this.hifAdress.Value;
        if (adress == "")
        {
            Alert("没有选择需要下载的文件！");

            return;
        }
        else
        {
            string fileName = adress.Substring(adress.LastIndexOf("/") + 1);
            adress = adress.Substring(0, adress.LastIndexOf("/"));
            string adress1 = adress.Substring(0, adress.LastIndexOf("/"));
            adress = adress.Substring(adress.LastIndexOf("/") + 1);
            adress1 = adress1.Substring(adress1.LastIndexOf("/") + 1);
            //string path = Server.MapPath(". ") + @"\UploadFile\Log" + @"\" + adress1 + @"\" + adress + @"\" + fileName;
            string path = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogFile"]) + @"\UploadFile\Log" + @"\" + adress1 + @"\" + adress + @"\" + fileName;

            if (this.hifAdress.Value.IndexOf("B") >= 0)
            {
                path = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogFile"]) + @"\UploadFile\Log" + @"\" + adress1 + @"\" + adress + @"\" + fileName;
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            ////通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }


}