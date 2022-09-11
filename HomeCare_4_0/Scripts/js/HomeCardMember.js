
var receiveData = {};
var userID;
var HomeCardrequestID;
var EnumCC;
var userRole;
var baseUrl;
var unitId;
var unitCode;
var username;
var homeCardTypeId;
var homeCardPrice;
var quotationList;
var quotationHomecardOptionalPackList;
var lastQuotationGoods;
var approverList = [];
var chargeList = [];
var ddlCostType = [];
var postponeHistory;

async function GetParameterToJS(baseUrlPa, unitIdPa, unitCodePa, RequestId, userIDPa, userRolePa, usernamePa) {
    baseUrl = baseUrlPa;
    unitId = unitIdPa;
    unitCode = unitCodePa;
    HomeCardrequestID = parseInt(RequestId);
    userID = userIDPa;
    userRole = userRolePa;
    username = usernamePa
  
}


async function getDDLddlReceivedStatusID() {
    var responce = await get(`${baseUrl}/homecare/api/v1/DropDown/HomeCardRequestStatus`);
    $.each(responce, (j, d) => {
        $('#ddlReceivedStatusID').append(`<option value="${d.id}" ${!d.id ? 'selected disabled' : ''}>${d.description}</option>`)
    });
}

async function GetHCpanel() {
    GetpostponeInvestigation();
    await getDDLddlReceivedStatusID(); 
    await ajaxGet(`${baseUrl}/homecare/api/v1/HomeCard/` + unitCode + "/Request", response => {
        receiveData = response.data.find(t => t.id === HomeCardrequestID);
    });
      
    homeCardTypeId = receiveData.homeCardTypeId;
    //get homeCardPrice
    var responsePrice = await ajaxGet(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/Price");
    var homecardfilterprice = responsePrice.data.find(t => t.homeCardTypeId === homeCardTypeId).vatIncludedPrice ;
    homeCardPrice = homecardfilterprice;

    $('#ddlReceivedStatusID').val(receiveData.requestStatusId);
    $('#ReceiveHCDate').text(thaiFormatDate(receiveData.homeCareContactDate));
    $('#ReceiveHCRemark').val(receiveData.homeCareRemark);
    $('#ReceiveHCUser').text(receiveData.homeCareFullName);
    $('#ReceiveCCDate').text(thaiFormatDate(receiveData.callCenterContactDate));
    $('#ReceiveCCRemark').val(receiveData.callCenterRemark);
    $('#ReceiveCCUser').text(receiveData.callCenterFullName);
    $('#ReceiveAppointmentDate').val(thaiFormatDate(receiveData.appointmentDate, false)); console.log("receiveData.appointmentDate " + receiveData.appointmentDate);
    $('#ReceiveAppointmentTimeForm').val(receiveData.appointmentTimeFrom);
    $('#ReceiveAppointmentTimeTo').val(receiveData.appointmentTimeTo);

    // Show icon for extended appointment history
    $('#ReceiveAppointmentGroup').append(`<span class="input-group-addon cursor-pointer" id="viewExtendedHistory" onclick="openModalExtendedHistory()" style="border-top-left-radius: 0px !important; border-top-right-radius: 4px !important; border-bottom-left-radius: 0px !important; border-bottom-right-radius: 4px !important; border-left: 0;"><i class="fa fa-book fa-lg" style="font-size: 16px; line-height: 1; color: #337ab7;"></i></span>`);
    $('#viewExtendedHistory')
      .mouseenter(function () { $(this).css('background-color', '#eee') })
      .mouseleave(function () { $(this).css('background-color', '#fff') });

    // Disable appointmentDateReal
    if (!receiveData.appointmentDate) {
        $('#chkAPDateReal').attr('disabled', 1);
    }
    // Disable select receiveStatus when mainProcessId > 2
    if (receiveData.requestStatusId == 2) {
        $('#ddlReceivedStatusID').attr('disabled', 1);

    }
    // Show icon for extend appointment
    if (receiveData.appointmentDate && (!receiveData.surveyAt || userRole == "CallCentre")) {
        $('#ReceiveAppointmentGroup').append(`
		        <span class="input-group-addon cursor-pointer" id="btnExtendedAppointment" onclick="openModalExtendedAppointment()" style="border-top-left-radius: 0px !important; border-top-right-radius: 4px !important; border-bottom-left-radius: 0px !important; border-bottom-right-radius: 4px !important;"><i class="fa fa-calendar-plus-o fa-lg"  style="font-size: 16px; line-height: 1; color: chocolate;"></i></a>
	      `);
        $('#viewExtendedHistory').css({
            'border-top-right-radius': '0px',
            'border-bottom-right-radius': '0px'
        });
        $('#btnExtendedAppointment')
          .mouseenter(function () { $(this).css('background-color', '#eee') })
          .mouseleave(function () { $(this).css('background-color', '#fff') });
    }
   
    // ซ่อนปุ่มและใส่ค่า DateReal เมื่อ DateReal มีค่า
    if (receiveData.surveyAt) {
        $('#ReceiveAppointmentDateReal').val(thaiFormatDate(receiveData.surveyAt , false));
        $('#chkAPDateReal').attr({ checked: true, disabled: true });
        //  $('#btnUploadFile').removeAttr('disabled');
        $('#btnExtendedAppointment').hide();
       
        if (userRole !== "CallCentre") {
            
            $('#btnQuotation').attr({ disabled: false });//enable quotation buttton
        }
    }

    //ยกเลิกรายการ
    var isActive = receiveData.isActive;
    if(!isActive)
    {
        // Set disable all input, select, button 
        $('#btnExtendedAppointment').hide();
        $('#btncancel').attr('disabled', 1);
        $('#Received-Receive').find('input, textarea, button, select').attr('disabled','disabled');
        $('#Received-Inform').find('input, textarea, button, select').attr('disabled', 'disabled');     
    }
}

async function saveHCPanel() {
    if (!validateReceivePanel() ) {
        return false;
    }
    
    var success = true;
    var refresh = false;
    // แถบข้อมูลรับเรื่อง

    var StatusID = $("#ddlReceivedStatusID").val();
    var appointmentDate = reorganizeDateFormat($('#ReceiveAppointmentDate').val());
    if (!receiveData.surveyAt ) {
        if (StatusID == "2"  ) {
            var body = {

                userId: userID,
                homeCardRequestId: HomeCardrequestID,
                requestStatusId: StatusID,
                homeCareRemark: $('#ReceiveHCRemark').val() ? $('#ReceiveHCRemark').val() : "",
                appointmentDate: appointmentDate,
                appointmentFrom: $('#ReceiveAppointmentTimeForm').val(),
                appointmentTo: $('#ReceiveAppointmentTimeTo').val(),
                surveyAt: reorganizeDateFormat($('#ReceiveAppointmentDateReal').val())
            };
        }
      
        if (StatusID == "3" || StatusID == "4" || StatusID == "5") {
            var body = {

                userId: userID,
                homeCardRequestId: HomeCardrequestID,
                requestStatusId: StatusID,
                homeCareRemark: $('#ReceiveHCRemark').val() ? $('#ReceiveHCRemark').val() : ""

            };
        }
        if (StatusID == "6" || StatusID == "7") {
            var body = {

                userId: userID,
                homeCardRequestId: HomeCardrequestID,
                requestStatusId: StatusID,
                callCentreRemark: $('#ReceiveCCRemark').val() ? $('#ReceiveCCRemark').val() : "",
                appointmentDate: reorganizeDateFormat($('#ReceiveAppointmentDate').val()),
                appointmentDate: $('#ReceiveAppointmentTimeForm').val(),
                appointmentTo: $('#ReceiveAppointmentTimeTo').val()

            };
        }

      
        var updated = await ajaxPut(`${baseUrl}/homecare/api/v1/HomeCard/` + unitCode + "/Request", body);
        if (!updated) {
            success = false;
            modalWaring("fail");

        } else {
            refresh = true;
            modalSuccess("success");
        }

        if (success) {
            modalSuccess();
            if (refresh) {
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload(true);
                });
            }
            page.loaded();
        }
       
    }
    if (receiveData.surveyAt) {
        page.loaded();
        window.location.reload(true);
    }

}

async function getQuotationPanel(homeCardQuotationId = null) {
  
    //check role for Create Quotation
    if (userRole == "CallCentre") {
        $('#btnQuotation').attr({ disabled: true });//disable ปุ่มสร้างQuotation
        $('#btncancel').attr({ disabled: true });
    }
    // Get dropdownlist  ลูกค้าขอเปลี่ยนแปลง / ยกเลิก  , customer cancel quotation reason
    ajaxGet(`${baseUrl}/homecare/api/v1/DropDown/CustomerCancelQuotationReason`, response => {
        bindingDdlist('DdlCustomerCancelQuotationReason', response.data.filter(d => d.system === 'YLS' && d.role === undefined || d.id === 0));
    });
    // Check Approver Name
    ajaxGet(`${baseUrl}/homecare/api/v1/Quotation/GetQuotationApprovers`, response => {
        approverList = response.data;
     
    });
    //get Quotation Panel
    await ajaxGet(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/QuotationByRequestId?homeCardRequestId=" + HomeCardrequestID,  response => {
        quotationList = response.data;
      
    });

    //Get Homecare for Opional Package :
    await ajaxGet(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/QuotationOptionalPackageByHomecardRequestId?homeCardRequestId=" + HomeCardrequestID, response => {
        quotationHomecardOptionalPackList = response.data;
        if (quotationHomecardOptionalPackList.length > 0 && quotationHomecardOptionalPackList[0].extra_package_id != 0 ) {

            $('#tbDetailTBody_OptionalPackage').empty();
            var item = 0;
            for (item of quotationHomecardOptionalPackList) {
                $('#tbDetailTBody_OptionalPackage').append(`
				    <tr>
					    <td class="text-center">
						     ${$('#tbDetailTBody_OptionalPackage tr').length + 1}  
					    </td>
					    <td>
                            <input id="hdfhomcardrequest_optpack_id" type="hidden" value="${item.id}" />
                            <input id="hdfhomcardrequest_extra_package_id" type="hidden" value="${item.extra_package_id}" />
						     ${item.extra_package_desc}  
					    </td>
					    <td class="text-center" >
                            ${item.extra_package_times}
					    </td>
					    <td>                       
						    <input id="txt_extra_package_price" class="form-control" type="text" value="${item.extra_package_price}"  />
					    </td>                 
				    </tr>
			    `);

                //-- Disable for have cancel optional package :
                if (item.is_active != "1") {
                    $("#txt_extra_package_price").attr("disabled", "disabled");
                }

                //ยกเลิกรายการ
                var isActive = receiveData.isActive;
                if (!isActive) {
                    $("#txt_extra_package_price").attr("disabled", "disabled");
                }

                //-- Check Disable for Approved or Customer Payment Status :             
                if (quotationList.length > 0) {
                    for (var item of quotationList) {
                        var getpaymentStatusId = item.paymentStatusId;
                        var getapprovedStatusId = item.approvedStatusId;
                        if (getpaymentStatusId != 1 || getapprovedStatusId != 0) {
                            $("#txt_extra_package_price").attr("disabled", "disabled");
                        }

                    }
                }

      
            }    

        } else {
            $('#divOptionalPackageBox').hide();
        }


 

    });

    if (quotationList.length > 0) {
        $("#Received-Quotation-detail").show();
        if (homeCardQuotationId) {
            lastQuotationGoods = quotationList.filter(q => q.homeCardQuotationId == homeCardQuotationId )[0];
        } else {
            lastQuotationGoods = quotationList[quotationList.length - 1];
            var tempQuotationList = [];
            for (var i = 0; i < quotationList.length; i++) {
                tempQuotationList.push({ id: quotationList[i].homeCardQuotationId, description: quotationList[i].homeCardQuotationId });
            }
          
            bindingDdlist('ddlQuotationGoodsName', tempQuotationList, tempQuotationList[tempQuotationList.length - 1].id);
            $('#ddlQuotationGoodsName option:last').text(tempQuotationList[tempQuotationList.length - 1].description + ' (ล่าสุด)');
            $('#ddlQuotationGoodsName option:last').css('color', 'green');
        }
   
    
        $('#txtQuotationCreatedBy').text(lastQuotationGoods.createdBy);
        $('#txtQuotationCreatedDate').text(thaiFormatDate(lastQuotationGoods.createdDate));      
        $('#txtQuotationPaymentStatus').text(lastQuotationGoods.paymentStatusDesc);
        $('#txtQuotationRemark').text(lastQuotationGoods.approvedRemark);

        $('#preQuotationItemDetailTBody').empty();
        $('#preQuotationItemDetailTBody').append(`
						<tr>
							<td class="text-center">
								${$('#preQuotationItemDetailTBody tr').length + 1} 
							</td>
						
							<td>
								บัตร HomeCard แพ็คเกจ ${lastQuotationGoods.homeCardName}
							</td>
						
							<td class="text-center">
								${1}  
							</td>
							<td>
								<input id="txtprice" class="form-control" type="text" value="${ lastQuotationGoods.homeCardPrice}" > </input>
							</td>
							
						</tr>
					`)

        // แสดงปุ่มอนุมัติตามสิทธิ
        if (approverList.filter(item => item.username == username.toLowerCase()).length > 0) {
            $('#DivApplyApprove').hide();
            $('#DivApprove').show();

        }
        else {
            $('#DivApplyApprove').show();
            $('#DivApprove').hide();
        }
     

        
        // Case of approvedStatus
        if (lastQuotationGoods.approvedStatusId == 0) {
            $('#DivQuotationApprove').find('button').removeAttr('disabled');
        }
        if (lastQuotationGoods.approvedStatusId == 1) {
            $('#btnQuotation').attr({ disabled: true });//disable ปุ่มสร้างQuotation
            $('#btnRequestApproval').attr({ disabled: true });//disable ปุ่ม
          
            $("#txtprice").attr({ disabled: true }); //disable price
  
            var lastIndex = approverList.findIndex(a => a.username == lastQuotationGoods.approverName);
            if (lastIndex < 0) {
                $('#txtQuotationApproveStatus').text(lastQuotationGoods.approvedStatusDesc + ' (จากคุณ ' + approverList[0].username + ')');
            } else {
                $('#txtQuotationApproveStatus').text(lastQuotationGoods.approvedStatusDesc + ' (จากคุณ ' + approverList[lastIndex + 1].username + ')');
            }
           
            
        } else if (lastQuotationGoods.approvedStatusId == 2) {
            $("#txtprice").attr({ disabled: true });
            if (userRole !== "CallCentre") {
                $('#btnQuotation').attr({ disabled: false }); //enable ปุ่มสร้างQuotation
            }
            $('#DivGenQuotationPDF').show();//แสดงออกรายงาน
            $('#txtQuotationApproveStatus').text(lastQuotationGoods.approvedStatusDesc + ' (โดยคุณ ' + lastQuotationGoods.approverName + ')');
        } else if (lastQuotationGoods.approvedStatusId == 3) {
            $('#txtQuotationApproveStatus').text(lastQuotationGoods.approvedStatusDesc + ' (โดยคุณ ' + lastQuotationGoods.approverName + ')');
            $('#txtQuotationApproveStatus').css('color', 'red');
            if (userRole !== "CallCentre") {
                $('#btnQuotation').attr({ disabled: false });//enable ปุ่มสร้างQuotation
            }
           // $('#DivQuotationApprove').find('button').attr('disabled', 1); //disable ปุ่ม approve
        } else {
            $('#txtQuotationApproveStatus').text('');
        }
       

        if (lastQuotationGoods.approvedStatusId == 1 || lastQuotationGoods.approvedStatusId == 2) {
            var approverName = lastQuotationGoods.approverName == null ? "" : lastQuotationGoods.approverName.toLowerCase();

            var lastIndex = approverList.findIndex(a => a.username == approverName );
            var currentIndex = approverList.findIndex(a => a.username == username.toLowerCase());

            if (currentIndex <= lastIndex) {
                $('#DivQuotationApprove').find('button').attr('disabled', 1);
            } else {
                $('#DivQuotationApprove').find('button').removeAttr('disabled');
            }
            if (lastQuotationGoods.approvedStatusId == 2) {
                $('#DivQuotationApprove').find('button').attr('disabled', 1);
              
            } 

        }
        
        // Change text color when quotation is paid
        if (lastQuotationGoods.paymentStatusId === 2) {
            $('#txtQuotationPaymentStatus').css('color', 'forestgreen');
            $("#btnReject").attr({ disabled: true });
            $("#btnQuotation").attr({ disabled: true });  //disable ปุ่มใบเสนอราคา 
            $("#flagChangeRequest").attr({ disabled: true });  //disable ปุ่มยกเลิก  
            $('#btncancel').attr({ disabled: true });//disable ปุ่ม
            $("#txtprice").attr({ disabled: true }); //disable price
            $("#btnSaveQuotation").attr({ disabled: true });  //disable button for saveQuotation()  
        } else {
            $('#txtQuotationPaymentStatus').css('color', 'red');
        }
    
        // flagChangeRequest cancel by customer
        if (lastQuotationGoods.customerReasonId !== 0 ) {
            $('#flagChangeRequest').prop('checked', 1);
            $('#flagChangeRequest').attr('disabled', 1); //disable ปุ่ม ยกเลิก
            $('#DdlCustomerCancelQuotationReason').val(lastQuotationGoods.customerReasonId);
            $("#txtprice").attr({ disabled: true }); //disable price
            $('#DivQuotationApprove').find('button').attr('disabled', 1);
            $("#btnSaveQuotation").attr({ disabled: true });  //disable button for saveQuotation()  
         
        } else {

            $('#flagChangeRequest').prop('checked', 0)
            $('#DdlCustomerCancelQuotationReason').val(0);
            $('#DdlCustomerCancelQuotationReason').attr('disabled', 1);

        }

    }

  
}

    async function saveQuotationPanel() {
        // อัพเดทใบเสนอราคา งานสั่งซื้อสินค้า
       

        if (!$('#flagChangeRequest').prop('checked')) {
            $('#DdlCustomerCancelQuotationReason').val(0);//clear dropdown CancelQuotationReason
            var updateBody = {
                quotationId: $("#ddlQuotationGoodsName").val(),
                homeCardRequestId: HomeCardrequestID,     
                homeCardPrice: $('#txtprice').val(),
                approveStatusId : lastQuotationGoods.approvedStatusId,
                username: username
              
            }
           
        }
        if ($('#flagChangeRequest').prop('checked')) {
            var updateBody = {
                quotationId: $("#ddlQuotationGoodsName").val(),
                homeCardRequestId: HomeCardrequestID,
                customerReasonId: $('#DdlCustomerCancelQuotationReason').val(),
                homeCardPrice: $('#txtprice').val(),            
                username: username
               
            }
        }

        ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/Quotation", updateBody, response => {
            if (response) {
                //---- *** Update HomeCardRequest for Optional Package.
                if (quotationHomecardOptionalPackList.length > 0) {
                    var updateBody_OptionalPack = {
                        home_card_request_id: $("#hdfhomcardrequest_optpack_id").val(),
                        extra_package_id: $("#hdfhomcardrequest_extra_package_id").val(),
                        extra_package_price: $("#txt_extra_package_price").val(), 
                        updated_by: username
                    }

                    var home_card_request_id = $("#hdfhomcardrequest_optpack_id").val();
                    ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + home_card_request_id + "/QuotationOptionalPack", updateBody_OptionalPack, response => {
                        if (response) {

                            modalSuccess();
                            $('#notificationModal').on('hidden.bs.modal', () => {
                                window.location.reload();
                            }) 
                        }
                    });
                }
                    
            }
        });


    }
    // สร้างใบเสนอราคา

    function createQuotation() {
        page.loading();
        var body = {
            unitId: unitId,
            homeCardRequestId: HomeCardrequestID,
            operateValue: 0 ,
            vatValue:  0,
            homeCardTypeId: homeCardTypeId,
            homeCardPrice: homeCardPrice,   
            username: username 
        }
        //Api Create new Quotation
        ajaxPost(baseUrl + "/homecare/api/v1/HomeCard/"+unitCode+"/Quotation" , body, (response) => {
            if (response) {
                modalSuccess("สร้างใบ quotation สำเร็จ ");
       
                $('#preTotalPrice').text(0);        
                $('#flagChangeRequest').prop('checked', false);
                $('#flagChangeRequest').removeAttr('disabled');
                $('#DdlCustomerCancelQuotationReason').attr('disabled', 1);
                $('#DdlCustomerCancelQuotationReason').val(0);
                $('#DivGenQuotationPDF').hide();
                $("#Received-Quotation-detail").show();
                window.location.reload();
                page.loaded();;
            }
        });
    }

    async function onChangeQuotationGoodsName() {
        var temp = quotationList.filter(quotation => quotation.homeCardQuotationId == $('#ddlQuotationGoodsName').val());
        $('#btnRequestApproval').removeAttr('disabled');//clear ปุ่ม
        $('#btnSaveQuotation').removeAttr('disabled'); //clear ปุ่ม
        $('#DivQuotationApprove').find('button').removeAttr('disabled');//clear ปุ่ม
        $('#flagChangeRequest').removeAttr('disabled');
        await getQuotationPanel(temp[0].homeCardQuotationId);

  
    }

    function applyApproveQuotation() {
        if (!$('#flagChangeRequest').prop('checked')) {
            $('#DdlCustomerCancelQuotationReason').val(0);//clear dropdown CancelQuotationReason
        }
        var updateBody = {
            quotationId: $("#ddlQuotationGoodsName").val(),
            homeCardRequestId: HomeCardrequestID,
            approveStatusId: 1,      
            approverRemark: $('#confirmReason').val(),
            homeCardPrice: $("#txtprice").val()  , 
            extraPackagePrice: $("#txt_extra_package_price").val(),
            username: username
        }

        ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/Quotation", updateBody, response => {
            if (response) {
                modalSuccess();
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload();
                })
            }
        });
    }

    function saveApproveQuotation() {
   
        var updateBody = {
            quotationId: $("#ddlQuotationGoodsName").val(),
            homeCardRequestId: HomeCardrequestID,
            approverName: username,   
            approveStatusId: 2 ,    
            homeCardPrice: $("#txtprice").val() , 
            extraPackagePrice: $("#txt_extra_package_price").val(),
            username: username
        }
        ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/Quotation", updateBody, response => {
            if (response) {
                modalSuccess();
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload();
                })
            }
        });
    }

    function saveRejectQuotation() {
        if (!validateRejectQuotation()) {
            return false;
        }
        var updateBody = {
            quotationId: $("#ddlQuotationGoodsName").val(),
            homeCardRequestId: HomeCardrequestID,
            approverName: username,
      
            approveStatusId: 3,
            homeCardPrice: $("#txtprice").val() ,
            approverRemark: $('#confirmReason').val(),
            username: username
        }
        ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/Quotation", updateBody, response => {
            if (response) {
                modalSuccess();
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload();
                })
            }
        });
    }


    function GetpostponeInvestigation() {
        // Get postponeInvestigation
     //   ajaxGet(`${baseUrl}/homecare/api/v1/Appointment/PostponeInvestigationAppointmentHistory/` + receiveId, response => {
        ajaxGet(baseUrl + '/homecare/api/v1/HomeCard/' + unitCode + '/SurveyPostPonement?homeCardRequestId=' + HomeCardrequestID, response => {
        while (response.data.length > 0) {
                postponeHistory = response.data.pop();
                //console.log("res", postponeHistory);

                if (postponeHistory.extendApproveFlag == 'ลูกค้ายืนยันเรียบร้อยแล้ว') {
                    break;
                } else {
                    postponeHistory = null;
                }
            }
            //console.log("finale", postponeHistory);
        });
    }

    function bindingDdlReceiveOverAppointmentDateReason() {
        var ddlOverAppointmentReasonUrl = `${baseUrl}/homecare/api/v1/DropDown/PostponeInvestigationAppointmentReason`;
        ajaxGet(ddlOverAppointmentReasonUrl, response => {
            bindingDdlist('ddlReceiveOverAppointmentDateReason', response.data, postponeHistory ? postponeHistory.description.includes('ลูกค้า') ? 1 : 0 : receiveData.data.receivePostponeAppointmentId);
        });
    }

    // =========================================================================== Events ===========================================================================
    // สถานะรับเรื่อง
    function onChangeddlReceivedStatus() {//TODO canchange when StatusIDOld == 1
        var StatusID = $("#ddlReceivedStatusID").val();
        var StatusIDOld = receiveData.requestStatusId;
        var HCDateOld = thaiFormatDate(receiveData.homeCareContactDate);
        var CCDateOld = thaiFormatDate(receiveData.callCenterContactDate);
        var InformDate = new Date(receiveData.createdAt);
   
        var d = new Date(Date.now()),
           today = [d.getDate().padLeft(),
            (d.getMonth() + 1).padLeft(),
            d.getFullYear()].join('/') + ' ' + [d.getHours().padLeft(),
           d.getMinutes().padLeft(),
                    d.getSeconds().padLeft()].join(':');

        var currentDatetime = moment(new Date()).format('DD/MM/YYYY HH:mm:ss');
        var lastUpdateDatetimeStatus = thaiFormatDate(receiveData.homeCareContactDate);

        var currentDateTimeConverted = moment(currentDatetime, 'DD/MM/YYYY HH:mm:ss');
        var lastUpdateDatetimeStatusConverted = moment(lastUpdateDatetimeStatus, 'DD/MM/YYYY HH:mm:ss');
        var duration = moment(currentDateTimeConverted).diff(lastUpdateDatetimeStatusConverted, 'hours', true);

        if (StatusID == "1") {
            $("#ddlReceivedStatusID").val(StatusIDOld ? StatusIDOld : 1);
            $("#ReceiveHCDate").text(HCDateOld);
            $("#ReceiveCCDate").text(CCDateOld);
            modalWaring("กรุณาเลือกสถานะรับเรื่อง");
        } else if (userRole != "CallCentre" && (StatusID == "6" || StatusID == "7")) {
            $("#ddlReceivedStatusID").val(StatusIDOld);
            $("#ReceiveHCDate").text(HCDateOld);
            $("#ReceiveCCDate").text(CCDateOld);
            modalWaring("คุณไม่มีสิทธิ์เลือกสถานะของ Call Centre");
        } else if (userRole == "CallCentre" && (StatusID == "2" || StatusID == "3" || StatusID == "4" || StatusID == "5")) {
            $("#ddlReceivedStatusID").val(StatusIDOld);
            $("#ReceiveHCDate").text(HCDateOld);
            $("#ReceiveCCDate").text(CCDateOld);
            modalWaring("คุณไม่มีสิทธิ์เลือกสถานะของ Home Care");
        }
        //else if (StatusID == "5" && (d - InformDate) / 36e5 < 24) {
        else if (((StatusIDOld != "4" && StatusID == "5")) || ((StatusID == "5") && (duration != NaN && duration < 24))) {
            $("#ddlReceivedStatusID").val(StatusIDOld);
            $("#ReceiveHCDate").text(HCDateOld);
            $("#ReceiveCCDate").text(CCDateOld);
            modalWaring("กรุณาติดต่อลูกค้าให้ครบภายใน 24 ชั่วโมง");
        } else if (((StatusIDOld != "5" && StatusID == "7")) || ((StatusID == "7") && (duration != NaN && duration < 72))) {
            $("#ddlReceivedStatusID").val(StatusIDOld);
            $("#ReceiveHCDate").text(HCDateOld);
            $("#ReceiveCCDate").text(CCDateOld);
            modalWaring("กรุณาติดต่อลูกค้าให้ครบภายใน 3 วัน");
        }
        else if (StatusID == "2") {
            $("#ReceiveHCDate").text(today);
    
            $("#ReceiveCCRemark").attr('disabled', 'disabled');
            $("#ReceiveHCRemark").removeAttr('disabled');
            if ($("#ReceiveAppointmentDate").val() == "") {
                $("#ReceiveAppointmentDate").removeAttr('disabled');
                $("#ReceiveAppointmentTimeForm").removeAttr('disabled');
                $("#ReceiveAppointmentTimeTo").removeAttr('disabled');
                //$("#ddlReceiveOverAppointmentDateReason").removeAttr('disabled');

            }
        } else if (StatusID == "3" || StatusID == "4" || StatusID == "5") {
            $("#ReceiveHCDate").text(today);
            $("#ReceiveCCRemark").attr('disabled', 'disabled');
            $("#ReceiveHCRemark").removeAttr('disabled');
            $("#ReceiveAppointmentDate").attr('disabled', 'disabled');
            $("#ReceiveAppointmentTimeForm").attr('disabled', 'disabled');
            $("#ReceiveAppointmentTimeTo").attr('disabled', 'disabled');
            $("#ddlReceiveOverAppointmentDateReason").attr('disabled', 'disabled');
      
        } else if (StatusID == "6" || StatusID == "7") {
            $("#ReceiveCCDate").text(today);
            $("#ReceiveHCRemark").attr('disabled', 'disabled');
            $("#ReceiveCCRemark").removeAttr('disabled');
            $("#ReceiveAppointmentDate").attr('disabled', 'disabled');
            $("#ReceiveAppointmentTimeForm").attr('disabled', 'disabled');
            $("#ReceiveAppointmentTimeTo").attr('disabled', 'disabled');
       
        } else {
            $("#ReceiveHCDate").text("");
            $("#ReceiveCCDate").text("");

            $("#ReceiveCCRemark").attr('disabled', 'disabled');
            $("#ReceiveHCRemark").attr('disabled', 'disabled');
        }
    };
    // วันเข้าตรวจสอบจริง
    function bindRealAppointmentDate() {
        if ($('#chkAPDateReal').prop('checked')) {
            var date = new Date().toJSON().slice(0, 10).replace(/-/g, '/').split('/');
            $('#ReceiveAppointmentDateReal').val(`${date[2]}/${date[1]}/${date[0]}`)
            $('#btnExtendedAppointment').hide();
        } else {
            $('#ReceiveAppointmentDateReal').val('');
            $('#btnExtendedAppointment').show();
        }
    }
    // Enabled overAppointmentDateReason
    function onchangeAppointmentDate() {
        var selectedDate = new Date(reorganizeDateFormat($("#ReceiveAppointmentDate").val()));
        var receiveHcDate = new Date();
        //console.log((selectedDate - receiveHcDate) / 8.64e+7);
        //If more than 1 day (2 or more): enabled ddlReason
        if (((selectedDate - receiveHcDate) / 8.64e+7) >= 2) {
            $('#ddlReceiveOverAppointmentDateReason').removeAttr('disabled');
        } else {
            $('#ddlReceiveOverAppointmentDateReason').attr('disabled', 1);
            $('#ddlReceiveOverAppointmentDateReason').val(0);
        }
    }
    // Bind realAppDate when reapirType = 2, 3
    function bindRealAppointmentDateWithType() {
        if (receiveData.data.repairTypeId == 2 || receiveData.data.repairTypeId == 3) {
            $('#chkAPDateReal').prop('checked', 1);
            $('#ReceiveAppointmentDateReal').val($('#ReceiveAppointmentDate').val())
        }
    }





    // Get fixing detail on fixing categories changed
    function onchangeFixingCategories() {
        ajaxGet(`${baseUrl}/homecare/api/v1/DropDown/FixingDetail/${$('#DdlFixingCategories').val()}`, response => {
            bindingDdlist('DdlFixingDetail', response.data, 0);
            ddlFixingDetail = response;
            //logging('DdlFixingDetail', response);
        });
    }
    // Get fixing issue on fixing detail changed
    function onchangeFixingDetail() {
        $.each(ddlFixingDetail.data, (i, d) => {
            if (d.id == $('#DdlFixingDetail').val()) {
                $('#txtVendorFixPriceUnit').text(d.units);
                objPrice = {
                    customerPackagePrice: d.customerPackagePrice,
                    customerWagePrice: d.customerWagePrice,
                    vendorPackagePrice: d.vendorPackagePrice,
                    vendorWagePrice: d.vendorWagePrice
                }
            }
        })
        onChangePriceType($('#ddlBillingType').val());
        $('#ddlBillingType').removeAttr('disabled');
    }

    //Add row discountTable
    function addTableRow(disabled = false) {
        var tempTrLength = $('#discountTBody tr').length + 1;
        if (tempTrLength == ddlCostType.length) { return false; }
        $('#discountTBody').append(`
				<tr>
        <td><p class="pt-5" >${tempTrLength}</p></td>
        <td>
            <select class="form-control" id="ddlCostType${tempTrLength}" onchange="onChangeCostType(this)" onfocus="beforeCostTypeChange = this.value" ${disabled ? 'disabled' : ''}></select>
        </td>
        <td>
            <div>
                <input type="number" class="form-control input-group-text" id="costValue${tempTrLength}" placeholder="0.00" min="0" onkeyup="calCostValueNet()" onclick="calCostValueNet()" readonly ${disabled ? 'disabled' : ''}>
                    <select class="form-control input-group-select" id="ddlCostUnit${tempTrLength}" onchange="calCostValueNet()" ${disabled ? 'disabled' : ''}></select>
						</div>
					</td>
            <td><label class="pt-5" id="costValueNet${tempTrLength}"></label></td>
            <td><i class="fa fa-close pt-5" onclick="removeTableRow(this)"></i></td>
				</tr>
        `)
        bindingDdlist('ddlCostType' + tempTrLength, ddlCostType, 0);
        bindingDdlist('ddlCostUnit' + tempTrLength, ddlCostUnit.data, 1);
        if ($('#discountTBody tr').length == ddlCostType.length - 1) {
            $('#btnAddRowCost').hide();
        }
        calCostValueNet();
    }

        // Delete row discountTable
        function removeTableRow(element) {
            $(element).parent().parent().remove();
            $('#discountTBody tr').each((i, element) => {
                $(element).find('p').text(i + 1);
                $(element).find('input').attr('id', 'costValue' + (i + 1));
                $(element).find('label').attr('id', 'costValueNet' + (i + 1));
                $(element).find('select').each((j, select) => {
                    if (j === 0) {
                        $(select).attr('id', 'ddlCostType' + (i + 1));
                    } else {
                        $(select).attr('id', 'ddlCostUnit' + (i + 1));
                    }
                });
            });
            $('#btnAddRowCost').show();
            $.each($('#discountTBody tr'), (i, d) => {
                if (!$('#ddlCostType' + (i + 1)).val()) {
                    $('#btnAddRowCost').hide();
                }
            })
            calCostValueNet();
        }
        // addReceiveItemDetailRow
        function addTableReceiveItemDetailRow(indexDateReason = 0, indexWorkStatus = 0, createdWorksheet = false, canDelete = true) {
            var tempTrLength = $('#tbodySubReceiveItem tr').length + 1;
            $('#tbodySubReceiveItem').append(`
				<tr>
            <td class="text-center">
                <p class="pt-5">${tempTrLength}</p>
                <input type="hidden" id="ReceiveItemDetailId${tempTrLength}" />
            </td>
            <td class="text-center">
                <select class="form-control" id="ddlReceiveItemDetailStatus${tempTrLength}" ${createdWorksheet ? 'disabled' : ''} style="min-width: 125px"></select>
            </td>
            <td class="text-center">
                <div class="input-group" onclick="itemAddvendor(this,${createdWorksheet})">
                    <input type="hidden" id="receiveItemDetailVendorCode${tempTrLength}" />
                    <div class="input-group-addon">
                        <a id="itemAddVendor"><i class="fa fa-user fa-lg"></i></a>
                    </div>
                    <div class="input-group">
                        <input type="text" id="receiveItemDetailVendorName${tempTrLength}" disabled readonly class="form-control" value="" />
                    </div>
                </div>
            </td>
            <td class="text-center">
                <select class="form-control" id="ddlReceiveItemDetailWorkType${tempTrLength}" style="min-width: 155px" disabled></select>
            </td>
            <td class="text-center">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control datepicker" data-val="true" id="receivItemDetailAppointmentDateForm${tempTrLength}" ${createdWorksheet ? 'disabled' : ''} onchange="onChangeReceivItemDetailAppointmentDateForm(${tempTrLength})" />
                </div>
            </td>
            <td class="text-center">
                <div class="input-group">
                    <div class="input-group-addon">
                        <i class="fa fa-calendar"></i>
                    </div>
                    <input type="text" class="form-control datepicker" data-val="true" id="receivItemDetailAppointmentDateTo${tempTrLength}" ${createdWorksheet ? 'disabled' : ''} />
                </div>
            </td>
            <td class="text-center">
                <select class="form-control" id="ddlReceiveItemDetailOverAppointmentDateReason${tempTrLength}" ${createdWorksheet ? 'disabled' : ''} disabled ></select>
            </td>
            <td class="text-center">
                <span class="fa fa-check" style="display: none" id="FlagworksheetCreated${tempTrLength}" /></span>
					</td>
        <td class="text-center">
            <input type="text" class="form-control" id="txtReceiveItemDetailRemark${tempTrLength}" ${createdWorksheet ? 'disabled' : ''} />
        </td>
        <td class="text-center">
            <span class="fa fa-close cursor-pointer ${canDelete ? '' : 'hidden'}" id="btnRemoveSubItem${tempTrLength}" onclick="removeTableReceiveItemDetailRow(this)" />
        </td>
    </tr>
    `)
            bindingDdlist(`ddlReceiveItemDetailOverAppointmentDateReason${tempTrLength}`, ddlReceiveItemDetailOverAppointmentDateReason.data, indexDateReason);
            bindingDdlist(`ddlReceiveItemDetailWorkType${tempTrLength}`, ddlRepairType.data, receiveData.data.repairTypeId);
            bindingDdlist(`ddlReceiveItemDetailStatus${tempTrLength}`, ddlWorkStatus.data, indexWorkStatus);
            $('.datepicker').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                startDate: new Date(),
                todayHighlight: true
            });
        }

            // Delete row discountTable
            function removeTableReceiveItemDetailRow(element) {
                $(element).parent().parent().remove();
                $('#tbodySubReceiveItem tr').each((i, element) => {
                    $(element).find('p').text(i + 1);
                    $.each($(element).find('select'), (j, selected) => {
                        switch (j) {
                            case 0:
                                $(selected).attr('id', `ddlReceiveItemDetailStatus${i + 1}`);
                                break;
                            case 1:
                                $(selected).attr('id', `ddlReceiveItemDetailWorkType${i + 1}`)
                                break;
                            case 2:
                                $(selected).attr('id', `ddlReceiveItemDetailOverAppointmentDateReason${i + 1}`)
                                break;
                        }
                    });
                    $.each($(element).find('input'), (j, selected) => {
                        switch (j) {
                            case 0:
                                $(selected).attr('id', `ReceiveItemDetailId${i + 1}`);
                                break;
                            case 1:
                                $(selected).attr('id', `receiveItemDetailVendorCode${i + 1}`)
                                break;
                            case 2:
                                $(selected).attr('id', `receiveItemDetailVendorName${i + 1}`)
                                break;
                            case 3:
                                $(selected).attr('id', `receivItemDetailAppointmentDateForm${i + 1}`)
                                break;
                            case 4:
                                $(selected).attr('id', `receivItemDetailAppointmentDateTo${i + 1}`)
                                break;
                            case 5:
                                $(selected).attr('id', `txtReceiveItemDetailRemark${i + 1}`)
                                break;
                        }
                    });
                    $.each($(element).find('span'), (j, selected) => {
                        switch (j) {
                            case 0:
                                $(selected).attr('id', `FlagworksheetCreated${i + 1}`);
                                break;
                            case 1:
                                $(selected).attr('id', `btnRemoveSubItem${i + 1}`)
                                break;
                        }
                    });
                });

            }
            //Check costType cant duplicate
            function onChangeCostType(element) {
                var countDup = 0;
                var countValue = 0;
                var index = 0;
                // Count duplicate and value
                for (var i = 0; i < $('#discountTBody tr').length; i++) {
                    if ($('#' + element.id).val() == $('#ddlCostType' + (i + 1)).val()) {
                        index = (i + 1);
                        countDup++;
                    }
                    if ($('#ddlCostType' + (i + 1)).val()) {
                        countValue++;
                    }
                }
                // Show btnAddRowCost เมื่อทุกบรรทัดมีค่า
                if (countValue == $('#discountTBody tr').length) {
                    if ($('#discountTBody tr').length != ddlCostType.length - 1) {
                        $('#btnAddRowCost').show();
                    }
                } else {
                    $('#btnAddRowCost').hide();
                }
                // Veridate ห้ามซ้ำ
                if (countDup > 1) {
                    $('#' + element.id).val(beforeCostTypeChange);
                    if (beforeCostTypeChange == 0) {
                        $('#costValue' + index).attr('readonly', 'true');
                    }
                    $('#costValue' + index).val(ddlCostType.filter(element => element.id == beforeCostTypeChange)[0].costValue);
                    modalWaring('กรุณาเลือกประเภทส่วนลดให้ไม่ซ้ำกัน');
                    calCostValueNet();
                    return false;
                }
                // Bind costvalue
                $.each(ddlCostType, (i, d) => {
                    if (d.id == $('#' + element.id).val()) {
                        $('#costValue' + index).val(d.costValue);
                        $('#costValue' + index).removeAttr('readonly');
                    }
                })
                calCostValueNet();
            }
            // Toggle disabled
            function toggleById(id) {
                $(id).prop('disabled', (_, bool) => { return !bool; });
            }

            // View picture on itemReceive
            function viewPicUploaded(element) {
                //logging($(element).parent().parent().find('label').text());
            }
            // Get SubWorkType and Location by MainWorkType
            async function onChangeMainWorkType(val, indexSub = 0, indexLocation = 0) {
                var promises = [
                    ajaxGet(`${baseUrl}/homecare/api/v1/DropDown/Work/Location/${val}`),// Get ddlWorkLocation
                    ajaxGet(`${baseUrl}/homecare/api/v1/DropDown/Work/SubWorkType/${val}`),// Get ddlSubWorkType
                ];
                [ddlWorkLocation, ddlSubWorkType] = await Promise.all(promises);
                // Binding ddlWorkLocation ddlSubWorkType
                bindingDdlist('ddlReceiveItemSubWorkType', ddlSubWorkType.data, indexSub);
                bindingDdlist('ddlReceiveItemLocation', ddlWorkLocation.data, indexLocation);
            }
                //Get ddlWorkDetail
                async function onChangeSubWorkType(val, index = 0) {
                    await ajaxGet(`${baseUrl}/homecare/api/v1/DropDown/Work/Detail/${val}`, response => {
                        ddlWorkDetail = response;
                        //Binding ddlWorkDetail
                        bindingDdlist('ddlReceiveItemWorkDetail', ddlWorkDetail.data, index);
                    })
                }
                    // binding txtPriority
                    function onChangeWorkDetail(workDetailid) {
                        var tempPriority = ddlWorkDetail.data.filter(element => element.id == workDetailid)[0].priority;
                        $('#txtReceiveItemPriority').val(tempPriority);
                    }
                    //chaeck ซ่อมซ้ำ
                    function checkDupFix() {
                        var body = {
                            "unitId": receiveData.data.unitId,
                            "guaranteeId": receiveItemModalGuaranteeId,
                            "spec1Id": $('#ddlReceiveItemMainWorkType').val() || 0,
                            "spec2Id": $('#ddlReceiveItemSubWorkType').val() || 0,
                            "spec3Id": $('#ddlReceiveItemWorkDetail').val() || 0,
                            "locationId": $('#ddlReceiveItemLocation').val() || 0
                        }
                        ajaxPost(`${baseUrl}/homecare/api/v1/Receive/CheckDuplicateFix`, body, response => {
                            if (response.data.trim()) {
                                $('#flagReceiveItemDuplicateFix').show();
                                $('#flagReceiveItemDuplicateFix').val(true);
                                modalWaring(response.data);
                            } else {
                                $('#flagReceiveItemDuplicateFix').hide();
                                $('#flagReceiveItemDuplicateFix').val(false);
                            }
                        })

                    }
                    // Add addTableReceiveItemRow
                    async function addTableReceiveItemRow(d = { no: $('#ReceiveItemTBody tr').length + 1, statusId: 1, repairId: 3 }) {

                    var ddlReceiveItemStatusStr = createDdlStringForm('ddlReceivedItemStatusID', ddlReceiveItemStatus.data, d.statusId || 0, !!!d.receiveItemId, true);
                    var ddlFlagRepairTransferStr = createDdlStringForm('ddlRepairID', ddlFlagRepairTransfer.data, d.repairId || 3);
                    var ddlReceiveItemCancelReasonStr = createDdlStringForm('ddlCancelResonID', ddlReceiveItemCancelReason.data, d.cancelReasonId || 0, (d.statusId != 3 && d.statusId != 4));
                    var ddlGuaranteeIDStr = createDdlStringForm('ddlGuaranteeID', ddlRepairGroup.data, d.guaranteeId || 0, !!d.receiveItemId);
                    $('#ReceiveItemTBody').append(`
				<tr id="${d.receiveItemId || 0}" onchange="changeReceiveItemList.includes(${d.receiveItemId}) ? '' : changeReceiveItemList.push(${d.receiveItemId || 0});">
        <td align="center">
            <label>${d.no}</label>
        </td>
        <td>
            ${ddlReceiveItemStatusStr}
        </td>
        <td>
            ${ddlGuaranteeIDStr}
        </td>
        <td>
            <textarea class="form-control" cols="30" rows="2" data-val="true" id="txtItemDetail" name="txtItemDetail" style="width:100%;" type="text" maxlength="512" ${!!d.receiveItemId ? 'disabled' : ''}>${d.detail || ''}</textarea>
        </td>
        <td>
            <textarea class="form-control" cols="30" rows="2" data-val="true" id="txtItemRemark" name="txtItemRemark" style="width:100%;" type="text" maxlength="512">${d.receiveRemark || ''}</textarea>
        </td>
        <td>
            ${ddlFlagRepairTransferStr}
        </td>
        <td class="text-center">
            <span class="fa fa-folder-open fa-lg viewFile cursor-pointer" aria-hidden="true" ${d.statusId != 2 ? 'style ="display: none"' : ''} onclick=openModalReceivedItemsDetail(${d.receiveItemId})></span>
					</td>
    <td class="text-center">
        <span class="fa fa-picture-o fa-lg viewFile cursor-pointer" aria-hidden="true" onclick="openModalViewEachImageBefore(${d.receiveItemId}, ${d.no - 1})"></span>
    </td>
    <td class="text-center">
        ${ddlReceiveItemCancelReasonStr}
    </td>
				</tr>
`);
                    if (d.receiveItemId) {
                        await ajaxGet(`${baseUrl}/homecare/api/v1/Receive/GetReceiveItemDetail/${d.receiveItemId} `, async response => {
                            if (response.data.length > 0) {
                                $.each(response.data, (i, d) => {
                                    if (d.worksheetCreateFlag) {
                                        $('#ReceiveItemTBody tr:last').find('input, select, textarea').attr('disabled', 1);
                                    }
                                })
                            }
                    });
                }
            }

            function toggleReceiveItemBtn() {
                $('#btnAddReceivedItem').attr('disabled') ? $('#btnAddReceivedItem').removeAttr('disabled') : $('#btnAddReceivedItem').attr('disabled', 1);
                $('#btnSaveReceivedItem').attr('disabled') ? $('#btnSaveReceivedItem').removeAttr('disabled') : $('#btnSaveReceivedItem').attr('disabled', 1);
                $('#btnRemoveReceivedItem').attr('disabled') ? $('#btnRemoveReceivedItem').removeAttr('disabled') : $('#btnRemoveReceivedItem').attr('disabled', 1);
            }

            function onChangeReceivedItemStatusID(val, element) {
                var data = $(element).parent().parent();

                switch (val) {
                    case '0':
                        break;
                    case '1':
                        data.find('#ddlCancelResonID').attr('disabled', true);
                        data.find('#ddlCancelResonID').val(0);
                        data.find('.fa-folder-open').hide();
                        break;
                    case '2':
                        data.find('#ddlCancelResonID').attr('disabled', true);
                        data.find('#ddlCancelResonID').val(0);
                        data.find('.fa-folder-open').show();
                        break;
                    case '3':
                        data.find('#ddlCancelResonID').removeAttr('disabled');
                        data.find('.fa-folder-open').hide();
                        break;
                    case '4':
                        data.find('#ddlCancelResonID').removeAttr('disabled');
                        data.find('.fa-folder-open').hide();
                        break;
                }
            }
            function onChangePriceType(val) {
                switch (val) {
                    case '0':
                        break;
                    case '1':
                        $('#customerBillingPrice').attr('readonly', true);
                        $('#customerBillingPrice').val(objPrice.customerPackagePrice || '0.00');
                        $('#vendorPackagePrice').attr('readonly', true);
                        $('#vendorWagePrice').attr('readonly', true);
                        $('#vendorPackagePrice').val('0.00')
                        $('#vendorWagePrice').val('0.00')
                        break;
                    case '2':
                        $('#customerBillingPrice').attr('readonly', true);
                        $('#customerBillingPrice').val(objPrice.customerWagePrice || '0.00');
                        $('#vendorPackagePrice').attr('readonly', true);
                        $('#vendorWagePrice').attr('readonly', true);
                        $('#vendorPackagePrice').val('0.00')
                        $('#vendorWagePrice').val('0.00')
                        break;
                    case '3':
                        $('#customerBillingPrice').removeAttr('readonly');
                        $('#customerBillingPrice').val('0.00');
                        $('#vendorPackagePrice').removeAttr('readonly');
                        $('#vendorWagePrice').attr('readonly', true);
                        $('#vendorPackagePrice').val('0.00')
                        $('#vendorWagePrice').val('0.00')
                        break;
                    case '4':
                        $('#customerBillingPrice').removeAttr('readonly');
                        $('#customerBillingPrice').val('0.00');
                        $('#vendorPackagePrice').attr('readonly', true);
                        $('#vendorWagePrice').removeAttr('readonly');
                        $('#vendorPackagePrice').val('0.00')
                        $('#vendorWagePrice').val('0.00')
                        break;
                }
                calTotalPrice();
            }
            // onChange Flag CustomerMaterialPrice
            function onChangeFlagCustomerMaterialPrice() {
                if ($('#flagCustomerMaterialPrice').prop('checked')) {
                    $('#customerMaterialPrice').removeAttr('readonly');
                    $('#vendorMaterialPrice').removeAttr('readonly');
                } else {
                    $('#customerMaterialPrice').attr('readonly', true);
                    $('#vendorMaterialPrice').attr('readonly', true);
                    $('#customerMaterialPrice').val('0.00');
                    $('#vendorMaterialPrice').val('0.00');
                }
            }

            // delete QuotationItemById
            function deleteQuotationItem(ele, id) {
                var body = {
                    quotationGoodsItemId: id,
                    updatedBy: '@UserDetail.CodeName'
                }
                ajaxDelete(`${baseUrl} /homecare/api / v1 / quotation / deletequotationgoodsitem`, body, response => {
                    if (response) {
                        $(ele).parent().parent().remove();
                        $.each($('#quotationItemDetailTBody tr'), (i, d) => {
                            $(d).find('td:first').text(i + 1);
                        })
                        $.each($('#preQuotationItemDetailTBody tr'), (i, d) => {
                            if (i == (+$(ele).parent().parent().find('td:first').text().trim() - 1)) {
                                d.remove();
                            }
                        });
                        $.each($('#preQuotationItemDetailTBody tr'), (i, d) => {
                            $(d).find('td:first').text(i + 1);
                            $($($(d).find('td')[7]).attr('id', 'itemPrice' + (i + 1)));
                        });
                        calAllTotalPrice();
                        calCostValueNet();
                    }
                })
            }

            // Onchange flagChangeRequest
            function onchangeflagChangeRequest() {
                if (!validateflagChangeRequest()) {
                    $('#flagChangeRequest').prop('checked', 0);
                    return false;
                }
                toggleById(DdlCustomerCancelQuotationReason)
            }
            // Remove receiveItem before save
            function removeReceiveItem() {
                $('#ReceiveItemTBody tr:last').remove();
            }
            // Enabled ddlReceiveItemDetailOverAppointmentDateReason
            function onChangeReceivItemDetailAppointmentDateForm(index, date = null) {
                var selectedDate = date ? new Date(reorganizeDateFormat(date)) : new Date(reorganizeDateFormat($('#receivItemDetailAppointmentDateForm' + index).val()));
                var receiveRealDate = new Date(receiveData.receiveAppointmentDateReal);
                //If more than 1 day (2 or more): enabled ddlReason
                if (((selectedDate - receiveRealDate) / 8.64e+7) > 5) {
                    $('#ddlReceiveItemDetailOverAppointmentDateReason' + index).removeAttr('disabled');
                } else {
                    $('#ddlReceiveItemDetailOverAppointmentDateReason' + index).attr('disabled', 1);
                    $('#ddlReceiveItemDetailOverAppointmentDateReason' + index).val(0);
                }
            }
                // ========================================================================= OpenModal =========================================================================
                // Open extended history modal
                async function openModalExtendedHistory() {
   
                    await page.loading();
                    ajaxGet(baseUrl + '/homecare/api/v1/HomeCard/' + unitCode + '/SurveyPostPonement?homeCardRequestId=' + HomeCardrequestID, response => {
                        console.log(baseUrl + '/homecare/api/v1/HomeCard/' + unitCode +'/SurveyPostPonement?homeCardRequestId=' + HomeCardrequestID);
                        if (response.data.length > 0) {
                            $('#tbodyExtendedAppointmentHistory').empty();
                            var row = 1;
                            $.each(response.data, (i, d) => {
                                $('#tbodyExtendedAppointmentHistory').append(`
						<tr>
						    <td>${row}</td>
							<td>${d.acceptedDescription}</td>
							<td>${d.createdBy} </td>
							<td>${d.oldAppointmentDateTime}</td>
							<td>${d.newAppointmentDateTime}</td>							
							<td>${d.homeCareRemark == null ? "-" : d.homeCareRemark}</td>
							<td>${d.callCentreRemark == null ? "-" : d.callCentreRemark }</td>
							<td>${d.updatedBy == null ? "-" : d.updatedBy }</td>
						</tr>
					`)
                                row++;
                            })
                        }
                        $("#modalExtendedAppointmentHistory").modal('show');
                        page.loaded();
                    });
                }


                // Open extend appointment modal
                async function openModalExtendedAppointment() {
    
                    // Get extended appointment
                    ajaxGet(baseUrl +"/homecare/api/v1/HomeCard/"+unitCode+"/SurveyPostPonement?homeCardRequestId="+ HomeCardrequestID, response => {
                        // If user role is call centre
                        if (response.data.length > 0 && userRole == "CallCentre") {
                            $("#ExtendedReceiveAppointmentDate").val(thaiFormatDate(response.data[0].newAppointmentDate,false) );
                            if (response.data[0].newAppointmentTimeFrom == null && response.data[0].newAppointmentTimeTo == null) {
                                $("#ExtendedReceiveAppointmentTimeForm").val('');
                                $("#ExtendedReceiveAppointmentTimeTo").val('');
                            } else {
                                $("#ExtendedReceiveAppointmentTimeForm").val(response.data[0].newAppointmentTimeFrom);
                                $("#ExtendedReceiveAppointmentTimeTo").val(response.data[0].newAppointmentTimeTo);
                            }
        
                            $("#txtExtendRemark").val(response.data[0].homeCareRemark);
                            $("#txtExtendRemarkCC").val(response.data[0].callCentreRemark);

                            if ("CallCentre" == userRole && (response.data[0].newAppointmentDate != '' && response.data[0].isAccepted == null)) {
                                $("[name='rdoConfirm']").prop('disabled', false);
                                $("#txtExtendRemarkCC").prop('disabled', false);
                                $('#btnUpdateExtended').prop('disabled', false);
                            } else if ("CallCentre" == userRole && (response.data[0].isAccepted == 1 || response.data[0].isAccepted == 0)) {
                                //$('#chkExtendedConfirm').prop('disabled', true);
                                $("[name='rdoConfirm']").prop('disabled', true);
                                $("#txtExtendRemarkCC").prop('disabled', true);
                                $('#btnUpdateExtended').prop('disabled', true);
                            } else if ("CallCentre" != userRole ) {
                                //$('#chkExtendedConfirm').prop('disabled', true);
                                $("[name='rdoConfirm']").prop('disabled', true);
                                $("#txtExtendRemarkCC").prop('disabled', true);
                                $('#btnUpdateExtended').prop('disabled', false);
                            } else {
                                //$('#chkExtendedConfirm').prop('disabled', true);
                                $("[name='rdoConfirm']").prop('disabled', true);
                                $("#txtExtendRemarkCC").prop('disabled', true);
                                $('#btnUpdateExtended').prop('disabled', true);
                            }

                            if ("CallCentre" == userRole) {
                                $("#ExtendedReceiveAppointmentDate").prop('disabled', true);
                                $("#ExtendedReceiveAppointmentTimeForm").prop('disabled', true);
                                $("#ExtendedReceiveAppointmentTimeTo").prop('disabled', true);           
                                $("#txtExtendRemark").prop('disabled', true);
                                //$("#chkExtendedConfirm").prop('checked',response[0].ExtendedApproveFlag);
                            }
                            console.log(response.data)
                            $("input[name=rdoConfirm][value=" + response.data[0].isAccepted + "]").attr('checked', 1);
                        } else {
                            $("#ExtendedReceiveAppointmentDate").val("");
                            $("#ExtendedReceiveAppointmentTimeForm").val("");
                            $("#ExtendedReceiveAppointmentTimeTo").val("");
                        }
                        $("#modalExtendedAppointment").modal('show');
                    });
                }

                // Open quotation history modal
                function openModalQuotationHistory() {
                    if (quotationList.length > 0) {
                        $('#quotationHistoryTBody').empty();
                        $.each(quotationList, (i, d) => {
                            $('#quotationHistoryTBody').append(`
    < tr >
    <td>
        ${i + 1}
    </td>
    <td>
        ${d.quotationGoodsName}
    </td>
    <td>
        ${d.approverName || ''}
    </td>
    <td>
        ${d.approverRemark || ''}
    </td>
    <td>
        ${thaiFormatDate(d.approvedDate) || ''}
    </td>
    <td>
        ${d.orderTypeDescription}
    </td>
    <td>
        ${d.orderConditionDescription}
    </td>
    <td>
        ${d.orderStatusDescription}
    </td>
    <td>
        ${d.remark}
    </td>
    <td>
        ${d.paymentStatusDescription}
    </td>
    <td ${d.customerReasonDescription ? 'style="background-color: #ff6666;"' : ''}>
        ${d.customerReasonDescription || ''}
    </td>
				</tr >
    `) // TODO ผู้อนุมัติ ความคิดเห็น วันที่อนมัติ
                        })
                    }
                    $("#modalQuotationHistory").modal('show');
                }
                // Open edit quotation modal


                async function openModalViewEachImageBefore(receiveId, index) {
                    await page.loading();
                    //logging(index);
                    $('#rowOfImageInput').empty();
                    await InitailModalFileUpload(receiveId, index);
                    $('#rowOfImageInput').append(`
    < div class="box box-default" id = "ItemImage${index}" >
        <div class="box-header with-border cursor-pointer" data-toggle="collapse" data-target="#ItemImage-detail${index}" aria-expanded="true" aria-controls="ItemImage-detail${index}">
            <a >
                <h3 class="box-title"><i class="fa fa-picture-o"></i> รูปรายการแจ้งซ่อมที่ ${index + 1}</h3>
            </a>
        </div>
        <div class="box-body collapse in" id="ItemImage-detail${index}">
            <div class="row">
                <div class="col-xs-12">
                    <input id="fileInput${index}" name="file" type="file" multiple />
                </div>
            </div>
        </div>
				</div >
    `);
                    page.loaded();;
                    $('#modalUploadImageBefore').modal('show');

                }

                async function openModalReceivedItemsDetail(receiveItemId) {
                    $('#txtReceiveItemProject').text(customerData.data.projectNameTh);
                    $('#txtReceiveItemUnit').text(customerData.data.unitCodeAddress);
                    var thisItem = receiveItemList.filter(element => element.receiveItemId == receiveItemId)[0];
                    //หัวข้อหลักใน Modal
                    $("#MainDataInModal").empty();
                    $("#MainDataInModal ").append(`
    < div class="col-xs-12 col-sm-6 col-lg-4" >
        <h5> หมวดงาน: <span class="text text-primary">${thisItem.guaranteeDescription}</span></h5>
				</div >
    `);
                    $("#MainDataInModal").append(`
    < div class="col-xs-12 col-sm-6 col-lg-2" >
        <h5> รายละเอียดการแจ้ง: <span class="text text-primary">${thisItem.detail}</span></h5>
				</div >
    `);
                    receiveItemModalGuaranteeId = thisItem.guaranteeId;
                    receiveItemModalId = receiveItemId;
                    ajaxGet(`${baseUrl}/homecare/api/v1/Receive/GetReceiveItemModal/${receiveItemId} `, async response => {
                    //logging('ReceiveItemModal', response);
                        if (response.data.specL1Id) {
                            bindingDdlist('ddlReceiveItemMainWorkType', ddlMainWorkType.data, response.data.specL1Id);
                    bindingDdlist('ddlReceiveItemWorkIssue', ddlWorkIssue.data, response.data.causeId);
                    var promises = [
                        onChangeMainWorkType(response.data.specL1Id, response.data.specL2Id, response.data.locationId),
                        onChangeSubWorkType(response.data.specL2Id, response.data.specL3Id)
                    ]
                    await Promise.all(promises);
                    onChangeWorkDetail(response.data.specL3Id);
                    checkDupFix();

                } else {
                        bindingDdlist('ddlReceiveItemMainWorkType', ddlMainWorkType.data, 0);
                bindingDdlist('ddlReceiveItemWorkIssue', ddlWorkIssue.data, 0);
                bindingDdlist('ddlReceiveItemSubWorkType', [ddlWorkIssue.data[0]], 0);
                bindingDdlist('ddlReceiveItemWorkDetail', [ddlWorkIssue.data[0]], 0);
                bindingDdlist('ddlReceiveItemLocation', [ddlWorkIssue.data[0]], 0);
                $('#txtReceiveItemPriority').val('');
            }
        });
        await ajaxGet(`${baseUrl}/homecare/api/v1/Receive/GetReceiveItemDetail/${receiveItemId}`, async response => {
        //logging('ReceiveItemDetail', response);
            $('#tbodySubReceiveItem').empty();
        worksheetCreateFlag = false;
        if (response.data.length > 0) {
            $.each(response.data, (i, d) => {
                addTableReceiveItemDetailRow(d.repairAppointmentDateReasonId, d.statusId, d.worksheetCreateFlag, false);
                $('#ReceiveItemDetailId' + (i + 1)).val(d.receiveItemDetailId);openModalConfirmWithReason
                $('#receiveItemDetailVendorName' + (i + 1)).val(d.propertyName);
                $('#receiveItemDetailVendorCode' + (i + 1)).val(d.vendorId);
                $('#receivItemDetailAppointmentDateForm' + (i + 1)).val(d.repairAppointmentDateFrom);
                $('#receivItemDetailAppointmentDateTo' + (i + 1)).val(d.repairAppointmentDateTo);
                $('#txtReceiveItemDetailRemark' + (i + 1)).val(d.remark);
                onChangeReceivItemDetailAppointmentDateForm(i + 1, d.repairAppointmentDateFrom);
                d.worksheetCreateFlag ? ($('#FlagworksheetCreated' + (i + 1)).show(), $('#btnRemoveSubItem' + (i + 1)).hide(), $('#ddlReceiveItemDetailOverAppointmentDateReason' + (i + 1)).attr('disabled', 1)) : ($('#FlagworksheetCreated' + (i + 1)).hide(), $('#btnRemoveSubItem' + (i + 1)).show());
                if (d.worksheetCreateFlag) {
                    worksheetCreateFlag = true;
                }
            })
        } else {
            addTableReceiveItemDetailRow();
        }
    });
    if (worksheetCreateFlag) {
        $('#boxReceivedRepair').find('input, select').attr('disabled', 1);
    } else {
        $('#boxReceivedRepair').find('input, select').removeAttr('disabled');
    }
    $('#modalReceivedItemsDetail').modal('show');
}
function openModalCloseReceiveRepair() {
    $('#modalCloseReceiveRepair').modal('show');
}

function openModalConfirmWithReason(title, txt, func, require) {
    $('#confirmTitle').text(title);
    $('#confirmTxtReason').text(txt);
  
    require ? $('#confirmStarReason').show() : $('#confirmStarReason').hide();
    $('#confirmBtn').removeAttr('onclick');
    $('#confirmBtn').attr('onclick', func + `();$('#modalConfirmWithReason').modal('hide')`);
    $('#modalConfirmWithReason').modal('show');
}

function openModalConfirmWOR(title, txt, func) {
    $('#confirmTitleWOR').text(title);
    $('#confirmTxt').text(txt);
    $('#confirmBtnWOR').removeAttr('onclick');
    $('#confirmBtnWOR').attr('onclick', func + `(); $('#modalConfirm').modal('hide')`);
    $('#modalConfirm').modal('show');
}
// =============================================================================== AllSave ===================================================================================
// Save extended appointment
async function saveModalExtendedAppointment() {
    if (!validateExtendPanel()) {
        return false;
    }
   
    if (userRole == "HC") {
        var body = {
            homeCardRequestId: HomeCardrequestID,
            newAppointmentDate: reorganizeDateFormat($('#ExtendedReceiveAppointmentDate').val()),
            newAppointmentFrom: $('#ExtendedReceiveAppointmentTimeForm').val(),
            newAppointmentTo: $('#ExtendedReceiveAppointmentTimeTo').val(),
            homeCareRemark: $('#txtExtendRemark').val() ? $('#txtExtendRemark').val() : null,
            username: username
        };
        ajaxPost(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/SurveyPostPonement", body, response => {
            if (response) {
                page.loaded();
                modalSuccess();
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload();
                })
                
            }
        });
    }
    if (userRole == "CallCentre" ) {
        var isAccepted;
        if ( $('#confirmFlagTrue').prop('checked') ) {
            isAccepted = 1;
        }
        if ( $('#confirmFlagFalse').prop('checked')) {
            isAccepted = 0;
        }       
        var body = {
            homeCardRequestId: HomeCardrequestID,
            isAccepted: isAccepted,
            callCentreRemark: $('#txtExtendRemarkCC').val() ? $('#txtExtendRemarkCC').val() : null,
            username: username
        };

       
        ajaxPut(baseUrl + "/homecare/api/v1/HomeCard/" + unitCode + "/SurveyPostPonement", body, response => {
            if (response) {
                page.loaded();
                modalSuccess();
                $('#notificationModal').on('hidden.bs.modal', () => {
                    page.loading();
                    window.location.reload();
                })

            }
        });
        
       


    }

    if (userRole !== "CallCentre" || userRole == "HC" ) {
       
        modalWaring("คุณไม่มีสิทธิ์ทำการเลื่อนนัดหมายตรวจสอบ");
    }

}



async function saveModalReceivedItemsDetail() {
    if (!validateRepairDetailPanel() || !validateRepairVendorPanel()) {
        return false;
    }
    await page.loading();
    if (!worksheetCreateFlag) {
        var bodyModal = {
            receiveItemId: receiveItemModalId,
            specL1Id: $('#ddlReceiveItemMainWorkType').val(),
            specL2Id: $('#ddlReceiveItemSubWorkType').val(),
            specL3Id: $('#ddlReceiveItemWorkDetail').val(),
            locationId: $('#ddlReceiveItemLocation').val(),
            causeId: $('#ddlReceiveItemWorkIssue').val(),
            duplicatedFixid: $('#flagReceiveItemDuplicateFix').val(),
            userCode: username
        };
        //logging('bodyModal', bodyModal);
        var success = await ajaxPut(`${baseUrl} /homecare/api / v1 / Receive / UpdateReceiveItemModal`, bodyModal);
        if (!success) {
            modalWaring("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
            page.loaded();;
            return false;
        } else {
            if ($('#tbodySubReceiveItem tr').length > 0) {
                modalSuccess();
                page.loaded();;
            }
        }
    }
    for (var i = 0; i < $('#tbodySubReceiveItem tr').length; i++) {

        //For skipping row
        if (isEmptyInput("ddlReceiveItemDetailStatus" + (i + 1)) &&
            isEmptyInput("receiveItemDetailVendorName" + (i + 1)) &&
            isEmptyInput("ddlReceiveItemDetailWorkType" + (i + 1)) &&
            isEmptyInput("receivItemDetailAppointmentDateForm" + (i + 1)) &&
            isEmptyInput("receivItemDetailAppointmentDateTo" + (i + 1)) &&
            isEmptyInput("ddlReceiveItemDetailOverAppointmentDateReason" + (i + 1))) {

            if (i + 1 == $('#tbodySubReceiveItem tr').length) {
                page.loaded();;
                $("#modalReceivedItemsDetail").modal("hide");
            }
            //console.log("skip" + (i + 1));
            continue;
        }
        //console.log("pass" + (i + 1));
        if ($('#receiveItemDetailVendorName' + (i + 1)).val().trim()) {
            var bodyDetail = {
                receiveItemId: receiveItemModalId,
                id: $('#ReceiveItemDetailId' + (i + 1)).val(),
                no: (i + 1),
                statusId: $('#ddlReceiveItemDetailStatus' + (i + 1)).val(),
                vendorId: $('#receiveItemDetailVendorCode' + (i + 1)).val(),
                vendorName: $('#receiveItemDetailVendorName' + (i + 1)).val().trim(),
                jobTypeId: $('#ddlReceiveItemDetailWorkType' + (i + 1)).val(),
                repairAppointmentDateFrom: reorganizeDateFormat($('#receivItemDetailAppointmentDateForm' + (i + 1)).val()),
                repairAppointmentDateTo: reorganizeDateFormat($('#receivItemDetailAppointmentDateTo' + (i + 1)).val()),
                reasonId: $('#ddlReceiveItemDetailOverAppointmentDateReason' + (i + 1)).val() || 0,
                remark: $('#txtReceiveItemDetailRemark' + (i + 1)).val(),
                userCode: '@UserDetail.CodeName',
                userId: '@UserDetail.UserID'
            };
            //logging('bodyDetail', bodyDetail);
            await ajaxPut(`${baseUrl} /homecare/api / v1 / Receive / UpdateReceiveItemDetail`, bodyDetail, response => {
                if (i + 1 == $('#tbodySubReceiveItem tr').length) {
                    if (response) {
                        modalSuccess();
                        page.loaded();;
                        $("#modalReceivedItemsDetail").modal("hide");
                    } else {
                        modalWaring("เกิดข้อผิดพลาดในการบันทึกข้อมูล");
                        page.loaded();;
                    }
                }
            });
        }
    }
}



function createWorkSheet() {
    var body = {
        receiveId: receiveId,
        username: '@UserDetail.CodeName',
        userId: '@UserDetail.UserID'
    }
    ajaxPost(`${baseUrl} /homecare/api / v1 / Worksheet / InsertWorksheet`, body, response => {
        //console.log(response);
        if (response) {
            modalSuccess();
            $('#notificationModal').on('hidden.bs.modal', () => {
                page.loading();
                window.location.reload();
            })
        }
    });
}

function previewWorkSheet() {
    ajaxGet(`${baseUrl}/homecare/api/v1/Receive/GetReceiveItemDetailGroup/${receiveId}`, response => {
        //logging(response);
        data = response.data;
        $("#tbPreviewWorkOrder tbody tr").remove();
        var vendorName = "";
        var colsp = 1;
        var countWO = 0;
        var rowspan = [];

        //console.log(data);
        if (data.length > 0) {

            $.each(data, function (key, value) {

                if (vendorName == value.vendorName) {
                    colsp = colsp + 1;
                } else {
                    rowspan.push(colsp);
                    //console.log(colsp);
                    countWO = countWO + 1;
                    colsp = 1;
                }
                vendorName = value.vendorName;
            });

            rowspan.push(colsp);
            //console.log(rowspan);
            rowspan.shift();
            //console.log(rowspan);
            vendorName = "";
            $.each(data, function (key, value) {

                $("#tbPreviewWorkOrder tbody").append("<tr></tr>");
                if (vendorName != value.vendorName) {
                    $("#tbPreviewWorkOrder tbody tr:last").append("<td class=\"text-left\">" + value.vendorName + "</td>");
                    $('#tbPreviewWorkOrder tr:last').find('td:first').prop("rowspan", rowspan.shift());
                }

                $("#tbPreviewWorkOrder tbody tr:last").append("<td class=\"text-left\">" + value.guaranteeName + "</td>");
                //$("#tbPreviewWorkOrder tbody tr:last").append("<td class=\"text-left\">"+value.Vendor_Type+"</td>");

                vendorName = value.vendorName;

            });

            $("#btnOpenWorkOrder").removeAttr("disabled");
        } else {
            $("#tbPreviewWorkOrder tbody").append("<tr></tr>");
            $("#tbPreviewWorkOrder tbody tr:last").append("<td class='text-center Bold text-danger' colspan='3'>ไม่มีข้อมูล</td>");
            $("#btnOpenWorkOrder").attr("disabled", "disabled");

        }
        $("#lblOpenWO").text(countWO + " ใบงาน");
    })
    $("#modalPreviewWorkOrder").modal("show");
}
//Close receiveRepair
function closeReceiveRepair() {
    if (!validateCloseReceiveReason()) {
        return false;
    }
    $('#modalConfirmWithReason').modal('hide');
    var body = {
        receiveId: receiveData.data.receiveId,
        remark: $('#confirmReason').val(),
        role: '@UserDetail.Role' == 'CallCenter' ? 'CC' : '@UserDetail.Role',
        updatedBy: '@UserDetail.CodeName'
    }
    ajaxDelete(`${baseUrl} /homecare/api / v1 / Receive / DeleteReceive`, body, response => {
        if (response) {
            modalSuccess();
            $('#notificationModal').on('hidden.bs.modal', () => {
                page.loading();
                window.location.reload(true);
            });
        }
    })
}
// ======================================================================= OtherFunctions =======================================================================
// Date format for post
function reorganizeDateFormat(date) {
    if (!date) { return '' }
    var temp = date.split('/');
    var newDate = new Date(temp[2] + '-' + temp[1] + '-' + temp[0]);

    return thaiTimeInUTC(newDate);
}

// Date format for display
function thaiFormatDate(date, withTime = true) {
    if (!date) { return '' }
    //date = UTCToLocal(date);
    var [y, m, d] = date.split('T')[0].split('-');
    var [t, z] = date.split('T')[1].split('.');
    var result = d + "/" + m + "/" + y + ` ${withTime ? t : ''}`;
    return result.trim() ;
}
    function UTCToLocal(date) {
        var localeDate = new Date(date);
        return date = new Date(localeDate.setHours(localeDate.getHours() + 7)).toJSON();
    }
    function thaiTimeInUTC(date) {
        return new Date(date.setHours(date.getHours() + date.getTimezoneOffset() / 60 + 7));
    }
    // Bind dropdown list
    function bindingDdlist(ddlId, ddlData, selectedVal) {
        $("#" + ddlId).empty();
        $.each(ddlData, (i, d) => {
            if (d.id == 0) {
                $(`#${ddlId}`).append(`<option value = "${d.id}" selected disabled> ${d.description}</option>`);
            } else if (selectedVal == d.id) {
                $(`#${ddlId}`).append(`<option value = "${d.id}" selected> ${d.description}</option>`);
            } else {
                $(`#${ddlId}`).append(`<option value = "${d.id}" > ${d.description}</option>`);
            }
        })
    }
    // Create ddlist in form string
    function createDdlStringForm(id, ddlData, selectedVal, disabled = false, func = false) {
        var tempstr = `<select id = "${id}" class="form-control" ${disabled ? 'disabled' : ''} ${func ? 'onchange="onChangeReceivedItemStatusID(this.value, this)"' : ''}> `;
        $.each(ddlData, (i, d) => {
            tempstr += `<option value = "${d.id}" ${selectedVal == d.id ? 'selected' : ''} ${d.id == 0 ? 'disabled' : ''}> ${d.description}</option > `
        });
        return tempstr + '</select>';
    }
        // สถานะรับเรื่อง function
        Number.prototype.padLeft = function (base, chr) {
            var len = (String(base || 10).length - String(this).length) + 1;
            return len > 0 ? new Array(len).join(chr || '0') + this : this;
        }



        function bindTBodyPreQuotationItemDetail(data) {
            $('#preQuotationItemDetailTBody').empty();
            $.each(data, (i, d) => {
                var normalPrice = d.packagePriceType == 1 ? d.customerPackagePrice : d.wagePriceType == 1 ? d.customerWagePrice : 0;
                var spacialPrice = d.packagePriceType == 2 ? d.customerPackagePrice : d.wagePriceType == 2 ? d.customerWagePrice : 0;
                $('#preQuotationItemDetailTBody').append(`
					<tr>
						<td class="text-center">
							${$('#preQuotationItemDetailTBody tr').length + 1}
						</td>
						<td>
							${d.workCategoryDescription}
						</td>
						<td>
							${d.workDetailDescription}
						</td>
						<td>
							${roundFixed(normalPrice)}
						</td>
						<td>
							${roundFixed(+spacialPrice + +d.customerMaterialPrice)}
						</td>
						<td>
							${roundFixed(d.quantity)}
						</td>
						<td>
							${d.workDetailUnits}
						</td>
						<td id="itemPrice${$('#preQuotationItemDetailTBody tr').length + 1}">
							${roundFixed((+normalPrice + +spacialPrice + +d.customerMaterialPrice) * d.quantity)}
						</td>
						<td>
							${d.remark}
						</td>
					</tr>
				`)
            })
            calAllTotalPrice();
        }
        function bindTBodyQuotationItemDetail(data) {
            $('#quotationItemDetailTBody').empty();
            $.each(data, (i, d) => {
                $('#quotationItemDetailTBody').append(`
					<tr>
						<td>
							${$('#quotationItemDetailTBody tr').length + 1}
						</td>
						<td>
							${d.workCategoryDescription}
						</td>
						<td>
							${d.workDetailDescription}
						</td>
						<td>
							${roundFixed(d.packagePriceType == 1 ? d.customerPackagePrice : 0.00)}
						</td>
						<td>
							${roundFixed(d.wagePriceType == 1 ? d.customerWagePrice : 0.00)}
						</td>
						<td>
							${roundFixed(d.packagePriceType == 2 ? d.customerPackagePrice : 0.00)}
						</td>
						<td>
							${roundFixed(d.wagePriceType == 2 ? d.customerWagePrice : 0.00)}
						</td>
						<td>
							${roundFixed(d.customerMaterialPrice)}
						</td>
						<td>
							${d.workDetailUnits}
						</td>
						<td>
							${(Math.round(d.quantity * 100) / 100).toFixed(2)}
						</td>
						<td>
							${(Math.round((d.customerPackagePrice + d.customerWagePrice + d.customerMaterialPrice) * d.quantity * 100) / 100).toFixed(2)}
						</td>
						<td>
							${d.remark}
						</td>
						<td>
							<i class="fa fa-close cursor-pointer" onclick="deleteQuotationItem(this, ${d.quotationGoodsItemId})"></i>
						</td>
					</tr>
				`) // TODO ปริมาณ
            })
            calAllTotalPrice();
        }

        // Calculate price on radio button in quotation modal changed
        function calTotalPrice() {
            var customerPrice = +$('#customerBillingPrice').val() + +$('#customerMaterialPrice').val();
            var weight = $('#materialQuantity').val() || 0;

            var TotalPrice = Math.round((parseFloat(customerPrice) * parseFloat(weight)) * 100) / 100;
            $('#TotalPrice').text(TotalPrice.toFixed(2));
            $('#lblTotalUnit').text(TotalPrice.toFixed(2));

        }
        // คำนวนราคารวมค่าวัสดุ
        function calAllTotalPrice() {
            var totalPrice = 0;
            for (var i = 1; i <= $('#preQuotationItemDetailTBody tr').length; i++) {
                totalPrice += +$('#itemPrice' + i).text();
            }
            $('#preTotalPrice').text(roundFixed(+totalPrice));
            $('#lblTotalPrice').text(roundFixed(+totalPrice));
            if (totalPrice > 0) {
                $('#DivCost').show();
            }
        }

        // Calculate ChargeValueNet
        function calChargeValueNet() {
            for (var i = 1; i <= $('#DivChargeDetail .row').length; i++) {
                if (i == 1) {
                    $('#chargeValueNet' + i).val(Math.round(+($('#totalPriceAfterDiscount').text() != 0 ? $('#totalPriceAfterDiscount').text() : $('#preTotalPrice').text()) * +$('#chargeValue' + i).val()) / 100);
                    $('#totalPriceAfterCharge' + i).val(Math.round(+($('#totalPriceAfterDiscount').text() != 0 ? $('#totalPriceAfterDiscount').text() : $('#preTotalPrice').text()) * (100 + +$('#chargeValue' + i).val())) / 100);
                } else {
                    $('#chargeValueNet' + i).val(Math.round(+$('#totalPriceAfterCharge' + (i - 1)).val() * +$('#chargeValue' + i).val()) / 100);
                    $('#totalPriceAfterCharge' + i).val(Math.round(+$('#totalPriceAfterCharge' + (i - 1)).val() * (100 + +$('#chargeValue' + i).val())) / 100);
                }
            };
            $('#totalPriceNet').text(($('#totalPriceAfterCharge' + $('#DivChargeDetail .row').length).val()));
        }

        function genQuotation() {
            ajaxGet(`${baseUrl}/homecare/api/v1/DocumentToken`, response => {
                var quotationid = $("#ddlQuotationGoodsName").val();
                var win = window.open(`${baseUrl}/homecare/Document/QuotationHomeCard/${unitCode}?paymentSlip=true&quotationHomeCardId=${quotationid}&Token=${response.data}`   , '_blank');
                if (win) {
                    win.focus();
                }
            })
        }

        function logging(...data) {
            //console.log(...data);
        }
        // Math.round().toFixed(2)
        function roundFixed(data, decimal = 2) {
            return (Math.round(data * 100) / 100).toFixed(decimal)
        }
            // Vendor by พี่สืบ
            function itemAddvendor(e, createdWorksheet) {
                if (!createdWorksheet) {
                    clearVendorSearch();
                    tempVendoorRow = $(e);
                    $("#modalAddVendor").appendTo("body").modal('show');
                    $("#tbAddvendor tbody tr").remove();
                    $("#tbAddvendor tbody").append("<tr></tr>");
                    $("#tbAddvendor tbody tr").append("<td class='text-center Bold text-danger' colspan='6'>ไม่มีข้อมูล</td>");
                }
            }
            $("#btnSearchVendor").on("click", function () {
                if ($("#textCrivendorCode").val() == "" && $("#textCrivendorName").val() == "" && $("#textCriIDCard").val() == "") {
                    modalWaring("กรุณาระบุเงื่อนไขในการค้นหา");
                } else {
                    searchVendor();
                }
            });
            async function searchVendor() {
                await page.loading();
                var body = {
                    Name: $("#textCrivendorName").val(),
                    TaxID: $("#textCriIDCard").val(),
                    Vendor: $("#textCrivendorCode").val() == "" ? "" : $("#textCrivendorCode").val()
                };
                $.ajax({
                    url: '@Url.Action("getListVendormaster", "Master")',
                    type: 'POST',
                    data: body,
                    traditional: true,
                    success: function (data) {
                        $("#tbAddvendor tbody tr").remove();
                        if (data.length == 0) {
                            $("#tbAddvendor tbody").append("<tr></tr>");
                            $("#tbAddvendor tbody tr").append("<td class='text-center Bold text-danger' colspan='6'>ไม่มีข้อมูล</td>");
                        } else {
                            $.each(data, function (key, value) {
                                $("#tbAddvendor tbody").append("<tr></tr>");
                                $("#tbAddvendor tbody tr:last").append("<td class=\"text-center\"><label id='VendorCode'>" + value.Vendor.substr(value.Vendor.length - 6) + "</label></td>");
                                $("#tbAddvendor tbody tr:last").append("<td class=\"text-left\"><label id='VendorName'>" + value.Name + "</lable></td>");
                                $("#tbAddvendor tbody tr:last").append("<td class=\"text-center\">" + value.CardID + "</td>");
                                $("#tbAddvendor tbody tr:last").append("<td class=\"text-center\">" + value.TaxID + "</td>");
                                $("#tbAddvendor tbody tr:last").append("<td class=\"text-center\"><a id='selectVendor' name='selectVendor[]'>เลือก</a></td>");
                            });
                            $("[name*='selectVendor[]']").each(function (idx, elem) {
                                $(this).click(function () {
                                    var VendorCode = $(this).closest('tr').find("#VendorCode").text()
                                    var VendorName = $(this).closest('tr').find("#VendorName").text()
                                    var no = tempVendoorRow.parent().parent().find('p').text();
                                    tempVendoorRow.closest('tr').find("#receiveItemDetailVendorName" + no).val(VendorName);
                                    tempVendoorRow.closest('tr').find("#receiveItemDetailVendorCode" + no).val(VendorCode);
                                    $("#modalAddVendor").modal('hide');
                                });
                            });
                        }
                        page.loaded();;
                    },
                });
            }
            function loadingUrgentMaintenanceForm() {
                // Request โดยพี่แสนดี ให้ข้าม Flow upload image before
                var skipUploadImageBefore = true;
                if (receiveData.data.flagUrgent || receiveData.data.repairTypeId == 3 || skipUploadImageBefore) {
                    if (receiveData.data.flagUrgent || receiveData.data.repairTypeId == 3) {
                        $('#chkAPDateReal').attr('disabled', 1);
                    }

                    $('#DivImageAttachment').hide();
                    if (receiveData.receiveAppointmentDateReal) {
                        $('#DivOfferPrice').show();
                    } else {
                        $('#Received-Quotation-detail').collapse();
                    }
                }
            }

            //===================================================================== Validate ===============================================================================
            //Validate Repair Detail
            function validateRepairDetailPanel() {
                if (isEmptyInput("ddlReceiveItemMainWorkType") ||
                    isEmptyInput("ddlReceiveItemSubWorkType") ||
                    isEmptyInput("ddlReceiveItemWorkDetail") ||
                    isEmptyInput("ddlReceiveItemLocation") ||
                    isEmptyInput("ddlReceiveItemWorkIssue")) {
                    modalWaring("กรุณากรอก\"ข้อมูลผู้แจ้งซ่อม\"ให้ครบถ้วน");
                    return false;
                }

                //console.log("success")
                return true;
            }



            function validateRepairVendorPanel() {
                var len = $('#tbodySubReceiveItem tr').length;

                for (var i = 1; i <= len; i++) {
                    if (isEmptyInput("ddlReceiveItemDetailStatus" + i) &&
                        isEmptyInput("receiveItemDetailVendorName" + i) &&
                        isEmptyInput("ddlReceiveItemDetailWorkType" + i) &&
                        isEmptyInput("receivItemDetailAppointmentDateForm" + i) &&
                        isEmptyInput("receivItemDetailAppointmentDateTo" + i) &&
                        isEmptyInput("ddlReceiveItemDetailOverAppointmentDateReason" + i)) {
                        continue;
                    }

                    if (isEmptyInput("ddlReceiveItemDetailStatus" + i)) {
                        modalWaring("กรุณาเลือก \"สถานะ\" ของรายการที่ " + i);
                        return false;
                    }

                    if (isEmptyInput("receiveItemDetailVendorName" + i)) {
                        modalWaring("กรุณาเลือก \"ผู้รับเหมา\" ของรายการที่ " + i);
                        return false;
                    }

                    //  if (isEmptyInput("ddlReceiveItemDetailWorkType" + i)) {
                    //     modalWaring("กรุณาเลือก \"ประเภทงานซ่อม\" ของรายการที่ " + i);
                    //     return false;
                    // }

                    if (!isValidDateRepairVendor("receivItemDetailAppointmentDateForm" + i, "receivItemDetailAppointmentDateTo" + i)) {
                        modalWaring("กรุณาเลือกกรอก \"วันที่นัดหมายซ่อม\"  ของรายการที่ " + i + " ให้ถูกต้อง");
                        return false;
                    }

                    //สาเหตุวันนัดเกินกำหนด
                    if (isExceedDateRepairVendor("receivItemDetailAppointmentDateForm" + i)) {
                        if (isEmptyInput("ddlReceiveItemDetailOverAppointmentDateReason" + i)) {
                            modalWaring("กรุณาเลือกเลือก \"สาเหตุวันนัดเกินกำหนด\" ของรายการที่ " + i);
                            return false;
                        }
                    }

                    //if (isEmptyInput("txtReceiveItemDetailRemark" + i)) {
                    //    modalWaring("กรุณาเลือกกรอก \"หมายเหตุ\" ของรายการที่ " + i);
                    //    return false;
                    //}
                }

                return true;
            }

            //Validate Extend data
            function validateExtendPanel() {
                if (userRole.toLocaleLowerCase() == 'callcentre') {
                    return true;
                }

                if (isEmptyInput("ExtendedReceiveAppointmentDate")) {
                    modalWaring("กรุณาเลือก: \"วันที่นัดหมายตรวจสอบ(ใหม่)\"");
                    return false;
                }

                if (!isValidExtendDateReceive()) {
                    modalWaring("กรุณาเลือก \"วันที่นัดหมายตรวจสอบ(ใหม่)\" ให้ถูกต้อง");
                    return false;
                }

                //Time validate
                if (isExceedTime("ExtendedReceiveAppointmentTimeForm", "ExtendedReceiveAppointmentTimeTo")) {
                    modalWaring("กรุณากรอก: \"เวลา\" เลื่อนนัดตรวจสอบให้ถูกต้อง");
                    return false;
                }

                return true;
            }

            //Validate Receive data
            function validateReceivePanel() {
                var selectedStatus = $('#ddlReceivedStatusID').val();

                //check Empty date
                if (isEmptyInput("ReceiveAppointmentDate") && selectedStatus == 2) {
                    modalWaring("กรุณากรอก: \"วันที่นัดหมายตรวจสอบ\"");
                    return false;
                }
                //chekcHCRemark-CCRemark
                if (userRole.toLocaleLowerCase().indexOf('hc') >= 0 && ['2', '3', '4', '5'].includes($('#ddlReceivedStatusID').val())) {
                    if (isEmptyInput("ReceiveHCRemark")) {
                        modalWaring("กรุณากรอก: \"หมายเหตุ HC\"");
                        return false;
                    }
                }
                if (userRole.toLocaleLowerCase() == 'callcentre' && ['6', '7'].includes($('#ddlReceivedStatusID').val())) {
                    if (isEmptyInput("ReceiveCCRemark")) {
                        modalWaring("กรุณากรอก: \"หมายเหตุ CC\"");
                        return false;
                    }
                }

                //DateCheck
               // if (isExceedDateReceive() && selectedStatus == 2) {
               //     if (!postponeHistory) {
               //         modalWaring("กรุณาเลือก: \"สาเหตุวันนัดเกินกำหนด\"");
                //        return false;
               //     }
               // }

                //Time validate
                if (isExceedTime("ReceiveAppointmentTimeForm", "ReceiveAppointmentTimeTo") && selectedStatus == 2) {
                    modalWaring("กรุณาเลือก: \"กรุณากรอกเวลาตรวจสอบให้ถูกต้อง\"");
                    return false;
                }

                //console.log("SUCCESS");
                return true;
            }

            function validateSaveCustomerChange() {
                if ($("#flagChangeRequest").prop('checked')) {
                    if (!$("#DdlCustomerCancelQuotationReason").val()) {
                        modalWaring("กรุณาเลือก \"เหตุผล ลูกค้าขอเปลี่ยนแปลง / ยกเลิก\"");
                        return false;
                    }
                }
                return true;
            }

            function validateQuotationItem() {
                if (isEmptyInput("DdlReceiveItemList")) {
                    modalWaring("กรุณาเลือก \"รายการรับแจ้งซ่อม\"");
                    return false;
                }

                if (isEmptyInput("DdlFixingCategories")) {
                    modalWaring("กรุณาเลือก \"หมวดงานที่ ผรม.ต้องซ่อม\"");
                    return false;
                }

                if (isEmptyInput("DdlFixingDetail")) {
                    modalWaring("กรุณาเลือก \"รายละเอียดที่ซ่อม\"");
                    return false;
                }

                if (isEmptyInput("ddlBillingType")) {
                    modalWaring("กรุณาเลือก \"ประเภท การเรียกเก็บ\"");
                    return false;
                }

                if (isEmptyInput("materialQuantity")) {
                    modalWaring("กรุณากรอก \"ปริมาณ\"");
                    return false;
                }

                if ($("#materialQuantity").val() <= 0) {
                    modalWaring("กรุณากรอก \"ปริมาณ ให้มีค่ามากกว่า 0\"");
                    return false;
                }

                if (isEmptyInput("QuotationItemRemark")) {
                    modalWaring("กรุณากรอก \"หมายเหตุ\"");
                    return false;
                }

                if ($("#TotalPrice").text() <= 0) {
                    modalWaring("ราคารวมต้องมีค่ามากกว่า 0");
                    return false;
                }
                return true;
            }

            function validateUploadImageBefore() {
                if (!receiveData.receiveAppointmentDateReal) {
                    modalWaring('กรุณาบันทึกวันเข้าตรวจสอบจริง ก่อนแนบรูปถ่าย');
                    return false;
                }
                return true;
            }

            function validateApproveQuotation() {
                if (quotationCostList.data.length <= 0) {
                    modalWaring('กรุณาบันทึกราคาของใบเสนอราคาก่อนยื่นขออนุมัติ');
                    return false;
                }
                return true;
            }

            function validateInsertQuotationCostItem() {
                if ($("#totalPriceNet").text() <= 0) {
                    return false;
                }
                return true;
            }

            function validateCloseReceiveReason() {
                if ($('#confirmReason').val() == '') {
                    modalWaring('กรุณากรอกเหตุผลการปิดใบรับเรื่อง');
                    $('#warningModal').on('hidden.bs.modal', () => {
                        $('#confirmReason').focus();
                        $('#warningModal').off();
                    })
                    return false
                }
                return true;
            }
            function validateRejectQuotation() {
                if ($('#confirmReason').val() == '') {
                    modalWaring('กรุณากรอกเหตุผลการไม่อนุมัติ');
                    return false
                }
                return true;
            }
            function validateflagChangeRequest() {
                if (lastQuotationGoods.paymentStatusId == 2) {
                    modalWaring('ไม่สามารถเปลี่ยนแปลง/ยกเลิกได้ เนื่องจากลูกค้าชำระค่าบริการเรียบร้อยแล้ว');
                    return false;
                }
                return true;
            }
            function validateInsertReceiveItem(data) {
                if (!data.find('#ddlGuaranteeID').val()) {
                    modalWaring('กรุณาเลือกหมวงงานก่อนทำการบันทึกข้อมูล');
                    $('#warningModal').on('hidden.bs.modal', () => {
                        data.find('#ddlGuaranteeID').focus();
                        $('#warningModal').off();
                    })
                    return false;
                }
                if (data.find('#txtItemDetail').val() == '') {
                    modalWaring('กรุณากรอกรายละเอียดการแจ้งก่อนทำการบันทึกข้อมูล');
                    $('#warningModal').on('hidden.bs.modal', () => {
                        data.find('#txtItemDetail').focus();
                        $('#warningModal').off();
                    })
                    return false;
                }
                return true;
            }
            //-------------------------------------- Sub validate methods -------------------------------------------//

            function isExceedDateReceive() {

                var selectedDate = new Date(reorganizeDateFormat($("#ReceiveAppointmentDate").val()));
                var receiveHcDate = new Date();

                if (selectedDate == NaN) {
                    return false;
                }

                //If more than 1 day (2 or more): return true
                if (((selectedDate - receiveHcDate) / 8.64e+7) < 2) {
                    return false;
                }
                return true;
            }

            function isValidExtendDateReceive() {
                //var selectedDate = new Date(reorganizeDateFormat($("#ReceiveAppointmentDate").val()));
                var selectedDate = new Date();
                var extendedDate = new Date(reorganizeDateFormat($("#ExtendedReceiveAppointmentDate").val()));

                //check date
                if (selectedDate < extendedDate) {
                    return true;
                }
                if (selectedDate < extendedDate)
                    return false;
            }

            function isValidDateRepairVendor(from, to) {
                if (isEmptyInput(from) || isEmptyInput(to)) {
                    return false;
                }

                var fromDate = new Date(reorganizeDateFormat($("#" + from).val()));
                var toDate = new Date(reorganizeDateFormat($("#" + to).val()));

                //check date
                if (fromDate > toDate) {
                    return false;
                }
                return true;
            }

            function isExceedDateRepairVendor(from) {
                if (isEmptyInput(from)) {
                    return false;
                }

                var fromDate = new Date(reorganizeDateFormat($("#" + from).val()));
                var receiveDateReal = new Date(receiveData.receiveAppointmentDateReal)

                //check date
                if (((fromDate - receiveDateReal) / 8.64e+7) < 5) {
                    return false;
                }
                return true;
            }

            //-------------------------------------- Mostly use validate function -------------------------------------------//
            function isEmptyInput(inputId) {
                if (!$("#" + inputId).val()) {
                    return true;
                }
                return false
            }

            function isExceedTime(timeFrom, timeTo) {
                //check empty
                if (isEmptyInput(timeFrom) || isEmptyInput(timeTo)) {
                    return true;
                }
                //check exceed time
                if ($("#" + timeFrom).val() >= $("#" + timeTo).val()) {
                    return true;
                }
                return false;
            }

            //Update Email
            function changeEmailPanel() {
                if ($("#editEmailInput").is(":visible")) {
                    $("#editEmailInput").hide();
                    $("#editEmailSave").hide();
                    $("#editEmailButton").show();
                    $("#editEmailCancelButton").hide();
                    $("#editEmailInput").val("");
                    $("#CustomerEmail").show();
                }
                else {
                    $("#editEmailInput").show();
                    $("#editEmailSave").show();
                    $("#editEmailButton").hide();
                    $("#editEmailCancelButton").show();
                    $("#editEmailInput").val($("#CustomerEmail").text());
                    $("#CustomerEmail").hide();
                }
            }

            async function cancelHomeCardRequest() 
            {
                if (!validateReceivePanel() ) 
                {
                    return false;
                }
                var success = true;
                var refresh = false;
                var body = {
                    userId: userID,
                    homeCardRequestId: HomeCardrequestID,
                    requestStatusId: 8, //HC_TM_Lookup WHERE [Type_ID] = 33 AND [Value] = 8
                    cancelremark: $('#confirmReason').val()
                };
      
                var updated = await ajaxPut(`${baseUrl}/homecare/api/v1/HomeCard/` + unitCode + "/Request", body);
                if (!updated) 
                {
                    success = false;
                    modalWaring("fail");
                } 
                else 
                {
                    //--- ** Check Update Optional Package :
                    if ($("#hdfhomcardrequest_optpack_id").val != '0' || $("#hdfhomcardrequest_optpack_id").val != '') {
                        var body_optpack = {
                            home_card_request_id: $('#hdfhomcardrequest_optpack_id').val(), 
                            extra_package_id: $('#hdfhomcardrequest_extra_package_id').val(),
                            updated_by: username,
                            remark: $('#confirmReason').val()
                        };

                        var updated_optpack = await ajaxPut(`${baseUrl}/homecare/api/v1/HomeCard/` + unitCode + "/RequestCancelOptionalPack", body_optpack);
                        if (updated_optpack) {
                            refresh = true;
                            modalSuccess("success");
                        } else {
                            success = false;
                            modalWaring("fail");
                        }

                    } else {
                        refresh = true;
                        modalSuccess("success");
                    }
                           
                }

                if (success) 
                {
                    modalSuccess();
                    if (refresh) 
                    {
                        $('#notificationModal').on('hidden.bs.modal', () => {
                            page.loading();
                            window.location.reload(true);
                        });
                    }
                    page.loaded();
                }     
            }