using UnityEngine;

/// <summary>
/// The class which manages the TV
/// </summary>
public class TV : MonoBehaviour
{
    #region Class Variables

    [Tooltip("A string which invites the player to turn the TV on"), SerializeField]
    private string onNotification;
    
    [Tooltip("A string which invites the player to turn the TV off"), SerializeField]
    private string offNotification;
    
    [Tooltip("The on screen of the TVs"), SerializeField]
    private GameObject[] tvOn = new GameObject[2]; // 0 - front TV, 1 - right TV
    
    [Tooltip("The material to switch when the mouse hovers on the TV"), SerializeField]
    private Material outlineMaterial;
    
    private Material _originalMaterial; // the original material of the TV
    private string _notification; // the current notification
    private bool _on; // is the TV on

    #endregion

    #region Monobehaviour 

    void Awake()
    {
        _originalMaterial = tvOn[0].GetComponent<SpriteRenderer>().material;
        _notification = onNotification;
    }
    private void OnMouseEnter()
    {
        foreach (var tv in tvOn) tv.GetComponent<SpriteRenderer>().material = outlineMaterial;
        TextManager.Use().ShowNotification(_notification);
    }
    
    private void OnMouseExit()
    {
        foreach (var tv in tvOn) tv.GetComponent<SpriteRenderer>().material  = _originalMaterial;
        TextManager.Use().HideNotification();
    }

    /// <summary>
    /// Turn TV on or off
    /// </summary>
    private void OnMouseDown()
    {
        _on = !_on;
        tvOn[0].SetActive(_on);
        tvOn[1].SetActive(_on);
        _notification = !_on ? onNotification : offNotification;
        TextManager.Use().ShowNotification(_notification);
        SoundManager.Use().TurnOnTVSounds(_on);
    }

    #endregion
}
