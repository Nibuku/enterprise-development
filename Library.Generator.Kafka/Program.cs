using Library.Application.Contracts.Dtos;
using Library.Generator.Kafka;
using Library.Generator.Kafka.Serializers;
using Library.Generator.Kafka.Services;
using Library.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<Guid, IList<CheckoutCreateDto>>("library-kafka",
    configureBuilder: kafkaBuilder =>
    {
        kafkaBuilder.SetKeySerializer(new KeySerializer());
        kafkaBuilder.SetValueSerializer(new ValueSerializer());
    });

builder.Services.AddScoped<IProducerService, GeneratorService>();
builder.Services.AddHostedService<KafkaProducerService>();

var host = builder.Build();
host.Run();
