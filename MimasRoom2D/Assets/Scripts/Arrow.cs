using UnityEngine;

/// <summary>
/// The class which manages the arrow gameobjects
/// </summary>
public class Arrow : MonoBehaviour
{
    #region Class Variables
    
    [Tooltip("The direction at which the arrow is pointing"), SerializeField]
    private int arrowDirection;
    
    [Tooltip("The arrow which points in the other direction"), SerializeField]
    private Arrow otherArrow;
    
    [Tooltip("The color of the arrow when the mouse is hovering above it"), SerializeField]
    private Color _hoverColor;
    
    [Tooltip("The color of the arrow when it's disabled"), SerializeField] 
    private Color _disabledColor;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor; // original color of the arrow
    private Vector3 _originalSize; // original size of the arrow
    private bool _enabled = true; // is the arrow enabled
    private const float HoverScaleMultiplier = 1.2f;
    private const int LeftArrow = -1;
    private const int RightArrow = 1;
    private const int LeftScene = 0;
    private const int RightScene = 2;

    #endregion


    #region Monobehaviour

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _originalSize = transform.localScale;
    }

    private void OnMouseEnter()
    {
        if (_enabled)
        {
            transform.localScale *= HoverScaleMultiplier;
            _spriteRenderer.color = _hoverColor;
        }
    }
    
    private void OnMouseExit()
    {
        if (_enabled)
        {
            transform.localScale /= HoverScaleMultiplier;
            _spriteRenderer.color = _originalColor;
        }
    }

    private void OnMouseDown()
    {
        if (_enabled)
        {
            SceneManager.Use().ChangeScene(arrowDirection);
            UpdateArrowColor();
            otherArrow.UpdateArrowColor();
        }
    }

    #endregion

    #region API

    /// <summary>
    /// Updates the arrow color according to state (enabled\disabled)
    /// </summary>
    public void UpdateArrowColor()
    {
        if ((SceneManager.Use().GetSceneNumber() == LeftScene && arrowDirection == LeftArrow) ||
            (SceneManager.Use().GetSceneNumber() == RightScene && arrowDirection == RightArrow))
        {
            _enabled = false;
            _spriteRenderer.color = _disabledColor;
            transform.localScale = _originalSize;
        }
        else if (!_enabled)
        {
            _enabled = true;
            _spriteRenderer.color = _originalColor;
        }
    }

    #endregion
}
