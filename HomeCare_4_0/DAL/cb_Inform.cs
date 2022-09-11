using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_Inform
    {

        private HomeCareDBEntities context;
        public cb_Inform()
        {
            this.context = new HomeCareDBEntities();
        }

        public TK_SP_Get_Customer_Info_By_Unit_Result getcustomerinfobuUnit(long UnitID)
        {
            return context.TK_SP_Get_Customer_Info_By_Unit(UnitID).FirstOrDefault();
        }
        public TK_SP_Get_Address_Result getcustomerContract(long? ProspectID)
        {
            return context.TK_SP_Get_Address(ProspectID).FirstOrDefault();
        }

        public List<HC_SP_Get_TD_Receive_Item_Result> getreceiveItem(long receivedID)
        {
            return context.HC_SP_Get_TD_Receive_Item(receivedID).ToList();
        }
    }
}