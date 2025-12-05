using BlazorWasm.Client;
using BlazorWasm.Client.Components;
using BlazorWasm.Client.Configs;
using Elsa.Api.Client.HttpMessageHandlers;
using Elsa.Studio.Contracts;
using Elsa.Studio.Core.BlazorWasm.Extensions;
using Elsa.Studio.Extensions;
using Elsa.Studio.Localization.Time;
using Elsa.Studio.Localization.Time.Providers;
using Elsa.Studio.Login.BlazorWasm.Extensions;
using Elsa.Studio.Login.HttpMessageHandlers;
using Elsa.Studio.Login.Services;
using Elsa.Studio.Models;
using Elsa.Studio.Shell;
using Elsa.Studio.Shell.Extensions;
using Elsa.Studio.WorkflowContexts.Extensions;
using Elsa.Studio.Workflows.Designer.Extensions;
using Elsa.Studio.Workflows.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using MudExtensions.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
WebAssemblyHostConfiguration configuration = builder.Configuration;
builder.RootComponents.RegisterCustomElsaStudioElements();
builder.RootComponents.RegisterCustomElement<WorkflowDefinitionEditorWrapper>("elsa-studio-workflow-definition-editor");
builder.RootComponents.RegisterCustomElement<WorkflowInstanceViewerWrapper>("elsa-workflow-instance-viewer");
builder.RootComponents.RegisterCustomElement<WorkflowInstanceListWrapper>("elsa-workflow-instance-list");
builder.RootComponents.RegisterCustomElement<WorkflowDefinitionListWrapper>("elsa-workflow-definition-list");

builder.Services.AddHttpClient("BlazorWasm.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWasm.ServerAPI"));

ElsaSettings elsaSettings = builder.Configuration.GetSection(nameof(ElsaSettings)).Get<ElsaSettings>()!;
builder.Services.AddSingleton(elsaSettings);


var backendApiConfig = new BackendApiConfig
{
	ConfigureBackendOptions = options => builder.Configuration.GetSection("Backend").Bind(options),
	ConfigureHttpClientBuilder = options => options.ApiKey = "00000000-0000-0000-0000-000000000000"
};


builder.Services.AddSingleton<ElsaBackendSettings>();
builder.Services.AddCore();
builder.Services.AddShell();
builder.Services.AddWorkflowsCore();
builder.Services.AddRemoteBackend(backendApiConfig);
builder.Services.Replace(ServiceDescriptor.Scoped<IRemoteBackendAccessor, ComponentRemoteBackendAccessor>());
builder.Services.AddLoginModule();
builder.Services.TryAddScoped<IAuthenticationProviderManager, DefaultAuthenticationProviderManager>();
builder.Services.AddWorkflowsModule();
builder.Services.AddWorkflowContextsModule();




//builder.Services.AddNotificationHandler<WFEngineGenericNotificationHandler>();
builder.Services.AddScoped<ITimeZoneProvider, LocalTimeZoneProvider>();
//builder.Services.AddActivityDisplaySettingsProvider<CustomActivityDisplaySettingsProvider>();
//builder.Services.AddActivityPortProvider<HttpRewriteRequestPortProvider>();
var app = builder.Build();
//var startupTaskRunner = app.Services.GetRequiredService<IStartupTaskRunner>();
//await startupTaskRunner.RunStartupTasksAsync();

await app.RunAsync();
