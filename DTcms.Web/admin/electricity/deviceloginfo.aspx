<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deviceloginfo.aspx.cs" Inherits="DTcms.Web.admin.electricity.deviceloginfo" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title></title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>

    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script src="/Skin/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Skin/layui/layui.js"></script>
    <link href="/Skin/layui/css/layui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>
    <style>
        .tab-content dl dd.seldd dd{
            margin-left:0;
        }
        .tab-content dl dd.seldd .layui-form-select {
            max-width:300px;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server" class="layui-form" >

        <!--内容-->
        <div id="floatHead" class="content-tab-wrap">
            <div class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a class="selected" href="javascript:;">记录信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content" >
            <dl>
                <dt>设备编号</dt>
                <dd>
                        <asp:Label ID="lblDeviceSN" runat="server" ></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>设备名称</dt>
                <dd>
                    <asp:Label ID="lblDeviceName" runat="server" ></asp:Label>
            </dl>
            <dl>
                <dt>安装位置</dt>
                <dd>
                    <asp:Label ID="lblPosition" runat="server" ></asp:Label>
            </dl>
            <dl>
                <dt>安装地点</dt>
                <dd>
                    <asp:Label ID="lblAddr" runat="server" ></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>说明</dt>
                <dd>
                    <asp:Label ID="lblRemark" runat="server" ></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>照片</dt>
                <dd>
                    <asp:Repeater ID="rpt" runat="server">
                        <ItemTemplate>
                            <img src="/api/Picture/Get?picid=<%# Eval("picid") %>" alt="右键在新标签打开查看大图" width="200px" height="150px"/>
                        </ItemTemplate>
                    </asp:Repeater>
                </dd>
            </dl>
        </div>
        <!--/内容-->

    </form>
    <script>
        layui.use(['form', 'layedit', 'laydate'], function () {
            var form = layui.form()
                , layer = layui.layer
                , layedit = layui.layedit
                , laydate = layui.laydate;

            //监听指定开关
            form.on('select', function (data) {
                //setTimeout('__doPostBack(\'' + data.elem.id+'\',\'\')', 0);
            });


        });
    </script>
</body>
</html>
