using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask throwableMask;
    PointsSystem pointsSystem;
    static public PlayerBehaviour instance { get; private set; }
    void Awake()
    {
        if(instance = null) instance = this;
        Physics2D.IgnoreLayerCollision(8, gameObject.layer);
        pointsSystem = new PointsSystem();
    }

    public PointsSystem GetPlayersPointSystem()
    {
        return pointsSystem;
    }
}
