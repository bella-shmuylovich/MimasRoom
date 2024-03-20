using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class which manages an intractable object in the game
/// </summary>
public class Interactions : MonoBehaviour
{
    #region Class Variables
    
    enum InteractionType{
        Text,
        Image,
        Scene
    };
    
    [Tooltip("The material of the objects when interactions are enabled"), SerializeField]
    private Material enabledOutlineMaterial;
    
    [Tooltip("The material of the objects when interactions are disabled"), SerializeField]
    private Material disabledOutlineMaterial;
    
    [Tooltip("A string which invites the player to interact with the object"), SerializeField]
    private string notification;
    
    [Tooltip("The Image interaction, if includes one"), SerializeField]
    private GameObject image;
    
    [Tooltip("The text interaction, if includes one"), SerializeField] 
    private List<string> text;
    
    [Tooltip("The scene interaction, if includes one"), SerializeField]
    private GameObject scene;
    
    [Tooltip("The Type of the main interaction"), SerializeField] 
    private InteractionType interactionType;
    
    [Tooltip("If the interaction is accessible from another scene"), SerializeField] 
    private GameObject twinInteraction;

    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Material _outlineMaterial;
    private bool _enabled; // are interactions enabled
    private const float DelayBeforeStartWriting = 0f;
    private const String Clock = "Clock";
    private const String ClockPrefix = "The time is ";
    private const String UnlockInteractionHint = "Interact with more objects to unlock";

    #endregion

    #region Monobehavoiur

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
        if (!_enabled) _outlineMaterial = disabledOutlineMaterial;
    }
    
    private void OnMouseEnter()
    {
        _spriteRenderer.material = _outlineMaterial;
        if (_enabled) TextManager.Use().ShowNotification(notification);
        else TextManager.Use().ShowNotification(UnlockInteractionHint);
    }
    
    private void OnMouseExit()
    {
        _spriteRenderer.material = _originalMaterial;
        TextManager.Use().HideNotification();
    }
    
    private void OnMouseDown()
    {
        if (_enabled)
        {
            StoryManager.Use().CheckInteraction(gameObject);
            if (twinInteraction != null) StoryManager.Use().CheckInteraction(twinInteraction);
            _spriteRenderer.material = _originalMaterial;
            if (interactionType.Equals(InteractionType.Text)) TextInteraction();
            else if (interactionType.Equals(InteractionType.Image)) ImageInteraction();
            else if (interactionType.Equals(InteractionType.Scene)) SceneInteraction();
        }
    }

    #endregion

    #region API

    /// <summary>
    /// Enable the iteraction
    /// </summary>
    /// <param name="isEnabled"></param>
    public void Enable(bool isEnabled)
    {
        _enabled = isEnabled;
        if (_enabled)
        {
            _outlineMaterial = enabledOutlineMaterial;
        }
        else
        {
            _originalMaterial = disabledOutlineMaterial;
        }
    }

    #endregion

    /// <summary>
    /// Start text interaction
    /// </summary>
    private void TextInteraction()
    {
        TextManager.Use().HideNotification();
        if (gameObject.name.Contains(Clock))
        {
            text = new List<string>() { ClockPrefix + StoryManager.Use().GetTime() };
        }
        TextManager.Use().Write(text.ToArray(), true, DelayBeforeStartWriting);
    }

    /// <summary>
    /// Start image interaction
    /// </summary>
    private void ImageInteraction()
    {
        TextManager.Use().HideNotification();
        ImageManager.Use().Show(image, text.ToArray());
    }

    /// <summary>
    /// Start scene interaction
    /// </summary>
    private void SceneInteraction()
    {
        TextManager.Use().HideNotification();
        SceneManager.Use().OpenMiniScene(scene);
    }
}
