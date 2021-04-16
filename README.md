# T4LogS - T4 Logs System

## What is T4LogS?
1. The main target: Support for storing Exception information arising from many different projects becomes easier with centralized storage.

   [View More Exception Class](https://docs.microsoft.com/en-us/dotnet/api/system.exception)
   
1. Microservices architecture, T4LogS support recording Exception from Small-Services with centralized storage.
1. In addition to the main information from Exception: Message, StackTrace. T4LogS also stores more information from Exception's properties.
1. Support classification T4LogSType: Error, Warning, Log, Info, Debug, Audit, Time, Other.
1. T4LogSWriteTime: Support for programmers to record the execution time of any Function (Method) in units: ns, ms, s.
1. Future, T4LogS will develop classes for methods Get, Read log files.

### Target Framework
**.NET Standard 2.0**

View More:

1. <https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md>
1. <https://docs.microsoft.com/en-us/dotnet/standard/net-standard>

## Installation
### Nuget Package


## Using the Code

- Initial Configuration (CALLING ONLY ONE in Project)
```csharp
T4LogS.Core.T4LogSOptions option = new Core.T4LogSOptions() {
   //File custom: view live file exception
   BreakLineCustom = "<br>",
   ExtensionCustom = "html",
   SaveFileCustom = true,
   FormatTextCustom = "",//Required to use the correct format
   //File custom: End

   SaveDetails = true,// View details: get all property exception
   LogsPath = Server.MapPath("T4LogS"),//Folder save file log
   options.SaveFileJson = true,// Accept save file json
};
```

- Using try-catch in code
```csharp
try
{
    int.Parse("a");
}
catch (Exception ex)
{
    //Write log fast
    new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error, "Description (Optional, default String.Empty)").Dispose();
    //or write append detail
    using (var log = new T4LogS.Core.T4LogSWriteException(ex, Core.T4LogSType.Error))
    {
        log.AppendDetail(new Core.T4LogSDetail()
        {
            Name = "Example Append Name",
            TargetName = "Example Append Target Name",
            Value = "Example Append Value",
        });
    }
}
```

- Using UseExceptionHandler for ASP.NET Core
```csharp
//File Startup.cs

public void ConfigureServices(IServiceCollection services){
   services.AddT4LogS(options =>
   {
      //Initial Configuration
      options.SaveFileCustom = true;
      options.SaveDetails = true;
      options.SaveFileJson = true;
   });
   ...
   services.AddMvc();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
   if (env.IsDevelopment())
   {
       app.UseDeveloperExceptionPage();
   }
   else
   {
      //app.UseExceptionHandler("/Home/Error");//Default ASP.NET CORE
      app.UseExceptionHandler((Action<IApplicationBuilder>)(options =>
      {
         options.Run((RequestDelegate)(async (context) =>
         {
            var error = await T4LogSExtensions.Execute(context, "Description (Optional, default String.Empty). Example: Username login from SESSION");
            System.Diagnostics.Debug.WriteLine(error.Exception);//Optional: Write Debug
            await context.Response.WriteAsync(error.Object.Message);//Optional: Write Response page
         }));
      }));
   }
}
```

- Using ASP.NET MVC Framework
```csharp
//File Global.asax
protected void Application_Start()
{
    ...
    T4LogS.Core.T4LogSOptions option = new Core.T4LogSOptions() {
        LogsPath = Server.MapPath("T4LogS")
    };
}

protected void Application_Error(object sender, EventArgs e)
{
    using (var t4log = new Core.T4LogSWriteException(Server.GetLastError(), Core.T4LogSType.Error, "Description (Optional, default String.Empty). Example: Username login from SESSION"))
    {
        System.Diagnostics.Debug.WriteLine(t4log.Exception);//Optional: Write Debug
    }
}
```

## License
This article, along with any associated source code and files, is licensed under MIT
