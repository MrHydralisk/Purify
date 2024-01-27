using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private StatsHandler statsHandler;

    private Vector3 moveDelta;

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float sprintSpeedMult = 1f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        statsHandler = GetComponent<StatsHandler>();
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

    public void Damage(float amount)
    {
        Stat statHealth = statsHandler.GetStat("Health");
        if (statHealth != null)
        {
            statHealth.ChangeValue(-amount);
            if (statHealth.isMin)
            {
                Kill();
            }
        }
        else
        {
            Debug.LogError("Stat Health not found");
        }
    }

    public void GivePoints(int amount)
    {
        Stat statEnergy = statsHandler.GetStat("Energy");
        if (statEnergy != null)
        {
            statEnergy.ChangeValue(amount);
        }
        else
        {
            Debug.LogError("Stat Energy not found");
        }
    }

    private void Kill()
    {
        Debug.Log("Died");
        Destroy(this.gameObject);
    }
}
