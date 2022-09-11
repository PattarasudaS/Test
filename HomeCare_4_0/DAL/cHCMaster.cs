using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cHCMaster
    {
        public List<Dropdown> HClist1 { get; set; }
        public List<Dropdown> HClist2 { get; set; }
        public List<Dropdown> HClist3 { get; set; }
        public List<Dropdown> HClist4 { get; set; }
    }

    public class Dropdown
    {
        public int value { get; set; }
        public string text { get; set; }
    }
}