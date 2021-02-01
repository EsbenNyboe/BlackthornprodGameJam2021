using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UICounter : MonoBehaviour
{

    [Header("Tweening")]
    [SerializeField] float shakeForce;
    [SerializeField] float shakeDuration;

    Text textPeople;
    Vector3 defaultScale;

    private void Start()
    {
        textPeople = GetComponent<Text>();
        defaultScale = textPeople.rectTransform.localScale;

        textPeople.text = "People On\n the Boat: " + GameManager.npcPointsSystem.currentPoints;
        GameManager.npcPointsSystem.OnPointsChanged += NpcPointsSystem_OnPointsChanged;
    }

    private void NpcPointsSystem_OnPointsChanged(object sender, PointsSystem.OnPointsDataEventArgs e)
    {
        textPeople.rectTransform.localScale = defaultScale;
        textPeople.rectTransform.DOShakeScale(shakeForce,shakeDuration);
        textPeople.text = "People On\n the Boat: " + GameManager.npcPointsSystem.currentPoints;
    }
}
