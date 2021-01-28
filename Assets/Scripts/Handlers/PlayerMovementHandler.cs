using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] float speed;
    
    Rigidbody2D rb;
     float horizontal;
    Vector3 defaultScale;


    void Awake()
    {
        defaultScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
     }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0)
        {
            CheckLookingDirection(horizontal);
            rb.AddForce(new Vector2(horizontal * speed, 0), ForceMode2D.Force);
        }
    }
    void CheckLookingDirection(float value)
    {
        if (value < 0) transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);  
        else if(value > 0)
        {
            transform.localScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
        }
    }
}
