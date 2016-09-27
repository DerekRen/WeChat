<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="SendAction" %>

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
    <script type="text/javascript">
        $(function () {//默认隐藏信息
            $("tr:[title='imgType']").hide();
            $("tr:[title='voiceType']").hide();
            $("tr:[title='musicType']").hide();
            $("tr:[title='videoType']").hide();
            $("table:[title='newsType']").hide();
            $("tr:[title='showOrhideType']").hide();
        });
        function getMsgType(value) {
            $("#hidMsgType").val(value);
            if (value == "text") {//文本消息
                $("table:[title='newsType']").hide();
                $("tr:[title='txtType']").show();
                $("tr:[title='imgType']").hide();
                $("tr:[title='voiceType']").hide();
                $("tr:[title='musicType']").hide();
                $("tr:[title='videoType']").hide();
                $("tr:[title='showOrhideType']").hide();
            }
            else if (value == "image") {//图片消息
                $("table:[title='newsType']").hide();
                $("tr:[title='txtType']").hide();
                $("tr:[title='voiceType']").hide();
                $("tr:[title='musicType']").hide();
                $("tr:[title='videoType']").hide();
                $("tr:[title='showOrhideType']").hide();
                $("tr:[title='imgType']").show();
            }
            else if (value == "voice") {//语音消息
                $("table:[title='newsType']").hide();
                $("tr:[title='voiceType']").show();
                $("tr:[title='txtType']").hide();
                $("tr:[title='imgType']").hide();
                $("tr:[title='musicType']").hide();
                $("tr:[title='videoType']").hide();
                $("tr:[title='showOrhideType']").hide();
            }
            else if (value == "video") {//视频消息
                $("table:[title='newsType']").hide();
                $("tr:[title='txtType']").hide();
                $("tr:[title='imgType']").hide();
                $("tr:[title='voiceType']").hide();
                $("tr:[title='musicType']").hide();
                $("tr:[title='videoType']").show();
                $("tr:[title='showOrhideType']").show();
            }
            else if (value == "music") {//音乐消息
                $("table:[title='newsType']").hide();
                $("tr:[title='txtType']").hide();
                $("tr:[title='imgType']").hide();
                $("tr:[title='voiceType']").hide();
                $("tr:[title='videoType']").hide();
                $("tr:[title='musicType']").show();
                $("tr:[title='showOrhideType']").show();
            }
            else {//异步加载(图文消息。图片是网络图片)
                $("tr:[title='txtType']").hide();
                $("tr:[title='imgType']").hide();
                $("tr:[title='voiceType']").hide();
                $("tr:[title='musicType']").hide();
                $("tr:[title='videoType']").hide();
                $("tr:[title='showOrhideType']").hide();
                $("#newsType").show();
                //var num = $("#hidNewsNum").val();
                //$.ajax({
                //    type: "GET",
                //    url: "AjaxCall.ashx",
                //    data: "action=GetnewsType&num=" + num + "&date=" + new Date(),
                //    success: function (data) {
                //        $(data).appendTo($("#newsType"));
                //    }
                //})
            }
        }
        //增加图文项
        function AddNews(num) {
            if (num == 11) {
                alert("最多添加10个图文描述");
                return;
            }
            $.ajax({
                type: "GET",
                url: "AjaxCall.ashx",
                data: "action=GetnewsType&num=" + num + "&date=" + new Date(),
                success: function (data) {
                    $(data).appendTo($("#newsType"));
                    $("#hidNewsNum").val(num);
                    num=num-1;
                    $("#newsAdd" + num).attr("disabled", true);
                    if(num>1)
                    {
                        $("#newsDel" + num).attr("disabled", true);
                    }
                }
            });
        }
        //删除图片项(只能从最后增加的依次删除)
        function DelNews(num) {
            $("tr:[title='newsType" + num + "']").hide();
            //$("#newsTitle" + num).val(""); //清空标题，后台添加以标题判断
            var num = $("#hidNewsNum").val();
            num--;
            $("#newsAdd" + num).attr("disabled", false);
            $("#newsDel" + num).attr("disabled", false);
            $("#hidNewsNum").val(num);
        }
        //添加验证
        function CheckISNull() {
            var num = $("#hidNewsNum").val();
            num++;
            var newsTitle = "";//标题
            var newsUrl = "";//点击跳转链接
            var newsPicUrl = "";//图片的链接
            var newsDes = "";//描述
            var newsType="";
            for (var i = 1; i < num; i++)
            {
                newsPicUrl += $("#newsPicUrl" + i).val() + ",";
                newsType += $("#newsTitle" + i).val() + "," + $("#newsUrl" + i).val() + "," + $("#newsPicUrl" + i).val() + "," + $("#newsDes" + i).val() + "|";
            }
            $("#hidnewsType").val(newsType);
            return true;;
        }
        //检查上传图片格式
        function CheckFileType(obj,isImg) {
            var reg = /^.*\.(jpg|gif|png|bmp)$/i;
            if (isImg == 0) {
                reg = /^.*\.(mp3|amr)$/i;
            }
            else if (isImg == 2) {
                reg = /^.*\.(mp4)$/i;
            }
            if (!obj.value.match(reg)) {
                alert("文件格式有误，重新上传");
                $(obj).val("");
                return false;
            }
            return true;
        }
        //检测url
        function checkIsUrl(obj) {
            var str = /^http[s]?:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
            if (!obj.value.match(str)) {
                alert("url格式不正确！");
                $(obj).val("");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <div class="s_content" id="sUi_list">
            <div class="list_result">
                <table class="form_much_list" width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <th class="leftBox">
                        <font color="red">*</font> 消息分类：
                        </th>
                        <td class="rightBox">
                            <input type="hidden" id="hidMsgType" value="text" runat="server" />
                            <asp:DropDownList ID="msgType" runat="server" CssClass="form_select" onchange="getMsgType(this.value)">
                                <asp:ListItem Value="text">text</asp:ListItem>
                                <asp:ListItem Value="image">image</asp:ListItem>
                                <asp:ListItem Value="voice">voice</asp:ListItem>
                                <asp:ListItem Value="video">video</asp:ListItem>
                                <asp:ListItem Value="music">music</asp:ListItem>
                                <asp:ListItem Value="news">news</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr title="txtType">
                        <th class="leftBox"><font color="red">*</font>文本内容：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" class="form_text"
                                MaxLength="200" Width="600" Style="max-width: 600px; min-height: 100px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="imgType" style="display: none">
                        <th class="leftBox"><font color="red">*</font>上传图片：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:FileUpload ID="Fileimg" runat="server"  onchange="CheckFileType(this,1)"/>
                            <font color="red">仅支持jpg格式</font>
                        </td>
                    </tr>
                    <tr title="voiceType" style="display: none">
                        <th class="leftBox"><font color="red">*</font>上传语音：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:FileUpload ID="Filevoice" runat="server" onchange="CheckFileType(this,0)"/>
                            <font color="red">仅支持mp3、amr格式</font>
                        </td>
                    </tr>
                    <tr title="showOrhideType" style="display: none">
                        <th class="leftBox">标题：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="title" runat="server" class="form_text" MaxLength="100" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="musicType" style="display: none">
                        <th class="leftBox"><font color="red">*</font>音乐链接：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="musicUrl" runat="server" class="form_text" onblur="checkIsUrl(this)" MaxLength="100" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="musicType" style="display: none">
                        <th class="leftBox" style="width:120px;"><font color="red">*</font>音乐高清链接：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="hqMusicUrl" runat="server" class="form_text" onblur="checkIsUrl(this)" MaxLength="100" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="musicType" style="display: none">
                        <th class="leftBox"><font color="red">*</font>上传音乐：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:FileUpload ID="FileMusic" runat="server" />
                            <font color="red">仅支持mp3、amr格式</font>
                        </td>
                    </tr>
                    <tr title="videoType" style="display: none">
                        <th class="leftBox"><font color="red">*</font>上传视频：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:FileUpload ID="FileVideo" runat="server" onchange="CheckFileType(this,2)"/>
                            <font color="red">仅支持mp4格式且不支持下载</font>
                        </td>
                    </tr>
                    <tr title="showOrhideType" style="display: none">
                        <th class="leftBox">描述：
                        </th>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="description" TextMode="MultiLine" runat="server" class="form_text"
                                MaxLength="200" Width="600" Style="max-width: 600px; min-height: 100px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table id="newsType" title="newsType" class="form_much_list" width="100%" cellspacing="0"
                    cellpadding="0" border="0">
                    <tr title="newsType1">
                        <td class="leftBox">标题：
                        </td>
                        <td class="rightBox">
                            <input type="text" id="newsTitle1" name="newsTitle1" class="form_text"
                                runat="server" style="width: 500px;" />
                            <input type="button" id="newsAdd1" class="form_bto_only" style="margin-top: 5px;"
                                value="+" onclick="AddNews(2)" />
                        </td>
                    </tr>
                    <tr title="newsType1">
                        <td class="leftBox">点击链接：
                        </td>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="newsUrl1" runat="server" class="form_text"  MaxLength="100" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="newsType1">
                        <td class="leftBox">图片链接：
                        </td>
                        <td class="rightBox" colspan="3">
                            <asp:TextBox ID="newsPicUrl1" runat="server" class="form_text" MaxLength="100" Width="600"></asp:TextBox>
                        </td>
                    </tr>
                    <tr title="newsType1">
                        <td class="leftBox">图片描述：
                        </td>
                        <td class="rightBox" colspan="3">
                            <textarea id="newsDes1" name="newsDes1" class="form_text" style="min-width: 600px; max-width: 600px; min-height: 100px; max-height: 200px;"
                                runat="server"></textarea>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%; text-align: center">
                    <asp:Button runat="server" ID="btn_Save" Text=" 保  存 " class="form_bto_only" OnClientClick="return  CheckISNull();"
                        OnClick="btn_Save_Click" />
                </div>
            </div>
        </div>
        <input type="hidden" id="hidNewsNum" value="1" runat="server" />
        <input type="hidden" id="hidnewsType"  runat="server" />
    </form>
</body>
</html>
