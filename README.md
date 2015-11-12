#NXSharp
即 njit8021xclient-sharp  
认证算法与我的Linux分支[njit8021xclient](https://github.com/bitdust/njit8021xclient)同步
fork from
---
njit8021xclient: https://github.com/liuqun/njit8021xclient

本项目是[tengattack的windows分支](https://github.com/tengattack/8021xclient-for-windows)的修改，将其输出修改为DLL文件。添加了Nxsharp（njit8021xclient-sharp的缩写）程序作为GUI前端。同时完成了对inode7.0中的[AES128验证算法](./documents/h3c_AES_MD5.md)的支持。
features
---
* windows 图形界面
* 断线自动重连
* h3c-AES-MD5 算法支持
* 可以配置认证版本号、认证密钥（获取方法见 [H3C_toolkit](https://github.com/tengattack/8021xclient-for-windows) 项目）

screenshot
---
![screenshot](https://cloud.githubusercontent.com/assets/6072743/11125371/8796e67c-89a4-11e5-8ffb-7861f1e2c246.png)

release
---
最新版本NXSharp下载地址：https://github.com/bitdust/nxsharp/releases

depends on
---
WinPcap: http://www.winpcap.org/  
dotNet Framework 3.5~4.5: http://www.microsoft.com/  

build
---
编译环境 Visual Studio 2013

依赖 winpcap，请从官方网站下载 

[Winpcap二进制安装包](http://www.winpcap.org/install/default.htm)  
以及  
[Winpcap开发包](http://www.winpcap.org/devel.htm),并将其解压放置于Wpdpack文件夹中。

more info
---
[关于inode7.0中基于AES128的验证算法](./documents/h3c_AES_MD5.md)

