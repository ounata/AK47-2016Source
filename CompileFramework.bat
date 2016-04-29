"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe %~dp0FrameworkAll.csproj /p:VisualStudioVersion=14.0 
xcopy .\Bin\*.xap .\MCSWebApp\Xap\ /Y /D /R
pause 