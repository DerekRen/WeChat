function GetLoad(obj){       //loading
    this.obj = obj;
    this.init();
}
GetLoad.prototype = {
    init:function(){
        var div = document.createElement("div");
        div.className = "loadingBox";
        this.obj.deploy && this.obj.deploy == true ? div.style.marginTop = "0" : div.style.marginTop = "-17px";
        this.obj.text ? div.innerHTML = this.obj.text :  div.innerHTML = "正在加载，请稍候...";
        div.style.height = "70px";
        div.style.lineHeight = "70px";
        div.style.backgroundColor = "#fff";
        if(this.obj.deploy){
            div.style.position = "absolute";
            div.style.backgroundColor = "#f5f5f5";
            div.style.height = "60px";
            div.style.lineHeight = "60px";
            if(this.obj.left){
                $(div).css("left",this.obj.left + "px");
            }
            if(this.obj.top){
                $(div).css("top",this.obj.top + "px");
            }
            if(this.obj.right){
                $(div).css("right",this.obj.right + "px");
            }
            if(this.obj.bottom){
                $(div).css("bottom",this.obj.bottom + "px");
            }
        }
        div.style.display = "none";
        $("body").append(div);
       this.div = div;
    },
    hide:function(){
        if(this.obj.deploy && this.obj.deploy == true){
            this.div.style.display = "none";
            return;
        }else{
            //$.colorbox.close();
            this.div.style.display = "none";
        }
    },
    show:function(){
        if(this.obj.deploy && this.obj.deploy == true){
            this.div.style.display = "block";
            return;
        }{
            this.div.style.display = "block";
//            $.colorbox(
//                {
//                    html:this.div,
//                    initialWidth:200,
//                    initialHeight:40,
//                    overlayClose:true,
//                    transition:'none',
//                    opacity:"0.6",
//                    speed:500,
//                    close:""
//                }
//            )
        }

    }
};

$.getLoading = new GetLoad({
        deploy:false
    });

//默认弹窗
function PopContent(obj){
    this.obj = obj;
    this.init();
}
PopContent.prototype = {
     _craeteHtml:function(){
         var div = document.createElement("div");
         div.style.marginTop = "-17px";
         var ico,mal;

         switch (this.obj.showType){
             case "normal" :
                 ico = "";
                 mal = "margin-left:0";
                 break;
             case "succeed" :
                 ico =  '<span class="tipPic suc"></span>';
                 break;
             case "fail" :
                 ico =  '<span class="tipPic fail"></span>';
                 break;
             default :
                 ico = "";
                 mal = "";
         }
         var txt = this.obj.text ? this.obj.text : "";
         $(div).html('<div class="popContent">'+
             '<div class="popMain">'+ ico +
             '<div class="txtBox" style="'+ mal +'">'+
             '<p>'+ txt +'</p>'+
             '<a href="javascript:;" class="popBtn">确定</a>'+
             '</div>'+
             '<div class="clear_float"></div>'+
             '</div>'+
             '</div>');
         this._div = div;
         $(".popBtn",div).bind("click",function(){
             $.colorbox.close();
         });
//         $("body").append(div);
     },
    open:function(){
//        console.log(window.parent)
        $.colorbox(
            {
                html:this._div,
                title:"提示",
                initialWidth:300,
                initialHeight:170,
                overlayClose:true,
                transition:'none',
                opacity:"0.6",
                speed:500,
                close:"关闭"
            }
        )
    },
    init:function(){
        this._craeteHtml();
    }
};
$.extend({
    getPop:function(obj){
        return new PopContent(obj);
    }
})


var listFn = {
    //搜索结果列表hover
    list_hover:function(e){
//        console.log(e)
        e.hover(function(){
//            console.log(e)
            $(this).addClass("list_search_hover");
        },function(){
            $(this).removeClass("list_search_hover");
        })
    },
    //搜索结果列表click
    list_click:function(e){
        e.on("click",function(){
            var uThis = $(this);
            if(uThis.hasClass("list_search_click")){
                uThis.removeClass("list_search_click");
            }
            else{
                uThis.addClass("list_search_click");
            }
        })
    },
    //搜索条件的更多
    list_more:function(e,allListName){
        e.on("click",function(){
//            console.log(this);
            var uThis = $(this),
                uLi = $(".screen_box",allListName);
            //下拉
            if(uThis.hasClass("triangleD")){
                uThis.removeClass("triangleD");
                uLi.addClass("expanded");
            }
            //收起
            else{
                uThis.addClass("triangleD");
                uLi.removeClass("expanded");
            }
        })
    },
    //结果列表全选
    list_chooseAll:function(e,table){
        var uInput = $("input",e),
            allInput = $("input",table);
        uInput.on("click",function(){
            if(this.checked == true){
                allInput.attr("checked","checked");
            }
            else{
                allInput.removeAttr("checked","checked");
            }
        })
    },
    //搜索列表分页
    list_page:function(){
        $(".list_search_page").paging(1337, { // 分页显示的列表总数
            format: '[< ccccc! >]', // 中间显示几块内容就放几个c
            perpage: 10, // 每页显示几块内容(paging后第一个参数/perpage=最大页数)
            lapping: 0, // don't overlap pages for the moment
            page: 1, // 从第几页开始显示 start at page, can also be "null" or negative
            onSelect: function (page) {
                //选中页数的回调
                // add code which gets executed when user selects a page, how about $.ajax() or $(...).slice()?
//                console.log(this);
//                console.log(this.slice[0]);//当前页-1
            },
            onFormat: function (type) {
                //打印日期的格式,和format的格式有关系
//                console.log(type)
                switch (type) {
                    case 'block': // n and c
                        return '<a href="#" class="pagenum">' + this.value + '</a>';
                    case 'next': // >
                        return '<a href="#" class="guidnum ">下一页</a>';
                    case 'prev': // <
                        return '<a href="#" class="guidnum ">上一页</a>';
                    case 'first': // [
                        return '<a href="#" class="guidnum ">首页</a>';
                    case 'last': // ]
                        return '<a href="#" class="guidnum ">尾页</a>';
                }
            }
        });
    },
    init:function(allListName){
        var uT = $(".list_search_list",allListName),
            uR = $("tr",uT),
            uTh = $("th",uT),
            more = $(".more_condi",allListName);
        this.list_hover(uR);
        this.list_click(uR);
        this.list_chooseAll(uTh,uT);
        this.list_more(more,allListName);
    }
};



/* 新列表收缩 */
function rowFluidControl(){
    if($(".row-fluidd").length > 0){
        $(".row-fluidd").each(function(){
        })
    }
}

$(document).ready(function(){

    //列表
    listFn.init($(".list_search_condi"));
});

