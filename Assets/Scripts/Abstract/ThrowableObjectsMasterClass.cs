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
    [SerializeField] protected ThrowableObjectScriptableObjectDefinition throwableScriptableObject;
    SpriteRendererAnimator spriteRendererAnimator;

    private void Start()
    {
        spriteRendererAnimator = GetComponent<SpriteRendererAnimator>();
        ChangeAnimationState(AnimationType.Idle);
    }
    public void ChangeAnimationState(AnimationType animationtype)
    {

        switch (animationtype)
        {
            case AnimationType.Idle:
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.idleSprites); break;
            case AnimationType.Held:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcPickedUp);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.heldSprites); break;
            case AnimationType.Thrown:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcThrown);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.thrownSprites, false, () => ChangeAnimationState(AnimationType.Air)); break;
            case AnimationType.Land:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollFloor);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.landSprites); break;
            case AnimationType.Drown:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollWater);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.drownSprites); break;
            case AnimationType.Air:
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.AirSprites); break;
        }

    }


    public enum AnimationType
    {
        Idle,
        Held,
        Thrown,
        Land,
        Drown,
        Air
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            ChangeAnimationState(AnimationType.Drown);
        }
    }
}
