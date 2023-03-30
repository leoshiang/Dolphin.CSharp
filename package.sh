cd Dolphin.Common
dotnet pack -p:PackageVersion=1.1.0 -o ../NugetOutput/
cd ../Dolphin.Dropbox
dotnet pack -p:PackageVersion=1.1.0 -o ../NugetOutput/
cd ../Dolphin.Extensions
dotnet pack -p:PackageVersion=1.1.0 -o ../NugetOutput/
cd ../Dolphin.IO
dotnet pack -p:PackageVersion=1.1.0 -o ../NugetOutput/
cd ../Dolphin.Object
dotnet pack -p:PackageVersion=1.1.0 -o ../NugetOutput/
cd ../