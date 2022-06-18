using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Redis;
using ServiceStack.Messaging.Redis;
using MyApp.ServiceModel;

namespace ProducerApp.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        PublishMessage(request);
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}