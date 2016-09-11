CertMgr.exe -del -r LocalMachine -s My -c -n Woodgrove
CertMgr.exe -del -r LocalMachine -s My -c -n FabrikamEnterprises
makecert -n "CN=Woodgrove" -pe -sr localmachine -ss My -a sha1 -sky exchange
makecert -n "CN=FabrikamEnterprises" -pe -sr localmachine -ss My -a sha1 -sky exchange
for /F "delims=" %%i in ('FindPrivateKey.exe My LocalMachine -n "CN=Woodgrove" -a') do (cacls.exe "%%i" /E /G "NT AUTHORITY\NETWORK SERVICE":R)
for /F "delims=" %%i in ('FindPrivateKey.exe My LocalMachine -n "CN=FabrikamEnterprises" -a') do (cacls.exe "%%i" /E /G "NT AUTHORITY\NETWORK SERVICE":R)

