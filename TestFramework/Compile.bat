@echo off
"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe TestFramework.csproj /p:VisualStudioVersion=14.0
pause