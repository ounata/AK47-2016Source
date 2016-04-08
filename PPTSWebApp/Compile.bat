"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe PPTSWebApp.csproj
xcopy ..\Bin\*.xap .\Xap /Y /D /R
pause