using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    Text textPeople;

    private void Start()
    {
        textPeople = GetComponent<Text>();
        textPeople.text = "People On\n the Boat: " + GameManager.npcPointsSystem.currentPoints;
        GameManager.npcPointsSystem.OnPointsChanged += NpcPointsSystem_OnPointsChanged;
    }

    private void NpcPointsSystem_OnPointsChanged(object sender, PointsSystem.OnPointsDataEventArgs e)
    {
        textPeople.text = "People On\n the Boat: " + GameManager.npcPointsSystem.currentPoints;
    }
}
