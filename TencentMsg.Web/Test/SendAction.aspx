<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendAction.aspx.cs" Inherits="SendAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../CommonFile/JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/jsl.format.js" type="text/javascript"></script>
    <script src="../CommonFile/JS/jquery.json-2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function jsonF(jsonVal) {
            //alert(jsonVal);
            var jsVal = jsl.format.formatJson(jsonVal);
            $('#json_input').val(jsVal);
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <textarea id="json_input" rows="30" cols="100" spellcheck="false" runat="server"></textarea>
    <div id="div_show" runat="server"></div>
        <br />
        <asp:Button ID="Button1" runat="server" Text="测试发送1" OnClick="Button1_Click" /><br />
    <asp:Button ID="Button2" runat="server" Text="移除缓存" onclick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" Text="测试get发送" 
        onclick="Button3_Click" />
    </form>
</body>
</html>
