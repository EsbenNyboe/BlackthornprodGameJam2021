using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    PointsSystem pointsSystem;
    static public PlayerBehaviour instance { get; private set; }
    SpriteRendererAnimator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] idleSprites;
    [SerializeField]Sprite[] holdSprites;
    [SerializeField]Sprite[] throwSprites;
    void Awake()
    {
        if(instance == null) instance = this;
         pointsSystem = new PointsSystem();
        spriteRenderer = GetComponent<SpriteRenderer>();
         animator = GetComponent<SpriteRendererAnimator>();
    }
    private void Start()
    {
        ChangeAnimationState(PlayerAnimationType.idle);
    }

    public void ChangeAnimationState(PlayerAnimationType animationtype)
    {
       
        switch (animationtype)
        {
            case PlayerAnimationType.idle: animator.ChangeSpriteArray(idleSprites); ; break;
            case PlayerAnimationType.holding: animator.ChangeSpriteArray(holdSprites); break;
            case PlayerAnimationType.throwNPC: animator.ChangeSpriteArray(throwSprites,false,()=>ChangeAnimationState(PlayerAnimationType.idle)); break;
        }
       
    }
    
    public PointsSystem GetPlayersPointSystem()
    {
        return pointsSystem;
    }

    public enum PlayerAnimationType
    {
        idle,
        holding,
        throwNPC

    }
}
