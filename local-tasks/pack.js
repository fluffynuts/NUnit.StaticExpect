const
  gulp = requireModule("gulp-with-help"),
  runSequence = require("run-sequence"),
  msbuild = require("gulp-msbuild");

gulp.task("pack",
  "Builds with the pack target",
  [],
  () => {
    return gulp.src([
      "**/NUnit.StaticExpect.NetStd.csproj"
    ]).pipe(msbuild({
      toolsVersion: "auto",
      targets: ["pack"],
      configuration: "Debug",
      stdout: true,
      verbosity: "minimal",
      errorOnFail: true,
      architecture: "AnyCPU",
      nologo: false
    }));
  });
