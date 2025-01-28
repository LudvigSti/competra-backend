using Microsoft.Win32;

namespace competra.wwwapi.Controllers
{
    public static class UserGroupsController
    {
        public static void configureUserGroupController(this WebApplication app)
        {
            var group = app.MapGroup("userGroup");
           // group.MapGet("/", GetAll);


        }


    }
}