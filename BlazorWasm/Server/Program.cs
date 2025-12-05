using BlazorWasm.Server;
using BlazorWasm.Server.Configs;
using BlazorWasm.Server.Services;
using Elsa.Alterations.Extensions;
using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;
using Elsa.Sql.Extensions;
using Elsa.Studio.Contracts;
using Elsa.Studio.Core.BlazorServer.Extensions;
using Elsa.Studio.Extensions;
using Elsa.Studio.Models;
using Elsa.Studio.Shell.Extensions;
using Elsa.Studio.WorkflowContexts.Extensions;
using Elsa.Studio.Workflows.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
	.AddInteractiveWebAssemblyComponents()
	.AddInteractiveServerComponents();


#region CORS
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	builder.AllowAnyOrigin() // deactivated
		   .AllowAnyMethod()
		   .AllowAnyHeader()
		   .WithExposedHeaders("x-elsa-workflow-instance-id"));
});
#endregion

var backendApiConfig = new BackendApiConfig
{
	ConfigureBackendOptions = options => builder.Configuration.GetSection("Backend").Bind(options),
	ConfigureHttpClientBuilder = options => options.ApiKey = "00000000-0000-0000-0000-000000000000"
};

ElsaSettings elsaSettings = builder.Configuration.GetSection(nameof(ElsaSettings)).Get<ElsaSettings>()!;
builder.Services.AddSingleton(elsaSettings);

builder.Services.AddHttpClient("BlazorWasm.ServerAPI", client =>
{
	client.BaseAddress = new Uri(elsaSettings.Url);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWasm.ServerAPI"));



// Studio
builder.Services.AddSingleton<ElsaBackendSettings>();
builder.Services.AddCore();
builder.Services.AddShell();
builder.Services.AddWorkflowsCore();
builder.Services.AddRemoteBackend(backendApiConfig);
builder.Services.Replace(ServiceDescriptor.Scoped<IRemoteBackendAccessor, ComponentRemoteBackendAccessor>());
//builder.Services.AddLoginModule();
builder.Services.AddScoped<IAuthenticationProviderManager, DefaultAuthenticationProviderManager>();
builder.Services.AddWorkflowsModule();
builder.Services.AddWorkflowContextsModule();

// WF Engine
builder.Services.AddElsa(elsa =>
{
	elsa.UseWorkflowRuntime(runtime =>
	{
		runtime.UseEntityFrameworkCore(ef => ef.UseSqlite("Data Source=elsa.sqlite.db;Cache=Shared;"));
		runtime.UseCache();
	});
	elsa.UseSql();
	elsa.UseWorkflowManagement(management =>
	{
		management.UseEntityFrameworkCore(ef => ef.UseSqlite("Data Source=elsa.sqlite.db;Cache=Shared;"));
		management.UseCache();
	});
	elsa.UseIdentity(identity =>
	{
		identity.TokenOptions = options => options.SigningKey = "sufficiently-large-secret-signing-key"; // This key needs to be at least 256 bits long.
		identity.UseAdminUserProvider();
	});
	// Configure ASP.NET authentication/authorization.
	elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());
	elsa.UseScheduling();
	elsa.UseHttp(options => options.ConfigureHttpOptions = httpOptions => httpOptions.BaseUrl = new("https://localhost:7234/"));
	elsa.UseCSharp();
	elsa.UseJavaScript((Elsa.Expressions.JavaScript.Features.JavaScriptFeature options) => { });
	elsa.AddSwagger();

	elsa.UseWorkflowsApi();
	elsa.UseAlterations();
	elsa.UseEmail();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}




app.UseHttpsRedirection();
//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

// AUTH & ROUTING
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ELSA
app.UseWorkflowsApi();
app.UseJsonSerializationErrorHandler();
app.UseWorkflows();

app.UseAntiforgery();
app.MapRazorPages();
app.MapRazorComponents<App>()
	.AddInteractiveWebAssemblyRenderMode()
	.AddInteractiveServerRenderMode()
	.AddAdditionalAssemblies(typeof(BlazorWasm.Client._Imports).Assembly);
app.MapControllers();

app.Run();
