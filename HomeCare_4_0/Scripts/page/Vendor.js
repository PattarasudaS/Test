var Vendor =function(){
    event = function () {
        $(document).ready(function () {
            $("#searchvendor").on("click", function () {
                if ($("[name='vendortypepop']:checked").val() != "P" && $("#vendorno").val() == "" && $("#vendorname").val() == "" && $("#vendorname2").val() == ""
                    && $("#vendoridcard").val() == "" && $("#vendortaxid").val() == "" && $("#vendorempcode").val() == ""
                    ) {
                    swal("", "กรุณาใส่เงื่อนไขในการต้นหา", "warning");
                } else {
                   // AutoCompleteSaveForm($("#frmserven"));
                    callajax("http://localhost:6544/api/PettyCash/LoadVendor", "POST", {
                        company: $("#drpcompcode").val(), vendorno: $("#vendorno").val(),
                        name: $("#vendorname").val(), empcode: $("#vendorempcode").val(),
                        vendortype: $("[name='vendortypepop']:checked").val(), name2: $("#vendorname2").val(),
                        idcard: $("#vendoridcard").val(), taxid: $("#vendortaxid").val()
                    }, function (data) { rendervendor(data); }, false);
                }
            });
            //$("#VendorModal").on('show.bs.modal', function (event) {
            //    var button = $(event.relatedTarget);
            //    $(this).find("#row").val(button.data("row"));
            //    $(this).find("#resultvendor").empty();
            //    $("#searchvendor").focus();
            //    if (event.keyCode == 13) {
            //        $("#searchvendor").click();
            //    }
            //});
            $("#VendorModal input").on("keyup", function (event) {
                var keyCode = (window.event) ? event.which : event.keyCode;
                console.log("keycode", keyCode);
                if (keyCode == 13) {
                    $("#searchvendor").click();
                }
            });
        });
    }
    var AutoCompleteSaveForm = function (form) {
        var iframe = document.createElement('iframe');
        iframe.name = 'uniqu_asdfaf';
        //iframe.style.cssText = 'position:absolute; width:1px; height:1px; top:-100px; left:-100px';
        document.body.appendChild(iframe);
        var oldTarget = form.target;
        var oldAction = form.action;
        $(form).prop('target', 'uniqu_asdfaf');
        $(form).prop('action', '');
        form.submit();
        setTimeout(function () {
            form.target = oldTarget;
            form.action = oldAction;
            document.body.removeChild(iframe);
        }, 200);
    }
    var rendervendor = function (data) {
        var output = "<table class='table table-bordered'><thead><tr>";
        output += "<th></th><th>Vendor</th><th>Name</th><th>Name2</th><th>Employee Code</th><th>Personal ID</th>";
        output += "<th>TAX ID</th></tr></thead>";
        $.each(data, function (i, item) {
            output += "<tr><td style='text-align:center;'><button type='button' id='vendorselect' class='btn  btn-primary btn-xs' value='" + item.LIFNR + " " + item.NAME1 + "' onclick='vendorselect(this);' ><span class='glyphicon glyphicon-ok' aria-hidden='true'  ></span></button></td><td>" + item.LIFNR + "</td><td>" + item.NAME1 + "</td><td>" + item.NAME2 + "</td><td>" + item.EMPLOYEE + "</td><td>" + item.STCD1 + "</td><td>" + item.STCD2;
            output += "</tr>";
        });
        if (data.length == 0) {
            output += "<tr><td style='text-align:center;' colspan='7'>ไม่พบข้อมูล</td></tr>";
        }
        output += "</table>";
        $("#resultvendor").html(output);
    }
    return {
        Event: function () { event(); }
    }

}();