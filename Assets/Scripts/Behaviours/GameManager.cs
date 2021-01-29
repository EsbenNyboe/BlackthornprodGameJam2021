﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool debugMode;
    public static bool debugModeStatic;
    [Header("Boat")]
    public float boatSpeedStart;
    public float maxBoatSpeed = 50;
    public float fatalBoatSpeed = 1f;
    public bool usePostWaveBoatSpeed;
    public float postWaveBoatSpeed;
    private Boat boat;
    [Header("Shark")]
    public float sharkBoostForce = 3;
    [Header("Wave")]
    //how much speed dereases by each second on wave
    //public float waveDragFactor = 1;
    public static float currentWaveDrag;
    public float waveDragFactorSmall;
    public float waveDragFactorMedium;
    public float waveDragFactorBig;

    public float maxWaveDragDynamics;
    public float minWaveDragDynamics;
    [Header("Npcs")]
    public int maxNpcCount;
    public PrefabFactory prefabFactory;
    public List<GameObject> spawnedNpcs;
    [Header("UI")]
    public GameObject LoseScreen;
    public GameObject WinScreen;

    public static bool gameLost;
    public static bool gameWon;

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
        debugModeStatic = debugMode;
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
        boat.speed = boatSpeedStart;
        gameLost = gameWon = false;
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.musicGame);
        spawnedNpcs = new List<GameObject>();
        UpdateNpcs(30);
    }

    // Update is called once per frame
    void Update()
    {
        //check for boat speed to low
        if (!gameLost && boat.speed <= fatalBoatSpeed)
        {
            gameLost = true;
            LoseGame();
        }
    }

    public void LoseGame()
    {
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.musicLose);
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
        gameWon = true;
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.musicWin);
        WinScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        //find this scene and re load it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateNpcs(int points)
    {
        int desiredSpawnedNpcs = points;
        desiredSpawnedNpcs -= spawnedNpcs.Count;

        if(desiredSpawnedNpcs > maxNpcCount)
        {
            desiredSpawnedNpcs -= (points - maxNpcCount);
        }

        for (int i = 0; i < desiredSpawnedNpcs; i++)
        {
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-4, -2), 0.2f, 0);
            spawnedNpcs.Add(prefabFactory.InstantiateProduct(PrefabFactory.FactoryProduct.ThrowableObjectDog, boat.transform.position + offset));
        }
    }
}
