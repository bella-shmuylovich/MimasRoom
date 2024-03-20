using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The class which manages the image panel
/// </summary>
public class ImagePanel : MonoBehaviour
{
    #region Monobehaviour

    private void OnMouseDown()
    {
        if (TextManager.Use().GetCanWrite()) ImageManager.Use().OnMouseClick();
        else
        {
            TextManager.Use().MouseClick();
        }
    }

    #endregion
}
