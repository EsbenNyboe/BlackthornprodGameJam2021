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
    [SerializeField] float deepDistance;//Time until the npc starts to go down
    [SerializeField] float drownSpeed;//How fast the npc should move down

    bool isNpcThrown;
    public NPCFactory.SpawnData mySpawnData;

    #region events
    public event EventHandler onNPCThrown;
    public event EventHandler onNPCDrown;
    #endregion

    [HideInInspector]
    public bool interactable;

    bool isThrown = false;
    bool drowned = false;
    float timer = 0;
    public static int npcsInTheAir;

    private void Start()
    {
        spriteRendererAnimator = GetComponent<SpriteRendererAnimator>();
        ChangeAnimationState(AnimationType.Idle);
        interactable = true;
        npcsInTheAir = 0;
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }
    private void Update()
    {
        if (GameManager.useInstantiation)
            if (!isNpcThrown) transform.position = mySpawnData.parent.position;
        if (isThrown)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                npcsInTheAir--;
                isThrown = false;
            }
        }
        //print(npcsInTheAir);
    }

    public void RemovePlayerPoints()
    {

    }
    public void setThrowableScriptableObject(ThrowableObjectScriptableObjectDefinition throwableScriptableObject)
    {
        this.throwableScriptableObject = throwableScriptableObject;
    }
    public void ChangeAnimationState(AnimationType animationtype)
    {
        switch (animationtype)
        {
            case AnimationType.Idle:
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.idleSprites); break;
            case AnimationType.Held:
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcPickedUp);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.heldSprites,false); break;
            case AnimationType.Thrown:
                npcsInTheAir++;
                isThrown = true;
                interactable = false;
                SoundSystem.instance.PlaySound(throwableScriptableObject.screamingSound);
                SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcThrown);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.thrownSprites,false, () => ChangeAnimationState(AnimationType.Air)); break;
            case AnimationType.Land:
                if (isThrown)
                    npcsInTheAir--;
                isThrown = false;
                interactable = true;
                SoundSystem.instance.PlaySound(throwableScriptableObject.impactSound);
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.landSprites,false,2, ()=>SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollFloor),() => ChangeAnimationState(AnimationType.Idle));break;
            case AnimationType.Drown:
                if (isThrown)
                    npcsInTheAir--;
                isThrown = false;
                interactable = false;

                if (!drowned)
                {
                    SoundSystem.instance.PlaySound(throwableScriptableObject.impactSound);
                    if (npcsInTheAir > 1)
                        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollWaterMultipleNpcs);
                    else
                        SoundSystem.instance.PlaySound(SoundSystem.SoundEnum.npcCollWater);
                }
                drowned = true;

                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.drownSprites); break;
            case AnimationType.Air:
                spriteRendererAnimator.ChangeSpriteArray(throwableScriptableObject.AirSprites); break;
        }
        if (GameManager.debugModeStatic)
            interactable = true;
    }
    public void SetSpawnData(NPCFactory.SpawnData myspawndata)
    {
        mySpawnData = myspawndata;
    }

    public void ThrownData()
    {
        mySpawnData.slotVacancy = false;
        isNpcThrown = true;
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
          //  StartCoroutine(DrownTweeningCourotine());

        }
    }

    IEnumerator DrownTweeningCourotine()
    {
        yield return new WaitForSeconds(drownTimer);

        GetComponent<Rigidbody2D>().DOMoveY(-deepDistance, 1 / drownSpeed);
     }
}
