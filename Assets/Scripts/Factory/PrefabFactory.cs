using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*********************************************************************************************
 * Prefab Factory
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

/// <summary>
/// Prefab factory in general. Attach this script to a Gameobject and fill the script with the information needed
/// </summary>
public class PrefabFactory : MonoBehaviour
{
    [Header("Resource entrance")]
    [SerializeField] ProductData[] productDatas;
    public GameObject throwableObjectPrefab;
    public ThrowableObjectScriptableObjectDefinition[] npcs;
    public static PrefabFactory instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    /// <summary>
    /// Enum to match with the prefabs
    /// </summary>
    public enum FactoryProduct
    {
        ThrowableObjectHusband,
        ThrowableObjectWife,
        ThrowableObjectKid,
        ThrowableObjectDog,
        ThrowableObjectGrandPa,
        ThrowableObjectGrandMa,
    }


    /// <summary>
    /// Instantiate a GameObject based on the enum linked to the prefab
    /// </summary>
    /// <param name="factoryProduct">Enum to indicate which prefab should be instantiated</param>
    /// <returns>The GameObject itself</returns>
    public GameObject InstantiateProduct(FactoryProduct factoryProduct)
    {
        return InstantiateProduct(factoryProduct, Vector3.zero);
    }
    /// <summary>
    /// Instantiate a GameObject based on the enum linked to the prefab and a position
    /// </summary>
    /// <param name="factoryProduct">Enum to indicate which prefab should be instantiated</param>
    /// <param name="position">World position where the GameObject is going to be instantiated</param>
    /// <returns>The GameObject itself</returns>
    public GameObject InstantiateProduct(FactoryProduct factoryProduct, Vector3 position)
    {
        GameObject spawnedNPC = Instantiate(throwableObjectPrefab, position, Quaternion.identity);
        int randomNpcIndex = UnityEngine.Random.Range(0, npcs.Length);
        spawnedNPC.GetComponent<ThrowableObjectsMasterClass>().setThrowableScriptableObject(npcs[randomNpcIndex]);
        return spawnedNPC;
    }
    //This is just a helper function to get the right prefab from the Product Data Vector/List based on the Enum chosen
    /*GameObject FindProduct(FactoryProduct factoryProduct)
    {
        return productDatas.Where(p => p.factoryProductType == factoryProduct).Select(p => p.prefab).First();
    }*/


    // This class is a resource that holds the link between Prefab and Enum for the PrefabFactory 
    [System.Serializable]
    public class ProductData
    {
        public GameObject prefab;
        public FactoryProduct factoryProductType;
    }


}
