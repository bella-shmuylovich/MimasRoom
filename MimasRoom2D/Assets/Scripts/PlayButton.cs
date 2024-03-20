using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The class which manages the ending video's play button
/// </summary>
public class PlayButton : MonoBehaviour
{
    #region Class Variables

    [Tooltip("The first frame of the ending video"), SerializeField]
    private GameObject firstFrame;
    
    [Tooltip("The GameObject The raw image which holds the ending video"), SerializeField]
    private GameObject endingVideo;
    
    [Tooltip("The colliders to disable when the ending video is playing"), SerializeField]
    private List<Collider2D> collidersToDisable;
    
    [Tooltip("The scroll view to freeze when the video is playing"), SerializeField]
    private ScrollRect scrollRect;
    
    [Tooltip("The contents of the scroll view to check when first frame is frame"), SerializeField]
    private RectTransform contents;

    private const float EndingVideoPosThreshold = 31.5f;
    private const float HoverScaleMultiplier = 1.2f;

    #endregion

    #region Monobehaviour

    private void OnMouseEnter()
    {
        if (contents.transform.position.y > EndingVideoPosThreshold) 
            transform.localScale *= HoverScaleMultiplier;
    }

    private void OnMouseExit()
    {
        if (contents.transform.position.y > EndingVideoPosThreshold)
            transform.localScale /= HoverScaleMultiplier;
    }

    /// <summary>
    /// Starts the ending video
    /// </summary>
    private void OnMouseDown()
    {
        if (contents.transform.position.y >EndingVideoPosThreshold)
        {
            endingVideo.SetActive(true);
            endingVideo.transform.position = firstFrame.transform.position;
            firstFrame.SetActive(false);
            StoryManager.Use().CheckInteraction(gameObject);
            scrollRect.horizontal = false;
            scrollRect.vertical = false;
            foreach (var col in collidersToDisable)
            {
                col.enabled = false;
            }
        }
    }

    #endregion
}
