
//获取文档元素
function getID(id) {
    return document.getElementById(id);
}
//检查文本框是否非空
//id为文本框
//显示错误框
//显示错误的内容
function notTxtEmpty(id, len) {
    var isspas = true;
    var IdValue = $("#" + id).val().replace(/^\s+|\s+$/g, ''); //IE不支持Trim方法，使用正则替换掉空格
    var IdLen = $("#" + id).length;
    if (len == null || len == "") {
        len = IdLen;
    }
    if (IdValue == null || IdValue == "" || IdLen > len) {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
        isspas = true;

    }
    return isspas;
}
function GreaterThanLength(id, len) {
    var isspas = true;
    var IdValue = $("#" + id).val().length;
    if (IdValue == null || IdValue > len) {
        $("#" + id).css('borderColor', 'red');
        isspas = false;
    }
    else {
        $("#" + id).css('borderColor', '');
        isspas = true;
    }
    return isspas;
}

function ShowErrorPrompt(id, msgType) {
    if (msgType == 1) {
        $("#" + id).css('borderColor', 'red');
    } else {
        $("#" + id).css('borderColor', '');
    }
}
//检查是否选择下拉列表
//id为下拉列表；
//error_id显示错误框；
//msg显示错误的内容
function notdllEmpty(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    if (IdValue == "" || IdValue == "0") {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
    }
    return isspas;
}

function notdllEmptyWithnot0(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    if (IdValue == "") {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
    }
    return isspas;
}

//检查文本框是否为固定电话
//id为文本框
//显示错误框
//显示错误的内容
function checkPhone(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    var reg = /(^\d{3,4}-)?\d{7,8}$/;
    if (!reg.test(IdValue)) {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
        isspas = true;
    }
    return isspas;
}
//检查文本框是否为手机号码
//id为文本框
//显示错误框
//显示错误的内容
function checkMobile(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    var reg = /^[1][3,5,8][0-9]{9}$/;
    if (!reg.test(IdValue)) {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
        isspas = true;
    }
    return isspas;
}
//检查文本框是否为邮箱
//id为文本框
//显示错误框
//显示错误的内容
function checkEmail(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    //    var reg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[com|net|org|edu|mil|tv|biz|info|cn]{2,4}$/; //验证格式修改于2010年9月26
    if (!reg.test(IdValue)) {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
        isspas = true;
    }
    return isspas;
}

//检查文本框是否为传真
//id为文本框
//显示错误框
//显示错误的内容
function checkFax(id) {
    var isspas = true;
    var IdValue = $("#" + id).val();
    var reg = /^(\d{3,4}-)?\d{7,8}$/;
    if (!reg.test(IdValue)) {
        ShowErrorPrompt(id, 1);
        isspas = false;
    }
    else {
        ShowErrorPrompt(id, 2);
        isspas = true;
    }
    return isspas;
}

//获取Radio的值:
//id为隐藏控件ID；
function GetRadioValueID(id) {
    $("input:radio").each(function () {
        if ($(this).attr("checked")) {
            var idRa = $(this).attr("id");
            var sps = idRa.split("_");
            $("#" + id).val(sps[1]);
        }
    })
}
//判断显示条数是否输入非负整数
function checkIntValue(id) {
    var isspas = true;
    var regex = /^(0|[1-9]\d*)$/;
    var TopSizeValue = $("#" + id).val();
    if (TopSizeValue != "") {
        if (!regex.test(TopSizeValue)) {
            ShowErrorPrompt(id, 1);
            isspas = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return isspas;
}

//判断显示条数是否输入整数
function checkIntValueNum(id) {
    var isspas = true;
    var regex = /^[-]{0,1}[0-9]{1,}$/;
    var TopSizeValue = $("#" + id).val();
    if (TopSizeValue != "") {
        if (!regex.test(TopSizeValue)) {
            ShowErrorPrompt(id, 1);
            isspas = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return isspas;
}

//判断显示条数是否输入浮点数
function checkDemicalValue(id) {
    var isspas = true;
    var regex = /^(\d*\.)?\d+$/;
    var TopSizeValue = $("#" + id).val();
    if (TopSizeValue != "") {
        if (!regex.test(TopSizeValue)) {
            ShowErrorPrompt(id, 1);
            isspas = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return isspas;
}

//判断显示条数是否输入浮点数包含负数
function checkDemicalValueNum(id) {
    var isspas = true;
    var regex = /^(-|\d)?\d*(\.?)\d+$/;
    var TopSizeValue = $("#" + id).val();
    if (TopSizeValue != "") {
        if (!regex.test(TopSizeValue)) {
            ShowErrorPrompt(id, 1);
            isspas = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return isspas;
}

//判断是否包含特殊字符（只允许汉字、英文字母、数字及下划线！）
function checkContainSpecialChar(id) {
    var isspas = true;
    var regex = /^[\u0391-\uFFE5\w]+$/;
    var TopSizeValue = $("#" + id).val();
    if (TopSizeValue != "") {
        if (!regex.test(TopSizeValue)) {
            ShowErrorPrompt(id, 1);
            isspas = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return isspas;
}

//post-Ajax请求
function QueryDataList(currentPage, divIDName) {
    $.getLoading.show();
    if (currentPage == null || currentPage == undefined || currentPage == "") {
        if ($("#currentPage") != null) {
            currentPage = $("#currentPage").val();
        } else {
            currentPage = "1";
        }
    }
    $("#currentPage").val(currentPage);
    if (divIDName == null || divIDName == undefined || divIDName == "") {
        divIDName = "textContent";
    }
    PostFormData("form1", divIDName);
}

//post-Ajax请求
function PostFormData(formID, contentDivID) {
    //父辈的from标签的action属性作为连接地址，并且发送其表单数据。method="post" id="formID"
    $("#" + formID).ajaxSubmit({
        success: function (responseData) {
            $("#" + contentDivID).html(responseData);
            listFn.list_hover($("#blockList tr"));
            $.getLoading.hide();
        },
        error: function () {
            $("#" + contentDivID).html("没有提交成功，请重新尝试。");
            $.getLoading.hide();
        }
    });
}

function ConvertEnterToTabSearchName() {
    if (event.keyCode == 13) {
        QueryDataList(1);
        return false;
    }
    return true;
}

/*********************合并制定列和行的单元格********************/
function QueryDataListRowSpan(currentPage, divIDName) {

    $.getLoading.show();
    if (currentPage == null || currentPage == undefined || currentPage == "") {
        if ($("#currentPage") != null) {
            currentPage = $("#currentPage").val();
        } else {
            currentPage = "1";
        }
    }
    $("#currentPage").val(currentPage);
    if (divIDName == null || divIDName == undefined || divIDName == "") {
        divIDName = "textContent";
    }
    PostFormDataRowSpan("form1", divIDName);
}

//post-Ajax请求
function PostFormDataRowSpan(formID, contentDivID) {
    //父辈的from标签的action属性作为连接地址，并且发送其表单数据。method="post" id="formID"
    $("#" + formID).ajaxSubmit({
        success: function (responseData) {
            $("#" + contentDivID).html(responseData);
            listFn.list_hover($("#blockList tr"));
            getMarkfn();
            var cellindexml = parseInt($("#cellindexml").val()),
             beginRowml = parseInt($("#beginRowml").val())
            SpanGrid($('#' + contentDivID).find('table')[0], cellindexml, beginRowml);

            var cellindex = parseInt($("#cellindex").val()),
            beginRow = parseInt($("#beginRow").val());

            var lastCellIndex = $('#hidlastcellindex').length == 0 ? 0 : parseInt($('#hidlastcellindex').val());
            if (lastCellIndex <= 0) {
                SpanGrid($('#' + contentDivID).find('table')[0], cellindex, beginRow);
            }
            else {
                for (var i = cellindex; i <= lastCellIndex; i++) {
                    SpanGrid($('#' + contentDivID).find('table')[0], i, beginRow);
                }
            }
            $.getLoading.hide();
        },
        error: function () {
            $("#" + contentDivID).html("没有提交成功，请重新尝试。");
            $.getLoading.hide();
        }
    });
}

function SpanGrid(tabObj, cellindex, beginRow) {
    var colIndex = cellindex;
    var rowBeginIndex = beginRow;
    if (tabObj != null) {
        var i, j, m;
        var intSpan;
        var strTemp;
        m = 0;
        for (i = rowBeginIndex; i < tabObj.rows.length; i = i + 1) {
            intSpan = 1;
            m++;
            strTemp = $(tabObj.rows[i].cells[colIndex]).attr("id");
            for (j = i + 1; j < tabObj.rows.length; j = j + 1) {
                if (strTemp == $(tabObj.rows[j].cells[colIndex]).attr("id")) {
                    intSpan = intSpan + 1;
                    tabObj.rows[i].cells[colIndex].rowSpan = intSpan;
                    tabObj.rows[j].cells[colIndex].style.display = "none";
                }
                else {
                    break;
                }
            }
        }
        i = j - 1;
    }
}

//判断输入框中的值是否有特殊字符
function checkSpecialCharacter(id) {
    var ispass = true;
    var pattern = new RegExp("[`~!@#$%^&*=|{}':;',\\[\\]<>《》〈〉/?～！@#￥……&*|{}‘；：”“'。，、？]");
    var controlValue = $("#" + id).val();
    if (controlValue != "") {
        if (pattern.test(controlValue)) {
            ShowErrorPrompt(id, 1);
            ispass = false;
        }
        else {
            ShowErrorPrompt(id, 2);
        }
    }
    return ispass;
}

/**************************************************************/
//判断日期大小
function dateCompare(startdate, enddate) {
    startdate = startdate.replace(/-/g, "/");
    enddate = enddate.replace(/-/g, "/");
    if (new Date(startdate) > new Date(enddate)) {
        return true;
    }
    return false;
}

/**
* 时间格式化
* formatDate("1999-01-30", "yyyy-MM-dd HH:mm:ss")
* formatDate("1999-1-29 1:59:00", "yyyy-MM-dd HH:mm:ss")
*/
function formatDate(date, format) {
    if (!date) return;
    if (!format) format = "yyyy-MM-dd";
    switch (typeof date) {
        case "string":
            date = new Date(date.replace(/-/, "/"));
            break;
        case "number":
            date = new Date(date);
            break;
    }
    if (!date instanceof Date) return;
    var dict = {
        "yyyy": date.getFullYear(),
        "M": date.getMonth() + 1,
        "d": date.getDate(),
        "H": date.getHours(),
        "m": date.getMinutes(),
        "s": date.getSeconds(),
        "MM": ("" + (date.getMonth() + 101)).substr(1),
        "dd": ("" + (date.getDate() + 100)).substr(1),
        "HH": ("" + (date.getHours() + 100)).substr(1),
        "mm": ("" + (date.getMinutes() + 100)).substr(1),
        "ss": ("" + (date.getSeconds() + 100)).substr(1)
    };
    return format.replace(/(yyyy|MM?|dd?|HH?|ss?|mm?)/g, function () {
        return dict[arguments[0]];
    });
}

//获取项目根目录
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    if (postPath.indexOf('/') != 0) {
        postPath = '/' + postPath;
    }
    else if (postPath.toLowerCase() != "/jingqu" && postPath.toLowerCase() != "/scenery") {
        postPath = "";
    }
    return (prePath + postPath);
}
var ApplicationPath = getRootPath(); //全局路径变量
function OpenNativeWindow(width, height, url) {
    //获得窗口的垂直位置
    var iTop = (window.screen.availHeight - 30 - height) / 2;
    //获得窗口的水平位置
    var iLeft = (window.screen.availWidth - 10 - width) / 2;
    window.open(url, "_blank", "height=" + height + ",width=" + width + ",toolbar =no,scrollbars=no,left=" + iLeft + ",top=" + iTop + ",menubar=no,scrollbars=yes, resizable=yes, location=no, status=no");
}

//封装c#中的string.Empty方法
String.format = function () {
    var i = 1, args = arguments;
    var str = args[0];
    var re = /\{(\d+)\}/g;
    return str.replace(re, function () { return args[i++] });
}
   