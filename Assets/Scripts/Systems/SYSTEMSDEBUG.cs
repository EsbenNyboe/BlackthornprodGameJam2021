using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField] bool forceWind;
    [SerializeField] float windForce;
    [SerializeField] float windAngle;
    [SerializeField] float forceAngle45Right;
    [SerializeField] float forceAngle45Left;
    [SerializeField] float forceAngle90;
  
       Vector2 windDirection;
    [SerializeField] GameObject tester;
    Vector3 originalPosition;
    ThrowObjectSystem throwObjectSystem;

    /* INSTRUCTIONS
     * Press E to throw the ball 45° to the right of the boat
     * Press Q to throw the ball 45° to the left of the boat
     * Press W to throw the ball 90° straight up
     * 
     * Press SPACE to reset the ball position
     * 
     * bool forceWind is to set the wind effect on or off
     * float windForce is the force that the wind apply on the ball's rigidbody
     * float windAngle is the angle that the wind is pushing towards
     * float forceAngle45Right, forceAngle45Left and forceAngle90 are the force amounts for each angle (it's a bit different from each other because 
     *                                                                                                  of the wind force)
     * GameObject tester is the ball itself
     * 
     * Feel free to tweek any options either from this debug script or from the Ball's rigidbody component so we can find
     * the sweet spot
     * 
     * in case you want to change the angle to which the ball is thrown with E, Q or W. Then go to the Update function inside this script
     * there's a function called ThrowObject() being used 3 times, the second parameter of this function is the angle. 
     * However, be aware that unity counts the angle units counter-clockwise, angle 0° being straight right.
     * 
     * I added a helper logic as well, so we can use -45° instead of 135° (left 45°), which you can see it being used 
     * in the Start() function of this script
   */
    private void Start()
    {
        throwObjectSystem = new ThrowObjectSystem();
        originalPosition = tester.transform.position;
        float auxAngle;
        //this is so we can use -45(left) or 45(right) instead of 135(left) and 45(right)
        if (windAngle >= 0)
        {
            auxAngle = windAngle;
        }
        else
        {
            auxAngle = 180 + windAngle;
        }
        //this fixes the weird way that Unity handles the angle units initial position
        auxAngle -= 90;
        Quaternion rotation = Quaternion.AngleAxis(auxAngle, Vector3.forward);
        windDirection = rotation * Vector3.up;
    }

    void Update()
    {
        if (forceWind)
        {
            tester.GetComponent<Rigidbody2D>().AddForce(windDirection.normalized * windForce, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tester.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            throwObjectSystem.ThrowObject(tester, 45, forceAngle45Right);
            forceWind = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (tester.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            throwObjectSystem.ThrowObject(tester, 90, forceAngle90);
            forceWind = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (tester.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            throwObjectSystem.ThrowObject(tester, -45, forceAngle45Left);
            forceWind = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            tester.transform.position = originalPosition;
            tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            forceWind = false;

        }

    }

}
