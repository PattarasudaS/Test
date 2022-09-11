//using HomeCare_4_0.SAP_HC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_SAPManagement
    {
        public static List<cVendorListResult> SAP_Load_Vendor(string p_LIFNR, string p_NAME, string p_STCD)
        {
            string IM_NAME = p_NAME;
            string IM_STCD = p_STCD;
            string IM_BUKRS = "10";
            string IM_SPERQ = "";
            string IM_ZWELS = "";

            string street = null;
            string sub_district = null;
            string district = null;
            string city = null;
            string postcode = null;
            string address = null;

            SI_Vendor_Search_Sync_Req.SI_Vendor_Search_Sync_ReqService XI_SERVICE = new SI_Vendor_Search_Sync_Req.SI_Vendor_Search_Sync_ReqService();
            SI_Vendor_Search_Sync_Req.BAPIRETURN1 EX_RETURN = new SI_Vendor_Search_Sync_Req.BAPIRETURN1();
            SI_Vendor_Search_Sync_Req.ZSPI_VENDOR_SEARCH[] EX_VENDOR_SEARCH = new SI_Vendor_Search_Sync_Req.ZSPI_VENDOR_SEARCH[1001];
            SI_Vendor_Search_Sync_Req.ZSPI_CONTRACT_PERSON[] EX_CONTRACT_PERSON = new SI_Vendor_Search_Sync_Req.ZSPI_CONTRACT_PERSON[1001];
            SI_Vendor_Search_Sync_Req.ZSPI_WITHT[] EX_WITHT = new SI_Vendor_Search_Sync_Req.ZSPI_WITHT[1001];

            SI_Vendor_Search_Sync_Req.RANGE_LIFNR[] IM_LIFNR = new SI_Vendor_Search_Sync_Req.RANGE_LIFNR[1];
            if (!string.IsNullOrEmpty(p_LIFNR))
            {
                SI_Vendor_Search_Sync_Req.RANGE_LIFNR IM_LIFNR_ITEM = new SI_Vendor_Search_Sync_Req.RANGE_LIFNR();
                IM_LIFNR_ITEM.SIGN = "I";
                IM_LIFNR_ITEM.OPTION = "EQ";
                IM_LIFNR_ITEM.LOW = p_LIFNR;
                IM_LIFNR[0] = IM_LIFNR_ITEM;
            }

            System.Net.NetworkCredential cr = new System.Net.NetworkCredential();
            cr.Domain = HomeCare_4_0.Properties.Settings.Default.PI_Domain_Prd.ToString();// ""
            cr.UserName = HomeCare_4_0.Properties.Settings.Default.PI_User_Prd.ToString();// SIRIPI
            cr.Password = HomeCare_4_0.Properties.Settings.Default.PI_Pwd_Prd.ToString();//sansiri
            XI_SERVICE.Credentials = cr;
            EX_RETURN = XI_SERVICE.SI_Vendor_Search_Sync_Req(IM_BUKRS, IM_LIFNR, IM_NAME, IM_SPERQ, IM_STCD, IM_ZWELS, ref EX_CONTRACT_PERSON, ref EX_VENDOR_SEARCH, ref EX_WITHT);

            List<cVendorListResult> listVendorResult = new List<cVendorListResult>();
            if (EX_RETURN.TYPE.Substring(0, 1) == "S")
            {
                
                for (int ii = 0; ii <= EX_VENDOR_SEARCH.Length - 1; ii++)
                {
                    cVendorListResult obj = new cVendorListResult();

                    street = EX_VENDOR_SEARCH[ii].STREET;
                    sub_district = EX_VENDOR_SEARCH[ii].SUB_DISTRICT;
                    district = EX_VENDOR_SEARCH[ii].DISTRICT;
                    postcode = EX_VENDOR_SEARCH[ii].POST_CODE;
                    city = EX_VENDOR_SEARCH[ii].CITY;
                    address = string.Format("{0} {1} {2} {3} {4}", street, sub_district, district, city, postcode);


                    obj.Vendor = EX_VENDOR_SEARCH[ii].LIFNR.ToString();
                    obj.Name = EX_VENDOR_SEARCH[ii].NAME1.Replace("'", "").ToString() + " " +EX_VENDOR_SEARCH[ii].NAME2.Replace("'", "").ToString();
                    obj.TaxID = EX_VENDOR_SEARCH[ii].STCD2.ToString();
                    obj.CardID = EX_VENDOR_SEARCH[ii].STCD3.ToString();
                    obj.Address = address.ToString();
                    obj.Email = EX_VENDOR_SEARCH[ii].EMAIL.ToString();
                    listVendorResult.Add(obj);
                }
            }
            return listVendorResult;
        }
    }

    public class cVendorListResult
    {
        public string Name { get; set; }
        public string CardID { get; set; }
        public string TaxID { get; set; }
        public string Vendor { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class cSAPManagement
    {
        public string Vendor { get; set; }
        public string Name { get; set; }
        public string TaxID { get; set; }
    }
}

