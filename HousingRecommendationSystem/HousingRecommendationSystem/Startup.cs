using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HousingRecommendationSystem.Startup))]
namespace HousingRecommendationSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
