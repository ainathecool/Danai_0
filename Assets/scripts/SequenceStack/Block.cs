using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "milkBox")
        {
            // Disable the Rigidbody2D component to stop physics interactions
            collision.rigidbody.isKinematic = true;

            // Set the y-position on top of the block
            float newY = transform.position.y + 1f;

            // Set the x-position based on the player's x-coordinate
            float newX = collision.transform.position.x;

            // Set the position of the milkBox
            collision.transform.position = new Vector3(newX, newY, transform.position.z);

            // Set the velocity of the milkBox to zero
            collision.rigidbody.velocity = Vector2.zero;

            // Set the block as the parent of the milkBox
            collision.transform.SetParent(transform);
        }
    
}
}
