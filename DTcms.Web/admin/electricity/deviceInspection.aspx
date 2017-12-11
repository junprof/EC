<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deviceInspection.aspx.cs" Inherits="DTcms.Web.admin.electricity.deviceInspection" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>内容管理</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script src="/Skin/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            //图片延迟加载
            $(".pic img").lazyload({ effect: "fadeIn" });
            //点击图片链接
            $(".pic img").click(function () {
                var linkUrl = $(this).parent().parent().find(".foot a").attr("href");
                if (linkUrl != "") {
                    location.href = linkUrl; //跳转到修改页面
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>内容列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHeads" class="toolbar-wrap" style="padding: 10px 0; width: 100%; *position: relative; *z-index: 1;">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container">
            <!--文字列表-->

                    <table width="1200" border="0" cellspacing="0" cellpadding="0" class="ltable" >
                        <tr>
                            <th align="center" width="120">设备名称</th>
                            <th align="center" width="120">设备编号</th>
                            <th align="left" width="260">位置地点</th>
                            <th align="center" width="150">记录时间</th>
                            <th>操作</th>
                        </tr>
                <%
                    if (datalist.Count>0)
                    {
                        for (int i = 0; i < datalist.Count; i++)
                        {
                            var item = datalist[i];
                     %>
                    <tr>
                        <td><%=item.device_name %></td>
                        <td><%=item.device_sn %></td>
                        <td><%=item.addr %></td>
                        <td><%=item.addtime %></td>
                        <td align="center">
          <a href="javascript:openwin('查看详情','deviceloginfo.aspx?type=<%=type %>&id=<%=item.id%>',1000,600)">查看详情</a>
      </td>

                    </tr>
                <%
        }
    }
    else
    {
                     %>
                        <tr><td align="center" colspan="13">暂无记录</td></tr>
                        <%
                            }
                             %>

  </table>


            <!--/文字列表-->
        </div>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"
                    OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
