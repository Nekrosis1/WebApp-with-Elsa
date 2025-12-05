namespace BlazorWasm.Client.Pages
{
	public partial class WorkflowInstances
	{
		private void OnViewWorkflowInstance(string instanceId)
		{
			_navManager.NavigateTo($"designer/instances/{instanceId}/view");
		}
	}
}
