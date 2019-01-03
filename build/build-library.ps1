$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$project = "$root/src/VersionGenerator/"
$output = "$root/artifacts/"
dotnet pack --configuration Release --output "$output" /p:Version=0.1.0 "$project"
