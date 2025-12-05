namespace BlazorWasm.Client.Configs
{
	public class ElsaBackendSettings
	{
		public string RemoteEndpoint { get; set; } = default!;
		public string? ApiKey { get; set; }
		public string? AccessToken { get; set; }
	}
}
