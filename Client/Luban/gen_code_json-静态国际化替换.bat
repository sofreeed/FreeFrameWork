set GEN_CLIENT=Tools\Luban.ClientServer\Luban.ClientServer.exe

%GEN_CLIENT% -j cfg --^
 -d Defines\__root__.xml ^
 --input_data_dir Datas ^
 --output_code_dir ..\Assets\LubanGen\Code ^
 --output_data_dir ..\Assets\LubanGen\json ^
 --gen_types code_cs_unity_json,data_json ^
 -s all ^
 --l10n:input_text_files Datas\locationtexttable.xlsx ^
 --l10n:text_field_name text_en ^
 --l10n:output_not_translated_text_file NotLocalized_CN.txt

pause