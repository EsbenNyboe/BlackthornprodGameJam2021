using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************************
 * Wwise Sound Scriptable Object Definition
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//Definition for the scriptable objects of Wwise references
[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "WwiseSoundReferences")]
public class WwiseSoundTriggersScriptableObjectDefinition : ScriptableObject
{
    public string landSoundEvent;
    public string thrownSoundEvent;
    public string drownSoundEvent;
}