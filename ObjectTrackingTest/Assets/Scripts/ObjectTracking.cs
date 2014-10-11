using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using OpenCvSharp;

public class ObjectTracking : MonoBehaviour {

	private CvCapture _cap;
    private Texture2D _tex;
    private IplImage _capImage;
    public GameObject background;

	// Use this for initialization
	void Start () {
        background = GameObject.Find("Quad");
		_cap = new CvCapture (0);
        _capImage = _cap.QueryFrame();
        _tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
	    background.renderer.sharedMaterial.mainTexture = _tex;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _capImage = _cap.QueryFrame();
	}

    public void ShowImage()
    {
        var raw = new Byte[3*_capImage.Width*_capImage.Height];
        System.IntPtr rawPtr;
        _capImage.GetRawData(out rawPtr);
        Marshal.Copy(rawPtr, raw, 0, raw.Length);
        _tex.LoadRawTextureData(raw);
        _tex.Apply();
    }
}
