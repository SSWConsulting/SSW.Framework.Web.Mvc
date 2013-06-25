
# pack and publish to nuget

.\Assets\nuget.exe pack .\package\SSW.Framework.Web.Mvc.nuspec
.\Assets\nuget.exe setApiKey f82b7b27-6972-4e46-b2d6-4adecc6f8c74
.\Assets\nuget.exe push SSW.Framework.Web.Mvc.1.0.2.nupkg