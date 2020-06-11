using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chatty.Controllers
{
    public class AdminManagementController : Controller
    {
        [Authorize(Roles = "Admin")]
        public string Index()
        {
            return "This is Admin Management panel";
        }
    }
}