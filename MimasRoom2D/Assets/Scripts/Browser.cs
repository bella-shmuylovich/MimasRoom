using System;
using TMPro;
using UnityEngine;

/// <summary>
/// The class which manages the browser
/// </summary>
public class Browser : MonoBehaviour
{
    #region Class variables

    [Tooltip("the input field for the url"), SerializeField]
    private TMP_InputField urlBox;
    
    [Tooltip("The site GameObject"), SerializeField]
    private GameObject site;

    private const String EmptyText = "";
    private const String SiteURL = "http://www.mimasroom.jp";

    #endregion

    #region Monobehaviour

    void Update()
    {
        HandleInput();
    }


    #endregion
    
    /// <summary>
    /// Handles the url input
    /// </summary>
    private void HandleInput()
    {
        if (urlBox.text != EmptyText)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (urlBox.text.Equals(SiteURL))
                {
                    site.SetActive(true);
                    SoundManager.Use().FadeIntoSuspension();
                }
            }
        }
        else if (!urlBox.isFocused && Input.GetKeyDown((KeyCode.Return))) urlBox.ActivateInputField();
    }
}
