<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msg_list.aspx.cs" Inherits="DTcms.Web.admin.electricity.msg_list" %>

<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>信息列表</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script src="/Skin/layer/layer.js" type="text/javascript"></script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>信息列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"></a>
      <div class="l-list">
        <ul class="icon-list">
          <%--<li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>--%>
        </ul>
      </div>
      <div class="r-list">
        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click">查询</asp:LinkButton>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
  <asp:Repeater ID="rptList" runat="server">
  <HeaderTemplate>
  <table width="1200px" border="0" cellspacing="0" cellpadding="0" class="ltable">
    <tr>
      <th align="center" width="100">设备名称</th>
      <th align="center" width="100">设备ID</th>
      <th align="center" width="70">短信提示</th>
      <th align="center" width="150">添加时间</th>
      <th align="left">标题</th>
      <th align="left">内容</th>
      <th align="center" width="70">是否处理</th>
      <th align="left">处理结果</th>
      <th align="center" width="200">操作</th>

    </tr>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
      <td align="center"><%# Eval("equipmentname") %></td>
      <td align="center"><%# Eval("onenetnum") %></td>
      <td align="center"><%# Eval("state")==null?"否":Convert.ToBoolean(Eval("state"))?"是":"否" %></td>
      <td align="center"><%#string.Format("{0:g}",Eval("addtime"))%></td>
      <td><%# Eval("title") %></td>
      <td><%# Eval("CONTENT") %></td>
      <td align="center"><%# Eval("ISPROCESSED") is DBNull?"否":Convert.ToInt16(Eval("ISPROCESSED"))==1?"是":"否" %></td>
      <td><%# Eval("REMARK") %></td>
      <td align="center">
          <%--<a href="javascript:openwin('查看历史','history_list.aspx?id=<%# Eval("item_id")%>&hid=<%#Eval("hid").ToString() %>',1000,700)">查看详情</a>--%>
      </td>
    </tr>
  </ItemTemplate>
  <FooterTemplate>
    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
  </table>
  </FooterTemplate>
  </asp:Repeater>
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
