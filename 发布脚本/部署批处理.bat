
SET WorkDir=C:\Windows\Microsoft.NET\Framework\v4.0.30319

SET WorkDir64=C:\Windows\Microsoft.NET\Framework64\v4.0.30319

SET OutputDir=D:\PublishFiles

SET CompileType=Release

SET SourceDir=D:\AK47-2016

SET PublishTarget=%3

echo Publishing site %SourceDir% to %OutputDir%

del /S /Q %OutputDir%\*.*

#rmdir /S /Q %OutputDir%\

%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\OACommonPages\OACommonPages.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\WorkflowDesigner\WorkflowDesigner.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\WfOperationServices\WfOperationServices.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\ResponsivePassportService\ResponsivePassportService.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\PassportService\PassportService.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\AccreditAdmin\AccreditAdmin.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\AppAdmin\AppAdmin.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\AppAdmin_LOG\AppAdmin_LOG.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\AUCenter\AUCenter.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\AUCenterServices\AUCenterServices.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\Diagnostics\Diagnostics.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\DocServiceHost\DocServiceHost.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCS.Dynamics.Web\MCS.Dynamics.Web.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCS.OA.Stat\MCS.OA.Stat.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCS.Web.API\MCS.Web.API.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCS.Web.Component\MCS.Web.Component.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCSOAPortal\MCSOAPortal.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\MCSResponsiveOAPortal\MCSResponsiveOAPortal.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\PermissionCenter\PermissionCenter.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\PermissionCenterServices\PermissionCenterServices.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\PPTS.Data.Common\PPTS.Data.Common.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\PPTS.Data.Customers\PPTS.Data.Customers.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\SyncMaterialFileToDocCenterService\SyncMaterialFileToDocCenterService.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\WfDemoService\WfDemoService.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\WfFormTemplate\WfFormTemplate.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\MCSWebApp\WfPlatformServices\WfPlatformServices.csproj



%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\MCS.Library.Data\MCS.Library.Data.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\MCS.Web.API\MCS.Web.API.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\MCS.Web.Component\MCS.Web.Component.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\MCS.Web.MVC.Library\MCS.Web.MVC.Library.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PermissionCenterServices\PermissionCenterServices.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Data.Common\PPTS.Data.Common.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Data.Customers\PPTS.Data.Customers.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Data.Customers.Entities\PPTS.Data.Customers.Entities.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Data.Orders\PPTS.Data.Orders.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Data.Products\PPTS.Data.Products.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Portal\PPTS.Portal.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.Web.MVC.Library\PPTS.Web.MVC.Library.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.WebAPI.Customers\PPTS.WebAPI.Customers.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.WebAPI.Orders\PPTS.WebAPI.Orders.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\PPTS.WebAPI.Products\PPTS.WebAPI.Products.csproj
%WorkDir%\msbuild /target:Build;_WPPCopyWebApplication /p:Configuration=%CompileType%;OutDir=%OutputDir% %SourceDir%\PPTSWebApp\ResponsivePassportService\ResponsivePassportService.csproj

echo Site published successfully

echo Coping css js config etc.

xcopy %SourceDir%\Config_SSP\*.* %OutputDir%\_PublishedWebsites\Config_%PublishTarget% /y /s /i

xcopy %SourceDir%\Bin\*.* %OutputDir%\_PublishedWebsites\Bin /y /s /i

xcopy %SourceDir%\MCSWebApp\css\*.* %OutputDir%\_PublishedWebsites\css /y /s /i

xcopy %SourceDir%\MCSWebApp\frames\*.* %OutputDir%\_PublishedWebsites\frames /y /s /i

xcopy %SourceDir%\MCSWebApp\HBWebHelperControl\*.* %OutputDir%\_PublishedWebsites\HBWebHelperControl /y /s /i

xcopy %SourceDir%\MCSWebApp\hta\*.* %OutputDir%\_PublishedWebsites\hta /y /s /i

xcopy %SourceDir%\MCSWebApp\images\*.* %OutputDir%\_PublishedWebsites\images /y /s /i

xcopy %SourceDir%\MCSWebApp\img\*.* %OutputDir%\_PublishedWebsites\img /y /s /i

xcopy %SourceDir%\MCSWebApp\JavaScript\*.* %OutputDir%\_PublishedWebsites\JavaScript /y /s /i

xcopy %SourceDir%\MCSWebApp\xap\*.* %OutputDir%\_PublishedWebsites\xap /y /s /i

echo File copied successfully

echo %CompileType%

Pause
