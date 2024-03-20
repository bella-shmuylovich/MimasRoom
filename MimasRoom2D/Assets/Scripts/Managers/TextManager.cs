using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The class which manages text viewing
/// </summary>
public class TextManager : MonoBehaviour
{
    [Tooltip("The panel which holds the text"), SerializeField] 
    private GameObject panel;
    
    [Tooltip("The text object"), SerializeField]
    private TextMeshProUGUI textUI;
    
    [Tooltip("The notification text object"), SerializeField]
    private TextMeshProUGUI notificationTextUI;
    
    [Tooltip("The time between sowing a new character when writing the text"), SerializeField]
    private float timeBetweenCharacters;

    private static TextManager _textManager;
    private string[] _text; // text to write
    private int _curTextIdx = -1;
    private bool _isWriting; // is currently writing
    private bool _canWrite = true;
    private bool _arrows; // should show arrows 
    private const String EmptyText = "";
    private const int StartTextIdx = -1;
    private const int Zero = 0;
    private const int MinTextLength = 0;

    #region Monobehaviour

    private void Awake()
    {
        _textManager = this;
    }

    #endregion

    #region API

    public static TextManager Use() => _textManager;

    public bool GetCanWrite() => _canWrite;

    /// <summary>
    /// Show the text panel and prepare to writing the text
    /// </summary>
    /// <param name="textToWrite"></param>
    /// <param name="arrows"></param>
    /// <param name="delayBeforeStart"></param>
    public void Write(string[] textToWrite, bool arrows, float delayBeforeStart)
    {
        if (_canWrite && textToWrite.Length > MinTextLength)
        {
            _canWrite = false;
            panel.SetActive(true);
            _arrows = arrows;
            if (_arrows) SceneManager.Use().EnableInteractions(false);
            _text = textToWrite;
            _curTextIdx = Zero;
            StartCoroutine(WriteText(_text[_curTextIdx], delayBeforeStart));
        }
    }

    /// <summary>
    /// Shows the notification text
    /// </summary>
    /// <param name="text"></param>
    public void ShowNotification(string text)
    {
        notificationTextUI.text = text;
        notificationTextUI.enabled = true;
    }
    
    /// <summary>
    /// Hides the notification text
    /// </summary>
    public void HideNotification()
    {
        notificationTextUI.text = "";
        notificationTextUI.enabled = false;
    }
    
    /// <summary>
    /// Handles mouse click - show all text, move to next text, close 
    /// </summary>
    public void MouseClick()
    {
        if (!_isWriting)
        {
            _curTextIdx++;
            if (_curTextIdx < _text.Length)
            {
                StartCoroutine(WriteText(_text[_curTextIdx], 0f));
            }
            else if (_curTextIdx >= _text.Length)
            {
                _curTextIdx = StartTextIdx;
                DeleteText();
                _isWriting = false;
            }
        }
        else
        {
            _isWriting = false;
            StopAllCoroutines();
            textUI.text = _text[_curTextIdx];
        }
    }
    
    #endregion
    
    /// <summary>
    /// Writes the text on the screen
    /// </summary>
    /// <param name="text"></param>
    /// <param name="delayBeforeStart"></param>
    /// <returns></returns>
    private IEnumerator WriteText(string text, float delayBeforeStart)
    {
        _isWriting = true;
        textUI.text = EmptyText;
        yield return new WaitForSeconds(delayBeforeStart);
        foreach (char c in text)
        {
            if (textUI.text.Length > MinTextLength)
            {
                textUI.text = textUI.text.Substring(Zero, textUI.text.Length);
            }
            textUI.text += c;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        _isWriting = false;
    }
    
    /// <summary>
    /// Hides the text
    /// </summary>
    private void DeleteText()
    {
        textUI.text = EmptyText;
        panel.SetActive(false);
        if (_arrows) SceneManager.Use().EnableInteractions(true);
        _curTextIdx = StartTextIdx;
        _canWrite = true;
    }
}
