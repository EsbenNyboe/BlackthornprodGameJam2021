using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    PointsSystem pointsSystem;
    static public PlayerBehaviour instance { get; private set; }
    void Awake()
    {
        if(instance = null) instance = this;
         pointsSystem = new PointsSystem();
    }

    public PointsSystem GetPlayersPointSystem()
    {
        return pointsSystem;
    }
}
