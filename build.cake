var target = Argument("target", "Default");

Task("Build").Does(() =>
	{
		MSBuild("./src/Logging.sln");
	});

Task("Default")
  .IsDependentOn("build");

RunTarget(target);