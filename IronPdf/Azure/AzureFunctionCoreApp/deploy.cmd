:: User and password from Publish profile $USER:PASSWORD e.g. $AzureFunction:Fasdfad...aaghR
:: Deploy URL from Publish profile e.g. https://azurefunction.scm.azurewebsites.net

SET _user=%1
SET _url=%2
SET _zip=%3

echo user=%_user%
echo url=%_url%
echo zip=%_zip%

curl.exe -X GET -u %_user% %_url%/api/deployments
curl.exe -X POST -u %_user% %_url%/api/zipdeploy -T %_zip%
curl.exe -X GET -u %_user% %_url%/api/deployments