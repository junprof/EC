<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="electricity_add.aspx.cs" Inherits="DTcms.Web.admin.electricity.electricity_add" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>新增</title>
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
                        <li><a class="selected" href="javascript:;">基本信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content" >
            <dl>
                <dt>是否启用</dt>
                <dd>
                        <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
                    <span class="Validform_checktip">*锁定后系统将不再采集信息</span>
                </dd>
            </dl>
            <dl>
                <dt>名称</dt>
                <dd>
                    <asp:TextBox ID="tbName" runat="server" CssClass="input normal" datatype="*1-20" sucmsg=" " ></asp:TextBox>
                    <span class="Validform_checktip">*字母、下划线，不可修改</span></dd>
            </dl>
            <dl>
                <dt>设备ID</dt>
                <dd>
                    <asp:TextBox ID="tbequipmentId" runat="server" CssClass="input normal" datatype="*1-64"></asp:TextBox></dd>
            </dl>
            <dl>
                <dt>所属用户</dt>
                <dd class="seldd">
                    <asp:DropDownList ID="ddlUser" runat="server" datatype="*1-20">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>区域</dt>
                <dd class="seldd">
                    <asp:DropDownList ID="ddlArea" runat="server" datatype="*1-20">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </dd>
            </dl>
            <dl>
                <dt>位置</dt>
                <dd>
                    <asp:TextBox ID="tbPosition" runat="server" CssClass="input normal" datatype="*1-64"></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>A相预警电流</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerAI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>A相告警电流</dt>
                <dd>
                    <asp:TextBox ID="tbwarningAI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>B相预警电流</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerBI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>B相告警电流</dt>
                <dd>
                    <asp:TextBox ID="tbwarningBI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>C相预警电流</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerCI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>C相告警电流</dt>
                <dd>
                    <asp:TextBox ID="tbwarningCI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>A
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>漏电流预警</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerLI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>mA
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>漏电流告警</dt>
                <dd>
                    <asp:TextBox ID="tbwarningLI" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>mA
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>1路预警温度</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerOneTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>1路告警温度</dt>
                <dd>
                    <asp:TextBox ID="tbwarningOneTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>2路预警温度</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerTwoTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>2路告警温度</dt>
                <dd>
                    <asp:TextBox ID="tbwarningTwoTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>3路预警温度</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerThreeTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>3路告警温度</dt>
                <dd>
                    <asp:TextBox ID="tbwarningThreeTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>
            <dl>
                <dt>4路预警温度</dt>
                <dd>
                    <asp:TextBox ID="tbtrailerFourTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <%--<dl>
                <dt>4路告警温度</dt>
                <dd>
                    <asp:TextBox ID="tbwarningFourTemperature" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>℃
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>--%>

            <dl>
                <dt>1路预警电压</dt>
                <dd>
                    <asp:TextBox ID="tbTrailerU1" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>V
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <dl>
                <dt>2路预警电压</dt>
                <dd>
                    <asp:TextBox ID="tbTrailerU2" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>V
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>
            <dl>
                <dt>3路预警电压</dt>
                <dd>
                    <asp:TextBox ID="tbTrailerU3" runat="server" CssClass="input normal" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " ></asp:TextBox>V
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>



        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-wrap">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
            </div>
        </div>
        <!--/工具栏-->
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
