@ECHO OFF

nuget restore SmokedGB.sln
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

msbuild.exe SmokedGB.sln /T:rebuild /P:Configuration=%1 "/P:Platform=Any Cpu"
