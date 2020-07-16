#CMake版本信息
cmake_minimum_required(VERSION 3.4.1)

#设置生成的so动态库最后输出的路径
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${PROJECT_SOURCE_DIR}/libs/${ANDROID_ABI})

#加载fin engine lib
add_library(unity-transfer-lib
    SHARED
    main.cpp
    UnityTransfer.cpp
)
target_link_libraries(unity-transfer-lib
    log
    android
)