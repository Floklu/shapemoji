using UnityEngine;
using UnityEngine.Video;

/**
 * TutorialVideo class is used for functionaliy during Tutorial Video Play
 */
public class TutorialVideo : MonoBehaviour
{
    /**
     * video player in the scene, to play video
     */
    private VideoPlayer _videoPlayer;

    /**
     * Start Method, to initialize variables
     */
    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += EndReached;
    }
    
    /**
     * Exit Method is used, to stop the tutorial video and unload tutorial scene
     */
    public void Exit()
    {
        _videoPlayer.Stop();
        GameSceneManager.Instance.LoadStartMenuScene();
    }
    
    /**
     * End Reached is triggered, when Video was played till the end.
     * Loads Start Menu Scene to continue
     */
    private void EndReached(VideoPlayer vp)
    {
        Exit();
    }

}
