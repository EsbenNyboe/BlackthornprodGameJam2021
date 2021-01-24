using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************************
 * Throwable Object Scriptable Object Definition
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//Definition for the scriptable objects of all Throwable objects
[CreateAssetMenu(fileName = "ThrowableObjectScriptableObjectDefinition", menuName = "Throwable Object")]
public class ThrowableObjectScriptableObjectDefinition : ScriptableObject
{
    public ThrowableObjectsType objectType;
    public int speedBonusGiven;
    public int pointsRemoved;
    public Sprite[] idleSprites;
    public Sprite[] pickUpSprites;
    public Sprite[] heldSprites;
    public Sprite[] thrownSprites;
    public Sprite[] landSprites;
    public Sprite[] drownSprites;
 }
