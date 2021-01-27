using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Boat")]
    public float maxBoatSpeed = 50;
    public float fatalBoatSpeed = 1f;
    private Boat boat;
    [Header("Shark")]
    public float sharkBoostForce = 3;
    [Header("Wave")]
    //how much speed dereases by each second on wave
    public float waveDragFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for boat speed to low
        if (boat.speed <= fatalBoatSpeed)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        print("Game Lost");
        //Make Boat sink
        boat.GetComponent<Rigidbody2D>().mass = 50;
    }
}
