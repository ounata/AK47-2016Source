"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe PPTSWebApp.csproj /p:VisualStudioVersion=14.0
xcopy ..\Bin\*.xap .\Xap /Y /D /R
pause