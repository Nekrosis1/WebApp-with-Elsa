using BlazorWasm.Client.Components;
using Elsa.Api.Client.Resources.WorkflowDefinitions.Models;
using Elsa.Studio.Contracts;
using Elsa.Studio.DomInterop.Contracts;
using Elsa.Studio.Workflows.Domain.Models;
using Elsa.Studio.Workflows.Domain.Notifications;
using Elsa.Studio.Workflows.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using System.Data;
using System.Net;
using System.Text.Json.Nodes;
using WfDefinition = Elsa.Api.Client.Resources.WorkflowDefinitions.Models;

namespace BlazorWasm.Client.Pages
{
	public partial class WorkflowDefinition
	{
		[Parameter]
		public string DefinitionId { get; set; } = default!;
		[Parameter]
		public Func<string, Task>? WorkflowDefinitionExecuted { get; set; }
		[Parameter]
		public Func<WfDefinition.WorkflowDefinition, Task>? WorkflowDefinitionVersionSelected { get; set; }
		private WorkflowDefinitionEditorWrapper Editor { get; set; } = new WorkflowDefinitionEditorWrapper();

		private async Task OnActivitySelected(JsonObject activity)
		{
			// Every time StateHasChanged is called, the designer will be refreshed as well, which causes it to be reinitialized.
			// To avoid this, don't call StateHasChanged anywhere on this component, but only on components that need to be refreshed.
			//await RefreshActivitySelected();
		}











		//// TEMPORARY: Will be removed when server-side events are implemented
		//private bool _skipNextConfigurationReload = false;
		//[Inject] private IGooTaskUIService _gooTaskUIService { get; set; } = default!;
		//[Inject] private IGooWorkflowConfigurationService _gooWorkflowConfigurationService { get; set; } = default!;
		//[Inject] private IRolePermissionClient _rolePermissionClient { get; set; } = default!;
		//[Inject] private IFiles Files { get; set; } = default!;
		//[Inject] private WFEngineNotificationService _notificationService { get; set; } = default!;
		//[Inject] private IWorkflowDefinitionClient _workflowDefinitionClient { get; set; } = default!;
		//[Inject] private ITasksClient _tasksClient { get; set; } = default!;
		//[Inject] private IRoleClient _roleClient { get; set; } = default!;
		//[Inject] private IUserProfileService _userProfileService { get; set; } = default!;

		//private bool _isAuthenticated = false;
		//private bool IsPopupVisible { get; set; }
		//private GenerateTaskUIMainComponent? popupComponent;
		//private Island toolbarComponent;
		//private string? _selectedActivityId => Editor.SelectedActivityId;
		//private WfDefinition.WorkflowDefinition? _selectedDefinitionVersionId;
		//private bool isButtonEnabled = false;
		//private bool isSwitchEnabled = false;
		//private WfDefinition.WorkflowDefinition? selectedVersionToSaving = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionSaved = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionToPublishing = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionPublished = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionToUnpublishing = new();
		//private WfDefinition.WorkflowDefinition? selectedVersiontoUnpublished = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionToExporting = new();
		//private WfDefinition.WorkflowDefinition? selectedVersionImported = new();
		//private ICollection<WorkflowDefinitionVersion>? selectedVersionDeletedCol = null;
		//private ICollection<string>? selectedWorkflowDefinitionIdDeleted = null;
		//public UITask gooTaskUI = new();
		//public WorkflowConfiguration workflowConfiguration = new();
		//public string ActivityId = string.Empty;

		//[Parameter]

		//[Parameter]
		//public Func<JsonObject, Task>? ActivitySelected { get; set; }
		//public IEnumerable<RoleManipulation> Roles { get; set; } = Enumerable.Empty<RoleManipulation>();
		//public IEnumerable<RoleManipulation> PermittedRoles { get; set; } = Enumerable.Empty<RoleManipulation>();
		//private WorkflowDefinitionEditorWrapper Editor { get; set; } = new WorkflowDefinitionEditorWrapper();
		//private string? _EditorDefinitionId => Editor.DefinitionId;
		//public WfDefinition.WorkflowDefinition? GetSelectedWorkflowDefinitionVersion() => Editor.GetSelectedWorkflowDefinitionVersion();
		//private bool showInputFile = false;
		//private UITaskImport gooTaskUIImport = new();
		//private UITaskImport gooTaskUIRevertingImport = new();
		//private UITaskImport gooTaskUIRevertedImport = new();


		#region Methods




		//private async Task LoadWorkflowDefinitionVersion()
		//{
		//	// TEMPORARY: Skip reload after local updates (will be removed when server-side events are implemented)
		//	if (_skipNextConfigurationReload)
		//	{
		//		_skipNextConfigurationReload = false;
				
		//		// Only update switch state, without reloading configuration
		//		if (!string.IsNullOrEmpty(_EditorDefinitionId))
		//		{
		//			WorkflowDefinitionSummary AvailableDefinition = await _workflowDefinitionClient.GetById(_EditorDefinitionId);
		//			if (AvailableDefinition.IsPublished || !AvailableDefinition.IsLatest && !AvailableDefinition.IsPublished)
		//			{
		//				ToggleSwitchState(false);
		//			}
		//			else
		//			{
		//				ToggleSwitchState(true);
		//			}
		//		}
		//		return;
		//	}
			
		//	// Normal reload logic (unchanged for real navigation)
		//	if (!string.IsNullOrEmpty(_EditorDefinitionId))
		//	{
		//		WorkflowDefinitionSummary AvailableDefinition = await _workflowDefinitionClient.GetById(_EditorDefinitionId);
		//		await GetWorkflowConfiguration(AvailableDefinition.Id);
		//		if (AvailableDefinition.IsPublished || !AvailableDefinition.IsLatest && !AvailableDefinition.IsPublished)
		//		{
		//			ToggleSwitchState(false);
		//		}
		//		else
		//		{
		//			ToggleSwitchState(true);
		//		}
		//	}
		//}


		//private async Task RefreshActivitySelected()
		//{
		//	if (!string.IsNullOrEmpty(_selectedActivityId))
		//	{
		//		UITask? tempGooTaskUI = GetDefintionVersionId();

		//		ActivityId = _selectedActivityId;
		//		if (tempGooTaskUI == null)
		//		{
		//			ToggleButtonState(false);
		//			return;
		//		}
		//		tempGooTaskUI.ActivityId = ActivityId;
		//		#region check if exists
		//		ToggleButtonState(true);
		//		if (!await IsGooTaskUIExists(tempGooTaskUI))
		//		{
		//			PermittedRoles = Enumerable.Empty<RoleManipulation>();
		//			gooTaskUI = new UITask { ActivityId = ActivityId, DefinitionId = DefinitionId, DefinitionVersionId = tempGooTaskUI.DefinitionVersionId, IsLatest = tempGooTaskUI.IsLatest, IsPublished = tempGooTaskUI.IsPublished };
		//			gooTaskUI.Version = tempGooTaskUI.Version;

		//			if (workflowConfiguration != null && workflowConfiguration.InstantWorkflow)
		//			{
		//				gooTaskUI.KeepUIOpen = true;
		//			}
		//		}
		//		#endregion
		//		await FetchPermissions();
		//	}
		//	else
		//	{
		//		ToggleButtonState(false);
		//	}
		//}

		//private UITask? GetDefintionVersionId()
		//{
		//	_selectedDefinitionVersionId = GetSelectedWorkflowDefinitionVersion();
		//	UITask? tempGooTaskUI = null;
		//	if (_selectedDefinitionVersionId != null)
		//	{
		//		tempGooTaskUI = new UITask { DefinitionId = _selectedDefinitionVersionId.DefinitionId, DefinitionVersionId = _selectedDefinitionVersionId.Id, IsLatest = _selectedDefinitionVersionId.IsLatest, IsPublished = _selectedDefinitionVersionId.IsPublished };
		//		tempGooTaskUI.Version = _selectedDefinitionVersionId.Version;
		//	}
		//	return tempGooTaskUI;
		//}

		//#region on Elsa Notifications
		//private void HandleNotification(INotification notification)
		//{
		//	switch (notification)
		//	{
		//		case WorkflowDefinitionSaving saving:
		//			selectedVersionToSaving = saving.WorkflowDefinition;
		//			break;
		//		case WorkflowDefinitionSaved saved:
		//			selectedVersionSaved = saved.WorkflowDefinition;
		//			Task.Run(AfterSaveEvent);
		//			break;
		//		case WorkflowDefinitionPublishing publishing:
		//			selectedVersionToPublishing = publishing.WorkflowDefinition;
		//			break;
		//		case WorkflowDefinitionPublished published:
		//			selectedVersionPublished = published.WorkflowDefinition;
		//			Task.Run(AfterPublishEvent);
		//			break;
		//		case WorkflowDefinitionRetracting retracting: // unpublish
		//			selectedVersionToUnpublishing = retracting.WorkflowDefinition;
		//			break;
		//		case WorkflowDefinitionRetracted retracted: // unpublished
		//			selectedVersiontoUnpublished = retracted.WorkflowDefinition;
		//			Task.Run(OnRetracted);
		//			break;
		//		case WorkflowDefinitionExporting exporting:
		//			selectedVersionToExporting = exporting.WorkflowDefinition;
		//			Task.Run(OnExporting);
		//			break;
		//		case ImportedWorkflowDefinition imported:
		//			selectedVersionImported = imported.WorkflowDefinition;
		//			break;
		//		case ImportedFile importedfile:
		//			IBrowserFile file = importedfile.File;
		//			if (file.ContentType == "application/x-zip-compressed" || file.ContentType == "application/zip")
		//			{
		//				Task.Run(() => OnImported(file));
		//			}
		//			break;
		//		case WorkflowDefinitionVersionDeleted definitionVersionDeleted:
		//			Console.WriteLine($"WF Version to delete: '{definitionVersionDeleted.WorkflowDefinitionVersion}'");
		//			Task.Run(() => _gooTaskUIService.HandleAfterDeleteEvent(FilterTypes.DefinitionVersionId, definitionVersionDeleted.WorkflowDefinitionVersion.WorkflowDefinitionVersionId));
		//			break;
		//		case WorkflowDefinitionDeleted definitionDeleted:
		//			Console.WriteLine($"WF to delete: '{definitionDeleted.WorkflowDefinitionId}'");
		//			Task.Run(() => _gooTaskUIService.HandleAfterDeleteEvent(FilterTypes.DefinitionId, definitionDeleted.WorkflowDefinitionId));
		//			break;
		//		case BulkWorkflowDefinitionVersionsDeleted bulkWorkflowDefinitionVersionsDeleted:
		//			selectedVersionDeletedCol = bulkWorkflowDefinitionVersionsDeleted.Versions;
		//			Console.WriteLine($"WF Versions to delete: '{selectedVersionDeletedCol.Count}'");
		//			foreach (WorkflowDefinitionVersion version in selectedVersionDeletedCol)
		//			{
		//				Task.Run(() => _gooTaskUIService.HandleAfterDeleteEvent(FilterTypes.DefinitionVersionId, version.WorkflowDefinitionVersionId));
		//			}
		//			break;
		//		case BulkWorkflowDefinitionsDeleted bulkWorkflowDefinitionsDeleted:
		//			selectedWorkflowDefinitionIdDeleted = bulkWorkflowDefinitionsDeleted.WorkflowDefinitionIds;
		//			Console.WriteLine($"WFs to delete: '{selectedWorkflowDefinitionIdDeleted.Count}'");
		//			foreach (string definitionId in selectedWorkflowDefinitionIdDeleted)
		//			{
		//				Task.Run(() => _gooTaskUIService.HandleAfterDeleteEvent(FilterTypes.DefinitionId, definitionId));
		//			}
		//			break;
		//		//case WorkflowDefinitionReverting reverting: // rollback
		//		//	Task.Run(OnWorkflowDefinitionReverting);
		//		default:
		//			break;
		//	}
		//}
		//#endregion

		//#region Save / Publish / Unpublish
		//private async Task AfterSaveEvent()
		//{
		//	if (selectedVersionSaved != null && selectedVersionToSaving != null)
		//	{
		//		if (selectedVersionToSaving.IsLatest && selectedVersionToSaving.IsPublished &&
		//			selectedVersionSaved.IsLatest && !selectedVersionSaved.IsPublished) // Case: from LatestAndPublished to new version LatestDraft version
		//		{
		//			// create new gooTaskUI entries with new DefintionVersionIds
		//			bool creatingSuccessfully = await _gooTaskUIService.CreateGooTaskUIsFromDefinitionVersionId(selectedVersionToSaving.Id, selectedVersionSaved.Id, selectedVersionSaved.IsPublished);
		//			workflowConfiguration.DefinitionVersionId = selectedVersionSaved.Id;
		//			workflowConfiguration.DefinitionId = selectedVersionSaved.DefinitionId;
		//			workflowConfiguration.Version = selectedVersionSaved.Version;
		//			creatingSuccessfully = await _gooWorkflowConfigurationService.UpsertGooWorkflowConfigurationIsFromDefinitionVersionId(workflowConfiguration);
		//			PermissionVersionUpdate permissionVersionUpdate = new()
		//			{
		//				OldDefinitionVersionId = selectedVersionToSaving.Id,
		//				NewDefinitionVersionId = selectedVersionSaved.Id,
		//			};
		//			await _rolePermissionClient.PatchPermissionToNewVersion(permissionVersionUpdate);
		//		}
		//		else if (selectedVersionToSaving.IsLatest && !selectedVersionToSaving.IsPublished &&
		//				selectedVersionSaved.IsLatest && !selectedVersionSaved.IsPublished) // Case: from LatestDraft to same LatestDraft version
		//		{
		//			#region workflow configuration
		//			// Preserve current InstantWorkflow value from UI
		//			bool currentInstantWorkflow = workflowConfiguration.InstantWorkflow;
		//			workflowConfiguration.DefinitionVersionId = selectedVersionSaved.Id;
		//			workflowConfiguration.DefinitionId = selectedVersionSaved.DefinitionId;
		//			workflowConfiguration.Version = selectedVersionSaved.Version;
		//			workflowConfiguration.InstantWorkflow = currentInstantWorkflow;
					
		//			// TEMPORARY: Set flag before upsert
		//			_skipNextConfigurationReload = true;
		//			bool savingSuccessfully = await _gooWorkflowConfigurationService.UpsertGooWorkflowConfigurationIsFromDefinitionVersionId(workflowConfiguration);
		//			#endregion
		//		}
		//	}
		//	else
		//	{
		//		Console.WriteLine("selectedVersionToSaving or selectedVersionSaved was null");
		//	}
		//	// refresh
		//	await RefreshActivitySelected();
		//	await LoadWorkflowDefinitionVersion();
		//}

		//private async Task AfterPublishEvent()
		//{
		//	if (selectedVersionPublished != null && selectedVersionToPublishing != null)
		//	{
		//		if (selectedVersionPublished != null)
		//		{
		//			// Preserve current InstantWorkflow value from UI before any operations
		//			bool currentInstantWorkflow = workflowConfiguration.InstantWorkflow;
		//			Console.WriteLine($"DEBUG: AfterPublishEvent - InstantWorkflow value before upsert: {currentInstantWorkflow}");
					
		//			if (selectedVersionToPublishing.IsLatest && !selectedVersionToPublishing.IsPublished &&
		//				selectedVersionPublished.IsLatest && selectedVersionPublished.IsPublished) // Case: from LatestDraft to LatestAndPublished
		//			{
		//				// publish all gooTaskUI
		//				bool savingSuccessfully = await _gooTaskUIService.UpdateGooTaskUIsFromDefinitionVersionId(selectedVersionPublished.Id, true);
		//				#region workflow configuration
		//				workflowConfiguration.DefinitionVersionId = selectedVersionPublished.Id;
		//				workflowConfiguration.DefinitionId = selectedVersionPublished.DefinitionId;
		//				workflowConfiguration.Version = selectedVersionPublished.Version;
		//				workflowConfiguration.InstantWorkflow = currentInstantWorkflow;
						
		//				// TEMPORARY: Set flag before upsert
		//				_skipNextConfigurationReload = true;
		//				savingSuccessfully = await _gooWorkflowConfigurationService.UpsertGooWorkflowConfigurationIsFromDefinitionVersionId(workflowConfiguration);
		//				#endregion
		//			}
		//			else if (selectedVersionToPublishing.IsLatest && selectedVersionToPublishing.IsPublished &&
		//					 selectedVersionPublished.IsLatest && selectedVersionPublished.IsPublished) // Case: LatestAndPublished to new LatestAndPublished version
		//			{
		//				// create new gooTaskUI entries with new DefintionVersionIds
		//				bool creatingSuccessfully = await _gooTaskUIService.CreateGooTaskUIsFromDefinitionVersionId(selectedVersionToPublishing.Id, selectedVersionPublished.Id, selectedVersionToPublishing.IsPublished);
		//				workflowConfiguration.DefinitionVersionId = selectedVersionPublished.Id;
		//				workflowConfiguration.DefinitionId = selectedVersionPublished.DefinitionId;
		//				workflowConfiguration.Version = selectedVersionPublished.Version;
		//				workflowConfiguration.InstantWorkflow = currentInstantWorkflow;
						
		//				// TEMPORARY: Set flag before upsert
		//				_skipNextConfigurationReload = true;
		//				creatingSuccessfully = await _gooWorkflowConfigurationService.UpsertGooWorkflowConfigurationIsFromDefinitionVersionId(workflowConfiguration);
		//				PermissionVersionUpdate permissionVersionUpdate = new()
		//				{
		//					OldDefinitionVersionId = selectedVersionToPublishing.Id,
		//					NewDefinitionVersionId = selectedVersionPublished.Id,
		//				};
		//				await _rolePermissionClient.PatchPermissionToNewVersion(permissionVersionUpdate);
		//			}
		//		}
		//	}
		//	else
		//	{
		//		Console.WriteLine("selectedVersionToPublishing or selectedVersionPublished was null");
		//	}
		//	// refresh
		//	await RefreshActivitySelected();
		//	await LoadWorkflowDefinitionVersion();
		//}

		//private async Task OnRetracted()
		//{
		//	if (selectedVersionToUnpublishing != null && selectedVersiontoUnpublished != null)
		//	{
		//		if (selectedVersionToUnpublishing.IsLatest && selectedVersionToUnpublishing.IsPublished &&
		//			selectedVersiontoUnpublished.IsLatest && !selectedVersiontoUnpublished.IsPublished) // Case: from LatestAndPublished to same version LatestDraft version
		//		{
		//			// unpublish
		//			bool savingSuccessfully = await _gooTaskUIService.UpdateGooTaskUIsFromDefinitionVersionId(selectedVersiontoUnpublished.Id, false);
		//		}
		//	}
		//	else
		//	{
		//		Console.WriteLine("selectedVersionToUnpublishing or selectedVersiontoUnpublished was null");
		//	}
		//	// refresh
		//	await RefreshActivitySelected();
		//	await LoadWorkflowDefinitionVersion();
		//}
		//#endregion

		//#region Export / Import
		//private async Task OnExporting()
		//{
		//	if (selectedVersionToExporting != null)
		//	{
		//		HttpResponseMessage response = await _gooTaskUIService.ExportGooUITasks(selectedVersionToExporting.Id);

		//		if (response.StatusCode == HttpStatusCode.OK)
		//		{
		//			using (Stream stream = await response.Content.ReadAsStreamAsync())
		//			{
		//				string fileName = $"{selectedVersionToExporting.Id}_gooUITaskDefinitions.zip";
		//				await Files.DownloadFileFromStreamAsync(fileName, stream);
		//			}
		//		}
		//		else if (response.StatusCode == HttpStatusCode.NoContent)
		//		{
		//			Console.WriteLine($"Export without gooUI.");
		//		}
		//		else
		//		{
		//			Console.WriteLine($"Failed to download file: {response.ReasonPhrase}");
		//		}
		//	}
		//	else
		//	{
		//		Console.WriteLine("selectedVersionToExporting was null");
		//	}
		//}

		//private async Task OnImported(IBrowserFile? file)
		//{
		//	selectedVersionImported = GetSelectedWorkflowDefinitionVersion();
		//	if (selectedVersionImported != null)
		//	{
		//		gooTaskUIImport.DefinitionId = selectedVersionImported.DefinitionId;
		//		gooTaskUIImport.DefinitionVersionId = selectedVersionImported.Id;
		//		gooTaskUIImport.Version = selectedVersionImported.Version;
		//		gooTaskUIImport.IsLatest = selectedVersionImported.IsLatest;
		//		gooTaskUIImport.IsPublished = selectedVersionImported.IsPublished;

		//		bool success = await _gooTaskUIService.ImportWorkflowTasks(gooTaskUIImport, file, workflowConfiguration);

		//		if (!success)
		//		{
		//			Console.WriteLine("Workflow task import failed.");
		//		}
		//	}
		//	else
		//	{
		//		Console.WriteLine("selectedVersionImported was null");
		//	}

		//	// refresh
		//	await RefreshActivitySelected();
		//	selectedVersionImported = new();
		//	await LoadWorkflowDefinitionVersion();
		//}
		//#endregion

		////private async Task OnWorkflowDefinitionReverting(WorkflowDefinitionVersionEventArgs workflowDefinitionVersionEvent)
		////{
		////	selectedVersionToSaving = GetSelectedWorkflowDefinitionVersion();
		////	gooTaskUIRevertingImport.IsPublished = selectedVersionToSaving.IsPublished;
		////	gooTaskUIRevertingImport.IsLatest = selectedVersionToSaving.IsLatest;
		////	gooTaskUIRevertingImport.Version = workflowDefinitionVersionEvent.Version;
		////	gooTaskUIRevertingImport.DefinitionId = workflowDefinitionVersionEvent.WorkflowDefinitionId;
		////	gooTaskUIRevertingImport.DefinitionVersionId = workflowDefinitionVersionEvent.WorkflowDefinitionVersionId;
		////}

		////private async Task OnWorkflowDefinitionReverted(WorkflowDefinitionVersionEventArgs workflowDefinitionVersionEvent)
		////{
		////	selectedVersionSaved = GetSelectedWorkflowDefinitionVersion();
		////	gooTaskUIRevertedImport.IsPublished = selectedVersionSaved.IsPublished;
		////	gooTaskUIRevertedImport.IsLatest = selectedVersionSaved.IsLatest;
		////	gooTaskUIRevertedImport.Version = workflowDefinitionVersionEvent.Version;
		////	gooTaskUIRevertedImport.DefinitionId = workflowDefinitionVersionEvent.WorkflowDefinitionId;
		////	gooTaskUIRevertedImport.DefinitionVersionId = workflowDefinitionVersionEvent.WorkflowDefinitionVersionId;

		////	HttpResponseMessage response = await _gooTaskUIService.RevertSpecificVersionGooUITasks(gooTaskUIRevertingImport, gooTaskUIRevertedImport);

		////	if (response.IsSuccessStatusCode)
		////	{
		////		string result = await response.Content.ReadAsStringAsync();
		////		Console.WriteLine(result);
		////		workflowConfiguration.DefinitionVersionId = workflowDefinitionVersionEvent.WorkflowDefinitionVersionId;
		////		workflowConfiguration.DefinitionId = workflowDefinitionVersionEvent.WorkflowDefinitionId;
		////		workflowConfiguration.Version = workflowDefinitionVersionEvent.Version;
		////		bool creatingWorkflowConfigurationSuccessfully = await _gooWorkflowConfigurationService.UpsertGooWorkflowConfigurationIsFromDefinitionVersionId(workflowConfiguration);
		////	}
		////	else
		////	{
		////		Console.WriteLine($"Error when importing tasks. Error Message: '{await response.Content.ReadAsStringAsync()}'");
		////	}

		////	// refresh
		////	await RefreshActivitySelected();
		////}

		//#region Toggle Button and Switch
		//private void ToggleButtonState(bool state)
		//{
		//	isButtonEnabled = state;
		//	toolbarComponent.Refresh();
		//}

		//private void ToggleSwitchState(bool state)
		//{
		//	isSwitchEnabled = state;
		//	toolbarComponent.Refresh();
		//}
		//#endregion

		//#region Popup logic
		//private async void OpenPopup()
		//{
		//	IsPopupVisible = true;
		//}

		//private async Task PopupVisibleChanged(bool popupVisible)
		//{
		//	IsPopupVisible = popupVisible;
		//}
		//#endregion

		//#region Get UITask and WF Configuration
		//private async Task<bool> IsGooTaskUIExists(UITask tempGooTaskUI)
		//{
		//	try
		//	{
		//		gooTaskUI = await _tasksClient.GetTask(tempGooTaskUI.ActivityId, tempGooTaskUI.DefinitionVersionId) ?? new();
		//		return gooTaskUI?._id != null;
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine($"No UITask data found.");
		//		return false;
		//	}
		//}

		//private async Task GetWorkflowConfiguration(string definitionVersionId)
		//{
		//	try
		//	{
		//		// Preserve current InstantWorkflow value in case configuration doesn't exist yet
		//		bool currentInstantWorkflow = workflowConfiguration.InstantWorkflow;
				
		//		#region check if exists
		//		WorkflowConfiguration workflowConfigurationTemp = await _gooWorkflowConfigurationService.GetGooWorkflowConfiguration(definitionVersionId);
		//		bool isExists = workflowConfigurationTemp._id != null;
		//		if (isExists)
		//		{
		//			workflowConfiguration._id = workflowConfigurationTemp._id;
		//			workflowConfiguration.DefinitionId = workflowConfigurationTemp.DefinitionId;
		//			workflowConfiguration.DefinitionVersionId = workflowConfigurationTemp.DefinitionVersionId;
		//			workflowConfiguration.Version = workflowConfigurationTemp.Version;
		//			workflowConfiguration.InstantWorkflow = workflowConfigurationTemp.InstantWorkflow;
		//		}
		//		#endregion
		//		#region set other parameters from designer
		//		else if (!isExists && selectedVersionSaved != null && selectedVersionSaved.Id == definitionVersionId)
		//		{
		//			workflowConfiguration.DefinitionId = selectedVersionSaved.DefinitionId;
		//			workflowConfiguration.DefinitionVersionId = definitionVersionId;
		//			workflowConfiguration.Version = selectedVersionSaved.Version;
		//			// Preserve the current InstantWorkflow value when creating new configuration
		//			workflowConfiguration.InstantWorkflow = currentInstantWorkflow;
		//		}
		//		else if (!isExists)
		//		{
		//			// Fallback for brand new workflows: preserve InstantWorkflow value even if no saved version exists yet
		//			// This ensures the UI state is maintained during the first publish
		//			WfDefinition.WorkflowDefinition? currentVersion = GetSelectedWorkflowDefinitionVersion();
		//			if (currentVersion != null)
		//			{
		//				workflowConfiguration.DefinitionId = currentVersion.DefinitionId;
		//				workflowConfiguration.DefinitionVersionId = definitionVersionId;
		//				workflowConfiguration.Version = currentVersion.Version;
		//			}
		//			workflowConfiguration.InstantWorkflow = currentInstantWorkflow;
		//		}
		//		#endregion
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine($"No WorkflowConfigration data found.");
		//	}
		//}
		//#endregion

		//#region Permissions and Roles
		//public async Task FetchPermissions()
		//{
		//	try
		//	{
		//		ActivityPermission thisActivity = new()
		//		{
		//			DefinitionVersionId = gooTaskUI.DefinitionVersionId,
		//			DefinitionId = gooTaskUI.DefinitionId,
		//			ActivityId = gooTaskUI.ActivityId,
		//		};
		//		List<RoleManipulation> roleManipulations = await _roleClient.GetByActivityPermission(thisActivity);
		//		PermittedRoles = roleManipulations;
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("Role Permission fetch failed");
		//		return;
		//	}
		//}

		//private async Task FetchRoles()
		//{
		//	try
		//	{
		//		List<RoleManipulation> allRoles = await _roleClient.GetAll();
		//		Roles = allRoles.Where(role => role.Name != Consts.Admin && role.Name != Consts.Designer).ToList();
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("Role Fetch Failed");
		//	}
		//}
		//#endregion

		//#region Dispose
		//public void Dispose()
		//{
		//	_notificationService.OnNotificationReceived -= HandleNotification;
		//	StartLoading();
		//}
		#endregion

	}
}