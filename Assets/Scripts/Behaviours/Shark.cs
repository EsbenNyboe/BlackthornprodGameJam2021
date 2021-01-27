using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [Header("General")]
    public float speed;
    [Header("Destination")]
    public bool goingToADestination;
    public Vector3 theDestination;
    public Vector3[] backAndForthBoatDestinations;
    private int lastDestinationID;

    //private Stuff
    private Boat boat;
    bool isSharkActive;
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
        
        isSharkActive = false;
        changeHidden(0);
    }

    // Update is called once per frame
    void Update()
    {
        print(goingToADestination);
        if (goingToADestination)
        {
            Vector3 dir = new Vector3(theDestination.x - transform.position.x, 0, 0).normalized;
            transform.Translate(dir * speed * Time.deltaTime);

            Vector3 newDestination = new Vector3(theDestination.x, theDestination.y);
            newDestination.y = transform.position.y;

            if (Vector2.Distance(transform.position, newDestination) < 0.3f)
            {
                OnDestination();
            }
        }
    }

    public void GoToDestination(Vector3 destination)
    {
        theDestination = destination;
        goingToADestination = true;

        //make shark visible
        changeHidden(1);
    }

    public void OnDestination()
    {
        goingToADestination = false;
        theDestination = Vector3.zero;
        if (lastDestinationID == 0)
        {
            lastDestinationID = 1;
        }
        else if (lastDestinationID == 1)
        {
            lastDestinationID = 0;
            //Hide the shark
            changeHidden(0);
            return;
        }
        print(lastDestinationID);
        Vector3 nextPos = backAndForthBoatDestinations[lastDestinationID];
        GoToDestination(nextPos);
        print(backAndForthBoatDestinations[lastDestinationID].x);
    }

    IEnumerator SharkHiddenCourotine()
    {
        changeHidden(0);
        yield return new WaitForSeconds(5);
        changeHidden(1);
    }
    //triggers when the object hits the shark
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSharkActive)
        {
            if (collision.gameObject.GetComponent<ThrowableObjectsMasterClass>())
            {//is throwable object
                boat.PermanentBoost(3);
                SYSTEMSDEBUG.instance.ResetBall();
                StartCoroutine(SharkHiddenCourotine());

            }
        }

    }

    public void changeHidden(int oneOrzero)
    {
        if(oneOrzero == 1)
        {
            isSharkActive = true;
            
        }
        else
        {
            isSharkActive = false;
        }
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = oneOrzero;
        GetComponent<SpriteRenderer>().color = color;
    }
}
