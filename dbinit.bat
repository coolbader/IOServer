echo Creating database projects...
dotnet new classlib -n IOServer.Logic.Migrates
dotnet new classlib -n IOServer.Logic.Models

echo Adding SqlSugar packages...
dotnet add IOServer.Logic.Migrates package SqlSugarCore
dotnet add IOServer.Logic.Migrates package SqlSugar.IOC

echo Adding projects to the solution...
dotnet sln IOServer.sln add IOServer.Logic.Migrates/IOServer.Logic.Migrates.csproj
dotnet sln IOServer.sln add IOServer.Logic.Models/IOServer.Logic.Models.csproj
