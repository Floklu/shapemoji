using UnityEngine;
using System.Collections.Generic;
 
/// <summary>
/// The script is responsible for the resolution of the camera
/// based on source: https://forum.unity.com/threads/force-camera-aspect-ratio-16-9-in-viewport.385541/
/// </summary>
public class CameraSetting : MonoBehaviour
{
 
 
    #region Variables
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;
    #endregion
 
    #region Methods
 
    #region rescale camera
    private void RescaleCamera()
    {
 
        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;
 
        float targetaspect = 256f/135f; //4096x2160
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();
 
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;
 
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
 
            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;
 
            Rect rect = camera.rect;
 
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
 
            camera.rect = rect;
        }
 
        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }
    #endregion
 
    #endregion
 
    #region Unity Methods
 
    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);
 
        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);
       
        Camera.main.rect = wp;
 
    }
 
    // Use this for initialization
    void Start () {
        RescaleCamera();
    }
   
    // Update is called once per frame
    void Update () {
        RescaleCamera();
    }
    #endregion
}