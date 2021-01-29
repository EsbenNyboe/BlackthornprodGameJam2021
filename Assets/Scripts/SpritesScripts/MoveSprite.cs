using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public float speed;
    public float timeToStart = 1;
    public bool isToDestroy = false;

    private float initalStart;
    Vector3 startPos;

    private void Start()
    {
        initalStart = timeToStart;
        startPos = transform.localPosition;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeToStart > -0.5f)
        timeToStart -= 1 * Time.deltaTime;

        if(timeToStart <= 0)
            transform.Translate(Time.deltaTime * speed * -1, 0, 0); //Move to left

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "resetSprite") //If sprite collides with resetSprite tag, move to start postion
        {
            timeToStart = initalStart;
            if (isToDestroy) Destroy(gameObject);
            transform.position = new Vector3(GameObject.Find("StartPos_Sprites").transform.position.x, startPos.y, startPos.z);
        }
    }
}
