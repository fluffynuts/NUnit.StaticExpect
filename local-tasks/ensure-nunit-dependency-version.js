const
    gulp = requireModule("gulp-with-help"),
    gutil = requireModule("gulp-util"),
    editXml = require("gulp-edit-xml"),
    path = require("path");

gulp.task("ensure-nunit-dependency-version", () => {
    var nunitVersion;
    gulp.src(path.join("src", "NUnit.StaticExpect", "NUnit.StaticExpect.csproj"))
        .pipe(editXml(xml => {
            const packagesNode = xml.Project.ItemGroup.filter(n => !!n["PackageReference"])[0];
            if (!packagesNode) {
                throw new Error("No ItemGroup with PackageReference node found");
            }
            const nunitInclude = packagesNode.PackageReference.filter(n => n.$.Include == "nunit")[0];
            if (!nunitInclude) {
                throw new Error("No nunit package reference inclusion found");
            }
            nunitVersion = nunitInclude.$.Version;
            return xml;
        }));
    return gulp.src("Package.nuspec")
        .pipe(editXml(xml => {
            const groups = xml.package.metadata[0].dependencies[0].group;
            groups.forEach(g => {
                g.dependency.forEach(dep => {
                    const 
                        depVersion = dep.$.version,
                        parts = depVersion.split(','),
                        min = parts[0].replace(/^\[/, ""),
                        newVersion = `[${nunitVersion},4)`;
                    dep.$.version = newVersion;
                });
            });
            return xml;
        }, { builderOptions: { renderOpts: { pretty: true } } })).pipe(gulp.dest("."));
});