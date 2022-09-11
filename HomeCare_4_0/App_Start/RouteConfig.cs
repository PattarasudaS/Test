using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HomeCare_4_0
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "SearchInformation",
            url: "{controller}/{action}/{ProjectID}/{UnitID}",
            defaults: new
            {
                controller = "Home",
                action = "SearchInformation",
                ProjectID = UrlParameter.Optional,
                UnitCode = UrlParameter.Optional,

            });
            //UploadFile
            routes.MapRoute(
            name: "uploadAttachmentFile",
            url: "{controller}/{action}/{ReferenceType}/{AttachmentTypeID}/{AttachmentDetail}",
            defaults: new
            {
                controller = "Fileupload",
                action = "uploadAttachmentFile",
                //ProjectID = UrlParameter.Optional,
                //UnitCode = UrlParameter.Optional,

                ReferenceType = UrlParameter.Optional,
                AttachmentTypeID = UrlParameter.Optional,
                AttachmentDetail = UrlParameter.Optional

            });

            routes.MapRoute(
              name: "SearchInformationByStatus",
              url: "{controller}/{action}/{MainProcessID}",
              defaults: new { controller = "Home", action = "SearchInformationByStatus", MainProcessID = UrlParameter.Optional }
          );

        }



    }
}
