using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HomeCare_4_0.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class UserAuthen : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public string EmpCode { get; set; }
        public int LocationID { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserAuthen> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationUserRole : IdentityUserRole { }
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole> { }
    public class ApplicationUserClaim : IdentityUserClaim { }
    public class ApplicationUserLogin : IdentityUserLogin { }
    public class ApplicationDbContext : IdentityDbContext<UserAuthen, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAuthen>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");
        }
    }
}