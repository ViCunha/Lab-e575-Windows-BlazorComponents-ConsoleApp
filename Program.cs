

using Lab_e575_Windows_BlazorComponents_ConsoleApp;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddLogging();

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();

await using var htmlRender = new HtmlRenderer(serviceProvider, loggerFactory);

var html = await htmlRender.Dispatcher.InvokeAsync
    (
        async () =>
        {
            var dictionary = new Dictionary<string, Object?>
            {
                { "Message", "This is a secrect message" }
            };

            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await htmlRender.RenderComponentAsync<BasicMessage>(parameters);

            return output.ToHtmlString();
        }
    );

Console.WriteLine("Hello, World!");

Console.WriteLine(html);
