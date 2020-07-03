# AndroidUnityRender

本项目目的是在移动端的 Native App 中以库的形式集成已经写好的 Unity 工程，利用 Unity 游戏引擎便捷的开发手段进行跨平台开发。

Unity官方文档 [Unity as a Library integration example to iOS and Android](https://github.com/Unity-Technologies/uaal-example)

## 项目构建过程

### 环境

- Android Studio `3.5.3`
- Unity version `2019.3.7f1`

### 新建工程操作步骤（安卓）

#### Step.1

- Android Studio 新建项目：
  ![](README.assets\1536438-20200511193628285-297005542.png)
- Unity 新建项目：![](README.assets\1536438-20200511193939816-1606248075.png)
- 最终工程结构如下：
- ![](README.assets\1536438-20200511193911156-612425801.png)

#### Step.2

- 通过 Unity 打开 UnityProject

- 选择 File -> Build Settings -> Switch Android Platform -> Export Project 

  ![](README.assets\1536438-20200511195140197-1426097963.png)


  ![](README.assets\1536438-20200511195320262-57340212.png)

- 这时候选择Export可能会提示JDK路径配置出错，没关系我们到 Preference -> Externl Tools 设置路径 
  ![](README.assets\1536438-20200511195637325-311104231.png)

![](README.assets\1536438-20200511195726879-2115298771.png)

- 可以点击Export了，路径选择可以自由选择，这里建议按照官方来
  ![](README.assets\1536438-20200511200219564-1834859668.png)

#### Step.3

- 通过 Android Studio 打开 NativeAndroidApp

- 选择 **setting.gradle** 文件添加 unityLibrary module

  ```C++
  include ':unityLibrary'
  project(':unityLibrary').projectDir = new File('..\\UnityProject\\androidBuild\\unityLibrary')
  ```

- 选择 **build.gradle**（Module：app）添加 dependencies

  ```C++
  dependencies {
      implementation project(':unityLibrary')
      implementation fileTree(dir: project(':unityLibrary').getProjectDir().toString() + ('\\libs'), include: ['*.jar'])
      // 自己项目的配置
  }
  ```

- 选择 **build.gradle**（Module：NativeAndroidApp）

  ```C++
  allprojects {
      repositories {
          google()
          jcenter()
  
          // Add Code
          flatDir {
              dirs "${project(':unityLibrary').projectDir}/libs"
          }
          // End
      }
  }
  ```

- 选择 NativeAndroidApp 的 **strings.xml** 添加

  ```C++
  <resources>
      <string name="app_name">NativeAndroidApp</string>
      <string name="action_settings">Settings</string>
      // Add Code
      <string name="game_view_content_description">Game view</string>
      // End
  </resources>
  ```
  之后再在 AndroidStudio 内 Sync project 即可。