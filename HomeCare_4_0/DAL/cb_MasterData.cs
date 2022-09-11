using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core.Objects;
using System.Web.Mvc;

namespace HomeCare_4_0.DAL
{
    public class cb_MasterData
    {
        private HomeCareDBEntities context;
        

        public cb_MasterData()
        {
            this.context = new HomeCareDBEntities();
        }
        public HC_SP_Get_ProjectDetail_Result getProjectDetailByProjectCode(string projectCode)
        {
            return context.HC_SP_Get_ProjectDetail(projectCode).FirstOrDefault();
        }

        public List<Dropdown> getHCList1()
        {
            List<HC_TM_List1> hclist1 = context.HC_TM_List1.Where(x => x.Active == true).OrderBy(x => x.name).ToList();
            List<Dropdown> list = new List<Dropdown>();

            foreach (var item in hclist1)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }

        
        public List<Dropdown> getHCList2(int List1ID)
        {
            List<HC_TM_List2> hclist2 = context.HC_TM_List2.Where(x=>x.tm_ListL1_id==List1ID && x.Active == true).OrderBy(x => x.name).ToList();
            List<Dropdown> list = new List<Dropdown>();

            foreach (var item in hclist2)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }
        public List<Dropdown> getHCList3(int List2ID)
        {
            List<HC_TM_List3> hclist3 = context.HC_TM_List3.Where(x => x.tm_ListL2_id == List2ID && x.Active == true).OrderBy(x => x.name).ToList();
            List<Dropdown> list = new List<Dropdown>();

            foreach (var item in hclist3)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }
        public List<Dropdown> getHCList4(int List3ID)
        {
            List<HC_TM_List4> hclist4 = context.HC_TM_List4.Where(x => x.tm_ListL3_id == List3ID && x.Active == true).OrderBy(x => x.name).ToList();
            List<Dropdown> list = new List<Dropdown>();

            foreach (var item in hclist4)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }
        public HC_TM_List4 getList4Unit(int list4ID)
        {
            HC_TM_List4 List4Detail = context.HC_TM_List4.Where(x => x.id == list4ID).FirstOrDefault();
            return List4Detail;
        }

        public List<HC_SP_Get_TM_Payment_Type_Result> getPaymentType(string approvedType, int warrantyExpired)
        {
            return context.HC_SP_Get_TM_Payment_Type(approvedType, warrantyExpired==0?false:true).ToList();
        }

        public List<Dropdown> getHCSpec1()
        {
            List<HC_TM_SpecL1> hcSpec1 = context.HC_TM_SpecL1.ToList();
            List<Dropdown> list = new List<Dropdown>();

            list.Add(new Dropdown { text = "-- กรุณาเลือก --", value = 0 });

            foreach (var item in hcSpec1)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }
        public List<Dropdown> getHCSpec2(long spec1ID)
        {
            List<HC_TM_SpecL2> hcSpec2 = context.HC_TM_SpecL2.Where(x => x.specL1_id == spec1ID).ToList();
            List<Dropdown> list = new List<Dropdown>();
            foreach (var item in hcSpec2)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }

      

        public List<Dropdown> getHCSpec3(long spec2ID)
        {
            List<HC_TM_SpecL3> hcSpec3 = context.HC_TM_SpecL3.Where(x => x.specL2_id == spec2ID).ToList();
            List<Dropdown> list = new List<Dropdown>();
            foreach (var item in hcSpec3)
            {
                Dropdown obj = new Dropdown();
                obj.value = item.id;
                obj.text = item.name;
                list.Add(obj);
            }
            return list;
        }
        public string getPriority(int spec3ID)
        {
            string  Priority =context.HC_SP_Get_TM_Priority(spec3ID).FirstOrDefault();
            return Priority;
        }
        public List<HC_SP_Get_TM_Location_Result> getLocation(long spec1ID)
        {
            return context.HC_SP_Get_TM_Location(spec1ID).ToList();
        }
        public List<HC_SP_Get_ExtendReason_Result> getExtendReason(int reasonType)
        {
            return context.HC_SP_Get_ExtendReason(reasonType).ToList();
        }

        public List<SelectListItem> getUser()
        {
            HomeCareDBEntities hc_db = new HomeCareDBEntities();
            var LookupQuery = from Lookup in hc_db.TK_V_UserAcct
                              where Lookup.Email.Contains("@Sansiri.com")
                              orderby Lookup.NameENG ascending
                              select new
                              {
                                  Lookup.ID,
                                  Lookup.NameENG
                              };
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var c in LookupQuery)
            {
                li.Add(new SelectListItem { Text = c.NameENG, Value = c.ID.ToString() });
            }

            return li;
        }

    }
}