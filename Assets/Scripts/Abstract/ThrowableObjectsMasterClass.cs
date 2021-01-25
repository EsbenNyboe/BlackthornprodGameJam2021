using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*********************************************************************************************
 * Throwable Object Master Class
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//This class is just the Master Class for all Throwable Objects
public abstract class ThrowableObjectsMasterClass : MonoBehaviour
{
    //A scriptable object has to be set in the script component
    [SerializeField]protected ThrowableObjectScriptableObjectDefinition throwableScriptableObject;
     
}
public enum ThrowableObjectsType
{
    GrandPa,
    GrandMa,
    Husband,
    Wife,
    Kid,
    Dog,

}