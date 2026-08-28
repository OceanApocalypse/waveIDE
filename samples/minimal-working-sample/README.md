# waveIDE Plugin Sample: Minimal Working Sample
This sample's goal was to test the most basic connection possible back when waveIDE was still very early in development. It's supposed to be used alongside `waveIDE.IpcTestServerLauncher` and not `waveIDE` itself, as it's not an actual functioning plugin.

**Developed by:** Ocean Apocalypse

## How to use
Start the server by running the `waveIDE.IpcTestServerLauncher` project. Then, run this project with the first commandline argument being the endpoint where `waveIDE.IpcTestServerLauncher` started the server.

## Features
- Showcases what's at the base of the flow of what will be the actual plugin system used by waveIDE.
- Performs an handshake, event subscription and asynchronous invocations.
- Showcases how plugins work despite Native AOT restrictions.
- Uses JSON serialization for integers and strings.
