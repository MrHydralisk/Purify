using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    private RaycastHit2D hit, hit2;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float sprintSpeedMult = 1f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float mult = Input.GetAxisRaw("Sprint");

        moveDelta = new Vector3(x, y, 0) * moveSpeed * (1 + mult * sprintSpeedMult) * Time.deltaTime;

        //Mirror sprite direction
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector2 boxTransform = new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y);

        //Can move or obstacle
        hit = Physics2D.BoxCast(boxTransform, boxCollider.size, 0, new Vector2(0, y), Mathf.Abs(moveDelta.y), LayerMask.GetMask("Entity", "Wall"));
        if (hit.collider != null)
        {
            moveDelta.y = 0;
        }
        
        hit2 = Physics2D.BoxCast(boxTransform, boxCollider.size, 0, new Vector2(x, 0), Mathf.Abs(moveDelta.x), LayerMask.GetMask("Entity", "Wall"));
        if (hit2.collider != null)
        {
            moveDelta.x = 0;
        }

        //Move
        transform.Translate(moveDelta);
    }
}
