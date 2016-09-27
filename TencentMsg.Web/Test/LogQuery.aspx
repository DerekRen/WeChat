<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogQuery.aspx.cs" Inherits="SceneryJobConfiguration_LogQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>日志查询</title>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#TreeView1").css("height", $(window).height() - 20 + "px");
             $("#iframequery").css("height", $(window).height() - 50 + "px");
         })
       </script>
</head>
<body>
    <div id="main">
        <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 20%; height: 100%; text-align: left; border: 1px solid #ccc;">
                    <asp:Panel ID="Panel1" runat="server" Style="overflow-y: scroll;">
                        <asp:TreeView ID="TreeView1" runat="server" text-align="left" Height="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                            NodeStyle-CssClass="text-align:left">
                            <NodeStyle CssClass="text-align:left" Width="150px" />
                        </asp:TreeView>
                    </asp:Panel>
                </td>
                <td style="width: 75%; height: 100%; text-align: center; border: 1px solid #ccc;">
                    <iframe src="" id="iframequery" frameborder="0" width="100%" height="90%"></iframe>
                    <asp:Button ID="Button1" runat="server" Text=" 下载文件" class="form_bto_only" OnClick="Button1_Click" />
                    <asp:HiddenField ID="hifAdress" runat="server" />
                    <asp:HiddenField ID="Hidhttphost" runat="server" />
                </td>
            </tr>
        </table>
        <div class="clear">
        </div>
        </form>
    </div>
</body>
</html>
