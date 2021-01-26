using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private Boat boat;
    public bool boatRidingNow;
    public float timeOnTheWaveToFall = 1;
    private float timeOnTheWave;
    private float startSpeed;
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
    }

    // Update is called once per frame
    void Update()
    {
          
        transform.Translate(Time.deltaTime * boat.speed * -1, 0, 0);

        if (boatRidingNow)
        {
            //Muniz:  timeOnTheWave += Time.deltaTime;  I think this was missing
            if (timeOnTheWave > timeOnTheWaveToFall)
            {
                boat.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                timeOnTheWave = 0;
                boat.speed = 0;
            }
            else
            { //Muniz: I added the if statement because sometimes the boat.speed is getting negative, so the waves
              //are going in the wrong direction
                if (boat.speed > 0) boat.speed -= Time.deltaTime / timeOnTheWaveToFall;
            }
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
