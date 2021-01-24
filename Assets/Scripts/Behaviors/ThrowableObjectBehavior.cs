using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********************************************************************************************
 * Throwable Object Behaviour
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//Behavior of the throwable object, child of ThrowableObjectsMasterClass
public class ThrowableObjectBehavior : ThrowableObjectsMasterClass
{
    //A scriptable object has to be set in the script component
    [SerializeField] ThrowableObjectScriptableObjectDefinition throwableScriptableObject;

    void Awake()
    {
        objectType = throwableScriptableObject.objectType;
        speedBonusGiven = throwableScriptableObject.speedBonusGiven;
        pointsRemoved = throwableScriptableObject.pointsRemoved;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
