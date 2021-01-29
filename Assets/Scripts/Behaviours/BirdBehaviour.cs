using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdBehaviour : MonoBehaviour
{

    [Header("Behaviour Control")]
    [SerializeField] float highestHeightPos;
    [SerializeField] float lowestHeightPos;
    [SerializeField] float speedHeightChangePos;
    [SerializeField] Vector3 firstPos;
    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 offScreenPos;
    [SerializeField] float speedPosChange;
    [SerializeField] float holdPositionTimer;

    bool flipMov;
    float heightRef;
    Tween logicTween;
    #region singleton
    static public BirdBehaviour instance { get; private set; }
    void Awake()
    {
        if (instance = null) instance = this;
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
        /*     if (transform.position.y >= highestHeightPos) flipMov = !flipMov;
             else if (transform.position.y <= lowestHeightPos) flipMov = !flipMov;

             if (flipMov)
             {
                 transform.position += new Vector3(0, Time.deltaTime * speedHeightChangePos, 0);
             }
             else
             {
                 transform.position -= new Vector3(0, Time.deltaTime * speedHeightChangePos, 0);
             }
     */
    }
    public void CallBird()
    {
        StopCoroutine(BirdCoroutine());
        StartCoroutine(BirdCoroutine());
    }
    IEnumerator BirdCoroutine()
    {
        logicTween?.Complete();
        logicTween = null;
        logicTween = transform.DOMove(firstPos, 100 / speedPosChange);
        yield return logicTween.WaitForCompletion();
        yield return new WaitForSeconds(holdPositionTimer);
        logicTween = transform.DOMove(lastPos, 100 / speedPosChange);
        yield return logicTween.WaitForCompletion();
        yield return new WaitForSeconds(holdPositionTimer);
        logicTween = transform.DOMove(offScreenPos, 100 / speedPosChange);

    }
    void HandleHeightMovement()
    {
        TweenCallback lowerHeight = () => transform.DOMove(new Vector3(transform.position.x, transform.position.y + lowestHeightPos), 100 / speedHeightChangePos).OnComplete(() => HandleHeightMovement());
        transform.DOMove(new Vector3(transform.position.x, transform.position.y - highestHeightPos), 100 / speedHeightChangePos).OnComplete(lowerHeight);
    }
}
