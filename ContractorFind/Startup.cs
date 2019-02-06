using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContractorFind.Startup))]
namespace ContractorFind
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
