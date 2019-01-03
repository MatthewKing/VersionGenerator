$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project = "$root/src/VersionGenerator.Tool/"
$output = "$root/artifacts/"
dotnet pack --configuration Release --output "$output" /p:Version=0.1.0 "$project"
