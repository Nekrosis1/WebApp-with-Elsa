using Elsa.Studio.Contracts;
using Elsa.Studio.Workflows.Domain.Models;
using Elsa.Studio.Workflows.Domain.Notifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WfDefinition = Elsa.Api.Client.Resources.WorkflowDefinitions.Models;

namespace BlazorWasm.Client.Pages
{
	public partial class WorkflowDefinitions
	{
		#region Properties
		//[Inject] private IGooTaskUIService _gooTaskUIService { get; set; } = default!;
		//[Inject] private WFEngineNotificationService _notificationService { get; set; } = default!;

		//private WfDefinition.WorkflowDefinition? selectedVersionImported = null;
		//private UITaskImport gooTaskUIImport = new();
		//public WorkflowConfiguration workflowConfiguration = new();
		//private ICollection<WorkflowDefinitionVersion>? selectedVersionDeletedCol = null;
		//private ICollection<string>? selectedWorkflowDefinitionIdDeleted = null;
		#endregion


		private void OnEditWorkflowDefinition(string definitionId)
		{
			_navManager.NavigateTo($"designer/definitions/{definitionId}/edit");
		}

		
	}
}
