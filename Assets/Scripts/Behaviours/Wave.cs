using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private Boat boat;
    public bool boatRidingNow;
    public float waveDragFactor = 1;
    private float timeOnTheWave;
    private float startSpeed;
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();

        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //set global wave drag factor
        waveDragFactor = gm.waveDragFactor;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Time.deltaTime * boat.speed * -1, 0, 0);

        if (boatRidingNow)
        { 
             if (boat.speed > 0) boat.speed -= Time.deltaTime * waveDragFactor;
        }
        else
        {
            timeOnTheWave = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            if (!boatRidingNow)
            {
                startSpeed = boat.speed;
            }
                boatRidingNow = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boat")
        {
            if (boatRidingNow)
            {
                boat.speed = startSpeed;
            }
                boatRidingNow = false;
        }
    }
}
