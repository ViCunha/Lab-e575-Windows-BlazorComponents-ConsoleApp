
### Overview
---
Razor components can be rendered outside of the context of an HTTP request. You can render Razor components as HTML directly to a string or stream independently of the ASP.NET Core hosting environment. This is convenient for scenarios where you want to generate HTML fragments, such as for generating email content, generating static site content, or for building a content templating engine.
   
### Key Aspects
---
- Razor component
- Console App

### Environment
---
- Microsoft Visual Studio 2022

### Actions
---
Create a C# console application
  - In the Microsoft Visual Studio, click in File > New Project > Console App

Configure the C# console application
  - In the project file (csproj), update the project Sdk
```
From: 
<Project Sdk="Microsoft.NET.Sdk">

To: 
<Project Sdk="Microsoft.NET.Sdk.Razor">
```

- Install the NuGet packages
```
Microsoft.AspNetCore.Components.Web
Microsoft.Extensions.Logging
```

Implement the code
- Create a new Razor component 
```
<h3>BasicMessage</h3>

<p>Now is: @_now</p>

<p>Message: @Message</p>

@code {

    [Parameter]
    public string Message { get; set; }

    private DateTime _now;

    protected override Task OnInitializedAsync()
    {
        _now = DateTime.UtcNow;

        return base.OnInitializedAsync();
    }
}
```

- Implement the code in Program.cs
```
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
```

### Media
---
![image](https://github.com/user-attachments/assets/502bde13-a5b3-4358-ae2d-dc40f7b01908)

### References
---
- [Render Razor components outside of ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-components-outside-of-aspnetcore?view=aspnetcore-8.0)
