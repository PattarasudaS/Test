using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cInform
    {
        public string[] listItem { get; set; }

        public long? CustomerID { get;  set; }
        public string CustomerName { get;  set; }
        public string CustomerTel { get;  set; }
        public long? CustomerTypeID { get;  set; }

        public string Receive_AppointmentDate { get; set; }
        public DateTime? Receive_AppointmentDateFormateDate { get; set; }
        public TimeSpan? Receive_AppointmentTime_From { get; set; }
        public TimeSpan? Receive_AppointmentTime_To { get; set; }
        public int? ddlOverAPDate_ReasonID { get; set; }




        public string ProjectCode { get;  set; }
        public long? ProjectID { get;  set; }
        public string Remark { get;  set; }
        public string UnitCode { get;  set; }
        public long? UnitID { get;  set; }

        public bool flagAgent { get; set; }
        public bool flagChina { get; set; }
    }
}