using BlazorWasm.Server.Configs;
using Elsa.Studio.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Services
{
	public abstract class BackendComponentBase : StudioComponentBase
	{
		[Parameter] public string? RemoteEndpoint { get; set; }
		[Parameter] public string? ApiKey { get; set; }
		[Parameter] public string? AccessToken { get; set; }
		[Inject] private ElsaBackendSettings BackendService { get; set; } = default!;

		protected override void OnInitialized()
		{
			if (!string.IsNullOrWhiteSpace(RemoteEndpoint))
				BackendService.RemoteEndpoint = RemoteEndpoint;

			if (!string.IsNullOrWhiteSpace(ApiKey))
				BackendService.ApiKey = ApiKey;

			if (!string.IsNullOrWhiteSpace(AccessToken))
				BackendService.AccessToken = AccessToken;
		}
	}
}
