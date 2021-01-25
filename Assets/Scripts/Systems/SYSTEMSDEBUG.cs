using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField]GameObject tester;
    Vector3 originalPosition;
   
    private void Start()
    {
       
        originalPosition = tester.transform.position;
         
    }

    void Update()
    {
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            tester.transform.position = originalPosition;
            tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            tester.GetComponent<WindBehavior>().DeactivateWindEffect();

        }

       

    }

  
}
