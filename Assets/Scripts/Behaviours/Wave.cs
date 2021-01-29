using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave : MonoBehaviour
{
    private Boat boat;
    public bool boatRidingNow;
    public float waveDragFactor = 1;
    private float timeOnTheWave;
    private GameManager gm;

    public enum WaveType
    {
        BigWave,
        MediumWave,
        SmallWave
    }
    public WaveType waveType;

    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //set global wave drag factor
        switch (waveType)
        {
            case WaveType.BigWave:
                waveDragFactor = gm.waveDragFactorBig;
                break;
            case WaveType.MediumWave:
                waveDragFactor = gm.waveDragFactorMedium;
                break;
            case WaveType.SmallWave:
                waveDragFactor = gm.waveDragFactorSmall;
                break;
        }
        //waveDragFactor = gm.waveDragFactor;

        GameManager.onGameLost += GameManager_onGameLost;
    }

    private void GameManager_onGameLost(object sender, System.EventArgs e)
    {
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Time.deltaTime * boat.speed * -1, 0, 0);

        if (boatRidingNow)
        {
            AkSoundEngine.SetRTPCValue("BoatSpeed", boat.speed);

            float speedDiscriminator = gm.maxWaveDragDynamics * boat.speed / ThrowInputHandlerSAFEMODE.impulseForceRead;
            if (speedDiscriminator < gm.minWaveDragDynamics)
                speedDiscriminator = gm.minWaveDragDynamics;
            else if (speedDiscriminator > gm.maxWaveDragDynamics)
                speedDiscriminator = gm.maxWaveDragDynamics;
            print("discr:" + speedDiscriminator);
            //speedDiscriminator = 1;
            if (boat.speed > 0)
            {
                boat.speed -= Time.deltaTime * waveDragFactor * speedDiscriminator;
            }
            if (boat.speed < 0)
            {
                boat.speed = 0;
            }
        }
        else
        {
            //if (boat.speed < gm.postWaveBoatSpeed)
            //    boat.speed += Time.deltaTime;

            timeOnTheWave = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            if (!boatRidingNow)
            {
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveEnter);
            }
            boatRidingNow = true;

            GameObject.Find("waterWaveParticle").GetComponent<ParticleSystem>().emissionRate = 100;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            if (boatRidingNow)
            {
                if (gm.usePostWaveBoatSpeed)
                    boat.speed = gm.postWaveBoatSpeed;
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveCleared);
            }
            boatRidingNow = false;

            GameObject.Find("waterWaveParticle").GetComponent<ParticleSystem>().emissionRate = 0;
        }
    }

    void OnDestroy()
    {
        GameManager.onGameLost -= GameManager_onGameLost;

    }
}
