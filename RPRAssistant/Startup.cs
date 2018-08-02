using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPRAssistant.Startup))]
namespace RPRAssistant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
