using System.Xml;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// The class which manages the ending
/// </summary>
public class EndingVideo : MonoBehaviour
{
    #region Class Variables

    [Tooltip("The game's camera"), SerializeField]
    private Camera camera;
    
    [Tooltip("The camera's movement speed"), SerializeField]
    private float cameraLerpSpeed;
    
    [Tooltip("The camera's zoom speed"), SerializeField]
    private float cameraZoomSpeed;
    
    [Tooltip("The camera's size in ending"), SerializeField]
    private float minCameraSize;
    
    [Tooltip("The ending video player"), SerializeField]
    private VideoPlayer endingVideoPlayer;
    
    [Tooltip("The ending canvas UI"), SerializeField]
    private GameObject endingCanvas;

    private bool _zoom = true;
    private const float OriginalCameraSize = 5f;
    private const float OriginalCameraPositionZ = -10f;
    private const float OriginalCameraPositionXY = 0f;

    #endregion

    #region Monobehaviour

    private void Start()
    {
        endingVideoPlayer.loopPointReached += EndingVideoFinished;
        endingVideoPlayer.Play();
        SoundManager.Use().FadeIntoEnding();
    }

    private void Update()
    {
        if (_zoom)
        {
            var cameraPos = camera.transform.position;
            var videoPos = transform.position;
            Vector3 pos = new Vector3(videoPos.x, videoPos.y, cameraPos.z);
            camera.transform.position = Vector3.MoveTowards(cameraPos, 
                pos, cameraLerpSpeed * Time.deltaTime);
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize,
                minCameraSize, cameraZoomSpeed * Time.deltaTime);
        }
    }

    #endregion

    /// <summary>
    /// Ends the game
    /// </summary>
    /// <param name="videoPlayer"></param>
    private void EndingVideoFinished(VideoPlayer videoPlayer)
    {
        endingVideoPlayer.Stop();
        endingCanvas.SetActive(true);
        _zoom = false;
        camera.transform.position = new Vector3(OriginalCameraPositionXY, OriginalCameraPositionXY,
            OriginalCameraPositionZ);
        camera.orthographicSize = OriginalCameraSize;
    }
}
