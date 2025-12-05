namespace BlazorWasm.Server.Configs
{
	public class ElsaSettings
	{
		public string Url { get; set; }
		public string ElsaApi { get; set; }
		public int? HttpTimeoutSeconds { get; set; }
		public int? ClientApiTimeoutSeconds { get; set; }
		public int? ElsaApiTimeoutSeconds { get; set; }
	}
}
