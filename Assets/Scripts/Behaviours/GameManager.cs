using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }
}
