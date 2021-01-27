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
        currentFrame = 0;
        index++;
        if (index >= currentSprites.Length)
        {
            if (loopTheAnimation) { index = 0; }
            else
            {
                actionWhenAnimationEnds?.Invoke();
                loopTheAnimation = true;
                actionWhenAnimationEnds = null;
            }
            if (destroyOnAnimationEnded) Destroy(gameObject);
        }
    }

    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    public void ChangeSpriteArray(Sprite[] sprites)
    {
        index = 0;
        this.currentSprites = sprites;

    }

    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="looptheanimation">if true, the animation loops</param>
    /// <param name="actionafterlastsprite">Action thats going to run after showing the last sprite of the array (only works if the sprite doesn't loop) </param>
    public void ChangeSpriteArray(Sprite[] sprites, bool looptheanimation, Action actionafterlastsprite)
    {
        ChangeSpriteArray(sprites);
        loopTheAnimation = looptheanimation;
        actionWhenAnimationEnds = actionafterlastsprite;

    }
    /// <summary>
    /// Change set of sprites "being animated"
    /// </summary>
    /// <param name="sprites">Vector of sprites which is going to be animated</param>
    /// <param name="looptheanimation">if true, the animation loops</param>
    /// <param name="destroyonanimationended">if true, the gameobject is destroyed when the last sprite is shown</param>
    /// <param name="actionafterlastsprite">Action thats going to run after showing the last sprite of the array (only works if the sprite doesn't loop) </param>

    public void ChangeSpriteArray(Sprite[] sprites, bool looptheanimation, bool destroyonanimationended, Action actionafterlastsprite)
    {
        ChangeSpriteArray(sprites, looptheanimation, actionafterlastsprite);
        destroyOnAnimationEnded = destroyonanimationended;
    }



}
