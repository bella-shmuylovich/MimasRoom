using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The class which manages an entry link
/// </summary>
public class Link : MonoBehaviour
{
    #region Class Variables

    [Tooltip("The entry heading text object"), SerializeField]
    private TextMeshProUGUI heading;
    
    [Tooltip("The fish GameObject"), SerializeField]
    private GameObject fish;
    
    [Tooltip("The contents of the entry"), SerializeField]
    private GameObject contents;
    
    [Tooltip("The text to write when opening an entry"), SerializeField] 
    private List<string> text;
    
    [Tooltip("A string which invites the player to open an entry"), SerializeField]
    private string notification;

    private bool _expanded; // is the entry open
    private const float HoverScaleMultiplier = 1.2f;
    private const float DelayBeforeStartWriting = 2f;

    #endregion

    #region Monobehaviour

    private void OnMouseEnter()
    {
        heading.fontStyle = FontStyles.Bold;
        fish.transform.localScale *= HoverScaleMultiplier;
        TextManager.Use().ShowNotification(notification);
    }

    private void OnMouseExit()
    {
        heading.fontStyle = FontStyles.Normal;
        fish.transform.localScale /= HoverScaleMultiplier;
        TextManager.Use().HideNotification();
    }

    private void OnMouseDown()
    {
        StoryManager.Use().CheckInteraction(transform.parent.gameObject);
        _expanded = !_expanded;
        if (_expanded) contents.SetActive(true);
        else contents.SetActive(false);
        TextManager.Use().HideNotification();
        TextManager.Use().Write(text.ToArray(), true, DelayBeforeStartWriting);
    }

    #endregion

}
