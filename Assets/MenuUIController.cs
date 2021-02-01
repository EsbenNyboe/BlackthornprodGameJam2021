using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{   [Header("Resource Income")]
    [SerializeField] RectTransform titleGameRect;
    Vector2 defaultpos;
    [Header("Tweening")]
    [SerializeField] float yUpMax; //How many units should be summed to the rect.y vector
    [SerializeField] float yDownMax; //How many units should be subtracted to the rect.y vector
    [SerializeField] float moveDuration; //How long it takes to do each movement
    void Start()
    {
        defaultpos = titleGameRect.position;
        HandleMoviment();
        
    }

    void HandleMoviment()
    {
        titleGameRect.DOMoveY(defaultpos.y + yUpMax, moveDuration).OnComplete(() => titleGameRect.DOMoveY(defaultpos.y - yDownMax, moveDuration).OnComplete(() => HandleMoviment()));
    }
     
}
