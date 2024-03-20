using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class which manages the flow between different areas
/// </summary>
public class SceneManager : MonoBehaviour
{
    #region Class Variables

    [Tooltip("The different areas of the game"), SerializeField] 
    private GameObject[] scenes = new GameObject[4];
    
    [Tooltip("Arrow GameObjects which are used to navigate the game"), SerializeField]
    private GameObject[] arrows;
    
    [Tooltip("A list of objects that can be interacted with in each scene"), SerializeField]
    private GameObject[] interactables = new GameObject[4];

    [Tooltip("The computer GameObject's screen child"), SerializeField]
    private GameObject screen;

    private static SceneManager _sceneManager;
    private int _currentScene = 1; // 0 - left, 1 - front, 2 - right, 3 - pictures
    private int _currentMiniScene; // 0 - none, 1 - pictures, 2 - computer
    private List<GameObject> _currentMiniScenes = new List<GameObject>();
    private bool _miniScene; // is mini scene currently running
    private const float FullAlpha = 1f;
    private const int LeftScene = 0;
    private const int RightScene = 2;
    private const int PicturesScene = 1;
    private const String Pictures = "Pictures";
    private const int ComputerScene = 2;
    private const String Computer = "Computer";
    private const int PictureInteractables = 3;
    private const int LeftArrow = 0;
    private const int RightArrow = 1;

    #endregion

    #region Monobehaviour 

    private void Awake()
    {
        _sceneManager = this;
    }

    #endregion

    #region API

    public static SceneManager Use() => _sceneManager;

    /// <summary>
    /// Change the scene
    /// </summary>
    /// <param name="changeDirection"></param>
    public void ChangeScene(int changeDirection)
    {
        if (_miniScene)
        {
            foreach (var scene in _currentMiniScenes) scene.SetActive(false);
            arrows[RightArrow].SetActive(true);
            scenes[_currentScene].SetActive(true);
            if (_currentMiniScene == ComputerScene)
            {
                screen.GetComponent<Collider2D>().enabled = true;
                var spriteRenderer = screen.GetComponent<SpriteRenderer>();
                Color tint = spriteRenderer.color;
                tint.a = FullAlpha;
                spriteRenderer.color = tint;
                SoundManager.Use().FadeOutOfSuspension();
            }
            _miniScene = false;
        }
        else if (_currentScene + changeDirection >= LeftScene &&
                 _currentScene + changeDirection <= RightScene) // navigate between 3 main scenes
        {
            scenes[_currentScene].SetActive(false);
            _currentScene += changeDirection;
            scenes[_currentScene].SetActive(true);
        }
    }

    /// <summary>
    /// Enable or disable interactions within the current scene
    /// </summary>
    /// <param name="enable"></param>
    public void EnableInteractions(bool enable)
    {
        if (!_miniScene)
        {
            foreach (var arrow in arrows) arrow.gameObject.SetActive(enable);
            foreach (Transform interactable in interactables[_currentScene].transform) interactable.gameObject.SetActive(enable);
        }
        else if (_currentMiniScene == PicturesScene)
        {
            foreach (Transform interactable in interactables[PictureInteractables].transform) interactable.gameObject.SetActive(enable);
            arrows[LeftArrow].gameObject.SetActive(enable); 
        }
    }

    /// <summary>
    /// Open a mini scene
    /// </summary>
    /// <param name="scene"></param>
    public void OpenMiniScene(GameObject scene)
    {
        scenes[_currentScene].SetActive(false);
        arrows[RightArrow].SetActive(false);
        _currentMiniScenes.Add(scene);
        scene.SetActive(true);
        if (scene.name.Equals(Pictures)) _currentMiniScene = PicturesScene;
        if (scene.name.Equals(Computer)) _currentMiniScene = ComputerScene;
        _miniScene = true;
    }

    /// <summary>
    /// Get the number of the current scene
    /// </summary>
    /// <returns></returns>
    public int GetSceneNumber() => _currentScene;
    
    #endregion
}
