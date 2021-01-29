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
        if (GetSlot() != null)
        {
        GameObject spawnedNPC = Instantiate(throwableObjectPrefab,GetSlot());
        int randomNpcIndex = UnityEngine.Random.Range(0, npcs.Length);
        spawnedNPC.GetComponent<ThrowableObjectsMasterClass>().setThrowableScriptableObject(npcs[randomNpcIndex]);
        return spawnedNPC;
        }
        return null;
    }

    

    Transform GetSlot()
    {
       
        return spawns.Where(p => p.slotVacancy == true).Select(p => p.parent).FirstOrDefault();
        
    }

    [Serializable]
    public class SpawnData
    {
        public Transform parent;
        public bool slotVacancy;
    }
}
