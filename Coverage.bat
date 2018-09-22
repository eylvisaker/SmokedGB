
packages\OpenCover.4.6.519\tools\OpenCover.Console.exe "-target:packages\xunit.runner.console.2.3.1\tools\net452\xunit.console.exe" -targetargs:"Thornbridge.UnitTests\bin\Debug\Thornbridge.UnitTests.dll Thornbridge.IntegrationTests\bin\Debug\Thornbridge.IntegrationTests.dll AgateLib\AgateLib.UnitTests\bin\Debug\AgateLib.UnitTests.dll AgateLib\AgateLib.FunctionalTests\bin\Debug\AgateLib.FunctionalTests.dll -noshadow" -excludebyfile:*\*.Designer.cs -output:Coverage.xml -register:user
@if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

REM packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:Coverage.xml -targetdir:testreport -reporttypes:html;htmlchart;htmlsummary -assemblyfilters:+ThornbridgeSaga
packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:Coverage.xml -targetdir:TestReport -reporttypes:html;htmlchart;htmlsummary -assemblyfilters:-MoonSharp.Interpreter;-Moq;
@if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%

