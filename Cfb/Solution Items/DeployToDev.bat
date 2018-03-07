@ECHO OFF
SET CACCESS_SOURCE=..\PrecompiledWeb\CAccess
SET CACCESS_TARGET=\\portaldev\WebSites\candidateportaldev
FOR /D %%F IN ("%CACCESS_TARGET%\*.*") DO RD /S /Q "%%F"
FOR /R "%CACCESS_TARGET%" %%F IN (*.*) DO DEL /F /S /Q "%%F"
DEL /Q "%CACCESS_SOURCE%\prod.*.config"
DEL /Q "%CACCESS_SOURCE%\test.*.config"
XCOPY /E /D /C /I /Y "%CACCESS_SOURCE%" "%CACCESS_TARGET%"
SET CACCESS_TARGET=
SET CACCESS_SOURCE=
