using Funq;
using ServiceStack;
using ProducerApp.ServiceInterface;
using ServiceStack.Redis;

[assembly: HostingStartup(typeof(AppHost))]

namespace ProducerApp;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            //_ = services.AddSingleton<IRedisClientsManager>(new RedisManagerPool("localhost:6379"));
        });

    public AppHost() : base("ProducerApp", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });
    }
}
