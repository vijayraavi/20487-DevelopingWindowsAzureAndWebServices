@echo off

@Echo Deleting certificate if already exists
"%~dp0..\..\..\..\tools\certmgr.exe" -del -c -n "DemoCert" -s -r LocalMachine My
@Echo Creating certificate for the demo
"%~dp0..\..\..\..\tools\makecert.exe" -n "CN=DemoCert" -pe -ss my -sr LocalMachine -sky exchange 

pause