@ECHO OFF

nuget restore SmokedGB.sln
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

cd Agate
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

nuget restore AgateLib-Windows.sln
if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

cd ..

msbuild.exe SmokedGB.sln /T:rebuild /P:Configuration=%1 "/P:Platform=Any Cpu"
