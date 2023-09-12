# onevpnBrowser

## What is this?
This is a small MS Windows executable that spins up a webview browser window, points it to a particular site, lets you log on there and returns once a specific cookie is set

## What might one use this for?
Some VPNs (Cisco Anyconnect, Fortinet)... ultimately use cookies to authenticate a VPN connection.

With companies going more secure, obtaining these cookies has moved beyond just "submitting a HTML form".

This executable lets you log in whatever way a particular VPN needs you to and then use that cookie to call something like openconnect (https://www.infradead.org/openconnect/) with said cookie.

Something like this could be done as a bash script which would start the VPN and give one full route creation control in `script.js`
```
@echo off
@setlocal enableextensions
@cd /d "%~dp0"
setlocal EnableDelayedExpansion
SET lf=
FOR /F "delims=" %%i IN ('"C:\...\onevpnBrowser.exe" https://vpn.somewhere.com COOKIENAME') DO if ("!out!"=="") (set out=%%i) else (set out=!out!%lf%%%i)
ECHO %out%
"C:\...\openconnect.exe" --protocol=vpnname -C "%out%" -s %cd%\script.js vpn.somewhere.com
@pause
```

