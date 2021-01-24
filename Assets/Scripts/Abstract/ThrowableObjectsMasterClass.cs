using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public ThrowableObjectsType objectType;
    public int speedBonusGiven;
    public int pointsRemoved;
    
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