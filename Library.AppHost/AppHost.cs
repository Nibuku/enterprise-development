using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongo").AddDatabase("library");

builder.AddProject<Projects.Library_Api>("api")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

var kafka = builder.AddKafka("library-kafka")
    .WithKafkaUI();

var kafkaSettings = builder.Configuration.GetSection("Kafka");
var bootstrapServers = builder.Configuration.GetConnectionString("library-kafka");
var groupId = kafkaSettings["GroupId"];
var topic = kafkaSettings["Topic"];

var generatorSettings = builder.Configuration.GetSection("Generator");
var batchSize = generatorSettings.GetValue<int?>("BatchSize");
var payloadLimit = generatorSettings.GetValue<int?>("PayloadLimit");
var waitTime = generatorSettings.GetValue<int?>("WaitTime");

var producer = builder.AddProject<Projects.Library_Generator_Kafka>("generator")
    .WithReference(kafka)
    .WaitFor(kafka)
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