#addin "Cake.Incubator"
#tool "nuget:?package=GitVersion.CommandLine"

// Task name definitions
const string TASK_DEFAULT = "Default";
const string TASK_CLEAN = "Clean";
const string TASK_GITVERSION = "GitVersion";
const string TASK_RESTORE = "Restore";
const string TASK_BUILD = "Build";
const string TASK_TEST = "Test";
const string TASK_PUBLISH = "Publish";
const string TASK_GENERATE_SQL = "GenerateSql";

string[] TASKS = new [] {
    TASK_DEFAULT,
    TASK_CLEAN,
    TASK_GITVERSION,
    TASK_RESTORE,
    TASK_BUILD,
    TASK_TEST,
    TASK_PUBLISH,
    TASK_GENERATE_SQL,
};

// Paths
const string PROJECT_PATH = "./src/Claimini.Web";
const string ARTIFACTS_PATH = "./artifacts";

// Script Arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var warningsAsErrors = Argument("warningsAsErrors", "true");

// Git version global variable
GitVersion gitVersion;

// This is the default task which will get ultimately executed (including all the tasks it depends on)
// if no other task is specified on the command-line with the -Target option
Task(TASK_DEFAULT)
    .Does(() =>
{
    foreach (string taskName in TASKS) {
        Information(taskName);
    }
});

Task(TASK_CLEAN)
    .Does(() =>
{
    CleanDirectory(ARTIFACTS_PATH);
    CleanDirectory(PROJECT_PATH + "/bin");
    CleanDirectory(PROJECT_PATH + "/obj");
});

Task(TASK_GITVERSION)
    .Does(() =>
{
    gitVersion = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = false
    });

    Information("AssemblySemVer: " + gitVersion.AssemblySemVer);
    Information("FullSemVer: " + gitVersion.FullSemVer);
    Information("NuGetVersion: " + gitVersion.NuGetVersion);
});

Task(TASK_RESTORE)
    .Does(() =>
{
    DotNetCoreRestore();
});

Task(TASK_BUILD)
    .IsDependentOn(TASK_CLEAN)
    .IsDependentOn(TASK_RESTORE)
    .IsDependentOn(TASK_GITVERSION)
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
     {
         Configuration = configuration,
         MSBuildSettings = new 	DotNetCoreMSBuildSettings {
             TreatAllWarningsAs = MSBuildTreatAllWarningsAs.Error
         },
     };

     DotNetCoreBuild(PROJECT_PATH, settings);
});

Task(TASK_TEST)
    .IsDependentOn(TASK_BUILD)
    .Does(() =>
{
    var projectFiles = GetFiles("./test/**/*.csproj");
    foreach(var file in projectFiles)
    {
        DotNetCoreTest(file.FullPath);
    }
});

Task(TASK_PUBLISH)
    .IsDependentOn(TASK_TEST)
    .IsDependentOn(TASK_BUILD)
    .Does(() =>
{
    var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp2.0",
        Configuration = configuration,
        OutputDirectory = MakeAbsolute(Directory(ARTIFACTS_PATH)),
    };

    DotNetCorePublish(PROJECT_PATH, settings);
});

// Generates an idempotent SQL migration file. This can be applied to any database version, as long as the DB exists
Task(TASK_GENERATE_SQL)
    .IsDependentOn(TASK_PUBLISH)
    .Does(() =>
{
    var dbContextName = "Claimini.Web.Data.ApplicationDbContext";
    var sqlFilePath = MakeAbsolute(File(ARTIFACTS_PATH + "/sql/IdempotentMigration.sql"));
    var arguments = "migrations script --idempotent --output " + sqlFilePath + " --context " + dbContextName;
    var processSettings = new ProcessSettings {
        Arguments = "ef " + arguments,
        WorkingDirectory = PROJECT_PATH,
    };
    var exitCode = StartProcess("dotnet.exe", processSettings);

    if (exitCode != 0) {
        throw new Exception("Program exited with exit code " + exitCode);
    }

    Information("EF Core SQL migration script generated at " + sqlFilePath);

    string scriptFileName = "ExecuteSqlFile.ps1";
    CopyFile("./scripts/" + scriptFileName, ARTIFACTS_PATH + "/sql/" + scriptFileName);
});

RunTarget(target);
