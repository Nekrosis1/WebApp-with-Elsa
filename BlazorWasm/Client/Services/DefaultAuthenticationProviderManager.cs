using BlazorWasm.Client.Configs;
using Elsa.Studio.Contracts;

namespace BlazorWasm.Client.Services;

//new in 3.6
public class DefaultAuthenticationProviderManager : IAuthenticationProviderManager
{
	private readonly string? _apiKey;

	public DefaultAuthenticationProviderManager(ElsaSettings elsaSettings)
	{
		_apiKey = "00000000-0000-0000-0000-000000000000";
	}

	public Task<string?> GetAuthenticationTokenAsync(string? tokenName, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(_apiKey);
	}
}
