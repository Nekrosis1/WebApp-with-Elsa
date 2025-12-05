A Very basic Web App using Elsa Studio and Elsa Core.
Showcasing invisible activity boxes when server rendering.


To switch between Interactive Server rendering and Wasm rendering:
Switch in App.razor in Server project between these:
    `<Routes @rendermode="InteractiveWebAssembly" />`  
    `<Routes @rendermode="InteractiveAuto" />`  
    `<Routes @rendermode="InteractiveServer" />`  

WebAssembly rendering also requires the lines
@inherits BackendComponentBase
to be commented back in in the files  
`Components/ThemedComponentWrapper.razor`  
`Components/WorkflowDefinitionEditiorWrapper.razor`  
`Components/WorkflowDefinitionListWrapper.razor`  
`Components/WorkflowInstanceListWrapper.razor`  
`Components/WorkflowInstanceViewerWrapper.razor`  
  
while InteractiveServer requires them to be commented out.

I switched to sqlite, which should make this work, db file is included in repo.
