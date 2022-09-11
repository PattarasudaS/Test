using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using HomeCare_4_0.Models;
using System.Data.SqlClient;

namespace HomeCare_4_0.ClassLib
{
    public class Cls_HC_TM_Lookup
    {
        public List<SelectListItem> Get_Master_DDL(string type)
        {
            HomeCareDBEntities hc_db = new HomeCareDBEntities();
            var LookupQuery = from Lookup in hc_db.HC_TM_Lookup
                              join LookupType in hc_db.HC_TM_Lookup_Type on Lookup.Type_ID equals LookupType.ID
                              where LookupType.Name == type && Lookup.Active == true
                              orderby Lookup.Order ascending
                              select new
                              {
                                  Lookup.Value,
                                  Lookup.DescriptionTH
                              };
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var c in LookupQuery)
            {
                li.Add(new SelectListItem { Text = c.DescriptionTH, Value = c.Value });
            }

            return li;
        }

        public List<SelectListItem> Get_PJ_Authen(Int64 User_ID, Int64 AllPJ)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            using (var hc_db = new HomeCareDBEntities())
            {
                var Parameter1 = new SqlParameter
                {
                    ParameterName = "User_ID",
                    Value = User_ID //1367
                };
                var Parameter2 = new SqlParameter
                {
                    ParameterName = "AllPJ",
                    Value = AllPJ //0
                };

                var DBdata = hc_db.Database.SqlQuery<Project_List_Model>("exec TK_SP_Get_Project_Authen @User_ID, @AllPJ ", Parameter1,Parameter2).ToList<Project_List_Model>();
                
                foreach (var c in DBdata)
                {
                    li.Add(new SelectListItem { Text = c.PJ_Name , Value = c.PJ_ID.ToString() });
                }
            }            
            return li;
        }

        public List<SelectListItem> Get_PJ_Authen_ByCode(Int64 User_ID, Int64 AllPJ)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            using (var hc_db = new HomeCareDBEntities())
            {
                var Parameter1 = new SqlParameter
                {
                    ParameterName = "User_ID",
                    Value = User_ID //1367
                };
                var Parameter2 = new SqlParameter
                {
                    ParameterName = "AllPJ",
                    Value = AllPJ //0
                };

                var DBdata = hc_db.Database.SqlQuery<Project_List_Model>("exec TK_SP_Get_Project_Authen @User_ID, @AllPJ ", Parameter1, Parameter2).ToList<Project_List_Model>();

                foreach (var c in DBdata)
                {
                    li.Add(new SelectListItem { Text = c.PJ_Name, Value = c.PJ_Code.ToString() });
                }
            }
            return li;
        }
        public IEnumerable<SelectListItem> Get_HC_List1()
        {
            HomeCareDBEntities hc_db = new HomeCareDBEntities();
            var LookupQuery = from Lookup in hc_db.HC_TM_List1
                              orderby Lookup.id ascending
                              select new
                              {
                                  Lookup.id,
                                  Lookup.name,
                              };

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var c in LookupQuery)
            {
                li.Add(new SelectListItem { Text = c.name, Value = c.id.ToString() });
            }

            return li;
        }

        public List<SelectListItem> Get_Unit_By_PJ_ID(long PJ_ID)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            using (var hc_db = new HomeCareDBEntities())
            {
                var Parameter1 = new SqlParameter
                {
                    ParameterName = "ProjectID",
                    Value = PJ_ID 
                };
                var DBdata = hc_db.Database.SqlQuery<AJAX_UnitViewModel>("exec TK_SP_Get_Unit_By_ProjectID @ProjectID ", Parameter1).ToList<AJAX_UnitViewModel>();

                foreach (var c in DBdata)
                {
                    li.Add(new SelectListItem { Text = c.Unit_Code + "(" + c.Unit_Address + ")", Value = c.Unit_ID.ToString() });
                }
            }
            return li;
        }

    }
}