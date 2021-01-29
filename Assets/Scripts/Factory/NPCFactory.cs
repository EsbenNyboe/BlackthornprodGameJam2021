using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


/// <summary>
/// Prefab factory in general. Attach this script to a Gameobject and fill the script with the information needed
/// </summary>
public class NPCFactory : MonoBehaviour
{
    [Header("Resource entrance")]
    [SerializeField] GameObject throwableObjectPrefab;
    [SerializeField] SpawnData[] spawns;

    public ThrowableObjectScriptableObjectDefinition[] npcs;
    public static NPCFactory instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    public GameObject InstantiateNPC()
    {
        SpawnData spawnData = GetData();
        spawnData.slotVacancy = true;
        GameObject spawnedNPC = Instantiate(throwableObjectPrefab, spawnData.parent);
        int randomNpcIndex = UnityEngine.Random.Range(0, npcs.Length);
        spawnedNPC.GetComponent<ThrowableObjectsMasterClass>().setThrowableScriptableObject(npcs[randomNpcIndex]);
        spawnedNPC.GetComponent<ThrowableObjectsMasterClass>().SetSpawnData(spawnData);
        return spawnedNPC;
       
    }



    SpawnData GetData()
    {
        SpawnData spawnData = spawns.Where(p => p.slotVacancy == false).Select(p => p).FirstOrDefault();
        if (spawnData == null) return null;
        
        return spawnData;
        
    }

    [Serializable]
    public class SpawnData
    {
        public Transform parent;
        public bool slotVacancy;
    }
}
