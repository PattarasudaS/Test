using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Common;

namespace HomeCare_4_0.Controllers
{
    public class GuaranteeController : BaseController
    {
        private Int64 User_ID;
        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();

        public ActionResult SearchGuarantee(long id)
        {
            GuaranteeViewModel GuaranteeModel = new GuaranteeViewModel();
            ViewBag.Title = "Search Infomation";
            User_ID = UserDetail.UserID;
            //GuaranteeModel.objCriteriaMaser = new CriteriaMaser();
            GuaranteeModel.ProjectID = id;

            return View(GuaranteeModel);
        }

        public ActionResult ApproveGuarantee(long unitID,long DocumentID)
        {
            GuaranteeViewModel GuaranteeModel = new GuaranteeViewModel();
            cb_Guarantee onj = new cb_Guarantee();

            var data = onj.GetCustomerInfoByUnit(unitID);

            UnitViewModel Data_Unit = new UnitViewModel();
            Data_Unit.PJ_Name = data.FirstOrDefault().Project_NameTH;
            Data_Unit.PJ_Code = data.FirstOrDefault().Project_Code;
            Data_Unit.Unit_Code = data.FirstOrDefault().Unit_Code;
            Data_Unit.Unit_Address = data.FirstOrDefault().Unit_Address;
            Data_Unit.Unit_Code_Address = data.FirstOrDefault().Unit_Code_Address;
            Data_Unit.Unit_Model = data.FirstOrDefault().Unit_Model;
            Data_Unit.StartGuaranteeDate = data.FirstOrDefault().StartGuaranteeDate;
            Data_Unit.EndGuaranteeDate = data.FirstOrDefault().EndGuaranteeDate;
            Data_Unit.ExtraGuaranteeDate = data.FirstOrDefault().ExtraGuaranteeDate;
            Data_Unit.Unit_ID = data.FirstOrDefault().Unit_ID;

            CustomerViewModel Data_Customer = new CustomerViewModel();
            Data_Customer.FullName = data.FirstOrDefault().FullNameTH_1;
            Data_Customer.FullTelephone = data.FirstOrDefault().Mobile_1 + "," + data.FirstOrDefault().Telephone_1;

            var data2 = onj.GetGuarantee(DocumentID).FirstOrDefault();

            GuaranteeModel.Data_Unit = Data_Unit;
            GuaranteeModel.Data_Customer = Data_Customer;
            GuaranteeModel.Data_GuaranteeDetail = data2;

            return View(GuaranteeModel);
        }

        [HttpPost]
        public JsonResult GetSearchGuarantee(long ProjectID)
        {
            cb_Guarantee objSearchManagement = new cb_Guarantee();
            var list = objSearchManagement.GetSearchGuarantee(ProjectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGuarantee(long DOCUMENTID)
        {
            cb_Guarantee objSearchManagement = new cb_Guarantee();
            var list = objSearchManagement.GetGuarantee(DOCUMENTID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertGuarantee(List<GuaranteeDetail> objGuarantee)
        {
            try
            {
                if (objGuarantee != null && objGuarantee.Count() > 0)
                {
                   foreach(var item in objGuarantee)
                    {
                       

                        if (item.UnitID != 0)
                        {
                            using (HomeCareDBEntities HomeCareDB = new HomeCareDBEntities())
                            {
                                //System.Data.Entity.Core.Objects.ObjectParameter JobNoText = new System.Data.Entity.Core.Objects.ObjectParameter("JobNoText", typeof(string));  // Define Parameter เพื่อรอรับค่า กลับ
                                List<HC_SP_Insert_TD_Guarantee_Result> result= HomeCareDB.HC_SP_Insert_TD_Guarantee(item.UnitID,item.GuaranteeDay, item.GuaranteeMonth, item.GuaranteeYear, item.GuaranteeRemark,UserDetail.CodeName).ToList();
                                if(result.Count>0)
                                {
                                    cb_Guarantee Guarantee = new cb_Guarantee();
                                    Guarantee.sendNoti(result.FirstOrDefault().TD_Approve_ID, (int)EnumConfiguration.EmailActionType.SendApprove, result.FirstOrDefault().JobNoText);
                                }
                               
                                //cNotificationMobileApproveResult NotificationAPI = new cNotificationMobileApproveResult();
                                //NotificationAPI.process = EnumConfiguration.ApproveType.INSURANCE_EXTEND.ToString();
                                //NotificationAPI.intno = (JobNoText.Value).ToString();
                                //NotificationAPI.doctype = "ZHC";
                                //NotificationAPI.email = "testvp@sansiri.com";

                                //cb_Notification Noti = new cb_Notification();
                                //Noti.NotificationMobileApprove(NotificationAPI);
                            }
                        }

                    }

                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
 
        }

        [HttpPost]
        public JsonResult UpdateGuarantee(cApproveGuarantee ApproveGuarantee)
        {
            cb_Guarantee obj = new cb_Guarantee();
            obj.UpdateGuarantee(ApproveGuarantee);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
            

        public ActionResult AddGuarantee(long id)
        {
            try
            {

                if (id != 0)
                {
                    GuaranteeViewModel data = new GuaranteeViewModel();
                    InformIndexViewModel Inform_data = new InformIndexViewModel();

                    List<GViewModel> Data_Inform_Item_List = new List<GViewModel>();
                    for (var i = 0; i < 5; i++)
                    {
                        GViewModel Data_Inform_Item_ = new GViewModel();
                        Data_Inform_Item_.UnitID = 0;
                        Data_Inform_Item_.InformItem_Detail = "";
                        Data_Inform_Item_.InformItem_Detail = "";
                        Data_Inform_Item_List.Add(Data_Inform_Item_);

                    }
                   
                    DropDownListModel Data_DLL_HC_Unit = new DropDownListModel();
                    Data_DLL_HC_Unit.Items = cls_hc_tm_lookup.Get_Unit_By_PJ_ID(id);
                    
                    data.Data_DLL_HC_Unit = Data_DLL_HC_Unit;
                    data.Data_Inform_Item_List = Data_Inform_Item_List; 



                    return View(data);
                }
                else
                {
                    // เซ็ต Error                                
                    ErrorViewModel Error = new ErrorViewModel();
                    Error.code = Cls_HC_Constance.HC_Error_404;
                    Error.message_1 = Cls_HC_Constance.HC_Error_404_Message_TH;
                    Error.HandleError = new HandleErrorInfo(new Exception(Cls_HC_Constance.HC_Error_404_Message_EN), "Inform", "Index"); ;
                    return View("Error", Error);
                }

            }
            catch (Exception ex)
            {
                // เซ็ต Error                
                ErrorViewModel Error = new ErrorViewModel();
                Error.code = Cls_HC_Constance.HC_Error_500;
                Error.HandleError = new HandleErrorInfo(ex, "Inform", "Index");
                return View("Error", Error);
            }


        }

        //[HttpPost]
        public JsonResult GetUnitData(RequestModel model)
        {
            if (model != null)
            {
                AJAX_UnitResultViewModel data = new AJAX_UnitResultViewModel();
                List<AJAX_UnitViewModel> lUnit = new List<AJAX_UnitViewModel>();
                List<SelectListItem> list_Of_Unit = new List<SelectListItem>();
                list_Of_Unit = cls_hc_tm_lookup.Get_Unit_By_PJ_ID(Convert.ToInt64(model.ID));

                foreach (var list in list_Of_Unit)
                {
                    AJAX_UnitViewModel obj = new AJAX_UnitViewModel();
                    obj.Unit_ID = Convert.ToInt64(list.Value);
                    obj.Unit_Code = list.Text;
                    lUnit.Add(obj);
                }
                data.data = lUnit;
                return Json(data);
            }
            else
            {
                return Json("An Error Has occoured");
            }

        }

        //[HttpPost]
        public JsonResult GetUnitDetail(RequestUnit model)
        {
            cb_Guarantee onj = new cb_Guarantee();
            var data = onj.GetCustomerInfoByUnit(model.UnitID);
            return Json(data);
        }


         


    }

}