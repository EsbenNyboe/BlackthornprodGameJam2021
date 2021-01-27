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

    #region singleton
    static public Boat _instance;
    private void Awake()
    {
        if (_instance == null) _instance = this;
    }
#endregion

    void Start()
    {
        
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
        startCamPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PermanentBoost(float boostValue)
    {
        speed += boostValue;
        StartCoroutine(PermanentBoostCoruntine(1.5f));

    }
    public void InstantBoost(float boostValue, float boostTime)
    {
        StartCoroutine(InstantBoostCoruntine(boostValue, boostTime));
    }

    public IEnumerator InstantBoostCoruntine(float boostValue, float boostTime)
    {
        float defaultSpeed = speed;
        speed += boostValue;
        //Camera.main.transform.position = startCamPos + new Vector3(-1f, 0, 0);
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(boostTime);
        speed = defaultSpeed;
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
        //Camera.main.transform.position = startCamPos;
    }
    public IEnumerator PermanentBoostCoruntine(float effectTime)
    {
         //Camera.main.transform.position = startCamPos + new Vector3(-1f, 0, 0);
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(effectTime);
      
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
        //Camera.main.transform.position = startCamPos;
    }


}
