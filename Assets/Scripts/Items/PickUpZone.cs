using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    public PickUpItem PIT;
    private SpriteRenderer SR;
    private bool isClose;
    private bool isHoldingF = false;
    private float holdTime = 0f;
    private float requiredHoldTime = 0.6f;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        SR.color = Color.white;
        StartCoroutine(Rotating());
    }
    void Update()
    {
        if (isClose)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isHoldingF = true;
                holdTime = 0f;
            }

            if (Input.GetKey(KeyCode.F))
            {
                holdTime += Time.deltaTime;

                if (holdTime >= requiredHoldTime)
                {
                    PIT.ActiveItem(true);
                    Destroy(gameObject);
                    isHoldingF = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.F) && isHoldingF)
            {
                PIT.ActiveItem(false);
                Destroy(gameObject);
                isHoldingF = false;
            }
        }
    }

    private IEnumerator Rotating()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            transform.Rotate(0, 0, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isClose = true;
            SR.color = Color.green;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isClose = false;
            SR.color = Color.white;
        }
    }
}
