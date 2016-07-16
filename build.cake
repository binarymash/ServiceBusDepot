#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=xunit.runner.console"

var compileConfig = Argument("configuration", "Debug");
var target = Argument("target", "Default");
var artifactsDir = Directory("Artifacts");

// version
var semVer = "0.0.0";

// unit testing
var artifactsForUnitTestsDir = artifactsDir + Directory("UnitTests");
var unitTestAssemblies = @".\src\ServiceBusDepot.UnitTests";
var openCoverSettings = new OpenCoverSettings();
var minCodeCoverage = 95d;

Task("Default")
	.IsDependentOn("Compile")
	.IsDependentOn("RunUnitTestsCoverageReport")
	.Does(() =>
	{
	});

Task("Clean")
	.Does(() =>
	{
		EnsureDirectoryExists(artifactsDir);
		CleanDirectories(artifactsDir);
		DotNetBuild(@".\src\ServiceBusDepot.sln", settings => settings.WithTarget("Clean"));
	});
	
Task("GetVersion")
	.Does(() =>
	{
	    var gitVersion = GitVersion();
		semVer = gitVersion.SemVer;
		Information("SemVer according to GitVersion: " + semVer);
	});

Task("Compile")
	.IsDependentOn("Clean")
	.IsDependentOn("GetVersion")
	.Does(() =>
	{
		Information("Build configuration is " + compileConfig);
		
		DotNetBuild(@".\src\ServiceBusDepot.sln", settings =>
			settings
				.SetConfiguration("Debug")
				.WithTarget("Build")
				.WithProperty("TreatWarningsAsErrors","true"));
	});

Task("RunUnitTests")
	.IsDependentOn("Compile")
	.Does(() =>
	{
		XUnit2(unitTestAssemblies,
			 new XUnit2Settings {
				Parallelism = ParallelismOption.All,
				HtmlReport = true,
				NoAppDomain = true,
				OutputDirectory = artifactsForUnitTestsDir
			});
	});

Task("RunUnitTestsCoverageReport")
	.Does(() =>
	{
		var coverageSummaryFile = artifactsForUnitTestsDir + File("coverage.xml");
		
		EnsureDirectoryExists(artifactsForUnitTestsDir);
		
		OpenCover(tool => 
			{
				tool.XUnit2(
					"./**/UnitTests.dll",
					new XUnit2Settings {
						Parallelism = ParallelismOption.All,
						HtmlReport = true,
						NoAppDomain = true,
						OutputDirectory = artifactsForUnitTestsDir
					});
			},
			new FilePath(coverageSummaryFile),
			new OpenCoverSettings()
			{
				Register="user",
				ArgumentCustomization=args=>args.Append(@"-oldstyle")
			}
			.WithFilter("+[PackageManager.*]*")
			.WithFilter("-[xunit*]*")
			.WithFilter("-[PackageManager.*.Tests]*")
			.WithFilter("-[*]PackageManager.Host.Program")
			.WithFilter("-[*]PackageManager.Host.Startup")
			.WithFilter("-[*]PackageManager.Host.ServiceCollectionExtensions.*")
			.WithFilter("-[*]PackageManager.Host.Dal.*")
			.WithFilter("-[*]PackageManager.Host.Migrations.*")
		);
		
		ReportGenerator(coverageSummaryFile, artifactsForUnitTestsDir);
		
		var sequenceCoverage = XmlPeek(coverageSummaryFile, "//CoverageSession/Summary/@sequenceCoverage");
		var branchCoverage = XmlPeek(coverageSummaryFile, "//CoverageSession/Summary/@branchCoverage");

		Information("Sequence Coverage: " + sequenceCoverage);
		
		if(double.Parse(sequenceCoverage) < minCodeCoverage)
		{
//			throw new Exception(string.Format("Code coverage fell below the threshold of {0}%", minCodeCoverage));
		};
	});

Task("Package")
	.IsDependentOn("Compile")
	.Does(() =>
	{
		Information("TODO: implement");
	});

Task("RunAcceptanceTests")
	.IsDependentOn("Compile")
	.Does(() => 
	{
		Information("TODO: implement");
	});

RunTarget(target);