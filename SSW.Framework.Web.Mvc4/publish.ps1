



# get api key
$p = Split-Path $MyInvocation.MyCommand.Path
$apiKey = [IO.File]::ReadAllText("$p\Nuget_Api.key")


# pack and publish to nuget
.\Assets\nuget.exe pack .\package\SSW.Framework.Web.Mvc.nuspec
.\Assets\nuget.exe setApiKey $apiKey
.\Assets\nuget.exe push SSW.Framework.Web.Mvc.1.0.2.nupkg

