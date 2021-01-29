using System.Collections;
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

    [Header("UI")]
    public GameObject LoseScreen;
    public GameObject WinScreen;

    public static bool gameLost;
    public static bool gameWon;


    [Header("Points System")]
    [SerializeField] int maxAmountOfPoints;
    [SerializeField] int startingPoints;
    //private stuff
    static public PointsSystem npcPointsSystem;
    //
    #region singleton
    static public GameManager instance;
    void Awake()
    {
        if (instance == null) instance = this;
        npcPointsSystem = new PointsSystem(maxAmountOfPoints, startingPoints);
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
       
        ThrowObjectSystem.onObjectThrown += ThrowObjectSystem_onObjectThrown;

        for (int i = 0; i < 8; i++)
        {
            NPCFactory.instance.InstantiateNPC();
        }
       

    }

    private void ThrowObjectSystem_onObjectThrown(object sender, ThrowObjectSystem.CustomEventArgsData e)
    {
        SpawnNPCLogic();
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

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SpawnNPCLogic()
    {
        if (npcPointsSystem.currentPoints <= 0) return;
        npcPointsSystem.RemovePoints(1);
        if (npcPointsSystem.currentPoints < 8)
        {
            NPCFactory.instance.InstantiateNPC();

        }
       
    }


}
