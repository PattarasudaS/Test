using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using HomeCare_4_0.Models;

namespace HomeCare_4_0.ClassLib
{
    public class Cls_HC_TM
    {
        //public List<SelectListItem> Get_HC_TM_Main_Status()
        //{
        //    HomeCareDBEntities hc_db = new HomeCareDBEntities();
        //    var LookupQuery = from Lookup in hc_db.HC_TM_Main_Process                             
        //                      where Lookup.Active == true
        //                      orderby Lookup.ID ascending
        //                      select new
        //                      {
        //                          Lookup.Value,
        //                          Lookup.DescriptionTH,
        //                          Lookup.DescriptionEN
        //                      };

        //    List<SelectListItem> li = new List<SelectListItem>();
        //    foreach (var c in LookupQuery)
        //    {
        //        li.Add(new SelectListItem { Text = c.DescriptionTH, Value = c.Value });
        //    }

        //    return li;
        //}
    }
}