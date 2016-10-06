@Echo off

%~d0
cd %~dp0

powershell -NonInteractive -Command "Set-ExecutionPolicy unrestricted"

@Echo Checking for missing Windows Azure components
Powershell .\BuildWindowsAzureComponents.ps1

pause