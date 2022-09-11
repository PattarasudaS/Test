async function bindEnddateProject(ele) {
	
	if ($('#worksheetCloseConfirm').prop('checked')) {
		$('#worksheetCloseConfirm').prop('checked', 0);
		// Check no item inProceed return false
		if (worksheetItemList.data.filter(w => w.statusId == 2).length == 0) {
			modalWaring('กรุณาทำรายการอย่างน้อยหนึ่งรายการก่อนทำการปิดใบงาน');
			return false;
		}
		// Check item on Proceed is not finish
		var tempWorksheetItem = worksheetItemList.data.filter(w => w.statusId == 2 && !w.finishDate);
		if (tempWorksheetItem.length) {
			modalWaring('กรุณากรอกข้อมูลใบงานรายการที่ ' + tempWorksheetItem[0].worksheetNo + ' ให้ครบถ้วนก่อนทำการปิดใบงาน');
			return false;
		}
		if ($("[name*=AprrovedType]:checked").val() === undefined) {
			modalWaring('กรุณาเลือกประเภทการอนุมัติ');
			return false;
		}
		console.log($('#PO_No2').val());
		if ($("[name*=AprrovedType]:checked").val() === 'Y') {
			if ($('#PO_No2').val() === null) {
				modalWaring('กรุณาตรวจสอบใบเลขที่ PO ให้เรียบร้อยก่อนทำการปิดใบงาน');
				return false;
			}
        }

		if ($("[id *= PaymentType]").val() == 0) {

			modalWarning("กรุณาเลือกรายละเอียดการอนุมัติ");
			return;
		}
		
		// Check closeJob with API
		var checkMessage = await ajaxGet(`${baseUrl}/homecare/api/v1/Worksheet/CheckCloseJob/${worksheetId}`);
		if (checkMessage.data.error) {
			modalWaring(checkMessage.data.error);
			return false;
		}
		$('#worksheetCloseConfirm').prop('checked', 1);
		bindTodayDate(ele);
	} else {
		bindTodayDate(ele);
	}
}