**RDP Keeper**

Small windows service written in C# which makes and keep RDP connections open. Common ussage is UI Automation where session must be opened.

It uses FreeRDP-Sharp to open RDP connections. It does not draw anything, the session is completelly invisible. For status check windows event log.

**Installation**

Run `InstallService.cmd` as administrator.

**Configuration**
The whole configuration is done in configuration file:
`RdpKeeper.exe.config`

```
  <rdpConnections>
    <rdpConnection hostname="" domain="" username="" password="" sendEnterAfterLogon="true" reconnectTimeoutMins="5" sendKeysTimeoutMins="1" desktopWidth="1920" desktopHeight="1080"/>
  </rdpConnections>
```

`sendEnterAfterLogon` Usefull when legal notice message is set
`sendKeysTimeoutMins` Sends F15 every x minutes to prevent screen saver