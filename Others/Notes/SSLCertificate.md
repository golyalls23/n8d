# SSL Certificate

$Certificate=New-SelfSignedCertificate –Subject testing.com -CertStoreLocation Cert:\CurrentUser\My 

Export-Certificate -Cert $Certificate -FilePath "C:\$certname.cer" 

$Certificate=Get-ChildItem –Path <Certificate path> Export-Certificate -Cert $Certificate -FilePath "C:\Users\admin\Desktop\$certname.cer" 


$Certificate=New-SelfSignedCertificate –DnsName ngitc.com, localhost -CertStoreLocation Cert:\CurrentUser\My
