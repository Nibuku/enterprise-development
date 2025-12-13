using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongo").AddDatabase("library");

var api=builder.AddProject<Projects.Library_Api>("api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

var kafka = builder.AddKafka("library-kafka")
    .WithKafkaUI();

var kafkaSettings = builder.Configuration.GetSection("Kafka");
var bootstrapServers = builder.Configuration.GetConnectionString("library-kafka");
var groupId = kafkaSettings["GroupId"];
var topic = kafkaSettings["Topic"];

var generatorSettings = builder.Configuration.GetSection("Generator");
var batchSize = generatorSettings.GetValue("BatchSize", 100);
var payloadLimit = generatorSettings.GetValue("PayloadLimit", 1000);
var waitTime = generatorSettings.GetValue("WaitTime", 5);

var client = builder.AddProject<Projects.Library_Wasm>("client")
    .WithReference(api)
    .WaitFor(api);

var producer = builder.AddProject<Projects.Library_Generator_Kafka>("generator")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WaitFor(mongoDb)
    .WithEnvironment("Kafka:Topic", topic)
    .WithEnvironment("Generator:BatchSize", batchSize.ToString())
    .WithEnvironment("Generator:PayloadLimit", payloadLimit.ToString())
    .WithEnvironment("Generator:WaitTime", waitTime.ToString());

builder.AddProject<Projects.Library_Infrastructure_Kafka>("consumer")
    .WithReference(kafka)
    .WithReference(mongoDb)
    .WaitFor(kafka)
    .WaitFor(producer)
    .WithEnvironment("Kafka:GroupId", groupId)
    .WithEnvironment("Kafka:BootstrapServers", bootstrapServers)
    .WithEnvironment("Kafka:Topic", topic);

builder.Build().Run();