# AC_Replay_Mover
##### Moves .acreplay files from one directory to another

### Usage: 

1. Download as zip.
2. Extract the zip.
3. Open following file:  AC_Replay_Mover/AC_Replay_Mover/App.config 
4. In the App.config file:
  a. Navigate to <configuration>
                    [...]
                        <appSettings>
                            [...]
                            <add key="sourceDirectory" value="C:\path\to\your\ACReplay\folder\"/>
		                        <add key="destinationDirectory" value="C:\path\to\desired\destination\folder\"/>
  b. Change the values of sourceDirectory and targetDirectory according to your liking
  c. Save the file
5. Press [Win] + R and type "shell:startup" to open your Autostart folder in a new window.
6. Copy "AC_Replay_Mover_Shortcut" from AC_Replay_Mover\AC_Replay_Mover\bin\Debug to your Autostart folder. Now it should start ony every restart of your PC.
