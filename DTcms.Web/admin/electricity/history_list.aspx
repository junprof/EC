<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="history_list.aspx.cs" Inherits="DTcms.Web.admin.electricity.history_list" %>

<%@ Import Namespace="DTcms.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>历史记录</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script src="/Skin/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">

        function openwin(title, url, winwidth, winheight) {
            if (winheight > $(window).outerHeight()) {
                winheight = $(window).outerHeight();
            }
            if (winwidth > $(window).outerWidth()) {
                winwidth = $(window).outerWidth();
            }
            var mindex = layer.open({
                type: 2,
                title: title,
                fix: false,
                moveOut: false,
                shade: false,
                shadeClose: true,
                maxmin: true,
                area: [winwidth + 'px', winheight + 'px'],
                content: url
            });

        }

    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

        <!--工具栏-->
        <div id="floatHeads" class="toolbar-wrap" style="padding: 10px 0; width: 100%; *position: relative; *z-index: 1;">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        </ul>
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
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container" style="padding-top: 10px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                    <th width="80" align="center">选择</th>
                    <th align="left" width="60">在线状态</th>
                    <th align="left" width="60">A相电流</th>
                    <th align="left" width="60">B相电流</th>
                    <th align="left" width="60">C相电流</th>
                    <th align="left" width="60">漏电流</th>
                    <th align="left" width="60">1路温度</th>
                    <th align="left" width="60">2路温度</th>
                    <th align="left" width="60">3路温度</th>
                    <th align="left" width="60">4路温度</th>
                    <th align="left" width="120">更新时间</th>
                    <th align="left" width="120">查询时间</th>
                </tr>
                <%
                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string val = ds.Tables[0].Rows[i]["value"].ToString();
                            string AI = val.Substring(14, 4);//A相电流
                            string BI = val.Substring(18, 4);//B相电流
                            string CI = val.Substring(22, 4);//C相电流
                            string LI = val.Substring(98, 4);//漏电流
                            string OneTemperature = val.Substring(102, 4);//1路温度
                            string TwoTemperature = val.Substring(106, 4);//2路温度
                            string ThreeTemperature = val.Substring(126, 4);//3路温度
                            string FourTemperature = val.Substring(130, 4);//4路温度

                            DTcms.Model.trailerModel model = JsonHelper.JSONToObject<DTcms.Model.trailerModel>(ds.Tables[0].Rows[i]["trailerval"].ToString());

                            DTcms.Model.trailerModel warningmodel = JsonHelper.JSONToObject<DTcms.Model.trailerModel>(ds.Tables[0].Rows[i]["warningval"].ToString());

                            if (model == null)
                            {
                                model = new DTcms.Model.trailerModel() { AI = 0, BI = 0, CI = 0, LI = 0, OneTemperature = 0, TwoTemperature = 0, ThreeTemperature = 0, FourTemperature = 0 };
                            }

                            if (warningmodel == null)
                            {
                                warningmodel = new DTcms.Model.trailerModel() { AI = 0, BI = 0, CI = 0, LI = 0, OneTemperature = 0, TwoTemperature = 0, ThreeTemperature = 0, FourTemperature = 0 };
                            }

                %>
                <tr>
                    <td align="center">
                        <label class="checkall">
                            <input type="checkbox" name="chkId" value="<%=ds.Tables[0].Rows[i]["id"] %>" /></label>
                    </td>
                    <td><%=ds.Tables[0].Rows[i]["online"]==null?"":!Convert.ToBoolean(ds.Tables[0].Rows[i]["online"])?"离线":"在线"%></td>
                    <td><%=getVal(AI,model.AI,warningmodel.AI) %>A</td>
                    <td><%=getVal(BI,model.BI,warningmodel.BI) %>A</td>
                    <td><%=getVal(CI,model.CI,warningmodel.CI) %>A</td>
                    <td><%=getVal(LI,model.LI,warningmodel.LI) %>mA</td>
                    <td><%=getVal(OneTemperature,model.OneTemperature,warningmodel.OneTemperature) %>℃</td>
                    <td><%=getVal(TwoTemperature,model.TwoTemperature,warningmodel.TwoTemperature) %>℃</td>
                    <td><%=getVal(ThreeTemperature,model.ThreeTemperature,warningmodel.ThreeTemperature) %>℃</td>
                    <td><%=getVal(FourTemperature,model.FourTemperature,warningmodel.FourTemperature) %>℃</td>
                    <td><%=ds.Tables[0].Rows[i]["updatetime"]!=null?Convert.ToDateTime(ds.Tables[0].Rows[i]["updatetime"]).ToString("yyyy-MM-dd HH:mm:ss"):"" %></td>
                    <td><%=ds.Tables[0].Rows[i]["addtime"]!=null?Convert.ToDateTime(ds.Tables[0].Rows[i]["addtime"]).ToString("yyyy-MM-dd HH:mm:ss"):"" %></td>

                </tr>
                <%
                    }
                }
                else
                {
                %>
                <tr>
                    <td align="center" colspan="13">暂无记录</td>
                </tr>
                <%
                    }
                %>
            </table>

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

    </form>
</body>
</html>
