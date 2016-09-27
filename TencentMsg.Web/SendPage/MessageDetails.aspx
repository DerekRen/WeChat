<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageDetails.aspx.cs" Inherits="MessageDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../CommonFile/JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/commonaction.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/s_index.js" type="text/javascript"></script>
    <link href="../CommonFile/CSS/common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server" action="AjaxCall.ashx">
    <div class="list_result" id="textContent" runat="server">
        <table id="tableId" class="form_much_list" width="100%" cellspacing="0" cellpadding="0"
            border="0">
            <tr>
                <th class="leftBox">
                    消息类型：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:Label ID="msgTypeTxt" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="leftBox">
                    Media_Id：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:Label ID="mediaId" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th class="leftBox">
                    标题：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:TextBox ID="title" runat="server" class="form_text" MaxLength="100" Width="600"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="leftBox">
                    主链接：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:TextBox ID="MainUrl" runat="server" class="form_text" MaxLength="100" Width="600"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="leftBox">
                    副链接：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:TextBox ID="SubUrl" runat="server" class="form_text" MaxLength="100" Width="600"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="leftBox">
                    描述：
                </th>
                <td class="rightBox" colspan="3">
                    <asp:TextBox ID="description" TextMode="MultiLine" runat="server" class="form_text"
                        MaxLength="200" Width="600" Style="max-width: 600px; min-height: 100px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
