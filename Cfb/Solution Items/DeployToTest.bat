@ECHO OFF
SET CACCESS_SOURCE=..\CandidatePortal\WebDeployment\CAccessDeployment\Debug
SET CACCESS_TARGET=\\portaltest\Webs\caccess.nyccfbtest.info
FOR /D %%F IN ("%CACCESS_TARGET%\*.*") DO RD /S /Q "%%F"
FOR /R "%CACCESS_TARGET%" %%F IN (*.*) DO DEL /F /S /Q "%%F"
XCOPY /E /D /C /I /Y "%CACCESS_SOURCE%" "%CACCESS_TARGET%"
DEL /Q "%CACCESS_TARGET%\prod.*.config"
DEL /Q "%CACCESS_TARGET%\cAccess.*.config"
DEL /Q "%CACCESS_TARGET%\Cfb.SharePoint.Properties.Settings.config"
DEL /Q "%CACCESS_TARGET%\Cfb.CandidatePortal.Security.Properties.Settings.config"
REN "%CACCESS_TARGET%\test.cAccess.sites.config" cAccess.sites.config
REN "%CACCESS_TARGET%\test.cAccess.electionCycle.config" cAccess.electionCycle.config
REN "%CACCESS_TARGET%\test.Cfb.SharePoint.Properties.Settings.config" Cfb.SharePoint.Properties.Settings.config
REN "%CACCESS_TARGET%\test.Cfb.CandidatePortal.Security.Properties.Settings.config" Cfb.CandidatePortal.Security.Properties.Settings.config
IF NOT EXIST "%CACCESS_TARGET%\_controltemplates" MKDIR "%CACCESS_TARGET%\_controltemplates"
SET CACCESS_TARGET=
SET CACCESS_SOURCE=