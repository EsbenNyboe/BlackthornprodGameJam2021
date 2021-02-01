using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChecker : MonoBehaviour
{
    public GameObject[] waves;
    [Space]
    public GameObject boat;
    public float maxDistanceToCheck;
    public float delayDistanceToCheck;

    // read only
    public int nextWave;
    public int waveCount;
    public float distanceToWave;
    public static float distanceToWaveRead;
    public bool lvlWonKinda;

    private void Start()
    {
        lvlWonKinda = false;

        AutoWaveArray();

        PlayWaveSoundLoop();
    }

    private void AutoWaveArray()
    {
        foreach (Transform wave in transform)
        {
            waveCount++;
        }
        waves = new GameObject[waveCount];
        waveCount = 0;
        foreach (Transform wave in transform)
        {
            waves[waveCount] = wave.gameObject;
            waveCount++;
        }
        FindTheNextWave();
        waveCount = 0;
    }

    private void FindTheNextWave()
    {
        float closestDistance = 100;
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].transform.position.x > 0)
                if (waves[i].transform.position.x - boat.transform.position.x < closestDistance)
                {
                    closestDistance = waves[i].transform.position.x - boat.transform.position.x;
                    nextWave = i;
                }
        }
        waveCount++;
        GameManager.currentWaveDrag = waves[nextWave].GetComponent<Wave>().waveDragFactor;
    }

    private void PlayWaveSoundLoop()
    {
        switch (waves[nextWave].GetComponent<Wave>().waveType)
        {
            case Wave.WaveType.BigWave:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveLoopBig);
                break;
            case Wave.WaveType.MediumWave:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveLoopMedium);
                break;
            case Wave.WaveType.SmallWave:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveLoopSmall);
                break;
        }
    }

    void Update()
    {
        distanceToWaveRead = distanceToWave;
        if (!lvlWonKinda)
        {
            distanceToWave = waves[nextWave].transform.position.x - boat.transform.position.x;

            if (distanceToWave < 0 && -distanceToWave < maxDistanceToCheck + delayDistanceToCheck)
                AkSoundEngine.SetRTPCValue("DistanceToWave", distanceToWave + delayDistanceToCheck);
            else if (distanceToWave > 0 && distanceToWave < maxDistanceToCheck)
                AkSoundEngine.SetRTPCValue("DistanceToWave", distanceToWave + delayDistanceToCheck);
            if (distanceToWave < 0)
                GameManager.currentWaveDrag = 1;
            //if (Mathf.Abs(distanceToWave) < maxDistanceToCheck + delayDistanceToCheck) // hmmm
            //    AkSoundEngine.SetRTPCValue("DistanceToWave", distanceToWave + delayDistanceToCheck);

            if (distanceToWave + delayDistanceToCheck + maxDistanceToCheck < 0)
            {
                //nextWave++;
                FindTheNextWave();
                if (waveCount > waves.Length - 1)
                    //if (nextWave > waves.Length - 1)
                    lvlWonKinda = true;
                else
                    PlayWaveSoundLoop();
            }
        }
    }

    private void OnDestroy()
    {
        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.waveLoopStop);
    }
}