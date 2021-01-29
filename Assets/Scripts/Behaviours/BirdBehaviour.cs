using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdBehaviour : MonoBehaviour
{

    [Header("Behaviour Control")]
    //[SerializeField] float highestHeightPos;
    //[SerializeField] float lowestHeightPos;
    //[SerializeField] float speedHeightChangePos;
    [SerializeField] Vector3 firstPos;
    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 offScreenPos;
    [SerializeField] float speedPosChange;
    [SerializeField] float holdPositionTimer;

    bool flipMov;
    float heightRef;
    Tween logicTween;
    SpriteRenderer spriteRenderer;
    #region singleton
    static public BirdBehaviour instance { get; private set; }
    void Awake()
    {
        if (instance = null) instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // HandleHeightMovement();

        //debug
        CallBird();
        //
    }

    // Update is called once per frame
    void Update()
    {
        /*   if (heightRef > highestHeightPos || heightRef < lowestHeightPos)
           {
               heightRef = 0;
               flipMov = !flipMov;
           }
           if (flipMov)
           {
               transform.position += new Vector3(0, Time.deltaTime * speedHeightChangePos, 0);
               heightRef += Time.deltaTime * speedHeightChangePos;
           }
           else
           {
               transform.position -= new Vector3(0, Time.deltaTime * speedHeightChangePos, 0);
               heightRef += Time.deltaTime * speedHeightChangePos;

           }*/

    }
    public void CallBird(Vector3 firstpos, Vector3 lastpos, Vector3 offscreenpos)
    {
        firstPos = firstpos;
        lastPos = lastpos;
        offScreenPos = offscreenpos;
        StopCoroutine(BirdCoroutine());
        StartCoroutine(BirdCoroutine());
    }
    public void CallBird()
    {
        StopCoroutine(BirdCoroutine());
        StartCoroutine(BirdCoroutine());
    }
    IEnumerator BirdCoroutine()
    {
        spriteRenderer.flipX = false;
        CheckFlipX(offScreenPos, firstPos); ;

        logicTween?.Complete();
        logicTween = null;
        logicTween = transform.DOMove(firstPos, 100 / speedPosChange);
        yield return logicTween.WaitForCompletion();
        yield return new WaitForSeconds(holdPositionTimer);
        CheckFlipX(firstPos, lastPos);
        logicTween = transform.DOMove(lastPos, 100 / speedPosChange);
        yield return logicTween.WaitForCompletion();
        yield return new WaitForSeconds(holdPositionTimer);
        CheckFlipX(lastPos, offScreenPos);
        logicTween = transform.DOMove(offScreenPos, 100 / speedPosChange);

    }
    void DoAction()
    {

    }
    void CheckFlipX(Vector3 beforeChange, Vector3 afterChange)
    {
        //the default sprite direction has to be looking to the left
        if (beforeChange.x < afterChange.x) spriteRenderer.flipX = true; // look to the right
        else { spriteRenderer.flipX = false; } //look to the left
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            DoAction();
        }
    }
    
}
