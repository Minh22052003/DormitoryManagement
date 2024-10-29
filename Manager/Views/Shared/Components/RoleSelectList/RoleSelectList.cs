using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.RoleSelectList
{
    public class RoleSelectList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    RoleName = "Administrator"
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Staff"
                },
                new Role
                {
                    RoleId = 3,
                    RoleName = "Student"
                }
            };
            return View(roles);
        }
    }
}
