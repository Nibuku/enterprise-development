using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.Kafka.Deserializers;
using Library.ServiceDefaults;
using Library.Infrastructure.Mongo.Repositories;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<KafkaConsumer>();

/*builder.AddKafkaConsumer<Guid, IList<CheckoutCreateDto>>("library-kafka",
    configureBuilder: kafkaBuilder =>
    {
        kafkaBuilder.SetKeyDeserializer(new KeyDeserializer());
        kafkaBuilder.SetValueDeserializer(new ValueDeserializer());
    },
    configureSettings: settings =>
    {
        settings.Config.GroupId = "library-group";
        settings.Config.AutoOffsetReset = AutoOffsetReset.Earliest;
        settings.Config.AllowAutoCreateTopics = true;
        settings.Config.EnableAutoCommit = false;
    }
);*/
builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();
builder.Services.AddHostedService<KafkaConsumer>();
//builder.Services.AddScoped<IRepositoryAsync<BookCheckout, int>, BookCheckoutMongoRepository>();


var host = builder.Build();
host.Run();