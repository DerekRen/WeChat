<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessageList.aspx.cs"
    Inherits="SendMessageList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../CommonFile/JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/commonaction.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/s_index.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/jquery.form.js" type="text/javascript"></script>
    <link href="../CommonFile/CSS/common.css" rel="stylesheet" type="text/css" />
    <link href="../CommonFile/CSS/ListPageStyle.css" rel="stylesheet" type="text/css" />
    <script src="../CommonFile/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        //回复消息
        function ReplyMsg(id) {
            var url = "<%=ApplicationPath %>/SendPage/SendMessage.aspx?id=" + id;
            OpenNativeWindow(800, 600, url);
        }
        //展开或者收起回复列表
        function SelectReplyMsg(obj, id, rid) {
            if ($(".hid" + id).is(":hidden")) {
                $(obj).val("收起回复");
                $(".hid" + id).slideDown('fast');
            }
            else {
                $(obj).val("展开回复");
                $(".hid" + id).slideUp('fast');
            }
        }
        //查看详情
        function MsgDetails(id) {
            var url = "<%=ApplicationPath %>/SendPage/MessageDetails.aspx?id=" + id;
            OpenNativeWindow(800, 600, url);
        }
        //下载内容
        function DownLoadContent(mediaId) {
            if (mediaId == null || mediaId == "") {
                alert("media_Id为空！");
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "AjaxCall.ashx",
                    data: "action=DownLoadContent&mediaId=" + mediaId + "&date=" + new Date(),
                    complete: function (data) {
                        if (data.responseText != 'error' && data.responseText != '') {
                            location.href = getRootPath() + "/" + data.responseText;
                        }
                        else {
                            alert("下载不成功！");
                        }
                    }
                });
            }
        }
        //获取网站跟目录
        function getRootPath() {
            var strFullPath = window.document.location.href;
            var strPath = window.document.location.pathname;
            var pos = strFullPath.indexOf(strPath);
            var prePath = strFullPath.substring(0, pos);
            return prePath
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" action="AjaxCall.ashx">
    <input type="hidden" id="currentPage" name="currentPage" value="1" />
    <input type="hidden" id="actionName" name="actionName" value="GetMessageList" />
    <div class="s_content" id="sUi_list">
        <div class="list_search_condi">
            <table class="table100">
                <tr>
                    <td>
                        <ul class="screen_box expanded">
                            <li>消息分类：<asp:DropDownList ID="CategoryName" runat="server" CssClass="form_select"
                                onchange="QueryDataList()">
                                <asp:ListItem Value="">全部</asp:ListItem>
                                <asp:ListItem Value="text">text</asp:ListItem>
                                <asp:ListItem Value="image">image</asp:ListItem>
                                <asp:ListItem Value="voice">voice</asp:ListItem>
                                <asp:ListItem Value="video">video</asp:ListItem>
                                <asp:ListItem Value="music">music</asp:ListItem>
                                <asp:ListItem Value="news">news</asp:ListItem>
                            </asp:DropDownList>
                            </li>
                            <li>发送时间：
                                <input id="txtBegin" name="txtBegin" type="text" runat="server" class="form_text"
                                    onclick="WdatePicker({onpicked:function(){txtEnd.click();},maxDate:'#F{$dp.$D(\'txtEnd\')}'})" />
                                &nbsp;至&nbsp;
                                <input id="txtEnd" name="txtEnd" type="text" runat="server" class="form_text" onclick="WdatePicker({minDate:'#F{$dp.$D(\'txtBegin\')}'})" />
                            </li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <ul class="list_search_btos">
                            <li>
                                <input id="btnSearch" type="button" class="form_bto_only" value="搜 索" onclick="QueryDataList(1)" />
                            </li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="list_result" id="textContent">
    </div>
    </form>
</body>
</html>
