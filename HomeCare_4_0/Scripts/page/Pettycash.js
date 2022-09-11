var callajax = function (url, medthod, param, callback, async) {
    $.ajax({
        url: url,
        type: medthod,
        "headers": {
            "content-type": "application/json",
        },
        data: JSON.stringify(param),
        success: function (data) {
            return callback(data);
        },
        error: function (xhr, status, error) { console.log("Error :" + error); $("body").removeClass("loading"); },
        async: async
    });
};

var hostapi = $.cookie("Hostapi");
var PageEvent = function () {
    var loadCompany=function (){
        callajax(hostapi+"/api/PettyCash/LoadCompanyCode", "POST", "", function (data) { rendercompany(data);},true);
    }
    var rendercompany = function(data){
        var option ="<option></option>";
        $.each(data,function(i,item){
            option +="<option value='"+item.Company+"'>"+item.ShortName+"</option>";
        });
        $("#drpCompcode").html(option);
    }
    var LoadCostcenter  = function () {
        callajax(hostapi+"/api/PettyCash/LoadCostCenter", "POST", {
            CompanyCode: $("#drpCompcode").val()
        }, function (data) { renderdrpCCTR (data)}, false);
    }
    var renderdrpCCTR = function (data) {
        var option = "<option></option>";
        $.each(data, function (i, item) {
            var t = RegExp('^[0-9]+$');
            if (t.test(item.KOSTL)) {
                var num = parseInt(item.KOSTL);
                if (num <= 999) {
                    item.KOSTL = "0" + num;
                } else {
                    item.KOSTL = num;
                }
                num = undefined;
            }
            option += "<option value='" + item.KOSTL + ":" + item.GSBER + "'>" + item.KOSTL + " : " + item.KTEXT + "</option>";
        });
        $("#drpCCTR").html(option);
    }
    var searchvendor = function (param) {
        callajax(hostapi + "/api/PettyCash/LoadVendor", "POST", {

        }, function (data) { }, false);
    };
    var setevent = function () {
        $("#drpCompcode").on("change", function () {
            LoadCostcenter();
        });
    }
    return {
        SetEvent: function () { setevent(); },
        LoadCompany: function () { loadCompany() }
    }
}();