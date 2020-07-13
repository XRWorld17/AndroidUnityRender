# AndroidUnityRender

本项目的目是通过**安卓和 Unity 共享 gl 上下文**实现以下的流程：

> Android/iOS native app 操作摄像头 -> 获取视频流数据 -> 人脸检测或美颜 -> 传输给 Unity 渲染 -> Unity做出更多的效果（滤镜/粒子）

详细实现方法请看：[Unity安卓共享纹理](https://www.cnblogs.com/zhxmdefj/p/13295243.html)

---

本项目是使用 Unity 导出库内嵌入安卓进行开发的，根目录主要有两个文件夹：

1. [NativeAndroidApp](https://github.com/zhxmdefj/AndroidUnityRender/tree/master/NativeAndroidApp) 安卓 native app
2. [UnityProject](https://github.com/zhxmdefj/AndroidUnityRender/tree/master/UnityProject) Unity Activity 的原项目

详细部署方法请看：[Android/iOS内嵌Unity开发示例](https://www.cnblogs.com/zhxmdefj/p/13273560.html) ，clone 后需要自己进行 [Unity 导出](https://www.cnblogs.com/zhxmdefj/p/13273560.html#1338828472) 和安卓工程的 [gradle 配置](https://www.cnblogs.com/zhxmdefj/p/13273560.html#2573950077)