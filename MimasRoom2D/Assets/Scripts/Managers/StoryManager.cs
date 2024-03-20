using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class which manages the story flow of the game
/// </summary>
public class StoryManager : MonoBehaviour
{
    #region Class Variables
    
    [Tooltip("List of objects to interact with on step 0"), SerializeField]
    private List<GameObject> interactionsStep0 = new List<GameObject>(); // fishtank, clock, TV, bowl, computer, internet, picture
    
    [Tooltip("List of objects to interact with on step 1"), SerializeField]
    private List<GameObject> interactionsStep1 = new List<GameObject>(); // poster, calendar, set pictures, concert pictures, set pictures, script
    
    [Tooltip("List of objects to interact with on step 2"), SerializeField]
    private List<GameObject> interactionsStep2 = new List<GameObject>(); // letters
    
    [Tooltip("List of objects to interact with on step 3"), SerializeField]
    private List<GameObject> interactionsStep3 = new List<GameObject>(); // Diary entries
    
    [Tooltip("List of objects to interact with on step 4"), SerializeField]
    private List<GameObject> interactionsStep4 = new List<GameObject>(); // New entry
    
    [Tooltip("List of objects to interact with on step 5"), SerializeField]
    private List<GameObject> interactionsStep5 = new List<GameObject>(); // Video play button

    private static StoryManager _storyManager;
    private List<GameObject>[] _interactions; // all interactions
    private int _step = -1;
    private readonly string[] _time = { "22:00", "23:00", "00:00", "01:00", "02:00", "03:00" }; // time to show on clock
    private const int Step3 = 3;
    private const int Step4 = 4;
    private const int Step6 = 6;
    private const int Zero = 0;
    private const int NewEntryInteractable = 0;
    #endregion

    #region Monobehaviour

    private IEnumerator Start()
    {
        _storyManager = this;
        _interactions = new[]
            { interactionsStep0, interactionsStep1, interactionsStep2, interactionsStep3, interactionsStep4, 
                interactionsStep5};
        UpdateStep(); // move to step 0
        yield return new WaitForFixedUpdate();
        UpdateStep(); // move to step 1
    }

    #endregion

    #region API

    public static StoryManager Use() => _storyManager;

    /// <summary>
    /// Check if all interactions were complete to move to next step
    /// </summary>
    /// <param name="interaction"></param>
    public void CheckInteraction(GameObject interaction)
    {
        if (_step < Step6)
        {
            if (_interactions[_step].Contains(interaction))
            {
                _interactions[_step].Remove(interaction);
            }
            if (_interactions[_step].Count == Zero)
            {
                UpdateStep();
            }
        }
    }

    /// <summary>
    /// Get the current time in game according to step
    /// </summary>
    /// <returns></returns>
    public string GetTime() => _time[_step];

    #endregion

    /// <summary>
    /// Move to next step
    /// </summary>
    private void UpdateStep()
    {
        _step++;
        if (_step < Step3)
        {
            foreach (var interaction in _interactions[_step])
            {
                interaction.GetComponent<Interactions>().Enable(true);
                interaction.GetComponent<Collider2D>().enabled = true;
            }
        }
        else if (_step == Step4)
        {
            interactionsStep4[NewEntryInteractable].SetActive(true);
        }
    }
}
