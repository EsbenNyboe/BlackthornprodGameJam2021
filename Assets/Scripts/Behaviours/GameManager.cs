using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Boat")]
    public float boatSpeedStart;
    public float maxBoatSpeed = 50;
    public float fatalBoatSpeed = 1f;
    public float postWaveBoatSpeed;
    private Boat boat;
    [Header("Shark")]
    public float sharkBoostForce = 3;
    [Header("Wave")]
    //how much speed dereases by each second on wave
    public float waveDragFactor = 1;

    public float nudgeAccerelationTime;
    public float nudgeDeaccelerationTime;
    [Header("UI")]
    public GameObject LoseScreen;
    public GameObject WinScreen;

    #region singleton
    static public GameManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    #region TriggerEvents
    static public event EventHandler onGameLost;
    #endregion

    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
        boat.speed = boatSpeedStart;
        boat.boatInertiaTime = nudgeAccerelationTime;
        boat.boatInertiaTimeDeacceleration = nudgeDeaccelerationTime;
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.startAmbience);
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.startMusic);
    }

    // Update is called once per frame
    void Update()
    {
        //check for boat speed to low
        if (boat.speed <= fatalBoatSpeed)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        onGameLost?.Invoke(this, EventArgs.Empty);
        print("Game Lost");
        //Make Boat sink
        boat.GetComponent<Rigidbody2D>().mass = 50;
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveLost);
        //wait 1 second to show lose UI, so you can see the boat sink
        StartCoroutine(showLoseScreenDelayed());
    }
    //waits 1 second to show lose UI, so you can see the boat sink
    IEnumerator showLoseScreenDelayed()
    {
        yield return new WaitForSeconds(2);
        LoseScreen.SetActive(true);
    }
    public void WinGame()
    {
        WinScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        //find this scene and re load it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
