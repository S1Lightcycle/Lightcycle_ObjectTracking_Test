using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using OpenCvSharp;

public class ObjectTracking : MonoBehaviour {

	private int height;
	private int width;
	private CvCapture _cap;
	private Texture2D _tex;
	private IplImage _capImage;
	public GameObject background;

	void Awake(){
		_cap = new CvCapture (0);
		_capImage = _cap.QueryFrame ();
		width = _capImage.Width;
		height = _capImage.Height;
		_tex = new Texture2D(0, 0, TextureFormat.RGB24, false);
		background.renderer.sharedMaterial.mainTexture = _tex;
		//UpdateAspectRatio ();
	}

	// Update is called once per frame
	void Update ()
	{
		_capImage = _cap.QueryFrame();
		ShowImage ();
	}
	
	public void ShowImage()
	{
		if (_tex.width != _capImage.Width || _tex.height != _capImage.Height)
			_tex.Resize(_capImage.Width, _capImage.Height);

		var raw = new Byte[3*_capImage.Width*_capImage.Height];
		System.IntPtr rawPtr;
		_capImage.GetRawData(out rawPtr);
		Marshal.Copy(rawPtr, raw, 0, raw.Length);
		_tex.LoadRawTextureData(raw);
		_tex.Apply();
		background.renderer.sharedMaterial.mainTexture = _tex;
	}

	void UpdateAspectRatio() {
		var s = background.transform.localScale;
		s.x = s.y * width / height;
		background.transform.localScale = s;
	}
}
