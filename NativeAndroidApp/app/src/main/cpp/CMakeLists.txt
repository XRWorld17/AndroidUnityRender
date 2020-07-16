cmake_minimum_required(VERSION 3.4.1)

#设置生成的so动态库最后输出的路径
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${PROJECT_SOURCE_DIR}/libs/${ANDROID_ABI})

##.h文件目录
#include_directories(
#        ${CMAKE_SOURCE_DIR} /includes #h文件目录
#)

add_library( cppso
        SHARED
        c_test.c
        cpp_test.cpp
)

find_library( #调用系统库
        log-lib
        log )

target_link_libraries( # Specifies the target library.
        cppso
        ${log-lib} )

