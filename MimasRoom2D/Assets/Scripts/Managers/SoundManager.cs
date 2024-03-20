using System.Collections;
using UnityEngine;

/// <summary>
/// The class which manages the music and sfx in the game
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region Class Variables

    [Tooltip("The AudioSource which plays the opening music"), SerializeField]
    private AudioSource openingMusic;
    
    [Tooltip("The AudioSource which plays the suspension music"), SerializeField]
    private AudioSource suspensionMusic;
    
    [Tooltip("The AudioSource which plays the ending music"), SerializeField]
    private AudioSource endingMusic;
    
    [Tooltip("The AudioSource which plays the TV sounds"), SerializeField]
    private AudioSource tvSounds;
    
    [Tooltip("The amount of times it takes for a short fade between tracks"), SerializeField]
    private float shortFadeDuration;
    
    [Tooltip("The amount of times it takes for a long fade between tracks"),SerializeField]
    private float longFadeDuration;
    
    [Tooltip("The loudest volume for ending, opening and suspension music"), SerializeField]
    private float maxSongsVolume;
    
    [Tooltip("The loudest volume for tv sounds"),SerializeField]
    private float maxTVVolume;
    
    private static SoundManager _soundManager;
    private const float MinVolume = 0f;
    private const float StartFadeTime = 0f;

    #endregion
    
    #region Monobehaviour 

    private void Awake()
    {
        _soundManager = this;
    }

    #endregion

    #region API

    public static SoundManager Use() => _soundManager;

    /// <summary>
    /// Fade into suspension music
    /// </summary>
    public void FadeIntoSuspension()
    {
        FadeOutOfTV();
        StartCoroutine(Fade(suspensionMusic, longFadeDuration, maxSongsVolume));
    }

    /// <summary>
    /// Fade out of suspension music
    /// </summary>
    public void FadeOutOfSuspension()
    {
        StartCoroutine(Fade(suspensionMusic,longFadeDuration, MinVolume));
        FadeIntoTV();
    }

    /// <summary>
    /// Fade into ending music
    /// </summary>
    public void FadeIntoEnding()
    {
        TurnOnTVSounds(false);
        FadeOutOfSuspension();
        StartCoroutine(Fade(endingMusic, shortFadeDuration, maxSongsVolume));
    }

    /// <summary>
    /// Fade out of opening music
    /// </summary>
    public void FadeOutOfOpening()
    {
        StartCoroutine(Fade(openingMusic, shortFadeDuration, MinVolume));
        FadeIntoTV();
    }

    /// <summary>
    /// Fade out of TV sounds
    /// </summary>
    public void FadeOutOfTV()
    {
        StartCoroutine(Fade(tvSounds, shortFadeDuration, MinVolume));
    } 
    
    /// <summary>
    /// Fade into TV sounds
    /// </summary>
    public void FadeIntoTV()
    {
        StartCoroutine(Fade(tvSounds, shortFadeDuration, maxTVVolume));
    } 

    /// <summary>
    /// Start TV sounds
    /// </summary>
    /// <param name="on"></param>
    public void TurnOnTVSounds(bool on)
    {
        if (on) tvSounds.Play();
        else tvSounds.Pause();
    }
    
    #endregion

    /// <summary>
    /// Fade the track to target volume
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <param name="source"></param>
    /// <param name="duration"></param>
    /// <param name="targetVolume"></param>
    /// <returns></returns>
    private IEnumerator Fade(AudioSource source, float duration, float targetVolume)
    {
        float time = StartFadeTime;
        float startVolume = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
        yield break;
    }
}
