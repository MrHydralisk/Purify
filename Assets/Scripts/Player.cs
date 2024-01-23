using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private Vector3 moveDelta;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float sprintSpeedMult = 1f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float mult = Input.GetAxisRaw("Sprint");

        moveDelta = new Vector3(x, y, 0) * moveSpeed * (1 + mult * sprintSpeedMult) * Time.deltaTime;

        //Mirror sprite direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Move
        rigidBody.MovePosition(transform.position + moveDelta);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Entity entity = collider.GetComponent<Entity>();
        if (entity != null)
        {
            entity.collideAction(this.gameObject);
        }
    }
}
