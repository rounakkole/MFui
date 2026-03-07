# MFui

### Credits & Thanks
```
dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor
www.github.com/twbs/bootstrap
www.github.com/vikramlearning/blazorbootstrap
www.mfapi.in/
Special thanks to the open source community!
```

### Dependencies
```
"Microsoft.AspNetCore.Components.WebAssembly" Version="10.0.0" 
"Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="10.0.0" PrivateAssets="all" 
"Blazor.Bootstrap" Version="3.5.0" 
"Newtonsoft.Json" Version="13.0.4" 
```

### Azure deployment
```
[0] Subscription = Select your free or default subscription
[0] Resource Group = Click “Create new” 

[0] New: Static Web App
[1] Enter name: <YourAppName>
[2] Plan type: Free
[3] Source: Github
[4] Select: repository
[5] App location: /
[6] Api location: <Blank>
[7] Deployment authorization policy: Deployment token
[8] Region for Azure Functions: East Asia

[0] Resource: Static Web App
[1] Manage deployment token: Copy
[2] Github: Repository >> Settings >> Secrets and variables >> Actions
[3] New repository secret: AZURE_STATIC_WEB_APPS_API_TOKEN = Paste
```







