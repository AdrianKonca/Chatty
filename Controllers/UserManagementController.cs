using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chatty.Controllers
{
    public class UserManagementController : Controller
    {
        [Authorize(Roles = "Root")]
        public string Index()
        {
            return "This is User Management panel";
        }
    }
}