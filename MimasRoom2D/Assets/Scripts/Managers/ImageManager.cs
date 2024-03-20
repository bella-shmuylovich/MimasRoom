using System.Collections;
using UnityEngine;

/// <summary>
/// The class which manages image viewing
/// </summary>
public class ImageManager : MonoBehaviour
{
    #region Class Variables
    
    [Tooltip("The panel on which the image is shown"), SerializeField] 
    private GameObject imagePanel;

    private static ImageManager _imageManager;
    private GameObject _image; // image to show
    private bool _isShowing;
    private bool _isWriting;
    private const float DelayBeforeText = 0f;

    #endregion

    #region Monobehaviour 

    private void Awake()
    {
        _imageManager = this;
    }

    #endregion

    #region API
    
    public static ImageManager Use() => _imageManager;

    /// <summary>
    /// Shows an image
    /// </summary>
    /// <param name="imageToShow"></param>
    /// <param name="text"></param>
    public void Show(GameObject imageToShow, string[] text)
    {
        if (!_isShowing)
        {
            _isShowing = true;
            SceneManager.Use().EnableInteractions(false);
            _image = imageToShow;
            imagePanel.SetActive(true);
            _image.SetActive(true);
            if (!_isWriting)
            {
                _isWriting = true;
                TextManager.Use().Write(text, false, DelayBeforeText);
            }
            if (_image.name.Contains("Letter")) SoundManager.Use().FadeIntoSuspension();
        }
    }

    public void OnMouseClick()
    {
        if (_isShowing)
        {
            _isShowing = false;
            _isWriting = false;
            HideImage();
        } 
    }

    #endregion
    
    /// <summary>
    /// Stops showing an image
    /// </summary>
    private void HideImage()
    {
        imagePanel.SetActive(false);
        _image.SetActive(false);
        SceneManager.Use().EnableInteractions(true);
        if (_image.name.Contains("Letter")) SoundManager.Use().FadeOutOfSuspension();
    }
   
}
