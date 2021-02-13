using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Websitecourse.Models;

[assembly: OwinStartupAttribute(typeof(Websitecourse.Startup))]
namespace Websitecourse
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


           CreateDefaultRolesAndUser();
        }
        public void CreateDefaultRolesAndUser()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManger.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManger.Create(role);
                //if no exit admin will create it
                //role.Name = "Students";
                //roleManger.Create(role);
                //role.Name = "Teacher";
                //roleManger.Create(role);
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Azza123",
                    Email = "Azza123@myDomain"
                };
                var Check = userManger.Create(user, "Azza123@");
                if (Check.Succeeded)
                {
                    userManger.AddToRole(user.Id, "Admin");
                }
            }
            //if (!roleManger.RoleExists("Students"))
            //{
            //    role.Name = "Students";
            //    roleManger.Create(role);
            //}
            //if (!roleManger.RoleExists("Teacher"))
            //{
            //    role.Name = "Teacher";
            //    roleManger.Create(role);
            //}
        }
        //        //else
        //        //{
        //        //    var e = new Exception("Could not add default user");
        //        //    var enumerator = Check.Errors.GetEnumerator();
        //        //    foreach (var error in Check.Errors)
        //        //    {
        //        //        e.Data.Add(enumerator, error);
        //        //    }
        //        //    throw e;
        //        //}
        //    }
        //}
    }
}
