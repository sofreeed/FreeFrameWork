set WORKSPACE=.\

set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=%WORKSPACE%\MiniTemplate

dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-bin ^
    -d bin  ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputCodeDir=..\Client\Assets\Scripts\HotUpdate\LubanGen ^
    -x outputDataDir=..\Client\Assets\AssetsPackage\LubanDatas\ ^
    -x pathValidator.rootDir=%WORKSPACE%\Projects\Csharp_Unity_bin ^
    -x l10n.textProviderFile=*@%CONF_ROOT%\l10n.json

pause