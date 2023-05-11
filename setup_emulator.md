## Prerequisite

Download and install [BlueStacks 5](https://www.bluestacks.com/bluestacks-5.html)


# Setup for running on emulator

1. Install the apk in BlueStacks
2. Enable ADB in BlueStacks

```
Settings > Advanced > Android Debug Bridge : enable
```
3. Check that emulator is recognized and see on which port is listening

```
adb devices
```
4. Setup in code port forwarding with port listening in emulator

For example
```
AltPortForwarding.ForwardAndroid(13000, 5037);
```
5. Launch game in BlueStacks
6. Setup AltTester connection port as seen in step 3
