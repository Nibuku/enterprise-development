using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongo")
                     .AddDatabase("library");

builder.AddProject<Projects.Library_Api>("api")
       .WithReference(mongoDb)
       .WaitFor(mongoDb);

var kafka = builder.AddKafka("library-kafka")
                   .WithKafkaUI();

var producer = builder.AddProject<Projects.Library_Generator_Kafka>("generator")
                      .WithReference(kafka)
                      .WaitFor(kafka)
                      .WithEnvironment("Generator:BatchSize", "10")
                      .WithEnvironment("Generator:PayloadLimit", "100")
                      .WithEnvironment("Generator:WaitTime", "5000")
                      .WithEnvironment("Kafka:Topic", "library-checkouts");

var consumer = builder.AddProject<Projects.Library_Infrastructure_Kafka>("library-infrastructure-kafka")
                      .WithReference(kafka)
                      .WithReference(mongoDb)
                      .WaitFor(kafka)
                      .WaitFor(producer)
                      .WithEnvironment("Kafka:GroupId", "library-group")           
                      .WithEnvironment("Kafka:BootstrapServers", "localhost:9092")
                      .WithEnvironment("Kafka:Topic", "library-checkouts");

builder.Build().Run();