using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TencentMsg.Common
{
    public class PageList
    {
        /// <summary>
        /// 查询列表也固定样式以及分页
        /// </summary>
        /// <param name="th">table中头部Th(可以添加样式,直接传CSS属性例："display:none,列名没有传空")</param>
        /// <param name="td">table中数据</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="totalRecord">总数</param>
        /// <param name="pageSize">每页显示</param>
        /// <returns></returns>
        public string BindQueryListNewPage(List<string> th, string td, int currentPage, int totalRecord, int perPageCount)
        {
            return BindQueryListNewPage(th, td, currentPage, totalRecord, perPageCount, string.Empty);
        }
        /// <summary>
        /// 查询列表也固定样式以及分页
        /// </summary>
        /// <param name="th">table中头部Th(可以添加样式,直接传CSS属性例："display:none,列名没有传空")</param>
        /// <param name="td">table中数据</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="totalRecord">总数</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="perPageCount">总共多少页</param>
        /// <param name="strMethod">特殊调用js方法名称</param>
        /// <returns></returns>
        public string BindQueryListNewPage(List<string> th, string td, int currentPage, int totalRecord, int perPageCount, string strMethod)
        {
            StringBuilder sbhtml = new StringBuilder();
            //sbhtml.Append(@"<div class=""info_list"">");
            sbhtml.Append(@"<table  class=""result_box table100 list_search_list"" id=""blockList"">");
            if (th.Count > 0)
            {
                sbhtml.Append(@"<tr>");
                foreach (var t in th)
                {
                    if (!t.Contains(','))
                    {
                        sbhtml.AppendFormat(@"<th>{0}</th>", t.ToString());
                    }
                    else
                    {
                        sbhtml.AppendFormat(@"<th style=""{0}"">{1}</th>", t.Split(',')[0], t.Split(',')[1]);
                    }
                }
                sbhtml.Append(@"</tr>");
            }
            if (td != string.Empty)
            {
                sbhtml.AppendFormat(@"{0}", td);
            }
            sbhtml.Append(@"</table>");
            //sbhtml.Append("</div>");
            if (currentPage != 0 && totalRecord != 0 && perPageCount != 0)
            {
                sbhtml.AppendFormat(@"{0}", InitPageStyle(currentPage, totalRecord, perPageCount, true, strMethod));
            }
            return sbhtml.ToString();
        }
        /// <summary>
        /// 获取分页样式
        /// </summary>
        /// <param name="currentPage">当前页码</param>
        /// <param name="totalRecord">记录总条数</param>
        /// <param name="perPageCount">每页显示条数</param>
        /// <returns></returns>
        public string InitPageStyle(int currentPage, int totalRecord, int perPageCount, bool newPage, string strMethod)
        {
            return PageStyle.DisplayPagers(currentPage, totalRecord, perPageCount, "", "", "", 1, newPage, strMethod);
        }
    }
}
