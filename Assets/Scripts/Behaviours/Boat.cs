using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : MonoBehaviour
{
    [Header("General")]
    //[HideInInspector]
    public float speed;
    public float boatInertiaTime; // 0.5f
    public float boatInertiaTimeDeacceleration;
    [SerializeField] float boatDistancePushed;
    
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
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PermanentBoost(float boostValue)
    {
        //don't do boost if speed to high
        if (speed > GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().maxBoatSpeed)
        {
            return;
        }

        StartCoroutine(PermanentBoostCoruntine(boostValue, 1.5f));

    }
    public void TemporaryBoost(float boostValue, float boostTime)
    {
        //don't do boost if speed to high
        if (speed > GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().maxBoatSpeed)
        {
            return;
        }
        StartCoroutine(InstantBoostCoruntine(boostValue, boostTime));
    }

    public IEnumerator InstantBoostCoruntine(float boostvalue, float boosttime)
    {
         
        Vector3 defaultPosition = transform.position;
        DOTween.To(() => speed, (bv) => speed = bv, boostvalue, boatInertiaTime);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition + new Vector3(boatDistancePushed, 0, 0), boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(boosttime + boatInertiaTime);
        DOTween.To(() => speed, (bv) => speed = bv, GameManager.instance.postWaveBoatSpeed, boatInertiaTimeDeacceleration);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition, boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
    }
    public IEnumerator PermanentBoostCoruntine(float boostvalue, float effectTime)
    {
        Vector3 defaultPosition = transform.position;
        DOTween.To(() => speed, (bv) => speed = bv, boostvalue, boatInertiaTime);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition + new Vector3(boatDistancePushed, 0, 0), boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(effectTime + boatInertiaTime);
      //  DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition, boatInertiaTime);


        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
    }


}
