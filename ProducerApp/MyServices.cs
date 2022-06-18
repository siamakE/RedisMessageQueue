using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Redis;
using ServiceStack.Messaging.Redis;
using MyApp.ServiceModel;

namespace ProducerApp.ServiceInterface;

public class MyServices : Service
{
    private IRedisClientsManager redisManager;
    private IMessageFactory MqFactory;
    public object Any(Hello request)
    {
        redisManager = new RedisManagerPool("localhost:6379");
        MqFactory = new RedisTransientMessageFactory(redisManager);

        using (IMessageQueueClient mqClient = MqFactory.CreateMessageQueueClient())
        {
            //var replyToMq = mqClient.GetTempQueueName();

            mqClient.Publish(new Message<Hello>(new Hello { Name = "MQ Worker" }));
            //{
            //    ReplyTo = replyToMq,
            //});

            //var responseMsg = mqClient.Get<HelloResponse>(replyToMq);
            //mqClient.Ack(responseMsg);
        }

        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}