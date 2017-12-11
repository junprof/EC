<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map_show.aspx.cs" Inherits="DTcms.Web.admin.electricity.map_show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title></title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <link href="style.css" rel="Stylesheet" type="text/css" />
    <link href="../../Skin/layui/css/layui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Skin/layui/layui.js"></script>
    <style type="text/css">
        #allmap
        {
            width: 100%;
            height: 100%;
            overflow: hidden;
            margin: 0;
            font-family: "微软雅黑";
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=ABWdfEux5sPpo34d4AxjRY0y"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

    <!--/工具栏-->
    <!--列表-->
    <div class="win_ifram">
        <div class="wini_head">
            <span>新增</span><a href="javascript:hidewin();" class="winih_close" title="关闭"></a>
        </div>
        <div class="wini_main">
            <iframe src="" name="wini_iframe" frameborder="0" scrolling="no" style="height: 100%;
                width: 100%; overflow: auto;" id="wini_iframe"></iframe>
        </div>
    </div>
    <div class="table-container">
        <div class="map_left">
            <div id="floatHead" class="toolbar-wrap1">
                <div class="toolbar">
                    <div class="l-list" style=" padding-top:5px;">

                        <div class="single-input">

                        </div>

                    </div>
                    <div class="l-list" style=" padding-top:5px;">
                        <ul class="icon-list">
                            <li><a class="add" href="javascript:showwin('add','','','');"><i></i><span>新增</span></a></li>
                            <%--<li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnDel" runat="server" CssClass="del" OnClientClick="return ExePostBack1('btnDel','删除');"
                                    OnClick="btnDelete_Click"><i></i><span>批量删除</span></asp:LinkButton></li>--%>
                            <li><asp:TextBox ID="tbKeys" placeholder="搜索关键词" runat="server" CssClass="input"></asp:TextBox></li>
                            <li><asp:Button ID="btnSearch" CssClass="btn" OnClick="btnSearch_Click" runat="server" Text=" 查 询 " /></li>
                        </ul>
                        <div class="menu-list">
                        </div>
                    </div>
                </div>
                <table width="352" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th style="width: 40px">
                            序号
                        </th>
                        <%--<th style="width: 30px">
                            &nbsp;
                        </th>--%>
                        <th align="center" style="width: 85px">
                            名称
                        </th>
                        <th align="left" style="width: 40px">
                            状态值
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                </table>
            </div>
            <div class="jcd_list" style="margin-top: 108px;">
                <table width="352" border="0" cellspacing="0" cellpadding="0" class="ltable ltable1">
                    <asp:Repeater ID="rptList1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center" style="width: 40px">
                                    <label><%#Eval("id")%></label>
                                </td>
                                <%--<td align="center" style="width: 30px">
                                    <label class="checkall">
                                        <asp:CheckBox ID="chkId" runat="server" Style="vertical-align: middle;" /></label>
                                    <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                                </td> --%>
                                <td align="left" style="width: 85px">
                                    <div style="overflow: hidden; width: 85px;">
                                        <label title="<%#Eval("name")%>">
                                            <%#Eval("name")%></label></div>
                                </td>
                                <td align="left" style="width: 40px">
                                    <div style="overflow: hidden; width: 40px;">
                                        <label title="">
                                            <%# Convert.ToBoolean(Eval("online").ToString())?"在线":"离线"%>
                                        </label></div>
                                </td>
                                <td align="center">
                                    <a class="img-btn edit" href="javascript:showwin('edit',<%#Eval("id")%>,'','');" title="修改"></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="pagelist">
  <div id="PageContent" runat="server" class="default"></div>
</div>
            </div>

        </div>
        <div class="map_right">
            <div id="allmap">
            </div>
        </div>
    </div>
    <!--/列表-->
    <!--内容底部-->
    <!--/内容底部-->
    </form>
    <script>
        function showwin(action, eid, elng, elat) {
            if (action == "add") {
                parent.openwin('新增', "electricity/electricity_add.aspx?action=Add&elng=" + elng.toString().replace(".", "D") + "&elat=" + elat.toString().replace(".", "D"), '700', '800');
            }
            else {
                parent.openwin('编辑', "electricity/electricity_add.aspx?action=Edit&id=" + eid + "&elng=" + elng.toString().replace(".", "D") + "&elat=" + elat.toString().replace(".", "D"), '700', '800');
            }
        }
        function hidewin() {
            $(".win_ifram").hide();
        }
        var openids=","+"<%=openids%>"+",";
        function getmappoint(pid) {
            remove_overlay();
            $.ajax({
                type: "POST",
                url: "/tools/admin_ajax.ashx?action=get_map",
                async: false,
                data: "areacode=<%=areacode %>&keywords=<%=keywords %>&page=<%=page %>&pageSize=<%=pageSize %>",
                success: function (msg) {
                    var obj = eval('(' + msg.toString() + ')');
                    var divstr = "";
                    for (var i = 0; i < obj.length; i++) {
                        var replaces = obj[i].gps.toString();
                        replaces = replaces.replace(/\[/g, "");
                        replaces = replaces.replace(/\]/g, "");
                        replaces = replaces.split(",");
                        var point = new BMap.Point(replaces[0], replaces[1]);
                        if (obj[i].id.toString() == pid) {
                            if (openids.indexOf("," + obj[i].sbid.toString() + ",") < 0) {
                                if(obj[i].online==true){
                                    addMarker(point, "HSEL", obj[i].title, obj[i].id);
                                }
                                 else {
                                    addMarker(point, "TSEL", obj[i].title, obj[i].id);
                                }
                            }
                            map.centerAndZoom(point, 16);
                            iszx = true;
                        } else if (obj[i].online==true) {
                            addMarker(point, "H", obj[i].title, obj[i].id);
                        }else if (obj[i].online==false){
                            addMarker(point, "", obj[i].title, obj[i].id);
                        }

                    }
                },
                error: function (data) {
                    //alert("出错了哦！")
                }
            });
        }
    </script>
    <script type="text/javascript">
        // 百度地图API功能
        var map = new BMap.Map("allmap");    // 创建Map实例
        map.centerAndZoom(new BMap.Point(<%=point1%>, <%=point2%>), 16);  // 初始化地图,设置中心点坐标和地图级别
        map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
        map.setCurrentCity("温州");          // 设置地图显示的城市 此项是必须设置的
        map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
        // 添加带有定位的导航控件
        var navigationControl = new BMap.NavigationControl({
            // 靠左上角位置
            anchor: BMAP_ANCHOR_TOP_LEFT,
            // LARGE类型
            type: BMAP_NAVIGATION_CONTROL_LARGE,
            // 启用显示定位
            enableGeolocation: true
        });
        map.addControl(navigationControl);
        map.addEventListener("click", function (e) {
            if (e.overlay) {
            }
            else {
                clearpoint();
                getmappoint();
                // 创建带图片标注
                var myIcon = new BMap.Icon("/images/baidu/marker_red.png", new BMap.Size(25, 25));
                var marker = new BMap.Marker(e.point, { icon: myIcon });
                // 创建标注可拖动
                marker.enableDragging(true);
                marker.addEventListener("click", function () {
                    var p = marker.getLabel().content;
                    getmappoint(p);
                    showwin('add', "", "", "");
                });
                var label = new BMap.Label("新增", { offset: new BMap.Size(20, -10) });
                label.setStyle({
                    color: "#333",
                    fontSize: "14px",
                    border: "1",
                    height: "22px",
                    lineHeight: "22px",
                    fontWeight: "bold",
                    display: "none"

                });
                marker.setLabel(label);
                map.addOverlay(marker);
                showwin('add', "", e.point.lng, e.point.lat);
                // 打开新增窗口
            }

        });
        function clearpoint() {
            map.clearOverlays();
        }
        // 编写自定义函数,创建标注
        function addMarker(point, flag, str, pid) {
            var opts = {
                width: 250,     // 信息窗口宽度
                title: "", // 信息窗口标题
                enableMessage: true//设置允许信息窗发送短息
            };
                var infoWindow = new BMap.InfoWindow(str, opts);  // 创建信息窗口对象
                //map.openInfoWindow(infoWindow, point); //开启信息窗口
                var myIcon = myIcon = new BMap.Icon("/images/baidu/marker_red.png", new BMap.Size(24, 26));
                if (flag == "T"){
                    myIcon = new BMap.Icon("/images/baidu/marker_yellow.png", new BMap.Size(24, 26));
                } else if (flag == "H") {
                    myIcon = new BMap.Icon("/images/baidu/marker_gray.png", new BMap.Size(24, 26));
                } else if (flag == "HSEL") {
                    myIcon = new BMap.Icon("/images/baidu/marker_gray_sel.png", new BMap.Size(24, 35));
                }
                else if (flag == "TSEL") {
                    myIcon = new BMap.Icon("/images/baidu/marker_red_sel.png", new BMap.Size(24, 35));
                }
                var marker = new BMap.Marker(point, { icon: myIcon });
                var label = new BMap.Label(pid, { offset: new BMap.Size(20, -10) });
                label.setStyle({
                    color: "#333",
                    fontSize: "14px",
                    border: "1",
                    height: "22px",
                    lineHeight: "22px",
                    fontWeight: "bold",
                    display: "none"

                });
                marker.setLabel(label);
                marker.addEventListener("click", function () {
                    var p = marker.getLabel().content;
                    getmappoint(p);
                    showwin('edit', pid, "", "");
                    $(".ltable1 tr").removeClass("sel");
                });
                // 添加移动事件
                marker.addEventListener("dragend", function (e) {
                    showwin('edit', pid, e.point.lng, e.point.lat);
                });
                marker.enableDragging(true);
                map.addOverlay(marker);



        }

        //清除覆盖物
        function remove_overlay() {
            map.clearOverlays();
        }
        $(document).ready(function () {
            getmappoint("");
        });
    </script>
    <script type="text/javascript">
        var ismove = false;
        var csX = 0;
        var csY = 0;
        var divcsX = 0;
        var divcsY = 0;
        $(".ltable1 tr").click(function () {
            var curid = Trim($(this).find("label").eq(1).text());
            getmappoint(curid);
            $(".ltable1 tr").removeClass("sel");
            $(this).addClass("sel");
        });
        function Trim(str) {
            return str.replace(/(^\s*)|(\s*$)/g, "");
        }
        $(".wini_head").mousedown(function (e) {
            ismove = true;
            csX = e.pageX;
            csY = e.pageY;
            divcsX = $(".win_ifram").position().left;
            divcsY = $(".win_ifram").position().top;
            //alert($(".win_ifram").position().left+","+$(".win_ifram").position().top);
        });
        $(document).mouseup(function (e) {
            ismove = false;
            //alert($(".win_ifram").position().left+","+$(".win_ifram").position().top);
        });
        $(document).mousemove(function (e) {
            if (ismove) {
                var wyX = divcsX + e.pageX - csX;
                var wyY = divcsY + e.pageY - csY;
                $(".win_ifram").offset({ top: wyY, left: wyX });
            }
        });
    </script>
</body>
</html>
