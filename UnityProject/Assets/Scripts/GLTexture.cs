using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GLTexture : MonoBehaviour
{
    private AndroidJavaObject mGLTexCtrl;
    private int mTextureId;
    private int mWidth;
    private int mHeight;

    public MeshRenderer meshRenderer;
    public Material material;
    public Texture tex;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        tex = material.mainTexture;
        // 实例化com.huya.nativeandroidapp.GLTexture类的对象
        mGLTexCtrl = new AndroidJavaObject("com.huya.nativeandroidapp.GLTexture");
        // 初始化OpenGL
        mGLTexCtrl.Call("setupOpenGL");

    }

    void Start()
    {
        BindTexture();

    }

    void Update()
    {

    }

    void BindTexture()
    {
        // 获取JavaPlugin生成的纹理ID
        mTextureId = mGLTexCtrl.Call<int>("getStreamTextureID");
        if (mTextureId == 0)
        {
            Debug.LogError("getStreamTextureID failed");
            return;
        }
        // Debug.Log("getStreamTextureID success");
        mWidth = mGLTexCtrl.Call<int>("getStreamTextureWidth");
        mHeight = mGLTexCtrl.Call<int>("getStreamTextureHeight");
        // 将纹理ID与当前GameObject绑定
        //GetComponent<MeshRenderer>().material.mainTexture = Texture2D.CreateExternalTexture
        //    (mWidth, mHeight, TextureFormat.ARGB32, false, false, (IntPtr)mTextureId);        
        material.mainTexture = Texture2D.CreateExternalTexture(mWidth, mHeight, TextureFormat.ARGB32, false, false, (IntPtr)mTextureId);
        // 更新纹理数据
        mGLTexCtrl.Call("updateTexture");
    }
}