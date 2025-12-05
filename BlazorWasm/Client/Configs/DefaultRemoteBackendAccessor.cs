using Blazored.LocalStorage;
using Elsa.Studio.Contracts;
using Elsa.Studio.Models;

namespace BlazorWasm.Client.Configs
{
	/// <summary>
	/// A default implementation of <see cref="IRemoteBackendAccessor"/> that uses the <see cref="BackendOptions"/> to determine the URL of the remote backend.
	/// </summary>
	public class ComponentRemoteBackendAccessor : IRemoteBackendAccessor
	{
		//private readonly ElsaBackendSettings _elsaBackendSettings;
		private readonly ElsaSettings _elsaSettings;
		private readonly ILocalStorageService _localStorageService;
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultRemoteBackendAccessor"/> class.
		/// </summary>
		//public ComponentRemoteBackendAccessor(ElsaBackendSettings elsaBackendSettings, ElsaSettings elsaSettings, ILocalStorageService localStorageService)
		public ComponentRemoteBackendAccessor(ElsaSettings elsaSettings, ILocalStorageService localStorageService)
		{
			//elsaBackendSettings = elsaBackendSettings;
			_elsaSettings = elsaSettings;
			_localStorageService = localStorageService;
		}

		/// <inheritdoc />
		//public RemoteBackend RemoteBackend => new(new Uri(_elsaBackendSettings.RemoteEndpoint));
		public RemoteBackend RemoteBackend => new(new Uri(_elsaSettings.Url + _elsaSettings.ElsaApi));
		//public AccessToken accessToken => new()
		//{

		//	Value = _localStorageService.GetItemAsync<string>("accessToken").ToString(),
		//};
		//public string? AccessToken { get; set; }
	}
}
