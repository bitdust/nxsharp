#include <stdio.h>
#include <stdlib.h>
#include "pcaphelper.h"
#include <winsock2.h>
#include <windows.h>
#include <process.h>
#pragma comment(lib, "ws2_32.lib")

//log
static char logbuffer[5][260] = {"", "", "", "", ""}; //store the log
static char outputbuffer[1300] = "";
static int writer_ptr = 0;
static int log_is_readed = 1;
extern _declspec(dllexport) char *ReadLog();
void WriteLog(char *info);

//mode switch


//auth thread
DWORD WINAPI auth_thread_func();
static 	char sUserName[100]; // ugly code 使用全局变量传送参数
static 	char sPassword[100];
static 	char sDeviceName[100];
static 	char sVersion[17];
static 	char sH3C_key[64];
static int mode = 0; // 模式1即断线自动重连；模式0无自动重连；模式2 pankiller不发送心跳包，自动重连。 使用全局变量传递工作模式

static HANDLE authThread = NULL;
extern int stop_flag = 1;
extern _declspec(dllexport) void StartAuthThread(const char *UserName, const char *Password, const char *DeviceName, const char *Version, const char *H3C_key, int mode_config);
extern _declspec(dllexport) void StopAuthThread();

//from auth.c
int Authentication(const char *UserName, const char *Password, const char *DeviceName, const char *Version, const char *H3C_key, int mode);

DWORD WINAPI auth_thread_func(PVOID pParam)
{
	Authentication(sUserName, sPassword, sDeviceName, sVersion, sH3C_key, mode);
	return 0;
}

void StartAuthThread(const char *UserName, const char *Password, const char *DeviceName, const char *Version, const char *H3C_key, int mode_config)
{
	stop_flag = 0;
	mode = mode_config;// 模式1即断线自动重连；模式0无自动重连；模式2 pankiller不发送心跳包，自动重连。
	strcpy(sUserName, UserName);
	strcpy(sPassword, Password);
	strcpy(sDeviceName, DeviceName);
	strcpy(sVersion, Version);
	strcpy(sH3C_key, H3C_key);
	printf("device = %s\n", sDeviceName);
	printf("version = %s\n", sVersion);
	printf("H3C_key = %s\n", sH3C_key);
	authThread = CreateThread(NULL, 0, auth_thread_func, NULL, 0, NULL);
	return;
}

void StopAuthThread()
{
	printf("stop it!");
	if (authThread == NULL)
	{
		printf("authThread == NULL");
		return;
	}
	stop_flag = 1;
	if (WaitForSingleObject(authThread, 10000) != WAIT_OBJECT_0) //等待进程自然退出
	{
		//ExitThread(authThread);//超时后手动退出进程
		printf("进程退出失败\n");
	}
	CloseHandle(authThread);//销毁句柄
	authThread = NULL;
	printf("\nstoped!\n");
	return;
}

char *ReadLog()
{
	//循环读取缓存的消息
	if (log_is_readed == 0)
	{
		memset(outputbuffer, 0, sizeof(outputbuffer));
		for (int i = 0; i < 5; i++)
		{
			if (strlen(logbuffer[i]) > 0)
			{	
				strcat(outputbuffer, logbuffer[i]);
				strcat(outputbuffer, "\r\n");
			}
		}
		memset(logbuffer, 0, sizeof(outputbuffer));
		log_is_readed = 1;
		return outputbuffer;
	}
	else
	{
		return "";
	}

}

void WriteLog(char* info)
{
	//循环写入缓存的消息
	if (strlen(info) < 99)
	{
		strcpy(logbuffer[writer_ptr], info);
		if (writer_ptr == 4)
			writer_ptr = 0;
		else
			writer_ptr++;
	}
	log_is_readed = 0;
	return;
}
