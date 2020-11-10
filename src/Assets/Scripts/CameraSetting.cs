using UnityEngine;

/**
 * The script is enforces the aspect ratio of the given height and width of the screen
   based on source: https://forum.unity.com/threads/force-camera-aspect-ratio-16-9-in-viewport.385541/
 */
public class CameraSetting : MonoBehaviour
{
    #region Properties

    public int screenHeight;
    public int screenWidth;

    #endregion

    #region Variables

    private int _screenSizeX;
    private int _screenSizeY;
    private Camera _gameCamera;

    #endregion

    #region Methods

    /**
     * Rescales Camera based on Screen-Size and adds pillarbox to top or side to enforce aspect ratio of game
     */
    private void RescaleCamera()
    {
        if (Screen.width == _screenSizeX && Screen.height == _screenSizeY) return;

        var targetaspect = screenHeight / (float) screenWidth; //4096x2160
        var windowaspect = Screen.width / (float) Screen.height;
        var scaleheight = windowaspect / targetaspect;

        if (scaleheight < 1.0f)
        {
            var rect = _gameCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            _gameCamera.rect = rect;
        }
        else // add pillarbox
        {
            var scalewidth = 1.0f / scaleheight;

            var rect = _gameCamera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            _gameCamera.rect = rect;
        }

        _screenSizeX = Screen.width;
        _screenSizeY = Screen.height;
    }

    #endregion

    #region Unity Methods

    /**
     * called before camera culls a scene
     */
    private void OnPreCull()
    {
        if (Application.isEditor || Camera.main is null) return;

        var mainCamera = Camera.main;
        var wp = mainCamera.rect;
        var nr = new Rect(0, 0, 1, 1);

        mainCamera.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;
    }

    /**
     * Initializes camera on start
     */
    private void Start()
    {
        _gameCamera = GetComponent<Camera>();
        RescaleCamera();
    }

    /**
     * Update camera every frame
     * TODO: check if rescale every frame causes performance problems
     */
    private void Update()
    {
        RescaleCamera();
    }

    #endregion
}