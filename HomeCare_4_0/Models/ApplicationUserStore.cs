using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Models
{
    public class ApplicationUserStore :
    UserStore<UserAuthen, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
    IUserStore<UserAuthen>,
    IDisposable
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
    }
}