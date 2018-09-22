
powershell .\Build.ps1 -cpu AnyCPU %*
if %ERRORLEVEL% NEQ 0 exit /b 1

powershell .\Build.ps1 -cpu x64 %*
if %ERRORLEVEL% NEQ 0 exit /b 3

REM This doesn't seem necessary, becuase Android apps build as AnyCPU.
REM powershell .\Build.ps1 -cpu ARM %*
REM if %ERRORLEVEL% NEQ 0 exit /b 2
