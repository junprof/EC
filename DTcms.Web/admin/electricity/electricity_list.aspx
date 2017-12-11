<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="electricity_list.aspx.cs" Inherits="DTcms.Web.admin.electricity.electricity_list" %>

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
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a id="btn_add" runat="server" class="add" href="javascript:openwin('新增设备','electricity_add.aspx?action=<%=DTcms.Common.DTEnums.ActionEnum.Add %>',850,700)"><i></i><span>新增</span></a></li>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnLock" runat="server" CssClass="save"
                                    OnClientClick="return ExePostBack1('btnLock','批量锁定');" OnClick="btnLock_Click"><i></i><span>批量锁定</span></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="btnOpen" runat="server" CssClass="save"
                                    OnClientClick="return ExePostBack1('btnOpen','批量启用');" OnClick="btnOpen_Click"><i></i><span>批量启用</span></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="btnDel" runat="server" CssClass="del"
                                    OnClientClick="return ExePostBack1('btnDel','批量删除');" OnClick="btnDel_Click"><i></i><span>批量删除</span></asp:LinkButton></li>

                        </ul>
                        <div class="rule-single-select" style="float: left;">
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="启用状态"></asp:ListItem>
                                <asp:ListItem Value="1" Text="锁定"></asp:ListItem>
                                <asp:ListItem Value="2" Text="启用"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="rule-single-select" style="float: left;">
                            <asp:DropDownList ID="ddlOnLine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOnLine_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="在线状态"></asp:ListItem>
                                <asp:ListItem Value="1" Text="在线"></asp:ListItem>
                                <asp:ListItem Value="0" Text="离线"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
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
            
                    <table width="1800" border="0" cellspacing="0" cellpadding="0" class="ltable" style="width:1800px;">
                        <tr>
                            <th width="60">选择</th>
                            <th align="left">名称</th>
                            <th align="left" width="100">设备ID</th>
                            <th align="left" width="100">用户</th>
                            <th align="left" width="120">地区</th>
                            <th align="left" width="60">状态</th>
                            <th align="left" width="60">在线状态</th>
                            <th align="left" width="60">A相电流</th>
                            <th align="left" width="60">B相电流</th>
                            <th align="left" width="60">C相电流</th>
                            <th align="left" width="60">漏电流</th>
                            <th align="left" width="60">1路温度</th>
                            <th align="left" width="60">2路温度</th>
                            <th align="left" width="60">3路温度</th>
                            <th align="left" width="60">4路温度</th>
                            <th align="left" width="60">1路电压</th>
                            <th align="left" width="60">2路电压</th>
                            <th align="left" width="60">3路电压</th>
                            <th align="left" width="60">4路电压</th>
                            <th align="left" width="60">5路电压</th>
                            <th align="left" width="60">6路电压</th>
                            <th align="left" width="60">7路电压</th>
                            <th align="left" width="60">8路电压</th>
                            <th align="left" width="120">更新时间</th>
                            <th width="90">操作</th>
                        </tr>
                <%
    if (ds!=null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    {
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string val = ds.Tables[0].Rows[i]["value"].ToString();
            string AI = string.IsNullOrEmpty(val)?"":val.Substring(14, 4);//A相电流
            string BI = string.IsNullOrEmpty(val)?"":val.Substring(18, 4);//B相电流
            string CI = string.IsNullOrEmpty(val)?"":val.Substring(22, 4);//C相电流
            string LI = string.IsNullOrEmpty(val)?"":val.Substring(98, 4);//漏电流
            string OneTemperature = string.IsNullOrEmpty(val)?"":val.Substring(102, 4);//1路温度
            string TwoTemperature = string.IsNullOrEmpty(val)?"":val.Substring(106, 4);//2路温度
            string ThreeTemperature = string.IsNullOrEmpty(val)?"":val.Substring(126, 4);//3路温度
            string FourTemperature = string.IsNullOrEmpty(val)?"":val.Substring(130, 4);//4路温度


                     %>
                    <tr>
                        <td align="center">
                            <%--<asp:CheckBox ID="chkId" CssClass="checkall"  runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />--%>
                            <label class="checkall"><input type="checkbox" name="chkId" value="<%=ds.Tables[0].Rows[i]["id"] %>" /></label>
                        </td>
                        <td><%=ds.Tables[0].Rows[i]["name"] %></td>
                        <td><%=ds.Tables[0].Rows[i]["onenetnum"] %></td>
                        <td><%=ds.Tables[0].Rows[i]["real_name"] %></td>
                        <td><%=ds.Tables[0].Rows[i]["areaname"] %></td>
                        <td><%=ds.Tables[0].Rows[i]["state"]==null?"":ds.Tables[0].Rows[i]["state"].ToString()=="1"?"锁定":ds.Tables[0].Rows[i]["state"].ToString()=="2"?"启用":"" %></td>
                        <td><%=ds.Tables[0].Rows[i]["online"]==null?"":!Convert.ToBoolean(ds.Tables[0].Rows[i]["online"])?"离线":"在线" %></td>
                        <td><%=getVal(AI,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerAI"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningAI"])) %>A</td>
                        <td><%=getVal(BI,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerBI"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningBI"])) %>A</td>
                        <td><%=getVal(CI,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerCI"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningCI"])) %>A</td>
                        <td><%=getVal(LI,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerLI"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningLI"])) %>mA</td>
                        <td><%=getVal(OneTemperature,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerOneTemperature"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningOneTemperature"])) %>℃</td>
                        <td><%=getVal(TwoTemperature,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerTwoTemperature"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningTwoTemperature"])) %>℃</td>
                        <td><%=getVal(ThreeTemperature,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerThreeTemperature"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningThreeTemperature"])) %>℃</td>
                        <td><%=getVal(FourTemperature,Convert.ToDouble(ds.Tables[0].Rows[i]["trailerFourTemperature"]),Convert.ToDouble(ds.Tables[0].Rows[i]["warningFourTemperature"])) %>℃</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><%=ds.Tables[0].Rows[i]["updatetime"]!=DBNull.Value?Convert.ToDateTime(ds.Tables[0].Rows[i]["updatetime"]).ToString("yyyy-MM-dd HH:mm:ss"):"" %></td>
                        <td align="center">
                            <%
                                if (ChkAdminLevel_s("electricity_list", DTEnums.ActionEnum.Edit.ToString()))
                                {
                                 %>
                            <a href="javascript:openwin('编辑设备','electricity_add.aspx?action=<%=DTEnums.ActionEnum.Edit %>&id=<%=ds.Tables[0].Rows[i]["id"]%>',850,700)">修改</a> | 
                            <%
                                }
                                 %>
                            <a href="javascript:openwin('查看历史','history_list.aspx?id=<%=ds.Tables[0].Rows[i]["id"]%>',1000,700)">查看历史</a>
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
