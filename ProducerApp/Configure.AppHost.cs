using Funq;
using ServiceStack;
using ProducerApp.ServiceInterface;
using ServiceStack.Redis;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;

[assembly: HostingStartup(typeof(AppHost))]

namespace ProducerApp;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            var mqServer = new RedisMqServer(new RedisManagerPool("localhost:6379"))
            {
                DisablePublishingToOutq = true,
            };
            services.AddSingleton<IMessageService> (mqServer);
        });

    public AppHost() : base("ProducerApp", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });
    }
}
