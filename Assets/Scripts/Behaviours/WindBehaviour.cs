using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*********************************************************************************************
 * Wind Behaviour
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//This class is going to be attached as a component in the target game object
public class WindBehaviour : MonoBehaviour
{
    [SerializeField] WindScriptableObjectDefinition windScriptableObject;
     

    bool forceWind;
    Vector2 windDirection;

   
    private void Start()
    {
        float auxAngle;
        //this is so we can use -45(left) or 45(right) instead of 135(left) and 45(right)
        if (windScriptableObject.windAngle >= 0)
        {
            auxAngle = windScriptableObject.windAngle;
        }
        else
        {
            auxAngle = 180 + windScriptableObject.windAngle;
        }
        //this fixes the weird way that Unity handles the angle units initial position
        auxAngle -= 90;
        Quaternion rotation = Quaternion.AngleAxis(auxAngle, Vector3.forward);
        windDirection = rotation * Vector3.up;
    }

    void Update()
    {
        if (forceWind)
        {
            GetComponent<Rigidbody2D>().AddForce(windDirection.normalized * windScriptableObject.windForce, ForceMode2D.Force);
        }

    }

    /// <summary>
    /// Activate the wind effect on this gameobject's rigidbody
    /// </summary>
    public void ActivateWindEffect()
    {
        forceWind = true;
    }
    /// <summary>
    /// Deactivate the wind effect on this gameobject's rigidbody
    /// </summary>
    public void DeactivateWindEffect()
    {
        forceWind = false;
    }
}
