# Change Log
All notable changes to this project will be documented in this file.
This project uses the changelog in accordance with [keepchangelog](http://keepachangelog.com/). Please use this to write notable changes, which is not the same as git commit log...

## [unreleased]

## [1.2.1.10] - 2019-12-17
- speedups (@grspy)
- log textbox scrolls to end when appending. (@grspy , @iceman)

## [1.2.1.9] - 2019-12-11
- Increase default "keep alive" setting to 10sec (@grspy)

## [1.2.1.8] - 2019-12-11
- Download / saving dump now uses unique naming. aka.  uid_{counter}.bin format (@grspy)

## [1.2.1.7] - 2019-12-09
- Upload one dump to several slots  (@grspy)

## [1.2.1.6] - 2019-12-03
- Annotate RevG log output  (@grspy)

## [1.2.1.4] - 2019-11-11
- New logo and icons  (@grspy)

## [1.2.1.3] - 2019-11-05
- Fixed various serial commands bugs and delays (@shinhub)
- Fixed mfkey32 data reception from current firmware (@shinhub)
- Changed UID behavior so they are read back from memory when what is set in GUI is invalid (@shinhub)

## [1.2.1.0] - 2019-09-17
- extended timeouts (@shinhub)
- 4k allowed on all slots (@shinhub)
- remember old UID when swapping sizes (@shinhub)
- fix mfkey32 (@shinhub)
- Support new NTAG/Ev dump format (@mceloff)
- 7byte uid identification from dump (@mceloff)
- Fixed application crash (@uspilot)

## [1.2.0.19] - 2019-03-12
- added scrollbars / resizeing of gui possible (@bogito)

## [1.2.0.18] - 2019-03-11
- moved legend to font

## [1.2.0.17] - 2019-03-11
- support for RevE firmware with or without MY-extensions. (@bogito)
- ultralight/ntag download dump now should not add extra empty header every time. (@iceman)

## [1.2.0.13] - 2019-02-22
- selected language in combobox (@iceman)

## [1.2.0.10] - 2019-02-22
- Spanish translations (@neijpass)
- Tablelayout adaptive regarding device (@bogito)

## [1.2.0.9] - 2019-02-01
- German translations (@vrumfondel)

## [1.2.0.8] - 2018-12-17
- General stability bug fixes
- New iClass dark template (@iceman)
- Bugfix - mf_detection (@kgamecarter)
- Highlighted active tagslot (@vrumfondel)
- Splash screen (@vrumfondel)
- Massive speedups in comport identification (@vrumfondel)
- RevG identify function (@vrumfondel) 
- Serial tab doesn't crash on download/upload
- Serial tab has enhances help text functionality with clickable text (@vrumfondel)
- Support for RevG devices (@vrumfondel)
- Increased dump formats (BIN/MCT/EML/JSON)  (@kgamecarter)
- Mf32 attack, in managed code and in parallell (@kgamecarter)
- Dark themed templates (@hiwanz)

## [1.1.0.0] - 2018-xx-xx
- Memory dump color templates (@iceman)
- Multilanguage support (@iceman)
  (english, chinese, dutch, french, german, greek, italian, swedish)
- Locked scrolling of dumpfiles
- Serial interface tab, (@iceman) 

## [1.0.0.8] - 2018-03-16
- bindiff comparision (@bogition)
- load / save of dumpfiles (@bogition)

## [1.0.0.7] - 2018-01-xx
- first version of GUI  (@iceman, @bogition)
