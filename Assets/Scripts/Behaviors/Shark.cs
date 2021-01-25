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
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();

        backAndForthBoatDestinations = new Vector3[2];
        backAndForthBoatDestinations[0] = new Vector3(5, -4, 0);
        backAndForthBoatDestinations[1] = new Vector3(-7, -4, 0);

        GoToDestination(boat.transform.position + new Vector3(2, -4, 0));
    }

    // Update is called once per frame
    void Update()
    {
        print(goingToADestination);
        if (goingToADestination)
        {
            Vector3 dir = new Vector3(theDestination.x - transform.position.x, 0, 0).normalized;
            print(dir);
            transform.Translate(dir * speed * Time.deltaTime);
            
            Vector3 newDestination = new Vector3(theDestination.x, theDestination.y);
            newDestination.y = transform.position.y;

            print(Vector2.Distance(transform.position, newDestination));
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
        }
        print(lastDestinationID);
        Vector3 nextPos = backAndForthBoatDestinations[lastDestinationID];
        GoToDestination(nextPos);
        print(backAndForthBoatDestinations[lastDestinationID].x);
    }
}
