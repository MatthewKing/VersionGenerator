$version = "0.5.0"
dotnet pack --configuration Release --version $version --output "$PSScriptRoot/artifacts" "$PSScriptRoot/src/VersionGenerator/VersionGenerator.csproj"
dotnet pack --configuration Release --version $version --output "$PSScriptRoot/artifacts" "$PSScriptRoot/src/VersionGenerator.Tool/VersionGenerator.Tool.csproj"
