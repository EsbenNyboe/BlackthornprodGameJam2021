using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SpriteRendererAnimator : MonoBehaviour
{

    public Sprite[] currentSprites;
    public int spritePerFrame = 6; //Custom frame speed
    public bool loopTheAnimation = true; //Loop the animation
    public bool destroyOnAnimationEnded = false; // Destroy gameobject when animation hits the last sprite
    private int index = 0;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private Action actionWhenAnimationEnds;
    private Action actionAtIndex;
    private int indexAction;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!loopTheAnimation && index == currentSprites.Length) return;
        currentFrame++;
        if (currentFrame < spritePerFrame) return;
        spriteRenderer.sprite = currentSprites[index];
        if (indexAction == index)
        {
            if (loopTheAnimation)
            {
                actionAtIndex?.Invoke();
            }
            else
            {
                actionAtIndex?.Invoke();
                actionAtIndex = null;
                indexAction = -1;
            }
        }
        currentFrame = 0;
        index++;
        if (index >= currentSprites.Length)
        {
            if (loopTheAnimation) { index = 0; }
            else
            {
                actionWhenAnimationEnds?.Invoke();
                actionWhenAnimationEnds = null;
            }
            if (destroyOnAnimationEnded) Destroy(gameObject);
        }
    }

    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="looptheanimation">If true, the animation loops</param>
    public void ChangeSpriteArray(Sprite[] sprites, bool looptheanimation = true)
    {
        index = 0;
        loopTheAnimation = looptheanimation;
        this.currentSprites = sprites;

    }
    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="looptheanimation">If true, the animation loops</param>
    /// <param name="spriteFrame">index of the sprite to show when to do the Action</param>
    /// <param name="actionatindex">Action that's going to run when the index sprite shows up</param>
    public void ChangeSpriteArray(Sprite[] sprites, bool looptheanimation, int spriteFrame, Action actionatindex)
    {
        if (spriteFrame > sprites.Length)
        {
            Debug.LogError("the indexAction of the the desire animation is out of range. (SpriteRendererAnimator)");
        }
        else
        {
            ChangeSpriteArray(sprites,looptheanimation);
            indexAction = spriteFrame - 1; //The  "-1" is to correct the difference of "second sprite is in the position 1 of the array"
            actionAtIndex = actionatindex;
        }

    }
    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="looptheanimation">If true, the animation loops</param>
    /// <param name="actionafterlastsprite">Action that's going to run after showing the last sprite of the array  </param>
    public void ChangeSpriteArray(Sprite[] sprites, bool looptheanimation, Action actionafterlastsprite)
    {

        if (!looptheanimation)
        {
            ChangeSpriteArray(sprites, false);
            actionWhenAnimationEnds = actionafterlastsprite;
        }
        else
        {
            ChangeSpriteArray(sprites, true, sprites.Length, actionafterlastsprite);
        }


        ;

    }
    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="spriteFrame">index of the sprite to show when to do the Action</param>
    /// <param name="actionatindex">Action that's going to run when the index sprite shows up</param>
    /// <param name="actionafterlastsprite">Action that's going to run after showing the last sprite of the array  </param>
    public void ChangeSpriteArray(Sprite[] sprites, int spriteFrame, Action actionatindex, Action actionafterlastsprite)
    {
        ChangeSpriteArray(sprites, false, spriteFrame, actionatindex);
        actionWhenAnimationEnds = actionafterlastsprite;


    }
    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="actionafterlastsprite">Action thats going to run after showing the last sprite of the array </param>
    /// <param name="destroyonanimationended">if true, the gameobject is destroyed when the last sprite is shown</param>
    public void ChangeSpriteArray(Sprite[] sprites, Action actionafterlastsprite, bool destroyonanimationended)
    {
        ChangeSpriteArray(sprites, false,actionafterlastsprite);
         
        destroyOnAnimationEnded = destroyonanimationended;
    }



}
