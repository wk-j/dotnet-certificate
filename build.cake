#addin "wk.StartProcess"
#addin "wk.ProjectParser"

using PS = StartProcess.Processor;
using ProjectParser;

var nugetToken = EnvironmentVariable("npi");
var name = "MyWeb";

var currentDir = new DirectoryInfo(".").FullName;
var info = Parser.Parse($"src/{name}/{name}.csproj");
var publishDir = ".publish";
var version = DateTime.Now.ToString("yy.MM.dd.HHmm");
var settings = new DotNetCoreMSBuildSettings();
settings.Properties["Version"] = new string[] { version };

Task("Publish").Does(() => {
    DotNetCorePublish($"src/{name}", new DotNetCorePublishSettings {
        OutputDirectory = $"{publishDir}/W",
        MSBuildSettings = settings,
        Runtime = "linux-x64"
    });
});

Task("Pack").Does(() => {

    CleanDirectory(publishDir);
    DotNetCorePack($"src/{name}", new DotNetCorePackSettings {
        OutputDirectory = publishDir,
        MSBuildSettings = settings
    });
});

Task("Publish-NuGet")
    .IsDependentOn("Pack")
    .Does(() => {
        var nupkg = new DirectoryInfo(publishDir).GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = nugetToken
        });
});

Task("Install")
    .IsDependentOn("Pack")
    .Does(() => {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        PS.StartProcess($"dotnet tool uninstall -g {info.PackageId}");
        PS.StartProcess($"dotnet tool install   -g {info.PackageId}  --add-source {currentDir}/{publishDir} --version {version}");
    });

var target = Argument("target", "Pack");
RunTarget(target);
