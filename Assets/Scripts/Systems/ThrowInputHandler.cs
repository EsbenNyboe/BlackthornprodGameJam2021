using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    [Header("Input Control")]
    [SerializeField] float maxPullDistance;//Max distance in world units that the player can actually pull and have any effect
    [SerializeField] float maxPullForce;//Max force that the object can be thrown
    [SerializeField] float minPullForce;//Min force that the object can be thrown
    [SerializeField] float boatImpulseForce;//"Force"(technically speed) applied to the boat when you throw someone
    [SerializeField] float boatImpulseTimer;//Time that it takes for the "Force"(technically speed) to be taken of the boat after throwing someone


    [Header("Raycast2D")]
    [SerializeField] LayerMask throwableMask; //Mask of the throwable object for the RayCast2D
    float forcemultiplier;
    GameObject targetObject;
    ThrowableObjectsMasterClass throwableObjectsBehavior;
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
                targetObject = hitInfo.transform.gameObject;
                throwableObjectsBehavior = hitInfo.transform.GetComponent<ThrowableObjectsMasterClass>();
                startPulling = true;

            }
        }
        //this boolean is to make sure the this function only cares about the "onButtonUnclicked" once the "onButtonClicked" is triggered
        if (startPulling)
        {

            mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(mouseOnWorldPosition, targetObject.transform.position);
            if (distance > maxPullDistance) forcemultiplier = 1;
            else
            {
                forcemultiplier = distance / maxPullDistance;
            }
            float finalForce = forcemultiplier * maxPullForce;
            if(finalForce < minPullForce)
            {
                finalForce = minPullForce;
            }
            launchDirection = (targetObject.transform.position - mouseOnWorldPosition).normalized;
            Debug.DrawRay(targetObject.transform.position, launchDirection * finalForce);
            throwableObjectsBehavior.ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Held);
            //enter the if statement if the player let go of the left mouse button
            if (Input.GetMouseButtonUp(0))
            {
                startPulling = false;
                /*  mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                  float distance = Vector2.Distance(mouseOnWorldPosition, forceTarget.transform.position);
                  if (distance > maxPullDistance) forcemultiplier = 1;
                  else
                  {
                      forcemultiplier = distance / maxPullDistance;
                  }
                  launchDirection = (forceTarget.transform.position - mouseOnWorldPosition).normalized;
                */
                DoAction();
            }

        }


    }

    /// <summary>
    /// Handles the main action of the if statement. In this case: Throws the object
    /// </summary>
    void DoAction()
    {
        if (targetObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None)
        {
            targetObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            targetObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        throwObjectSystem.ThrowObject(targetObject, launchDirection, forcemultiplier * maxPullForce);
        targetObject.GetComponent<WindBehaviour>().ActivateWindEffect();

        //This line takes care of the throwing AND "in the air" animation
        throwableObjectsBehavior.ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Thrown);


        Boat._instance.TemporaryBoost(boatImpulseForce, boatImpulseTimer);
        targetObject.layer = 0;
        targetObject = null;
    }



    /* private void OnDrawGizmos()
     {
         Gizmos.DrawWireSphere(mouseOnWorldPosition, RAYCAST_RADIUS);

     }*/
}
