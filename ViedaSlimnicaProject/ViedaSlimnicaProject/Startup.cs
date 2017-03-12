using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using ViedaSlimnicaProject;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{   

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}