using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [Header("General")]
    public float speed = 5;
    [Header("Physics")]
    public Transform centerOfMass;
    //private stuff
    private Vector3 startCamPos;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
        startCamPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Boost(float boostValue, float boostTime)
    {
        StartCoroutine(BoostCoruntine(boostValue, boostTime));
    }

    public IEnumerator BoostCoruntine(float boostValue, float boostTime)
    {
        speed += boostValue;
        Camera.main.transform.position = startCamPos + new Vector3(-1f, 0, 0);
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(boostTime);
        speed -= boostValue;
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
        Camera.main.transform.position = startCamPos;
    }
}
