using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public Shark sharkSpawedInScene;
    //private stuff
    private Boat boat;
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * boat.speed * -1, 0, 0);
    }

    //check for boat in the zone
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boat")
        {
            print("why?");
            sharkSpawedInScene.GoToDestination(sharkSpawedInScene.backAndForthBoatDestinations[0]);
        }
    }
}
