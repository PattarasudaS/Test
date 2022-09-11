using HomeCare_4_0.ClassLib;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HomeCare_4_0.Common
{
    public class EmailContent
    {

        public string GetMailContent(cEmailContent EmailContent)
        {

            string strContent = string.Empty;

            strContent = strContent +
            // "<meta http-equiv='Content-Type' content='text/html;charset=utf-8' />" +
            "<style type='text/css'>" +
            ".style1{" +
            "width: 15%;" +
            "text-align:right;}" +
            ".style2{" +
            "width: 25%; }" +
            ".style3{" +
            "width: 7%;" +
            "text-align:right;  }" +
            ".style4{" +
            "width: 25%;}" +
            ".style5{" +
            "width: 25%;}" +
            "</style>" +
            "<div style='font-size: 12px;'>" +
            "&nbsp; คุณสามารถตรวจสอบข้อมูลได้ที่ <a href='http://sansiriapp4.sansiri.com/HomeCare/Received/ReceivedDetail/" + EmailContent.receiveDetail.Receive_ID + "'>HomeCare</a> (คลิ๊กเพื่อไปใบรับเรื่องดังกล่าวหากยัง login ค้างอยู่)</td>" +
            "<br><br>" +
            "<table style='width:100%;border: 1px solid #666666;' >" +
            "<tr><td class='style1' style='font-size: 12px;' align='right'><b>ชื่อ-นามสกุลลูกค้า : </b></td>" +
            "<td class='style2' style='font-size: 12px;'>" + EmailContent.unitInfo.FullNameTH_1 + "</td>" +
            "<td class='style3'>&nbsp;</td>" +
            "<td class='style4'>&nbsp;</td>" +
            "<td>&nbsp;</td>" +
            "<td class='style5'>&nbsp;</td>" +
            "</tr>" +
            "<tr>" +
            "<td class='style1' style='font-size: 12px;'><b>โครงการ : </b></td>" +
            "<td style='font-size: 14px;'>" + EmailContent.unitInfo.Project_NameTH.Trim() + "</td>" +
            "<td class='style3' style='font-size: 12px;'><b>ยูนิต : </b></td>" +
            "<td class='style4' style='font-size: 12px;'>" + EmailContent.unitInfo.Unit_Code.Trim() + "</td>" +
            "<td class='style3' style='font-size: 12px;'><b>เลขที่บ้าน : </b></td>" +
            "<td style='font-size: 12px;'> &nbsp;" + EmailContent.unitInfo.Unit_Address.Trim() + "</td>" +
            "</tr>" +
            "<tr>" +
            "<td class='style1' style='font-size: 12px;'><b>วันที่รับแจ้ง : </b></td>" +
            "<td style='font-size: 12px;'>" + EmailContent.receiveDetail.Inform_Date + "</td>" +
            "<td class='style3' style='font-size: 12px;'><b>ผู้รับแจ้ง : </b></td>" +
            "<td class='style4' style='font-size: 12px;'>" + (string.IsNullOrEmpty(EmailContent.receiveDetail.Inform_User) ? "-" : EmailContent.receiveDetail.Inform_User.Trim()) + "</td>" +
            "<td>&nbsp;</td><td>&nbsp;</td></tr>";
            if (EmailContent.Type == EnumConfiguration.EmailContentType.RecievedHC.ToString() || EmailContent.Type == EnumConfiguration.EmailContentType.ConfirmRecieve.ToString())
            {
                strContent = strContent + "<tr>" +
                "<td class='style3' style='font-size: 12px;'><b>วันที่รับเรื่อง : </b></td>" +
                "<td class='style4' style='font-size: 12px;'>" + (string.IsNullOrEmpty(EmailContent.receiveDetail.Receive_HC_Date) ? "-" : EmailContent.receiveDetail.Receive_HC_Date.Trim()) + "</td>" +
                "<td class='style3' style='font-size: 12px;'><b>ผู้รับเรื่อง  : </b></td>" +
                "<td class='style4' style='font-size: 12px;'>" + (string.IsNullOrEmpty(EmailContent.receiveDetail.Receive_HC_User) ? "-" : EmailContent.receiveDetail.Receive_HC_User.Trim()) + "</td>";


                if (EmailContent.receiveDetail.Receive_Status_ID == 2)
                {
                    strContent = strContent + "<td class='style3' style='font-size: 12px;'><b>สถานะรับเรื่อง : </b></td>" +
                    "<td class='style4' style='font-size: 12px; '>" + (string.IsNullOrEmpty(EmailContent.receiveDetail.Receive_Status_Name) ? "-" : EmailContent.receiveDetail.Receive_Status_Name.Trim()) + "</td>";
                }
                else
                {
                    strContent = strContent + "<td class='style3' style='font-size: 12px; color:red; '><b>สถานะรับเรื่อง : </b></td>" +
                    "<td class='style4' style='font-size: 12px; color:red; '>" + (string.IsNullOrEmpty(EmailContent.receiveDetail.Receive_Status_Name) ? "-" : EmailContent.receiveDetail.Receive_Status_Name.Trim()) + "</td>";
                }


                strContent = strContent + "</tr>";
            }
            strContent = strContent + "<tr>" +
            "<td class='style1' style='font-size: 12px;'><b>เลขที่ใบรับเรื่อง : </b></td>" +
            "<td style='font-size: 12px;'>" + EmailContent.receiveDetail.Receive_JobNoText + "</td>" +
            "<td class='style3' style='font-size: 12px;'><b>หมายเหตุ HC  : </b></td>" +
            "<td class='style4' colspan='3' style='font-size: 12px;'>" + EmailContent.receiveDetail.Receive_HC_Remark+ "</td>" +
            "</tr>";

            strContent = strContent +
            "<tr>" +
            "<td class='style1'>&nbsp;</td>" +
            "<td>&nbsp;</td>" +
            "<td>&nbsp;</td>" +
            "<td class='style4'>&nbsp;</td>" +
            "<td>&nbsp;</td>" +
            "<td>&nbsp;</td>" +
            "</tr>" +
            "<tr>";


            string strOwner = string.Empty;
            if (EmailContent.customerContract != null)
            {
                strOwner = "ชื่อ : " + EmailContent.unitInfo.FullNameTH_1.Trim() + "<br>" +
                                     "ที่อยู่ที่จัดส่งเอกสาร : " + EmailContent.customerContract.ProspectAddress.Trim() + "<br>" +
                                     "บุคคลที่ติดต่อ : " + EmailContent.customerContract.contactPerson.Trim() + "<br>" +
                                     "โทรศัพท์บุคคลที่ติดต่อ : " + EmailContent.customerContract.contactPhone.Trim() + "<br>" +
                                     "โทรศัพท์ : " + EmailContent.customerContract.ProspectTelephone.Trim();
                strOwner.Replace("  ", " ");
            }

            string Inform_CustomerName = string.IsNullOrEmpty(EmailContent.receiveDetail.Inform_CustomerName) ? "-" : EmailContent.receiveDetail.Inform_CustomerName.Trim();
            string Inform_CustomerTel = string.IsNullOrEmpty(EmailContent.receiveDetail.Inform_CustomerTel) ? "-" : EmailContent.receiveDetail.Inform_CustomerTel.Trim();
            string Inform_Remark = string.IsNullOrEmpty(EmailContent.receiveDetail.Inform_Remark) ? "-" : EmailContent.receiveDetail.Inform_Remark;


            strContent = strContent +
                "<td class='style1' valign='top' style='font-size: 12px;'><b>รายละเอียดเจ้าของแปลง : </b></td>" +
                "<td colspan='5' style='font-size: 12px;'>" + strOwner + "</td>" +
                "</tr>" +
                "<tr>" +
                "<td class='style1' style='font-size: 12px;'><b>สถานะผู้แจ้ง : </b></td>" +
                "<td style='font-size: 14px;'>" + EmailContent.receiveDetail.Inform_CustomerType_Name.Trim() + "</td>" +
                "<td class='style3' style='font-size: 12px;'><b>ชื่อ : </b></td>" +
                "<td class='style4' style='font-size: 12px;'>" + Inform_CustomerName + "</td>" +
                "<td class='style3' style='font-size: 12px;'><b>เบอร์โทร : </b></td>" +
                "<td style='font-size: 12px;'> &nbsp;" + Inform_CustomerTel + "</td>" +
                "</tr>" +
                "<tr>" +
                "<td class='style1' style='font-size: 12px;'><b>หมายเหตุในใบรับเรื่อง : </b></td>" +
                "<td style='font-size: 12px;'>" + Inform_Remark + "</td>" +
                "<td>&nbsp;</td>" +
                "<td class='style4'>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "<tr>" +
                "<td class='style1'>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "<td class='style4'>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "<td>&nbsp;</td>" +
                "</tr>" +
                "</table>" +
                "<br/>";

            strContent = strContent +
            "<table align='center' style='width:100%; border: 1px solid #666666; border-color:#666666;' border=1 cellpadding=1 cellspacing=1 >" +
            "<tr bgcolor='#B7CBEB'>" +
            "<td width='7%' align='center' style='font-size: 12px;'><b>No</b></td>" +
            "<td width='25%' align='center' style='font-size: 12px;'><b>หมวดประกัน</b></td>" +
            "<td width='20%' align='center' style='font-size: 12px;'><b>รายละเอียดการแจ้ง</b></td>" +
            "</tr>";
            int row = 1;
            foreach (var item in EmailContent.receiveItem)
            {
                string item_Detai = string.IsNullOrEmpty(item.Detail) ? "" : item.Detail;

                strContent = strContent +
                        "<tr>" +
                        "<td align='center' style='font-size: 12px;'>&nbsp;" + row + "</td>" +
                        "<td style='font-size: 12px;'>&nbsp;" + item.GuaranteDescription + "</td>" +
                        "<td style='font-size: 12px;'>&nbsp;" + item_Detai + "</td>" +
                        "</tr>";
                row++;

            }

            strContent = strContent +
            "</table>" +
            "</div>";




            return strContent;
        }


        public string GetMailContentAppv(cEmailApprove objEmailApprove)
        {
            string closeJob = string.Empty;

            if (string.IsNullOrEmpty(objEmailApprove.worksheet.WorkSheet_CloseJobDate))
            {
                closeJob = "ปิดเรื่อง";
            }
            else
            {
                closeJob = "ยังไม่ปิดเรื่อง";
            }



            string strMailContent = string.Empty;
            strMailContent = "<div style='font-size: 12px;'>" +
           "&nbsp; คุณสามารถตรวจสอบข้อมูลได้ที่ <a href='http://sansiriapp4.sansiri.com/HomeCare/Worksheet/Worksheetdetail/" + objEmailApprove.worksheet.WorkSheet_ID + "'>HomeCare</a> (คลิ๊กเพื่อไปใบงานดังกล่าวหากยัง login ค้างอยู่)</td>" +
           "</div><br><br>" +
           "<table style='width:800px;'>" +
           "<tr><td>" +
           "<table style='border: 1px solid #666666;'>" +
           "<tr><td class='style3' colspan='0' rowspan='0'>" +
           "<table style='width:800px;' cellpadding='0' cellspacing='0'>" +
           "<tr ><td  valign=top bgcolor='#dae3ef' style='font-size: 12px;'>&nbsp;&nbsp;<b>อนุมัติใบงาน</b></td></tr>" +
           "<tr><td>" +
           "<table style='width:800px;'>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>เลขที่ใบงาน :</b></td>" +
           "<td class='style3' style='font-size: 12px;'>" + objEmailApprove.worksheet.WorkSheet_JobNoText.Trim() + "</td>" +
           "<td class='style1' style='font-size: 12px;'><b>เลขที่ใบอนุมัติ</b>&nbsp; :</td>" +
           "<td style='font-size: 12px;'>" + objEmailApprove.approve.Approve_JobNoText.ToString().Trim() + "</td>" +
           "<td  class='style1'>&nbsp;</td>" +
           "<td>&nbsp;</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>ชื่อ-นามสกุล ลูกค้า : </b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.FullNameTH_1.ToString().Trim() + "</td>" +
           "<td>&nbsp;</td>" +
           "<td>&nbsp;</td>" +
           "<td  class='style1' style='font-size: 12px;'><b>สถานะการปิดเรื่อง :</b></td>" +
           "<td style='font-size: 12px;'>" + closeJob + "</td>" +
           "</tr><tr>" +
           "<td class='style1' style='font-size: 12px;'><b>โครงการ : </b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.Project_NameTH.Trim() + "</td>" +
           "<td class='style1' style='font-size: 12px;'><b>ยูนิต :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.Unit_Code.Trim() + "</td>" +
           "<td  class='style1' style='font-size: 12px;'><b>เลขที่บ้าน :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.Unit_Code_Address.Trim() + "</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>วันที่รับเรื่อง :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.receiveDetail.Inform_Date + "</td>" +
           "<td class='style1' style='font-size: 12px;'><b>ผู้รับเรื่อง : </b></td>" +
           "<td style='font-size: 12px;'>" + objEmailApprove.receiveDetail.Inform_User + "</td>" +
           "<td class='style1' style='font-size: 12px;'><b>&nbsp;</b></td>" +
           "<td style='font-size: 12px;'>&nbsp;</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>วันที่อนุมัติเป็นใบแจ้งซ่อม :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.approve.Approve_CreatedDate + "</td>" +
           "<td>&nbsp;</td>" +
           "<td>&nbsp;</td>" +
           "<td  style='text-align: right'>&nbsp;</td>" +
           "<td>&nbsp;</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>วันเริ่มต้นประกัน :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.StartGuaranteeDate.Trim() + "</td>" +
           "<td class='style1' style='font-size: 12px;'><b>วันที่หมดประกัน :</b></td>" +
           "<td style='font-size: 12px;' >" + objEmailApprove.customerInfo.EndGuaranteeDate.Trim() + "</td>" +
           "<td  style='text-align: right'>&nbsp;</td>" +
           "<td>&nbsp;</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' valign=top style='font-size: 12px;'><b>ผู้รับเหมา : </b></td>" +
           "<td  colspan='5' style='font-size: 12px;'>" + objEmailApprove.worksheet.VendorName.Trim() + "</td>" +
           "</tr>" +
           "<tr>" +
           "<td class='style1' style='font-size: 12px;'><b>รวมราคาที่อนุมัติ :</b></td>" +
           "<td  colspan='5' style='font-size: 12px;'>" + objEmailApprove.worksheet.NetPrice + " บาท</td>" +
           "</tr>" +
           "<tr>" +
           "<td>&nbsp;</td>" +
           "<td  colspan='5'>&nbsp;</td>" +
           "</tr>" +
           "<tr>" +
           "<td>&nbsp;&nbsp; </td>";
            if (objEmailApprove.flagFinal)
            {
                strMailContent = strMailContent +
           "<td colspan='5'><span style='font-size: 12px;'><b>ประเภทการอนุมัติ :</b>&nbsp;" + (objEmailApprove.approve.PaymentType == "N" ? "ไม่มีค่าใช้จ่าย" : "มีค่าใช้จ่าย") + "</span>&nbsp;&nbsp;&nbsp;&nbsp; <span style='font-size: 14px;'><b>Status :</b>&nbsp;" + objEmailApprove.approve.NextRole + "&nbsp;&nbsp;" + (objEmailApprove.flagApp == true ? "อนุมัติ" : "ไม่อนุมัติ") + " " + "</span></td>";
            }
            else
            {
                strMailContent = strMailContent +
           "<td colspan='5'><span style='font-size: 12px;'><b>ประเภทการอนุมัติ :</b>&nbsp;" + (objEmailApprove.approve.PaymentType == "N" ? "ไม่มีค่าใช้จ่าย" : "มีค่าใช้จ่าย") + "</span>&nbsp;&nbsp;&nbsp;&nbsp; <span style='font-size: 14px;'><b>Status :</b>&nbsp;รออนุมัติจาก : " + objEmailApprove.approve.NextRole + "</span></td>";
            }


            strMailContent = strMailContent +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" +
            "</table>" +
            "</td></tr><tr><td>&nbsp;</td></tr>" +
            "<tr>" +
            "<td>";

            strMailContent = string.Concat(strMailContent, GetMailContentAppvItem(objEmailApprove.listWorksheetDetail));

            var total_OperatingPercent = ((objEmailApprove.worksheet.TotalPrice) * ((100 + objEmailApprove.worksheet.OperatingPercent) / 100));


            strMailContent = strMailContent +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td>" +
            "<br>" +
            "<table style='width:800px; border: 1px solid #666666;'>" +
            "<tr><td  align=right style='font-size: 12px;'>รวมราคาตามรายการ :</td>" +
            "<td style='font-size: 12px;'>" + objEmailApprove.worksheet.TotalPrice.ToString("#,##0.00") + "</asp:TextBox>&nbsp;บาท</td>" +
            "<td  align=right style='font-size: 14px;'>Vat :</td>" +
            "<td style='font-size: 12px;'>" + objEmailApprove.worksheet.VatPercent + "&nbsp;%&nbsp;&nbsp;เป็นเงิน" + ((total_OperatingPercent * objEmailApprove.worksheet.VatPercent) / 100).ToString("#,##0.00") + "&nbsp;บาท</td>" +
            "</tr><tr>" +
            "<td  align=right style='font-size: 12px;'>รวมค่าดำเนินการ : </td>" +
            "<td style='font-size: 12px;'>" + objEmailApprove.worksheet.OperatingPercent + "&nbsp;%&nbsp;&nbsp;เป็นเงิน" + ((objEmailApprove.worksheet.TotalPrice * objEmailApprove.worksheet.OperatingPercent) / 100).ToString("#,##0.00") + "&nbsp;บาท&nbsp;&nbsp;</td>" +
            "<td  align=right style='font-size: 12px;'>ราคาสุทธิ : </td>" +
            "<td style='font-size: 12px;'>" + @objEmailApprove.worksheet.NetPrice.ToString("#,##0.00") + "&nbsp;บาท</td>" +
            "</tr><tr>" +
            "<td align=rightstyle='font-size: 12px;'> รวมเป็นเงิน :</td>" +
            "<td style='font-size: 12px;'>" + @objEmailApprove.worksheet.NetPrice.ToString("#,##0.00") + "&nbsp;บาท</td>" +
            "<td  style='text-align: right'> &nbsp;</td>" +
            "<td> &nbsp;</td></tr></table>" +
            "</td>" +
            "</tr>" +
            "</table>";


            return strMailContent;

        }
        public string GetMailContentAppvItem(List<HC_SP_Get_TD_Approve_Item_Detail_Result> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" <table class='' id='tbApproveitemDetail' style='margin: 8px 8px 0px 8px; width:800px; border: 1px solid #666666;'>");
            sb.AppendLine("<thead style='font-size:12px;background-color:#dae3ef'>");
            sb.AppendLine("<tr class=''>");
            sb.AppendLine("<th style='text-align:center'>No.</th>");
            sb.AppendLine("<th style='text-align:center'>หมวดราคางาน HC </th>");
            sb.AppendLine("<th style='text-align:center'>ลักษณะปัญหา </th> ");
            sb.AppendLine("<th style='text-align:center'>ลักษณะปัญหาอื่น</th>");
            sb.AppendLine("<th style='text-align:center'>รายละเอียด</th>");
            sb.AppendLine("<th style='text-align:center'>ราคาต่อหน่วย</th>");
            sb.AppendLine("<th style='text-align:center'>ราคาพิเศษต่อหน่วย</th>");
            sb.AppendLine("<th style='text-align:center'>ปริมาณ</th>");
            sb.AppendLine("<th style='text-align:center'>หน่วย</th>");
            sb.AppendLine("<th style='text-align:center'>รวมเป็นเงิน</th>");
            sb.AppendLine("<th style='text-align:center'>หมายเหตุ</th>");
            sb.AppendLine("<th style='text-align:center'>หมายเหตุการอนุมัติ</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody style='font-size:12px'>");

            int row = 0;
            //Loop
            foreach (var item in list)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='text-align:center'><label id='lblNo' name='No'>" + (row + 1) + "</label></td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_List1Name + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_List2Name + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_List3Name + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_List4Name + "</td>");
                sb.AppendLine("<td style='text-align:right'>" + item.AppvItem_StandardPrice + "</td>");
                sb.AppendLine("<td style='text-align:right'>" + item.AppvItem_SpecialPrice + "</td>");
                sb.AppendLine("<td style='text-align:right'>" + item.AppvItem_Quantity + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_Unit + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_Price + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_Remark + "</td>");
                sb.AppendLine("<td style='text-align:left'>" + item.AppvItem_Remark_Approve + "</td>");
                row++;
                sb.AppendLine("</tr>");

            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }


        public string GetMailContentToSupport(cEmailSupport EmailSupport)
        {
            string WOID = string.Empty;
            int i= 0;
            foreach (var item in EmailSupport.DocumentItem)
            {
                if(i==0)
                {
                    WOID = item.WorkSheetID.ToString();
                }
                else
                {
                    WOID += ","+item.WorkSheetID.ToString();
                }
                i++;
            }

            string strMailContent = string.Empty;
            strMailContent = "<style>" +
            ".style1{text-align: right;width: 15%;}" +
            ".style2{text-align:left;width: 20%;}" +
            ".style3{text-align:left;}" +
            "</style>" +
            "<div style='font-size: 12px;'>" +
            "&nbsp; คุณสามารถตรวจสอบข้อมูลได้ที่ <a href='http://sansiriapp4.sansiri.com/HomeCare'>http://sansiriapp4.sansiri.com/HomeCare</a>" +
            "</div><br><br>" +
            "<table style='width:100%;'>" +
            "<tr><td>" +
            "<table style='border: 1px solid #666666; '>" +
            "<tr><td class='style3' colspan='0' rowspan='0'>" +
            "<table style='width: 1200px' cellpadding='0' cellspacing='0'>" +
            "<tr ><td  valign=top bgcolor='#dae3ef' style='font-size: 12px;'>&nbsp;&nbsp;<b>แจ้ง" + EmailSupport.FormType + "</b></td></tr>" +
            "<tr><td>" +
            "<table style='width:100%;'>" +
            "<tr>" +
            "<td class='style1' style='font-size: 12px;'><b>แจ้งโดย :</b></td>" +
            "<td class='style3' style='font-size: 12px;'>" + UserDetail.Fullname + "</td>" +
            "<td class='style1' style='font-size: 12px;'><b>สิทธิ์ :</b></td>" +
            "<td style='font-size: 12px;'>" + UserDetail.Role + "</td>" +
            "<td  class='style1'>&nbsp;</td>" +
            "<td >&nbsp;</td>" +
            "</tr>" +
            "<tr>" +
            "<td class='style1' style='font-size: 12px;'><b>โครงการ : </b></td>" +
            "<td style='font-size: 12px;' >" + EmailSupport.DocumentItem.FirstOrDefault().ProjectName + "</td>" +
            "<td class='style1' style='font-size: 12px;'><b>จำนวน :</b></td>" +
            "<td style='font-size: 12px;' >" + EmailSupport.DocumentItem.Count + " รายการ</td>" +
            "<td  class='style1' style='font-size: 12px;'></td>" +
            "<td style='font-size: 12px;' ></td>" +
            "</tr>" +
            "<td class='style1' style='font-size: 12px;'><b>รายการเลขใบงานคือ :</b></td>" +
            "<td  colspan='5' style='font-size: 12px;'>[ " + WOID + " ]</td>" +
            "</tr>" +
            "<tr>" +
            "<td >&nbsp;</td>" +
            "<td  colspan='5'>&nbsp;</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" +
            "</table>" +
            "</td>" +
            "</tr>" +
            "</table>" +
            "</td></tr><tr><td>&nbsp;</td></tr>" +
            "<tr>" +
            "<td>";

            strMailContent = strMailContent + GetMailContentToSupportItem(EmailSupport) + "</table>";
            strMailContent = strMailContent +
            "</td>" +
            "</tr>" +
            "</table>";
            return strMailContent;
        }
        public string GetMailContentToSupportItem(cEmailSupport EmailSupport)
        {


            string strSql2 = string.Empty;
            string strRow = string.Empty;


            if (EmailSupport.DocumentItem.Count > 0)
            {

                strRow = strRow +
                    "<table width=100% border=1 cellpadding=1 cellspacing=1 style='border: 1px solid #666666; border-color:#666666 '>" +
                    "<tr>" +
                    "<td>" +
                    "<table style='width:100%;'>" +
                    "<tr bgcolor='#dae3ef'>" +
                    "<td style='font-size: 12px; text-align:center'>ลำดับที่</td>" +
                    "<td style='font-size: 12px; text-align:center'>โครงการ</td>" +
                    "<td style='font-size: 12px; text-align:center'>บ้านเลขที่</td>" +
                    "<td style='font-size: 12px; text-align:center'>แปลง</td>" +
                    "<td style='font-size: 12px; text-align:center'>ชื่อ - นามสกุล</td>" +
                    "<td style='font-size: 12px; text-align:center'>เลขที่ใบงาน</td>" +
                    "<td style='font-size: 12px; text-align:center'>วันที่ใบงาน</td>" +
                    "<td style='font-size: 12px; text-align:center'>ผู้รับเหมาใบงาน</td>" +
                    "<td style='font-size: 12px; text-align:center'>ผู้รับเหมาสำหรับพิมพ์ F2 </td>" +
                    " <td style='font-size: 12px; text-align:center'>สถานะใบงาน</td>" +
                    "</tr>";
                int row = 1;
                foreach (var item in EmailSupport.DocumentItem)
                {
                    
                    strRow = strRow +
                    "<tr>" +
                    "<td style='font-size: 12px;'>&nbsp;" + row+ "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.ProjectName)? item.ProjectName.Trim().ToString():string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.UnitAddress) ? item.UnitAddress.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.UnitCode) ? item.UnitCode.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.CustomerName) ? item.CustomerName.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.WorkSheetJobNo) ? item.WorkSheetJobNo.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.WorkSheetCreateDate) ? item.WorkSheetCreateDate.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.WorkSheetVendorName) ? item.WorkSheetVendorName.Trim().ToString() : string.Empty) + "</td>" +
                    "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(EmailSupport.VendorNameForF2) ? EmailSupport.VendorNameForF2.Trim().ToString() : string.Empty) + "</td>" +
                   "<td style='font-size: 12px;'>&nbsp;" + (!string.IsNullOrEmpty(item.WorkSheet_Status) ? item.WorkSheet_Status.Trim().ToString() : string.Empty) + "</td>" + 
                    "</tr>";
                    row++;
                }

                strRow = strRow +
                        "<tr>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                        "</tr>" +
                        "</table></td></tr></table><br>";
            }

            return strRow;
        }


        public string GetMailContentWorkSheet(cEmailWorkSheet EmailWorkSheet)
        {

            string strMailContent = string.Empty;
            strMailContent = "<table>" +
            "<tr style='font-size: 12px; font-family:Tahoma;'>" +
            "   <td>คุณสามารถตรวจสอบข้อมูลได้ที่ <a href='http://sansiriapp4.sansiri.com/HomeCare/Worksheet/Worksheetdetail/" + EmailWorkSheet.worksheet.WorkSheet_ID+"'>HomeCare</a></td>" +
            "</tr>" +
            "<tr><td>&nbsp;</td></tr>" +
            "<tr>" +
            "   <td>" +
            "       <table style='width:800px; border: 1px solid #666666; font-size: 12px; font-family:Tahoma;'>" +
            "       <tr><td valign=top bgcolor='#FF7766' colspan='4'><b>ข้อมูลใบงาน</b></td></tr>" +
            "       <tr><td>" +
            "       <tr>" +
            "           <td><b>เลขที่ใบงาน :</b></td>" +
            "           <td>" + EmailWorkSheet.worksheet.WorkSheet_JobNoText.Trim() + "</td>" +
            "           <td><b>สถานะใบงาน :</b></td>" +
            "           <td style='color:blue;'>" + EmailWorkSheet.worksheet.WorkSheet_Status.Trim() + "</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>ชื่อ-นามสกุล ลูกค้า :</b></td>" +
            "           <td colspan='3'>" + EmailWorkSheet.customerInfo.FullNameTH_1.ToString().Trim() + "</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>โครงการ :</b></td>" +
            "           <td>" + EmailWorkSheet.customerInfo.Project_NameTH.Trim() + "</td>" +
            "           <td><b>ยูนิต :</b></td>" +
            "           <td>" + EmailWorkSheet.customerInfo.Unit_Code_Address.Trim() + "</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>วันที่รับแจ้ง :</b></td>" +
            "           <td>" + EmailWorkSheet.receiveDetail.Inform_Date + "</td>" +
            "           <td><b>ผู้รับแจ้ง :</b></td>" +
            "           <td>" + EmailWorkSheet.receiveDetail.Inform_User + "</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>วันเริ่มต้นประกัน :</b></td>" +
            "           <td>" + EmailWorkSheet.customerInfo.StartGuaranteeDate.Trim() + "</td>" +
            "           <td><b>วันที่หมดประกัน :</b></td>" +
            "           <td>" + EmailWorkSheet.customerInfo.EndGuaranteeDate.Trim() + "</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>วันขยายประกัน :</b></td>" +
            "           <td>" + EmailWorkSheet.customerInfo.ExtraGuaranteeDate + "</td>" +
            "           <td>&nbsp;</td>" +
            "           <td>&nbsp;</td>" +
            "       </tr>" +
            "       <tr>" +
            "           <td><b>ผู้รับเหมา : </b></td>" +
            "           <td colspan='3'>" + EmailWorkSheet.worksheet.VendorName.Trim() + "</td>" +
            "       </tr>" +
            "       </table>" +
            "   </td>" +
            "</tr>" +
            "<tr><td>&nbsp;</td></tr>" +
            "<tr>" +
            "   <td>";

            strMailContent = string.Concat(strMailContent, GetMailContentWorkSheetItem(EmailWorkSheet.worksheetItem));

            strMailContent = strMailContent +
            "   </td>" +
            "</tr>" +
            "</table>";

            return strMailContent;

        }
        public string GetMailContentWorkSheetItem(List<HC_SP_Get_TD_WorkSheet_Item_Result> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" <table id='tbApproveitemDetail' style='width:800px; border: 1px solid #666666; font-size: 12px; font-family:Tahoma;'>");
            sb.AppendLine("<thead style='background-color:#FF7766'>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th style='text-align:center'>No.</th>");
            sb.AppendLine("<th style='text-align:center'>สถานะ</th>");
            sb.AppendLine("<th style='text-align:center'>หมวดงาน</th>");
            sb.AppendLine("<th style='text-align:center'>ลักษณะงานหลัก</th> ");
            sb.AppendLine("<th style='text-align:center'>ลักษณะงานรอง</th>");
            sb.AppendLine("<th style='text-align:center'>ตำแหน่งที่เกิด</th>");
            sb.AppendLine("<th style='text-align:center'>Priority</th>");
            sb.AppendLine("<th style='text-align:center'>กำหนดแล้วเสร็จ</th>");
            sb.AppendLine("<th style='text-align:center'>วันที่ซ่อมเสร็จ</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            int row = 0;
            foreach (var item in list)
            {
                string WI_Status = "รอดำเนินการ";
                if (item.WI_Status_ID == 2){WI_Status = "ดำเนินการ";}
                else if (item.WI_Status_ID == 3){WI_Status = "ไม่ดำเนินการ";}

                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='text-align:center'><label id='lblNo' name='No'>" + (row + 1) + "</label></td>");
                sb.AppendLine("<td style='text-align:center'>" + WI_Status + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.Guarantee_Name.Trim() + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.SpecL1_Name.Trim() + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.SpecL2_Name.Trim() + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.Location_Name.Trim() + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.Priority_Name + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.WI_ExpectDate + "</td>");
                sb.AppendLine("<td style='text-align:center'>" + item.WI_FinishDate + "</td>");
                sb.AppendLine("</tr>");
                row++;
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        public string GetMailContentExportF2ToVendor(cEmailSupport EmailSupport)
        {
            string content = string.Empty;
            string WOID = string.Empty;
            int i = 0;
            foreach (var item in EmailSupport.AttachedFiles)
            {
                if (i == 0)
                {
                    WOID = item.WorkSheetName.ToString();
                }
                else
                {
                    WOID += ", " + item.WorkSheetName.ToString();
                }
                i++;
            }

            content = @"<!DOCTYPE html> <html><body>
                          <table align=""center"" role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""margin: auto;"">
                              <tr>
                                  <td style=""padding: 20px 0; text-align: center"">
                                      <div style=""height: 120px; text-align: center;"">
                                          <img src=""https://sansiri-conss-ro77.s3.ap-southeast-1.amazonaws.com/www/public/images/sansiri-new-logo.png""
                                               height=""100"" border=""0"" style=""height: 180px; background: transparent; font-family: 'Cordia New', 'Sarabun'; font-size: 18px; line-height: 15px; color: darkgray;"">
                                      </div>
                                  </td>
                              </tr>
                          </table>
                        <div style='font-size: 12px;text-align: center;'> คุณสามารถตรวจสอบข้อมูลได้ที่ <a href='https://sansiriapp4.sansiri.com/HomeCare'>https://sansiriapp4.sansiri.com/HomeCare</a>
                            <table style='width: 1200px' cellpadding='0' cellspacing='0'>
                                <tr>
                                    </br><td valign=top style='font-size: 16px; text-align: center;'><b>แจ้งรายการเอกสาร " + EmailSupport.FormType + @"</b></td></br>
                                </tr>
                                <tr>
                                    <td>
                                        <table style='width:100%;'>
                                            <tr>
                                                <td class='style1' style='font-size: 12px;'><b>แจ้งโดย : </b></td>
                                                <td class='style3' style='font-size: 12px;'>" + UserDetail.Fullname + @"</td>
                                                <td class='style1' style='font-size: 12px;'><b>สิทธิ์ : </b></td>
                                                <td style='font-size: 12px;'>" + UserDetail.Role + @"</td>
                                            </tr>
                                            <tr>
                                                <td class='style1' style='font-size: 12px;'><b>โครงการ : </b></td>
                                                <td style='font-size: 12px;'>" + EmailSupport.DocumentItem.FirstOrDefault().ProjectName +
                                                @"</td>
                                                <td class='style1' style='font-size: 12px;'><b>จำนวน : </b></td>
                                                <td style='font-size: 12px;'>" + EmailSupport.DocumentItem.Count + @" รายการ </td>
                                                <td class='style1' style='font-size: 12px;'></td>
                                                <td style='font-size: 12px;'></td>
                                            </tr>
                                            <tr>
                                                <td class='style1' style='font-size: 12px;'><b>รายการเลขใบงาน : </b></td>
                                                <td colspan='5' style='font-size: 12px;'>" + WOID + @"</td>
                                            </tr>
                                        </table>
                                    </td>
                                <tr>
                                </tr>
                            </table>
                            <p>เอกสารแจ้งขอให้เข้าดำเนินการแก้ไขงาน ความชำรุดบกพร่องจากการก่อสร้าง (HC-F2) ท่านสามารถดูรายละเอียดตาม Link ด้านล่าง</p>
                        </div>
                        <div style='font-size: 12px;text-align: center;'>";
                                    
            content = string.Concat(content, GetMailContentExportF2ToVendorItem(EmailSupport.AttachedFiles));

            content += @"
            </div> 
            <div style='font-size: 12px;'>
                <p></br>
                    กรณี ที่ท่านไม่ส่งช่างเข้าดำเนินการภายในระยะเวลาที่กำหนด ทางบริษัทฯ จะจัดหาผู้รับเหมารายอื่นเข้าดำเนินการแทน และเรียกเก็บค่าใช้จ่ายจากท่าน หรือหักเงิน Retention (เงินประกันผลงาน) ที่มีอยู่
                    </br>
                    หากต้องการสอบถามข้อมูลเพิ่มเติม กรุณาติดต่อเจ้าหน้าที่ HC ประจำโครงการ</p>
            </div></body></html>";

            return content;
        }
        public string GetMailContentExportF2ToVendorItem(List<cEmailSupport.F2AttachedFiles> attached)
        {
            string result = string.Empty;
            foreach(var path in attached)
            {

                result = result + @"</br><a href='" + path.FilePath + "' width='100%' height='100%'>"+ path.WorkSheetName +"</a>";
            }
            return result;
        }
    }
}