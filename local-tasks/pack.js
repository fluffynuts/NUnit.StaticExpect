const
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  spawn = requireModule("spawn"),
  msbuild = require("gulp-msbuild");

gulp.task("pack", [ "build-for-release" ], () => {
  return doPack();
});

gulp.task("build-for-release", () => {
    return gulp.src([
      "**/*.sln",
      "!**/node_modules/**/*.sln",
      "!./tools/**/*.sln"
    ]).pipe(msbuild({
      toolsVersion: "auto",
      targets: ["Clean", "Build"],
      configuration: "BuildForRelease",
      stdout: true,
      verbosity: "minimal",
      errorOnFail: true,
      architecture: "x64",
      nologo: false
    }));
});

function doPack() {
  return spawn(
    "tools/nuget.exe",
    ["pack", "Package.nuspec", "-OutputDirectory", packageDir]
  );
}
