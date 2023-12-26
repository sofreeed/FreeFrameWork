set WORKSPACE=.\

set LUBAN_DLL=%WORKSPACE%\Tools\Luban\Luban.dll
set CONF_ROOT=%WORKSPACE%\MiniTemplate

dotnet %LUBAN_DLL% ^
    -t all ^
    -d text-list   ^
    --conf %CONF_ROOT%\luban.conf ^
	--validationFailAsError ^
    -x outputDataDir=..\Client\Assets\AssetsPackage\LubanDatas\Localize ^
	-x l10n.textProviderFile=*@%CONF_ROOT%\l10n.json ^
	-x l10n.textListFile=texts.txt
pause