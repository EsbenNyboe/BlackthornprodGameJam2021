using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChecker : MonoBehaviour
{
    public GameObject[] waves;
    [Space]
    public GameObject boat;
    public float maxDistanceToCheck;

    // read only
    public int nextWave;
    public float distanceToWave;
    public bool lvlWonKinda;

    //public enum WaveType
    //{
    //    BigWave,
    //    MediumWave,
    //    SmallWave
    //}
    //public WaveType waveType;

    private void Start()
    {
        nextWave = 0;
        lvlWonKinda = false;

        PlayWaveSoundLoop();
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
        if (!lvlWonKinda)
        {
            distanceToWave = waves[nextWave].transform.position.x - boat.transform.position.x;
            if (Mathf.Abs(distanceToWave) < maxDistanceToCheck)
                AkSoundEngine.SetRTPCValue("DistanceToWave", distanceToWave);

            if (distanceToWave + maxDistanceToCheck < 0)
            {
                nextWave++;
                if (nextWave > waves.Length - 1)
                    lvlWonKinda = true;
                else
                    PlayWaveSoundLoop();
            }
        }
    }
}