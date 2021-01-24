using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField] bool forceWind;
    [SerializeField] float windForce;
    [SerializeField] float windAngle;
    [SerializeField] float maxPullDistance;
    [SerializeField]float maxPullForce;
    float forcemultiplier;

    Vector2 windDirection;
    [SerializeField] GameObject tester;
    Vector3 originalPosition;
    ThrowObjectSystem throwObjectSystem;

    [SerializeField] LayerMask throwableMask;
    bool startPulling;
     Vector3 mouseOnWorldPosition;
    const float RAYCAST_RADIUS = 3;
    Vector3 launchDirection;

    /* INSTRUCTIONS
      TURN ON GIZMOS

    Left mouse button start the pulling process, once you click it, hold it and move it around. 
    A ray will show you the direction of the trajectory that the ball will follow once you release the
    mouse button. The green circle following the mouse will show you how close you'll need to be to actually "pull the power" to charge
     
    Space resets the ball position
   
    

    float maxPullDistance ---> max distance in world units
    float maxPullForce ---> max possible force applied to the ball's rigidbody
    float forcemultiplier ---> multiplier of the force applied to the ball's rigidbody so it can go from 0% to 100% of the maxForce
    LayerMask throwableMask ---> LayerMask to focus the raycast
    bool startPulling ---> boolean to start the calculation process
    Vector3 mouseOnWorldPosition ---> holds the mouse position inside the world
    const float RAYCAST_RADIUS = 3 ---> raycast radius. Also being show in the gizmos
    Vector3 launchDirection ---> direction of the launch


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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            tester.transform.position = originalPosition;
            tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            forceWind = false;

        }

        mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//This won't be here when we build the actual system
        //but,for debug/gizmos reasons, we are getting the information directly in the update

        if (Input.GetMouseButtonDown(0))
        {
             RaycastHit2D hitInfo = Physics2D.CircleCast(mouseOnWorldPosition, RAYCAST_RADIUS, Vector2.zero,0, throwableMask);
            if (hitInfo)
            {
                startPulling = true;

            }
        }
        if (startPulling)
        {
           

            //this part should not be in here, because we don't need to calculate all of this before we get in the next if statement
            //However, for debug/gizmos reasons, we are doing the calculation here
            float distance = Vector2.Distance(mouseOnWorldPosition, tester.transform.position);

            if (distance > maxPullDistance) forcemultiplier = 1;
            else
            {
                forcemultiplier = distance / maxPullDistance;
            }

            launchDirection = (tester.transform.position - mouseOnWorldPosition).normalized;
            Debug.DrawRay(tester.transform.position, launchDirection * forcemultiplier* maxPullForce);
            if (Input.GetMouseButtonUp(0))
            {
                startPulling = false;
               
                if (tester.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                throwObjectSystem.ThrowObject(tester, launchDirection, forcemultiplier* maxPullForce);
                forceWind = true;
            }

        }
       

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mouseOnWorldPosition, RAYCAST_RADIUS);
        
    }
}
