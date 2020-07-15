using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class CameraHolder : MonoBehaviour
{

    public Texture2D texture;
    int textureId;
    
    //public Material CyberpunkMat;
    //public Material MosaicMat;

    public bool isLock;

    int i = 0;

    //引用C 、C++中的方法
    [DllImport("cppso")]
    private static extern int addInt(int a, int b);

    #region nativeFunction
    AndroidJavaObject nativeCameraHolder;
#if UNITY_ANDROID
    private void _openCamera()
    {
        //Debug.Log("_openCamera");
        nativeCameraHolder.Call("openCamera");
    }
    private void _closeCamera()
    {
        //Debug.Log("_closeCamera");
        nativeCameraHolder.Call("closeCamera");
    }
    private bool _isFrameUpdated()
    {
        //Debug.Log("_isFrameUpdated");
        return nativeCameraHolder.Call<bool>("isFrameUpdated");
    }
    private bool _isTextureReaded()
    {
        //Debug.Log("_isTextureReaded");
        return nativeCameraHolder.Call<bool>("isTextureReaded");
    }
    private int _updateTexture()
    {
        //Debug.Log("_updateTexture");
        return nativeCameraHolder.Call<int>("updateTexture");
    }
    private bool _copyTexture()
    {
        //Debug.Log("_copyTexture");
        return nativeCameraHolder.Call<bool>("copyTexture");
    }
    private int _getWidth()
    {
        //Debug.Log("_getWidth");
        return nativeCameraHolder.Call<int>("getWidth");
    }
    private int _getHeight()
    {
        //Debug.Log("_getHeight");
        return nativeCameraHolder.Call<int>("getHeight");
    }
#endif
    #endregion
    
    void Awake()
    {
#if UNITY_ANDROID
        nativeCameraHolder = new AndroidJavaObject("com.huya.nativeandroidapp.CameraHolder");
        if (nativeCameraHolder == null)
            Debug.Log("Start nativeCameraHolder is null");
#endif
    }

    // Use this for initialization
    void Start()
    {
        //调用方法中相加函数
        i = addInt(1, 2);

        isLock = true;
        _openCamera();
    }

    void Stop()
    {
        _closeCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("use c = " + i);
        Debug.Log("isLock = "+ isLock);
        if (_isFrameUpdated())
        {
            textureId = _updateTexture();
            if (texture == null && textureId != 0)
            {
                // entry only once
                Debug.Log("create external texture");
                texture = Texture2D.CreateExternalTexture(_getWidth(), _getHeight(), 
                    TextureFormat.RGB565, false, false, (IntPtr)textureId);
                texture.wrapMode = TextureWrapMode.Clamp;
                texture.filterMode = FilterMode.Bilinear;
            }
            else if (textureId != 0)
            {
                if (isLock)
                {
                    isLock = false;
                    texture.UpdateExternalTexture((IntPtr)textureId);
                    GetComponent<MeshRenderer>().material.mainTexture = texture;
                    _copyTexture();//set isLock=true after copy
                }
                else
                    Debug.Log("Waiting");
            }
        }
    }

    void setIsLock(string boolStr)
    {
        Debug.Log(boolStr);
        if (boolStr == "0")
        {
            isLock = false;
            Debug.Log(isLock);
        }
        else
        {
            isLock = true;
            Debug.Log(isLock);
        }
    }
    //void SwitchShader(string str)
    //{
    //    //Debug.Log("SwitchShader Called");
    //    //Debug.Log("currMat      = " + GetComponent<MeshRenderer>().material.name);
    //    //Debug.Log("CyberpunkMat = " + CyberpunkMat.name + "(Instance)");
    //    //Debug.Log("MosaicMat    = " + MosaicMat.name + "(Instance)");
    //    if (GetComponent<MeshRenderer>().material.name == (CyberpunkMat.name + " (Instance)"))
    //    {
    //        //Debug.Log("Switch to MosaicMat");
    //        GetComponent<MeshRenderer>().material = MosaicMat;        
    //    }
    //    else
    //    {
    //        //Debug.Log("Switch to CyberpunkMat");
    //        GetComponent<MeshRenderer>().material = CyberpunkMat;
    //    }
    //}
}
