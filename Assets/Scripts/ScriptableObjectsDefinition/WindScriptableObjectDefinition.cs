using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************************
 * Wind Scriptable Object Definition
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//Definition for the scriptable objects of wind references
[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Wind")]
public class WindScriptableObjectDefinition : ScriptableObject
{
     public float windForce;
    public float windAngle;

}
