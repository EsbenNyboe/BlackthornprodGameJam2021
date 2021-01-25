using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*********************************************************************************************
 * Throw Input Handler
 * Author: Muniz
 * Youtube: https://www.youtube.com/channel/UCAOamcXgoT0gVjV1AG5b1Fg
 * Twitter: @MrFBMuniz
 * Created: 24/01/2021 : dd/mm/yyyy
 * 
 *  Event: BlackthornProd GameJam #3
 * *******************************************************************************************/

//Handler of the player input for the main mechanic. 
public class ThrowInputHandler : MonoBehaviour
{

    [SerializeField] float maxPullDistance;//Max distance in world units that the player can actually pull and have any effect
    [SerializeField] float maxPullForce;//Max force that the object can be thrown
    [SerializeField] LayerMask throwableMask; //Mask of the throwable object for the RayCast2D
    float forcemultiplier;
    GameObject forceTarget;
    ThrowObjectSystem throwObjectSystem;
    bool startPulling;
    Vector3 mouseOnWorldPosition;
    const float RAYCAST_RADIUS = 3;
    Vector3 launchDirection;
 


    /// <summary>
    /// Configure the main properties of the input handler. 
    /// </summary>
    /// <remarks>You can change the maxPullForce and maxPullDistance through this function or through the editor.</remarks>
    /// <param name="maxpullforce">Max force that the object can be thrown</param>
    /// <param name="maxpulldistance">Max distance in world units that the player can actually pull and have any effect</param>
    public void ConfigInputHandler(float maxpullforce, float maxpulldistance)
    {
        maxPullForce = maxpullforce;
        maxPullDistance = maxpulldistance;

    }
    private void Awake()
    {
        throwObjectSystem = new ThrowObjectSystem();


    }

   // As default, left mouse button is the main controller.You can change it in the Update function
    void Update()
    {
        //enter the if statement if the player clicks on the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.CircleCast(mouseOnWorldPosition, RAYCAST_RADIUS, Vector2.zero, 0, throwableMask);
            if (hitInfo && hitInfo.transform.gameObject != null)
            {
                forceTarget = hitInfo.transform.gameObject;
                startPulling = true;

            }
        }
        //this boolean is to make sure the this function only cares about the "onButtonUnclicked" once the "onButtonClicked" is triggered
        if (startPulling)
        {
            //enter the if statement if the player let go of the left mouse button
            if (Input.GetMouseButtonUp(0))
            {
                startPulling = false;
                mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distance = Vector2.Distance(mouseOnWorldPosition, forceTarget.transform.position);
                if (distance > maxPullDistance) forcemultiplier = 1;
                else
                {
                    forcemultiplier = distance / maxPullDistance;
                }
                launchDirection = (forceTarget.transform.position - mouseOnWorldPosition).normalized;
                // Debug.DrawRay(forceTarget.transform.position, launchDirection * forcemultiplier * maxPullForce);
                DoAction();
            }

        }


    }

    /// <summary>
    /// Handles the main action of the if statement. In this case: Throws the object
    /// </summary>
    void DoAction()
    {
        if (forceTarget.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) forceTarget.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        throwObjectSystem.ThrowObject(forceTarget, launchDirection, forcemultiplier * maxPullForce);
        forceTarget.GetComponent<WindBehavior>().ActivateWindEffect();
         forceTarget = null;
    }

    /* private void OnDrawGizmos()
     {
         Gizmos.DrawWireSphere(mouseOnWorldPosition, RAYCAST_RADIUS);

     }*/
}
