���ȴ���֤��

����һ����ʾ CA������Ϊ RootCA�� ��֤��
Makecert -n "CN=RootCA" -r -sv C:\RootCA.pvk C:\RootCA.cer
����ʾ��������Ի���

˽Կ C:\RootCA.pvk
֤���ļ� C:\RootCA.cer

Ȼ�󴴽���һ��֤�飬�Ի���������Ϊ��������
�������洴��֤���Ӧ�� CA��RootCA����Ϊ��֤��İ䷢�ߡ�-ic C:\RootCA.cer��
��֤�����ձ��浽������-sr LocalMachine���ĸ��˴洢����-ss My��
Makecert -n "CN=ZhangJin-PC" -ic C:\RootCA.cer -iv C:\RootCA.pvk -sr LocalMachine -ss My -pe -sky exchange


��Ӷ˿�֤���
netsh http add sslcert ipport=0.0.0.0:3721 certhash=b8a5a7fcaff4ea3d298ab8afb6fd1dbbc5a4be32 appid={863D3511-E43B-41D9-B454-97F88420D092}
ɾ����
netsh http delete sslcert ipport=0.0.0.0:3721

