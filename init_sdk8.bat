@echo off
echo Creating a new dotnet solution named IOServer...
dotnet new sln -n IOServer

echo Creating class library projects...
dotnet new classlib -n IOServer.Logic.Grains
dotnet new classlib -n IOServer.Logic.IGrains

echo Creating webapi projects...
dotnet new webapi -n IOServer.Logic.Host
dotnet new webapi -n IOServer.Logic.Client

echo Adding projects to the solution...
dotnet sln IOServer.sln add IOServer.Logic.Grains/IOServer.Logic.Grains.csproj
dotnet sln IOServer.sln add IOServer.Logic.IGrains/IOServer.Logic.IGrains.csproj
dotnet sln IOServer.sln add IOServer.Logic.Host/IOServer.Logic.Host.csproj
dotnet sln IOServer.sln add IOServer.Logic.Client/IOServer.Logic.Client.csproj

echo Solution and projects created successfully.
pause


