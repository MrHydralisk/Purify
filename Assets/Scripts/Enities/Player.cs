using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private StatsHandler statsHandler;

    private Vector3 moveDelta;

    [Header("Debug")]
    public Logger logger;

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

        float mult2 = moveSpeed * (1 + mult * sprintSpeedMult) * Time.deltaTime;
        moveDelta = new Vector3(x * mult2, y * mult2, 0);


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

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 0.75f);
        int collidersCount = colliders.Count();
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IInteractable interactable = null;
        float minValue = float.MaxValue;
        for (int i = 0; i < collidersCount; i++)
        {
            IInteractable inter = colliders[i].GetComponent<IInteractable>();
            if (inter != null)
            {
                float dist = Vector3.Distance(cursorPos, colliders[i].transform.position);
                if (dist < minValue)
                {
                    interactable = inter;
                    minValue = dist;
                }
            }
        }
        if (interactable != null)
        {
            interactable.interactionAction(this.gameObject);
        }
    }

        private void OnTriggerEnter2D(Collider2D collider)
    {
        ICollidable collidable = collider.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.collideAction(this.gameObject);
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
            logger.LogError("Stat Health not found", this);
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
            logger.LogError("Stat Energy not found", this);
        }
    }

    private void Kill()
    {
        logger.Log("Died", this);
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
