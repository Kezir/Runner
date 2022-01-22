using UnityEngine;
using System.Collections.Generic; // List
using UnityEngine.SceneManagement;
using Lean.Gui;

/// <summary> 
/// Controls the main gameplay 
/// </summary> 
public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public LeanWindow losePanel;
    public RoadSpawner spawnerRoads;

    private void Start()
    {
        spawnerRoads.Init();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void ContinueAdsReward()
    {
        PlayerMovement.Instance.MoveToMiddle();
        PlayerMovement.Instance.MoveBack();
        CloseLosePanel();
        PlayerMovement.Instance.alive = true;
        Time.timeScale = 1;
    }

    public void Continue()
    {
        if(AdverisementsController.Instance != null)
        {
            // do poprawy
            AdverisementsController.Instance.ShowRewardedAd();
        }
        else
        {
            Debug.Log("No videos");
            Restart();
        }
        
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenLosePanel()
    {
        losePanel.TurnOn();
    }

    public void CloseLosePanel()
    {
        losePanel.TurnOff();
    }

    public void OpenPause()
    {

    }
}