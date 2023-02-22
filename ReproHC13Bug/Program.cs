using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using ReproHC13Bug;

var services = new ServiceCollection();
services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<InputValue>()
    .AddType<CustomDirective>()
    .AddType<EmailAddressDirective>()
    .TryAddTypeInterceptor<ValidationTypeInterceptor>()
    .InitializeOnStartup();
await using var container = services.BuildServiceProvider();
var resolver = container.GetRequiredService<IRequestExecutorResolver>();
var schema = (await resolver.GetRequestExecutorAsync()).Schema;
var serialized = schema.Print();
Console.WriteLine(serialized);