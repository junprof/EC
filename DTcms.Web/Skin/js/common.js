function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
} 

$.request = function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
}

//执行回传函数
function ExePostBack1(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        parent.openwin_msg("对不起，请选中您要操作的记录！", "");
        return false;
    }
    var msg = "确定要批量" + objmsg + "吗？";
    if (arguments.length = objmsg = 2) {
        //msg = objmsg;
    }

    layer.confirm(msg, {
        btn: ['确定', '关闭'] //可以无限个按钮
        
    }, function (index, layero) {
        __doPostBack(objId, '');
        layer.close(index);
    }, function (index) {
        layer.close(index);
    });

    return false;
}