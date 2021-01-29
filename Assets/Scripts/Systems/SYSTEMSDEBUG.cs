using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField] GameObject tester;
    ThrowObjectSystem handler;
    Vector3 originalPosition;
    static public SYSTEMSDEBUG instance;
    Quaternion defaultrotation;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        
        originalPosition = tester.transform.position;
        defaultrotation = tester.transform.rotation;
        handler = new ThrowObjectSystem();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {


            handler.ThrowObject(tester, Vector2.up, 10);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetBall();
        }



    }

    public void ResetBall()
    {
        tester.transform.position = originalPosition;
        tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        tester.transform.rotation = defaultrotation;
        Physics2D.IgnoreCollision(tester.GetComponent<Collider2D>(), Boat._instance.GetComponent<Collider2D>(), false);
        tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        tester.GetComponent<WindBehaviour>().DeactivateWindEffect();
        tester.GetComponent<ThrowableObjectsMasterClass>().ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Land);
        tester.layer = 8;
    }


}
