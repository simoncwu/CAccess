@ECHO OFF
SET CACCESS_SOURCE=..\CandidatePortal\WebDeployment\CAccessDeployment\Release
SET CACCESS_TARGET=\\portalprod\Webs\caccess.nyccfb.info
rem CHOICE /C YN /M "Preparing to deploy to production. Is this correct? " /T 10 /D N
rem IF ERRORLEVEL 2 GOTO ABORT
ECHO Preparing to deploy to production. Press Ctrl-C to abort.
PAUSE
ECHO !!! WARNING !!!
ECHO !!! WARNING !!!
ECHO !!! WARNING !!!
ECHO !!! WARNING !!!
ECHO !!! WARNING !!!
rem CHOICE /C YN /M "YOU ARE ABOUT TO CHANGE THE PRODUCTION C-ACCESS ENVIRONMENT. CONTINUE? " /T 10 /D N
rem IF ERRORLEVEL 2 GOTO ABORT
ECHO YOU ARE ABOUT TO CHANGE THE PRODUCTION C-ACCESS ENVIRONMENT. PRESS CTRL-C TO ABORT.
PAUSE
COPY /Y app_offline.htm "%CACCESS_TARGET%"
FOR /D %%F IN ("%CACCESS_TARGET%\*.*") DO RD /S /Q "%%F"
FOR /R "%CACCESS_TARGET%" %%F IN (*.*) DO DEL /F /S /Q "%%F"
XCOPY /E /D /C /I /Y "%CACCESS_SOURCE%" "%CACCESS_TARGET%"
DEL /Q "%CACCESS_TARGET%\test.*.config"
DEL /Q "%CACCESS_TARGET%\cAccess.*.config"
DEL /Q "%CACCESS_TARGET%\Cfb.SharePoint.Properties.Settings.config"
DEL /Q "%CACCESS_TARGET%\Cfb.CandidatePortal.Security.Properties.Settings.config"
REN "%CACCESS_TARGET%\prod.cAccess.sites.config" cAccess.sites.config
REN "%CACCESS_TARGET%\prod.cAccess.electionCycle.config" cAccess.electionCycle.config
REN "%CACCESS_TARGET%\prod.Cfb.SharePoint.Properties.Settings.config" Cfb.SharePoint.Properties.Settings.config
REN "%CACCESS_TARGET%\prod.Cfb.CandidatePortal.Security.Properties.Settings.config" Cfb.CandidatePortal.Security.Properties.Settings.config
IF NOT EXIST "%CACCESS_TARGET%\_controltemplates" MKDIR "%CACCESS_TARGET%\_controltemplates"
DEL /Q "%CACCESS_TARGET%\app_offline.htm"
SET CACCESS_TARGET=
SET CACCESS_SOURCE=
ECHO Completed deployment to production.
PAUSE
GOTO END
:ABORT
ECHO Aborted deployment to production.
PAUSE
:END