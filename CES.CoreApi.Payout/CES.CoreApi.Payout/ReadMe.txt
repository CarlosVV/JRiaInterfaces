Requirment to use Golden Crown  Web Service



Import of certificates

From TChain.zip
1.	Root certificate (“Sundry Root CA512”) must be imported into Trusted Root Certification Authorities – file “g0 - to RootAuth”,
2.	Intermediate certificate (“Class 3 CA512”) must be imported into Intermediate Certification Authorities - file “g23 - to IntermediateAuth”.
3.	Intermediate certificate (“Class 4 CA512”) must be imported into Intermediate Certification Authorities - file “g24 - to IntermediateAuth”.
4.	GC server certificate (“Stable Server DS”) must be imported into Trusted Publishers –file “StableServer_of2102 - to Trusted”
From Operator.zip
5.	Partner’s personal key and certificate must be import into Personal. Password for key double click  is “1”, file “prv_key.pfx”.

Note: You can find the zip files under CES.CoreApi.PayoutTests >Resources > Certificates

Web Api Config Settings 

Already installed only for Info
<runtime>
		<AppContextSwitchOverrides value="Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate=true" />
</runtime>

How can we give IIS the correct permissions to read a certificate from the certificate store?
1)	Open the certificates MMC.
2)	Open MMC
3)	Click File –> Add/Remove Snap-in…
4)	Choose Certificates and click Add
5)	Select Computer Account and click Finish
6)	Check if the certificate is available in the "Local Computer\Personal" cert store.
7)	Check if the private key of the certificate is marked as exportable.
8)	Right click on the certificate and choose "All Tasks --> Manage Private Keys" and "IIS_USER" or other user or app pool account that the IIS 7.5 app pool is using (ApplicationPoolIdentity).


Addition 
Make sure the location is the computer name and not the domain name.
Prefix the following to the AppPool name "IIS AppPool\"  then click "Check Name"
It should find the AppPool,  remove the prefix and then it should add OK.
Do this for both the client and server certs.
