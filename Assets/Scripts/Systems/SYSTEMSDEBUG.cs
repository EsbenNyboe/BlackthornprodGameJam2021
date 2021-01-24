using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField] GameObject tester;
    ThrowObjectSystem throwObjectSystem;
    private void Start()
    {
        throwObjectSystem = new ThrowObjectSystem();
    }

    // testing the ThrowObject class
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            throwObjectSystem.ThrowObject(tester, 80,10);
            Debug.Log("input received");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            throwObjectSystem.ThrowObject(tester, -25, 10);
            Debug.Log("input received");
        }
    }

}
