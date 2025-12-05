using Microsoft.AspNetCore.Components;

namespace BlazorWasm.Client.Pages
{
	public partial class WorkflowInstance
	{
		/// <summary>
		/// The ID of the workflow instance 
		/// </summary>
		[Parameter] public string WorkflowInstanceId { get; set; } = default!;
	}
}
