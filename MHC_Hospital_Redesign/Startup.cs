using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MHC_Hospital_Redesign.Startup))]
namespace MHC_Hospital_Redesign
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
