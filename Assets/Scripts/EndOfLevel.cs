using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    public static bool levelWon;
    void Start()
    {
        levelWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            levelWon = true;
            FindObjectOfType<GameManager>().WinGame();
        }
    }

}
