//
// Created by Administrator on 2020/7/15.
//
#include <jni.h>
#include "includes/c_test.h"
#include "stdio.h"
const char * cFunction(){
    const char * str = "String from C !";
    return str;
}

//Android中java代码调用此方法
jint
Java_com_huya_nativeandroidapp_MainActivity_TestAddInt( JNIEnv* env, jobject thiz ,jint a,jint b)
{
//    return addInt(a,b);
}

//Unity中C#代码调用此方法
int addInt(int a, int b)
{
    return a + b;
}