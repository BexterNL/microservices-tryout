New-SelfSignedCertificate -DnsName "keesz.int", "www.keesz.int", "api.keesz.int", "connect.keesz.int", "game.keesz.int", "identity.keesz.int" -CertStoreLocation cert:\LocalMachine\My -FriendlyName "Keesz development Certificate" -Subject keesz.int

# exporting the certificate
# $CertPassword = ConvertTo-SecureString -String "Overdekop01" -Force –AsPlainText
# Export-PfxCertificate -Cert cert:\LocalMachine\My\F6A1A4C8EB50D4F76BEA1B635854D28FADF079F0 -FilePath C:\test.pfx -Password $CertPassword