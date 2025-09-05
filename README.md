# PortTunnel_forWindowsXP_1

Lightweight TCP port forwarder built for legacy Windows (original project targets .NET Framework 2.0). The app reads a simple settings file and creates TCP listeners that tunnel traffic to configured remote endpoints.

---

## Features
- Forward local TCP ports to remote hosts/ports.
- Simple plain-text configuration (no GUI config dialogs).
- Lightweight single-file WinForms UI with start/stop controls and runtime log.

---

## Requirements
- Windows (original project targeted Windows XP; works on newer Windows if .NET compat is present).
- .NET Framework 2.0 runtime (project compiled for .NET 2.0).
- Visual Studio to build (you can open the solution in modern Visual Studio; adjust target framework if needed).

---

## Build (Visual Studio)
1. Open the solution with __File > Open > Project/Solution__.
2. If needed, set the project target framework using __Project > Properties > Application__.
3. Build the project (__Build > Build Solution__).
4. Run the executable as Administrator if you need to listen on privileged ports.

---

## Configuration: PortTunnelSettings.txt
Place a file named `PortTunnelSettings.txt` in the app folder (same folder as the .exe). Each non-empty line defines one tunnel. Tokens are space-separated; order does not matter.

Supported tokens:
- `listenAddress=IP` — local address to bind (use `0.0.0.0` for all interfaces).
- `listenPort=PORT` — local port to accept connections.
- `connectAddress=IP` — remote target host/IP to forward to.
- `connectPort=PORT` — remote target port.

Example:
