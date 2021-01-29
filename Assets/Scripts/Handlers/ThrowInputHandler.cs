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
/*
//Handler of the player input for the main mechanic. 
public class ThrowInputHandler : MonoBehaviour
{
    [Header("Input Control")]
    [SerializeField] float maxPullDistance;//Max distance in world units that the player can actually pull and have any effect
    [SerializeField] float maxPullForce;//Max force that the object can be thrown
    [SerializeField] float minPullForce;//Min force that the object can be thrown
    [SerializeField] float boatImpulseForce;//"Force"(technically speed) applied to the boat when you throw someone
    [SerializeField] float boatImpulseTimer;//Time that it takes for the "Force"(technically speed) to be taken of the boat after throwing someone
    [SerializeField] float raycastRadius;//Raycast (circle) size. We use this raycast to locate the nearest NPC to throw

    [Header("Tweening")]
    [SerializeField] float throwBodyShakeDuration;
    [SerializeField] float throwBodyShakeForce;
    [SerializeField] Transform npcHeldPosition;
    [SerializeField] float timeToHoldNPC;


    [Header("Raycast2D")]
    [SerializeField] LayerMask throwableMask; //Mask of the throwable object for the RayCast2D
    float forcemultiplier;
    GameObject targetObject;
    ThrowableObjectsMasterClass throwableObjectsBehavior;
    ThrowObjectSystem throwObjectSystem;
    bool startPulling;
    Vector3 mouseOnWorldPosition;
    Vector3 launchDirection;

    bool isHoldingSomething;

    //quick-temporary fix
    bool pulled;
    //



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

    void Update()
    {
        if (!isHoldingSomething)
        {
            //Picks the NPC up
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {

                RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, raycastRadius, Vector2.zero, 0, throwableMask);
                if (hitInfo && hitInfo.transform.gameObject != null)
                {
                    isHoldingSomething = true;
                    targetObject = hitInfo.transform.gameObject;
                    throwableObjectsBehavior = hitInfo.transform.GetComponent<ThrowableObjectsMasterClass>();
                    throwableObjectsBehavior.ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Held);
                    targetObject.transform.DOMove(npcHeldPosition.position, timeToHoldNPC);

                }
            }
        }
        else
        {
            targetObject.transform.position = npcHeldPosition.position;
            
            //Start aiming
            if (Input.GetMouseButtonDown(0))
            {
                startPulling = true;
            }

            //this boolean is to make sure the this function only cares about the "onButtonUnclicked" once the "onButtonClicked" is triggered
            if (startPulling)
            {
                

                mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseOnWorldPosition.z = 0;
                float distance = Vector2.Distance(mouseOnWorldPosition, targetObject.transform.position);
                if (distance > maxPullDistance) forcemultiplier = 1;
                else
                {
                    forcemultiplier = distance / maxPullDistance;
                }
                float finalForce = forcemultiplier * maxPullForce;
                if (finalForce < minPullForce)
                {
                    finalForce = minPullForce;
                }
                launchDirection = (targetObject.transform.position - mouseOnWorldPosition).normalized;
                
                if (!pulled)
                {
                    throwableObjectsBehavior.ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Held);
                    PlayerBehaviour.instance.ChangeAnimationState(PlayerBehaviour.PlayerAnimationType.holding);
                    pulled = true;
                }
                //enter the if statement if the player let go of the left mouse button
                Debug.DrawRay(targetObject.transform.position, launchDirection * finalForce);
                if (Input.GetMouseButtonUp(0))
                {
                    startPulling = false;
                   
                    
                    DoAction(finalForce);
                    pulled = false;
                    isHoldingSomething = false;
                }

            }
        }

    }

    /// <summary>
    /// Handles the main action of the if statement. In this case: Throws the object
    /// </summary>
    void DoAction(float finalforce)
    {
       
         Physics2D.IgnoreCollision(targetObject.GetComponent<Collider2D>(), Boat._instance.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(targetObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        if (targetObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None)
        {
            targetObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            targetObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        PlayerBehaviour.instance.ChangeAnimationState(PlayerBehaviour.PlayerAnimationType.throwNPC);
        throwObjectSystem.ThrowObject(targetObject, launchDirection, finalforce);

        targetObject.transform.DOShakeScale(throwBodyShakeDuration, launchDirection * throwBodyShakeForce);
        targetObject.GetComponent<WindBehaviour>().ActivateWindEffect();
 
        //This line takes care of the throwing AND "in the air" animation
        throwableObjectsBehavior.ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Thrown);


        Boat._instance.TemporaryBoost(boatImpulseForce, boatImpulseTimer);

        targetObject.layer = 0;
        targetObject = null;

    }



    
} 
                    */
