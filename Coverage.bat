
packages\OpenCover.4.6.519\tools\OpenCover.Console.exe "-target:packages\xunit.runner.console.2.4.0\tools\net452\xunit.console.exe" -targetargs:"SmokedGB.UnitTests\bin\Debug\SmokedGB.UnitTests.dll -noshadow" -excludebyfile:*\*.Designer.cs -output:Coverage.xml -register:user
@if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

