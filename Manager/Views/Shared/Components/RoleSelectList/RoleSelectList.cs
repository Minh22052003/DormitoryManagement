using Manager.Data;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Views.Shared.Components.RoleSelectList
{
    public class RoleSelectList : ViewComponent
    {
        private readonly RoleData _roleData;
        public RoleSelectList(IHttpContextAccessor httpContextAccessor)
        {
            _roleData = new RoleData(httpContextAccessor);
        }
        public IViewComponentResult Invoke()
        {
            var roles = _roleData.GetAllRole().Result;
            return View(roles);
        }
    }
}
