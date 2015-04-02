using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AdBoard.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, this.Name));
            userIdentity.AddClaim(new Claim(ClaimTypes.Surname, this.Surname));
            userIdentity.AddClaim(new Claim(ClaimTypes.Gender, this.Gender));
            userIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, this.DateOfBirthday));
            userIdentity.AddClaim(new Claim(ClaimTypes.Country, this.Country));
            userIdentity.AddClaim(new Claim(ClaimTypes.StreetAddress, this.StreetAddress));
            userIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, this.MobilePhone));
            return userIdentity;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DateOfBirthday { get; set; }
        public string Country { get; set; }
        public string StreetAddress { get; set; }
        public string MobilePhone { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}