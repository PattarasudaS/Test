using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.DAL
{
    public class cb_SearchManagement
    {
        private HomeCareDBEntities context;
        public cb_SearchManagement()
        {
            this.context = new HomeCareDBEntities();
        }

        
        public List<HC_SP_Get_RW_Search_Result> GetSearchInformation(long ProjectID, long unitID)
        {
            long VendorID = 0;
            if (UserDetail.Role == "VENDOR")
            {
                VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode)? UserDetail.VendorCode : "0");
            }
            return context.HC_SP_Get_RW_Search(ProjectID, unitID,VendorID).ToList();
        }
        public List<HC_SP_Get_RW_Search_Noti_Result> GetSearchInformationByStatus(int MainProcessID)
        {
            long VendorID = 0;
            if(UserDetail.Role== "VENDOR")
            {
                VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }
            
            if(MainProcessID==1 || MainProcessID==2)
            {
                return context.HC_SP_Get_RW_Search_Noti(UserDetail.UserID, VendorID).Where(x => x.Receive_Status_ID== MainProcessID).ToList();
            }else
            {
                return context.HC_SP_Get_RW_Search_Noti(UserDetail.UserID, VendorID).Where(x => x.ApproveStatusID == MainProcessID).ToList();
            }
            
        }

    }
}