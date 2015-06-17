首先创建证书

生成一个表示 CA【名称为 RootCA】 的证书
Makecert -n "CN=RootCA" -r -sv C:\RootCA.pvk C:\RootCA.cer
会提示密码输入对话框

私钥 C:\RootCA.pvk
证书文件 C:\RootCA.cer

然后创建另一个证书，以机器名称作为主题名称
并以上面创建证书对应的 CA【RootCA】作为该证书的颁发者【-ic C:\RootCA.cer】
该证书最终保存到本机【-sr LocalMachine】的个人存储区【-ss My】
Makecert -n "CN=ZhangJin-PC" -ic C:\RootCA.cer -iv C:\RootCA.pvk -sr LocalMachine -ss My -pe -sky exchange


添加端口证书绑定
netsh http add sslcert ipport=0.0.0.0:3721 certhash=b8a5a7fcaff4ea3d298ab8afb6fd1dbbc5a4be32 appid={863D3511-E43B-41D9-B454-97F88420D092}
删除绑定
netsh http delete sslcert ipport=0.0.0.0:3721

