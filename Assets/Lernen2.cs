using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lernen2 : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Lernen>().ChangeSpeed(3, true);

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Lernen>().isCanMove = true;
        }
    }
}
