//
// Created by Administrator on 2020/7/15.
//

#include <jni.h>
#include "cpp_test.h"
extern "C" JNIEXPORT jstring JNICALL
Java_com_huya_nativeandroidapp_MainActivity_stringFromCpp(
        JNIEnv *env,
        jclass type)
{
    const char * cppStr = cppFunction();
    return env->NewStringUTF(cppStr);
}

