using UnityEngine;

/// <summary>
/// The class which manages the computer screen
/// </summary>
public class Screen : MonoBehaviour
{
    #region Class Variables
    
    [Tooltip("The material to switch when the mouse hovers on the screen"), SerializeField]
    private Material outline;
    
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider;
    private Material _originalMaterial;
    private bool _on;
    private const float ScreenOnAlpha = 0.4f;

    #endregion

    #region Monobehaviour
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnMouseEnter()
    {
        if (!_on) _spriteRenderer.material = outline;
    }
    
    private void OnMouseExit()
    {
        if (!_on) _spriteRenderer.material = _originalMaterial;
    }
    
    /// <summary>
    /// "Turns on" the screen
    /// </summary>
    private void OnMouseDown()
    {
        _on = true;
        _spriteRenderer.material = _originalMaterial;
        Color tint = _spriteRenderer.color;
        tint.a = ScreenOnAlpha;
        _spriteRenderer.color = tint;
        _polygonCollider.enabled = false;
    }
    

    #endregion
}
