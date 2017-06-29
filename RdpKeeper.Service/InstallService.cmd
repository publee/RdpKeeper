@echo off
rem set environment variables
set installutilexe=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set installutilexe=%%D\installutil.exe

rem validation
if not defined installutilexe echo error: can't find installutil.exe & goto :end
if not exist "%installutilexe%" echo error: %installutilexe%: not found & goto :end


%installutilexe% "%~dp0RdpKeeper.exe"


:end
pause
