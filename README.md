# 哔哩下载姬

<p align="center">
    <a href="https://github.com/leiurayer/downkyi/stargazers" style="text-decoration:none" >
        <img alt="GitHub Repo stars" src="https://img.shields.io/github/stars/leiurayer/downkyi">
    </a>
    <a href="https://github.com/leiurayer/downkyi/network" style="text-decoration:none" >
        <img alt="GitHub forks" src="https://img.shields.io/github/forks/leiurayer/downkyi">
    </a>
    <a href="https://github.com/leiurayer/downkyi/issues" style="text-decoration:none">
        <img alt="GitHub issues" src="https://img.shields.io/github/issues/leiurayer/downkyi">
    </a>
    <a href="https://github.com/leiurayer/downkyi/blob/main/LICENSE" style="text-decoration:none" >
        <img alt="GitHub" src="https://img.shields.io/github/license/leiurayer/downkyi">
    </a>
</p>

![image](https://github.com/fsa99/downkyi/assets/96764100/08333867-7212-41a1-a305-7ccf495bbe24)
在原作者的基础上做了如下改动或优化：

1、重构了下载完成界面的展示，近似于移动端收藏夹中的列表形式;

2、添加了多种排序方式以及分页展示(可自主设置单页条数);

3、优化下载完成后，数据库储存逻辑，避免出现下载成功数据库删除Base数据，导致的软件重启后找不到已下载好的记录；

4、增加视频宽度、高度、旋转角度的记录，用于区分横屏还是竖屏，尤其是单个视频页中存在横屏和竖屏时，都会下载，容易区分；
![image](https://github.com/fsa99/downkyi/assets/96764100/059f63cc-c9c5-44d7-9968-35f32882bfc7)

5、增加了一键生成upup资源(适用于老版单机的upupoo)······从此收藏夹里的视频便是你的桌面视频壁纸
使用：在下载完成界面中每条下载记录对应的第三个操作按钮；在设置-upup资源 下方的拖入框中也可生成
备注:使用视频的avid作为upupoo资源的文件夹名称，即themeno。 以及在设置中可以修改目标路径等，最重要最重要最重要：UPUP设置中下方有拖入框，可以将任意位置(非本软件下载的视频)的视频拖入其中在目标文件夹生成upup资源及其相关配置文件；相当方便

![image](https://github.com/fsa99/downkyi/assets/96764100/e1655555-7ced-4764-a452-8b6fcd260fb3)
![image](https://github.com/fsa99/downkyi/assets/96764100/7d3d7aaa-2197-468c-9f55-216145042a83)![image](https://github.com/fsa99/downkyi/assets/96764100/dc101100-968b-446c-badc-77409c561011)




哔哩下载姬（DownKyi）是一个简单易用的哔哩哔哩视频下载工具，具有简洁的界面，流畅的操作逻辑。哔哩下载姬可以下载几乎所有的B站视频，并输出mp4格式的文件；采用Aria下载器多线程下载，采用FFmpeg对视频进行混流、提取音视频等操作。

[更多详情](src/README.md)

## 下载

<p align="left">
    <a href="https://github.com/leiurayer/downkyi/releases/latest" style="text-decoration:none">
       <img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/leiurayer/downkyi">
    </a>
    <a href="https://github.com/leiurayer/downkyi/releases/latest" style="text-decoration:none">
       <img alt="GitHub Release Date" src="https://img.shields.io/github/release-date/leiurayer/downkyi">
    </a>
    <a href="https://github.com/leiurayer/downkyi/releases" style="text-decoration:none">
       <img alt="GitHub all releases" src="https://img.shields.io/github/downloads/leiurayer/downkyi/total">
    </a>
</p>

[更新日志](CHANGELOG.md)

## 问题

- Aria下载失败：检查aria2c.exe是否可以正常工作和是否允许通过防火墙；或者尝试切换端口号。
- 内建下载器失败：请提issue，也欢迎pr。
- 下载时卡在“混流中”：检查ffmpeg.exe是否可以正常工作。
- 去水印：宽/高为水印的尺寸，X/Y为水印在图像中的位置（以左上角为原点），这四个数据可通过Photoshop获得。

## 赞助

如果这个项目对您有很大帮助，并且您希望支持该项目的开发和维护，请随时扫描一下二维码进行捐赠。非常感谢您的捐款，谢谢！

如果该二次开发对您有较大的帮助，并且希望添加更多功能，请随时扫描下方二维码进行捐赠，也可定制开发功能。左图为原作者的捐赠方式，右图为本人的赞赏码

![image](https://github.com/fsa99/downkyi/assets/96764100/071e5c77-1430-4f62-b062-cd31cee13a6a)



## 开发

### x86 & x64

发布的压缩包中aria2c.exe和ffmpeg.exe均为32位，如果需要请用下面链接中的文件替换。

- [aria2-1.36.0-win-32bit](third_party/aria2-1.36.0-win-32bit-build1.zip)
- [aria2-1.36.0-win-64bit](third_party/aria2-1.36.0-win-64bit-build1.zip)
- [FFmpeg](https://github.com/leiurayer/FFmpeg-Builds/releases/tag/latest)

### 相关项目

- [哔哩哔哩-API收集整理](https://github.com/SocialSisterYi/bilibili-API-collect)：B站API归档
- [Prism](https://github.com/PrismLibrary/Prism)：MVVM框架
- [WebPSharp](https://github.com/leiurayer/WebPSharp)：WebP格式图片支持，[NuGet程序包](third_party/WebPSharp.0.5.1.nupkg)

## 免责申明

1. 本软件只提供视频解析，不提供任何资源上传、存储到服务器的功能。
2. 本软件仅解析来自B站的内容，不会对解析到的音视频进行二次编码，部分视频会进行有限的格式转换、拼接等操作。
3. 本软件解析得到的所有内容均来自B站UP主上传、分享，其版权均归原作者所有。内容提供者、上传者（UP主）应对其提供、上传的内容承担全部责任。
4. **本软件提供的所有内容，仅可用作学习交流使用，未经原作者授权，禁止用于其他用途。请在下载24小时内删除。为尊重作者版权，请前往资源的原始发布网站观看，支持原创，谢谢。**
5. 因使用本软件产生的版权问题，软件作者概不负责。
