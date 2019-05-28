## Challenge Scenario

A new subject has been brought into the Forensic Coroner’s Office.
  
* __Subject__: Barbara Waller
* __Age__: 39
* __Occupation__: House Wife
* __Installed implant features or upgrades__: None on record.  
* __Background__: Barbara Waller is a mother of 3 children aged 4, 7, and 4 months, and married to Walter Waller, a financial analyst at Freemans Bank. 
* __Known circumstances of death__: Barbara is the first of a series of five other new mothers all belonging to the same “Mommy and Me” class that have died with eerily similar circumstances all within days/hours of each other. 
* __Objective__: Determine and investigate the cause of death, retrieve as much data as possible.
* __Special Note__: Due to the contagious nature of these deaths be VERY cautious in your investigation.
* __Password__: DEFEC8ED_2

# Solution

Unzipping the solution zip file as a first step gives us the following structure:


```
.\BrendaWaller_LastCloudLog\BWaller-QVRX8PCC9FCR_05-13-2019.log
.\DEFEC8ED_2.bin
```

The log looks interesting:
```

     
                                          ,ssJ
                            ,   ##^5#m  @#b  '@#  .#C77m
                      ,m    @b  @# ,#C  ##    @#b  @#, ^  ,##W%W,
                 ,sM   @#    @p j#b7#M  @#    ## ., ^@#  @#b    #b   ,
               %#Q     ^##   j#  @#  7%T '7%%T"   %ms##` @#    @## ,##^%m,
            @m  "##""   "@#m#W~      s##Mm###M##M#mpp    ^@p ,##M ,##m     %p
        ,,   7@p ^@# ,sM      ,#######",`",~,"||"`7%###m,   ,``  @#b  |^    ;#M,
        7##W#####  8"~    s#####^                    `|@###mp    '`       ,##"  7
          @m   '"7     ,s##b                            `^|@##,          ##b
           |@m      ,###W7"                                 ^%##p       '7
                    7@#N                                      @##Q
            ,s###mm,  "@#N                                     ^7##
         ;###"``^^7@##, "##,                                     @#m
        ###         '@#m "@#                                      "##
       @#b            @#b @##                                     '@##
       @#             @## @##                                       @#b
       @#b            @#b @#b                                      '@#~
       '@#m         ,### @##                                      ,###
         "@##m,,,,s###^ @##`                                      %##,
           '"7%WWWT|  ,##M                                         @##
                   ,###W`  ,##b                                    @##
                  %#########T@#p                                  @##
                      ~,~    ^@#m          ;###p                ]##W
                               "@##mm,,sm###C^%###m             @##
                                  '^755"""`     '@##p         ,###
                                                  '@#b   @####M"|
                                                    @#Mmm##b
                                                     '`^"75b
    
Status and configuration

Owner:                          Barbara Waller
Serial Number:                  QVRX8PC9FCR
MAC:                            4E:52:4F:AF:BC:02
Version:                        10.4.1.2
Origional Install Date:         24-08-2019
Last Full System Reboot:        13-05-2019 07:06:32
Reasoning:                      Scheduled system update
Linked Family Devices:          4E:52:4F:AF:B6:B5
                                4E:52:4F:AF:E6:13
                                4E:52:4F:AF:E6:3B
                                4E:52:4F:AF:F4:21
Carrier:                        NeroSoft_G
SECRID:                         3DFAF1CD6E93A43CA188468F3E84F087FC7987212DCDEBD92E1DEB2442DF0904
Frequent Netwok connections:    Wallers
                                Mommy&Me_Guest
                                tcp_cafe_Guest
NeuroSoft Network ID:           Neuro_blue_QVRX8PCC9FCR                               
```

But nothing jumps out.

Moving on to the next file, we poke at it with ```file```:

```
 file DEFEC8ED_2.bin
DEFEC8ED_2.bin: DOS/MBR boot sector, code offset 0x3c+2, OEM-ID "mkfs.fat", sectors/cluster 8, reserved sectors 8, root entries 512, Media descriptor 0xf8, sectors/FAT 200, sectors/track 32, heads 64, hidden sectors 40, sectors 409600 (volumes > 32 MB) , reserved 0x1, serial number 0xb47d722f, unlabeled, FAT (16 bit)
```
Seems like a FAT16 volume dump - we can mount this with ```mnt``` quite easily.


Alternatively, peeking at the dump in a hex editor allows us to recognize the __FAT16__ file system tag.

![FAT](img/fig_1.png "DEFEC8ED_2 Filesystem Identified")

If using 010Editor we can apply the Drive.bt template and get a look at what is in the filesystem before we mount.
```
* Welcome/
    * NewTaste.pdf.locked
    * UnderstandYourBaby.pdf.locked 
    * Zoombies.pdf.locked
    * CortexViper.pdf.locked
    * Mindsweeper.pdf.locked 
* READ_IT.TXT
* User-Guide.pdf.locked
* Documents/
	* FlagFile.rtf.locked
	* PICS/
		* IMG_0214.jpg.locked
			.
			.
			.
		* IMG_0239.jpg.locked
	* txt_backup/
		* msg.txt.locked
	* Downloads/
		* 
		* hottstuff.jpg.locked 
		* Idris-Elba.jpg.locked
		* Idris-Elba_sextape.exe 
		* Idris_Elba.jpg.locked 
		* Sleep061316.full.pdf.locked 
		* Baby Growth and Development Month by Month What to Expect_files/
			* _lots of files_ 		
```

Let's try mounting with 

```mnt -t vfat -o loop,ro,noexec DEFEC8ED_2.bin \tmp\forensics\defec8ed_2.bin ```

Or using OSFMount on Windows. Make sure you mount it as read-only!

```
.
├── Documents
│   ├── FlagFile.rtf.locked
│   ├── pics
│   │   ├── IMG_0214.jpg.locked
│   │   ├── IMG_0215.jpg.locked
│   │   ├── IMG_0216.jpg.locked
│   │   ├── IMG_0217.jpg.locked
│   │   ├── IMG_0218.jpg.locked
│   │   ├── IMG_0219.jpg.locked
│   │   ├── IMG_0220.jpg.locked
│   │   ├── IMG_0221.jpg.locked
│   │   ├── IMG_0222.jpg.locked
│   │   ├── IMG_0223.jpg.locked
│   │   ├── IMG_0224.jpg.locked
│   │   ├── IMG_0225.jpg.locked
│   │   ├── IMG_0226.jpg.locked
│   │   ├── IMG_0227.jpg.locked
│   │   ├── IMG_0228.jpg.locked
│   │   ├── IMG_0229.jpg.locked
│   │   ├── IMG_0230.jpg.locked
│   │   ├── IMG_0231.jpg.locked
│   │   ├── IMG_0232.jpg.locked
│   │   ├── IMG_0233.jpg.locked
│   │   ├── IMG_0234.jpg.locked
│   │   ├── IMG_0235.jpg.locked
│   │   ├── IMG_0236.jpg.locked
│   │   ├── IMG_0237.jpg.locked
│   │   ├── IMG_0238.jpg.locked
│   │   └── IMG_0239.jpg.locked
│   └── txt_backup
│       └── msgs.txt.locked
├── Downloads
│   ├── Baby Growth and Development Month by Month   What to Expect_files
│   │   ├── analytics.js.locked
│   │   ├── apstag.js.locked
│   │   ├── bridge3.htm.locked
│   │   ├── client.js.locked
│   │   ├── css.css.locked
│   │   ├── default.js.locked
│   │   ├── edh.js.locked
│   │   ├── fbevents.js.locked
│   │   ├── first-year-month-by-month-week-8-article.jpg.locked
│   │   ├── gtm.js.locked
│   │   ├── icong1.png.locked
│   │   ├── ima3.js.locked
│   │   ├── integrator.js.locked
│   │   ├── mbm-month-10-article.jpg.locked
│   │   ├── mbm-month-11-article.jpg.locked
│   │   ├── mbm-month-2-article.jpg.locked
│   │   ├── mbm-month-3-article.jpg.locked
│   │   ├── mbm-month-4-article.jpg.locked
│   │   ├── mbm-month-5-article.jpg.locked
│   │   ├── mbm-month-6-article.jpg.locked
│   │   ├── mbm-month-7-article.jpg.locked
│   │   ├── mbm-month-8-article.jpg.locked
│   │   ├── mbm-month-9-article.jpg.locked
│   │   ├── mbm-week-1-article.jpg.locked
│   │   ├── mbm-week-2-article.jpg.locked
│   │   ├── mbm-week-3-article.jpg.locked
│   │   ├── mbm-week-4-article.jpg.locked
│   │   ├── mbm-week-5-article.jpg.locked
│   │   ├── mbm-week-6-article.jpg.locked
│   │   ├── mobile-profile-icon-new.svg
│   │   ├── react-dom.js.locked
│   │   ├── react-with-addons.js.locked
│   │   ├── sublanding.css.locked
│   │   ├── sublanding.js.locked
│   │   ├── webfont.js.locked
│   │   └── wte-new-logo.svg
│   ├── Baby Growth and Development Month by Month   What to Expect.htm.locked
│   ├── hottstuff.jpg.locked
│   ├── Idris_Elba.jpg.locked
│   ├── Idris-Elba.jpg.locked
│   ├── Idris-Elba_sextape.exe
│   └── Sleep061316.full.pdf.locked
├── READ_IT.txt
├── System Volume Information
│   └── IndexerVolumeGuid
├── User-Guide.pdf.locked
└── Welcome
    ├── CortexViper.pdf.locked
    ├── Mindsweeper.pdf.locked
    ├── NewTaste.pdf.locked
    ├── UnderstandYourBaby.pdf.locked
    └── Zoombies.pdf.locked

7 directories, 78 files
```
At a first glance, there are a lot of locked files, with two exceptions:
`READ\_IT.txt` and `Idris-Elba_sextape.exe`. Very suspicious. We also spot a `FlagFile.rtf.locked` file.


![MOV](img/fig_2.png "Very Suspicious Sex Tape")



Let's open the `READ_IT.txt` file and...
```
Files has been encrypted with NeuroTear
Pay 100 bitcoins or face full neuro shutdown
You have 24 hours to make this payment.
```

Ouch. Ransomware.

The only other file that's unencrypted in this dump is the EXE.

Running `file Idris-Elba_sextape.exe` returns:

```
Idris-Elba_sextape.exe: PE32 executable (GUI) Intel 80386 Mono/.Net assembly, for MS Windows
```

Since this looks like a Windows PE executable, it's time to switch to using Windows. Fire up a VM, mount the image again, and let's poke at this in more depth.


### Idris-Elba_sextape.exe

Since `file` identified this as a .NET executable, let's try disassembling it. These screenshots are from ILSpy and DnSpy, either is valid:

![FLAG_1_PRE](img/fig_initial_disasm.png "The First Flag")

That was easy - it looks like it's plain unobfuscated C#. And the code looks a little sloppy - a Windows Form as the base?


It does not take long to discover the _SendPassword()_ function with a little prize just for looking.

![FLAG_1](img/fig_3.png "The First Flag")

But there are no smoking gun clues left in the rest of the code flow, which looks something like this:

![FLAG_1](img/fig_flow.png "The First Flag")


Let's see if we can find a similar ransomware in the wild. The idea is that maybe this challenge was inspired by an existing one - or at least there is something that will help us out here.

Searching for "NeuroTear", and nothing interesting, "NeuroTear ransomware", nothing of note, but then "Neuro Tear ransomware" leads us to this:

[Hidden Tear (Wikipedia)](https://en.wikipedia.org/wiki/Hidden_Tear)


And within the article:

```
 Hidden Tear has an encryption backdoor, thus allowing him to crack various samples.[4]
```

Following the links to the [original blogpost from Sen](https://utkusen.com/blog/dealing-with-script-kiddies-cryptear-b-incident.html) and reading along, we see the exploit: The key generation function for NeuroTear uses the Random class from .NET, which when instantiated with the default constructor, uses the system tick value (milliseconds since system boot - $Environment.TickCount) for the seed. Since the RNG is deterministic and always returns the same RNG result sequence with the same seed, if we seed the RNG in our keygen function with the exact tick value the victim system was using when the ransomware started, we can recover the key!


His explanation however is a little wonky - Since the victim system is not the current machine we're using we cannot use the timestamp calculation trick he uses. There is a *reasonable* expectation that the challenge designers did not expect a brute-forceing of the entire 32-bit integer space by trying every integer value as a seed; we have to figure out the tick some other way.


We know when the files were modified by looking at the timestamps; all of the locked files were modified on 05/13/2019 15:01:00. If we can figure out the system boot time, then we can calculate a range for the possible tick values.

Let's go back to the log, there was something familiar there:

![log](img/fig_5.png "BWaller-QVRX8PCC9FCR_05-13-2019.log")  	

Let's assume that that was the last full system boot. Using WolframAlpha to figure out the delta [here](https://www.wolframalpha.com/input/?i=(07:06:32+to+15:01:00)+to+sec):


![wolfram](img/fig_wolfram.png "wolframalpha")  	 

That's our estimated tick count. 

Now we need a test file to decrypt - we want to keep this as small as possible to test seed values as fast as possible. There is one candidate - `icong1.png.locked` which seems to be a tiny PNG file. Let's check the original file by searching the Internet for the original file. Luckily this is made easy - the file name `Baby Growth and Development Month by Month   What to Expect.htm.locked` is *probably* the original page title, so once we search for that:


![baby](img/fig_icong.png "baby development page")  	 

The header for `icong1.png` confirms that this is a normal PNG, so we can quickly validate by testing the decrypted result's first eight bytes against the sequence `0x89 0x50 0x4E 0x47 0x0D 0x0A 0x1A 0x0A`.

Let's write a quick and dirty application to decrypt the code - copy directly from the disassembled view, swap the encrypt operation to decrypt, and test a five minute window. Full source is available in this repo.

The generate-and-test-key code is wrapped in a `Parallel.ForEach` instantiation, which will attempt to parallelize the operation since this is a very CPU-heavy task. 

After a (few) cups of coffee, the computer beeps, letting us know that the key was found!

Plug that key into a quick decryptor function:

![decryptor](img/fig_decryptall.png "decrypt")  	 

And we can crack open the final RTF file for the final flag! Whew!

