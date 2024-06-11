echo Creating Ocelot API Gateway project...
dotnet new webapi -n IOServer.Gateway

echo Adding Ocelot package...
dotnet add IOServer.Gateway package Ocelot

echo Configuring Ocelot in the project...
echo "var builder = WebApplication.CreateBuilder(args);" > IOServer.Gateway/Program.cs
echo "builder.Services.AddOcelot();" >> IOServer.Gateway/Program.cs
echo "var app = builder.Build();" >> IOServer.Gateway/Program.cs
echo "app.UseOcelot().Wait();" >> IOServer.Gateway/Program.cs
echo "app.Run();" >> IOServer.Gateway/Program.cs

echo Adding Gateway project to the solution...
dotnet sln IOServer.sln add IOServer.Gateway/IOServer.Gateway.csproj

echo Ocelot API Gateway project created and configured successfully.
