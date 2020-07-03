using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UICameraHolder : MonoBehaviour
{

    public Texture2D texture;
    int textureId;

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
    private int _updateTexture()
    {
        //Debug.Log("_updateTexture");
        return nativeCameraHolder.Call<int>("updateTexture");
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
        _openCamera();
        GetComponent<RectTransform>().sizeDelta = new Vector2((float)_getWidth(), (float)_getHeight());
    }

    void Stop()
    {
        _closeCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFrameUpdated())
        {
            textureId = _updateTexture();
            if (texture == null && textureId != 0)
            {
                Debug.Log("create external texture");
                texture = Texture2D.CreateExternalTexture(_getWidth(), _getHeight(),
                    TextureFormat.RGB565, false, false, (IntPtr)textureId);
                texture.wrapMode = TextureWrapMode.Clamp;
                texture.filterMode = FilterMode.Bilinear;
            }
            else if (textureId != 0)
            {
                texture.UpdateExternalTexture((IntPtr)textureId);
            }

            //if (GetComponent<RawImage> () != null) {
            //	GetComponent<RawImage> ().texture = texture;
            //	GetComponent<RawImage> ().color = Color.white;
            //} else {
            //	GetComponent<Renderer> ().material.mainTexture = texture;
            //}

            GetComponent<RawImage>().texture = texture;
        }

    }
}
