<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test_page.aspx.cs" Inherits="DTcms.Web.test_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
 .preview, .img, img
 {
 width:100px;
 height:100px;
 }
 .preview
 {
border:0px none #000;
}
    </style>
<script type="text/javascript" charset="utf-8" src="/scripts/jquery/jquery-1.11.2.min.js"></script>
    <script>
        function preview(file) {
            document.getElementById('uimg').innerHTML = "";
            for (var i = 0; i < file.files.length; i++) {
                if (file.files && file.files[i]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var div = document.createElement('span');
                        div.innerHTML = "<img src='" + e.target.result + "'>";
                        div.className = "preview";
                        document.getElementById('uimg').appendChild(div);
                    }
                    reader.readAsDataURL(file.files[i]);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" action="/api/Picture/Upload" method="post" enctype="multipart/form-data">
    <input type="file" name="file" id="file" accept="image/*" multiple="multiple" onchange="preview(this)"/><input type="submit" value="保存" />
    <div id="uimg"></div>
    </form>
</body>
</html>
