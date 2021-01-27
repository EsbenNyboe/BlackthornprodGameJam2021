using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class SYSTEMSDEBUG : MonoBehaviour
{
    [SerializeField] GameObject tester;
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

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
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
        tester.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        tester.GetComponent<WindBehaviour>().DeactivateWindEffect();
        tester.GetComponent<ThrowableObjectsMasterClass>().ChangeAnimationState(ThrowableObjectsMasterClass.AnimationType.Idle);
        tester.layer = 8;
    }


}
