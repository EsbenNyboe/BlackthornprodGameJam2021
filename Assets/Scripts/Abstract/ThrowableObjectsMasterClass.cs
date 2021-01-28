using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
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
    [Header("Scriptable Object")]
    [SerializeField] protected ThrowableObjectScriptableObjectDefinition throwableScriptableObject;
    SpriteRendererAnimator spriteRendererAnimator;

    [Header("Tweening")]
    [SerializeField] float drownTimer;//Time until the npc starts to go down
    [SerializeField] float drownSpeed;//How fast the npc should move down

    #region events
    public event EventHandler onNPCThrown;
    public event EventHandler onNPCDrown;
    #endregion

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
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.heldSprites); break;
            case AnimationType.Thrown:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcThrown);
                spriteRendererAnimator.ChangeSpriteArrayNoLoop(throwableScriptableObject.thrownSprites, () => ChangeAnimationState(AnimationType.Air)); break;
            case AnimationType.Land:
                
                spriteRendererAnimator.ChangeSpriteArrayNoLoop(throwableScriptableObject.landSprites,2, ()=>SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollFloor),() => ChangeAnimationState(AnimationType.Idle));break;
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
            onNPCDrown?.Invoke(this, EventArgs.Empty);

        }
    }

    IEnumerator DrownTweeningCourotine()
    {
        yield return new WaitForSeconds(drownTimer);

        transform.DOMove(transform.position + Vector3.down * 3, 1 / drownSpeed);
    }
}
