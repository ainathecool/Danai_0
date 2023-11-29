using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(touchPos.x < 0)
            {
                rb.AddForce(Vector2.left * moveSpeed);
            }
            else
            {
                rb.AddForce(Vector2.right * moveSpeed);

            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "milkBox")
        {
            // Disable the Rigidbody2D component to stop physics interactions
            collision.rigidbody.isKinematic = true;

            // Set the position on top of the player's head (you may need to adjust this)
            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

            collision.rigidbody.velocity = Vector2.zero;
        }
    }
}
