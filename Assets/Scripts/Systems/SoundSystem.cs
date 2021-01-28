﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SoundSystem : MonoBehaviour
{
    [SerializeField] SoundData[] soundDataArray;

    static public SoundSystem instance;
    

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void PlaySound(SoundEnum soundtype)
    {
        
        AkSoundEngine.PostEvent(GetMatchingSound(soundtype), gameObject);
    }

    string GetMatchingSound(SoundEnum soundtype)
    {
       return soundDataArray.Where((sounddata)=>sounddata.soundType == soundtype).Select((sounddata)=>sounddata.eventName).FirstOrDefault();
    }
    [System.Serializable]
    public class SoundData
    {
        public SoundEnum soundType;
        public string eventName;
    }

    public enum SoundEnum
    {
        npcPickedUp,
        npcPreparedForThrow,
        npcThrown,
        npcCollFloor,
        npcCollBird,
        npcDroppedByBird,
        npcCollShark,
        npcCollWater,
        playerFootsteps,
        waveEnter,
        waveCleared,
        waveLost,
        startAmbience,
        startMusic
    }
}