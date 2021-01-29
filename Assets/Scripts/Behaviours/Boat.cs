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

    [Header("UI Elements")]
    public VelocityBar velocityBar;
    public VelocityBar peopleCountBar;
    public int peopleOnBoat; //Idk where is the real variable thats just a holder (kelvyn)
    MoveSprite boatPortLevelEnd;

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
        boatPortLevelEnd = FindObjectOfType<EndOfLevel>().transform.parent.GetComponent<MoveSprite>(); // HELP: how do we ignore this line when in the menu?
        GameManager.npcPointsSystem.OnPointsChanged += NpcPointsSystem_OnPointsChanged;
    }

    private void NpcPointsSystem_OnPointsChanged(object sender, PointsSystem.OnPointsDataEventArgs e)
    {
        if (peopleCountBar != null) peopleCountBar.SetVelocity(peopleOnBoat);
    }

    // Update is called once per frame
    void Update()
    {
        //Ui Elements
        if (velocityBar != null) velocityBar.SetVelocity(speed);
 

        if (!EndOfLevel.levelWon)
            boatPortLevelEnd.speed = speed;
        else
            boatPortLevelEnd.speed = 0;
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
        StopCoroutine(InstantBoostCoroutine(boostValue, boostTime));
        StartCoroutine(InstantBoostCoroutine(boostValue, boostTime));
    }

    public IEnumerator InstantBoostCoroutine(float boostvalue, float boosttime)
    {
        boostvalue = boostvalue / GameManager.currentWaveDrag;
        boosttime = boosttime / GameManager.currentWaveDrag;
        float correctedInertiaAcc = boatInertiaTimeAcceleration / GameManager.currentWaveDrag;
        DOTween.Complete(gameObject);
       // Vector3 defaultPosition = transform.position;
        DOTween.To(() => speed, (bv) => speed = bv, boostvalue, correctedInertiaAcc);
        //DOTween.To(() => transform.position, (pos) => transform.position = pos, defaultPosition + new Vector3(boatDistancePushed, 0, 0), boatInertiaTime);

        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.localPosition - new Vector3(0.5f, 0, 0);
        yield return new WaitForSeconds(boosttime + correctedInertiaAcc);
        float correctedInertiaDeacc = boatInertiaTimeDeacceleration / GameManager.currentWaveDrag;
        DOTween.To(() => speed, (bv) => speed = bv, GameManager.instance.postWaveBoatSpeed, correctedInertiaDeacc);
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
