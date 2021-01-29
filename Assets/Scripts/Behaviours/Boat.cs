using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : MonoBehaviour
{
    [Header("General")]
    //[HideInInspector]
    public float speed;

    [SerializeField] float boatInertiaTimeDeacceleration;// time the takes to the boat to lose maxboostspeed
    [SerializeField] float boatInertiaTimeAcceleration; // time the takes to the boat to gain maxboostspeed
   // [SerializeField] float boatDistancePushed;//Distance the boat is pushed when boosted (not working properly)

    [Header("Tweening")]
    [SerializeField] float boatShakeDuration;//Boat shake duration when lands on water
    [SerializeField] float boatShakeStrength;//Boat shake force
    private Vector3 defaultScale;

    [Header("Water Particle")]
    public GameObject waterParticle; //Particle to water collision
    public Transform waterPos; //Pos to water particle

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
        defaultScale = transform.localScale;
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

        StartCoroutine(PermanentBoostCoroutine(boostValue, 1.5f));

    }
    public void TemporaryBoost(float boostValue, float boostTime)
    {
        //don't do boost if speed to high
        if (speed > GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().maxBoatSpeed)
        {
            return;
        }
        StartCoroutine(InstantBoostCoroutine(boostValue, boostTime));
    }

    public IEnumerator InstantBoostCoroutine(float boostvalue, float boosttime)
    {
        DOTween.Complete(gameObject);
       // Vector3 defaultPosition = transform.position;
        DOTween.To(() => speed, (bv) => speed = bv, boostvalue, boatInertiaTimeAcceleration);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition + new Vector3(boatDistancePushed, 0, 0), boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(boosttime + boatInertiaTimeAcceleration);
        DOTween.To(() => speed, (bv) => speed = bv, GameManager.instance.postWaveBoatSpeed, boatInertiaTimeDeacceleration);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition, boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
    }
    public IEnumerator PermanentBoostCoroutine(float boostvalue, float effectTime)
    {
        Vector3 defaultPosition = transform.position;
        DOTween.To(() => speed, (bv) => speed = bv, boostvalue, boatInertiaTimeAcceleration);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition + new Vector3(boatDistancePushed, 0, 0), boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(effectTime + boatInertiaTimeAcceleration);
      //  DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition, boatInertiaTime);


        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            //transform.localScale = defaultScale;
            //transform.DOShakeScale(boatShakeDuration, boatShakeStrength*Vector3.down);
            Instantiate(waterParticle, waterPos.transform.position, Quaternion.identity);
        }
    }
}
