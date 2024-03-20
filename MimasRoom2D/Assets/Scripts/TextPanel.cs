using UnityEngine;

/// <summary>
/// The class which manages the text panel
/// </summary>
public class TextPanel : MonoBehaviour
{
    #region MonoBehaviour

    private void OnMouseDown()
    {
        TextManager.Use().MouseClick();
    }

    #endregion
}
