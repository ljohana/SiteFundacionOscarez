using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SitioFundacionOscarez.Startup))]
namespace SitioFundacionOscarez
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
