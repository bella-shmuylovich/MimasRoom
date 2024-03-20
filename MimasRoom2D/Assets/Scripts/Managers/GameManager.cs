using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

/// <summary>
/// The game manager
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Class Variables

    [Tooltip("Choose if to play the opening video or skip it"), SerializeField]
    private bool playOpeningVideo;
    
    [Tooltip("The opening video player"), SerializeField]
    private VideoPlayer openingVideoPlayer;
    
    [Tooltip("GameObject with raw image component that holds the opening video"), SerializeField] 
    private GameObject openingVideo;
    
    [Tooltip("Colliders to enable when the game starts"), SerializeField] 
    private Collider2D[] collidersToEnable;
    
    [Tooltip("The speed in which the opening video fades out"), SerializeField]
    private float fadeSpeed;

    private StoryManager _storyManager;
    private bool _fadeIntoGame;
    private const float FadeOpeningAlphaThreshold = 0.01f;

    #endregion

    #region Monobehaviour 

    private void Awake()
    {
        _storyManager = GetComponent<StoryManager>();
    }

    private void Start()
    {
        if (playOpeningVideo)
        {
            openingVideoPlayer.loopPointReached += OpeningVideoFinished;
            openingVideo.SetActive(true);
            openingVideoPlayer.Play();
        }
        else
        {
            _storyManager.enabled = true;
            foreach (var col in collidersToEnable)
            {
                col.enabled = true;
            }
            SoundManager.Use().FadeOutOfOpening();
        }
    }

    private void FixedUpdate()
    {
        if (_fadeIntoGame)
        {
            Color color = Color.Lerp(   openingVideo.GetComponent<RawImage>().color, Color.clear, fadeSpeed * Time.deltaTime);
            openingVideo.GetComponent<RawImage>().color = color;
            if (color.a <= FadeOpeningAlphaThreshold)
            {
                _fadeIntoGame = false;
                openingVideo.SetActive(false);
            }
        }
    }

    #endregion

    #region API

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MimasRoom");
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    /// <summary>
    /// Fades out of opening video, and starts the game
    /// </summary>
    /// <param name="videoPlayer"></param>
    private void OpeningVideoFinished(VideoPlayer videoPlayer)
    {
        openingVideoPlayer.Stop();
        videoPlayer.enabled = false;
        _storyManager.enabled = true;
        openingVideo.GetComponent<RawImage>().color = Color.black;
        _fadeIntoGame = true;
        foreach (var col in collidersToEnable)
        {
            col.enabled = true;
        }
        SoundManager.Use().FadeOutOfOpening();
    }
    
}
