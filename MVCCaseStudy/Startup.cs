using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCCaseStudy.Startup))]
namespace MVCCaseStudy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
