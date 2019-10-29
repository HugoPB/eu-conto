using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EuConto.Infrastructure
{
    public class RouteProvider
    {
        public static void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //Home page default
            routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            //Login
            routeBuilder.MapRoute("Login", "login/",
                new { controller = "User", action = "Login" });
            //Register
            routeBuilder.MapRoute("Register", "register/",
                new { controller = "User", action = "Register" });
            //Logout
            routeBuilder.MapRoute("Logout", "Logout/",
                new { controller = "User", action = "SignOut" });

            //Profile
            routeBuilder.MapRoute("Profile", "profile/",
                new { controller = "User", action = "Profile" });

            //ProfileEdit
            routeBuilder.MapRoute("ProfileEdit", "profileedit/",
                new { controller = "User", action = "ProfileEdit" });

            //User Like Story            
            routeBuilder.MapRoute("UserLikeStory", "userlikestory/",
            new { controller = "User", action = "UserLikeStory" });

            //User Like Chapter            
            routeBuilder.MapRoute("UserLikeChapter", "userlikechapter/",
            new { controller = "User", action = "UserLikeChapter" });

            //-----------------------------------------------------------------------------------
            //User Storys
            routeBuilder.MapRoute("UserStorys", "UserStorys/",
                new { controller = "Story", action = "UserStorys" });

            //User Story Create and Edit
            routeBuilder.MapRoute("UserStoryCreateEdit", "UserStoryCreateEdit/",
                new { controller = "Story", action = "UserStoryCreateEdit" });

            //User Story Delete
            routeBuilder.MapRoute("UserStoryDelete", "UserStoryDelete/",
                new { controller = "Story", action = "UserStoryDelete" });

            //User Chapter
            routeBuilder.MapRoute("UserStoryChapters", "UserStoryChapters/",
                new { controller = "Story", action = "UserStoryChapters" });

            //User Sections
            routeBuilder.MapRoute("UserStoryChapterSectionsCreateEdit", "UserStoryChapterSectionsCreateEdit/",
                new { controller = "Story", action = "UserStoryChapterSectionsCreateEdit" });

            //User Sections Read
            routeBuilder.MapRoute("UserStoryChapterSectionsRead", "UserStoryChapterSectionsRead/",
            new { controller = "Story", action = "UserStoryChapterSectionsRead" });

            //User Chapter Delete
            routeBuilder.MapRoute("UserStoryChapterSectionsDelete", "UserStoryChapterSectionsDelete/",
                new { controller = "Story", action = "UserStoryChapterSectionsDelete" });

            //-----------------------------------------------------------------------------------

        }
    }
}
