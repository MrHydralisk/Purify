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

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 0.75f);
        colliders = colliders.Where((Collider2D c) => c.GetComponent<IInteractable>() != null).ToArray();
        if (colliders.Count() > 0)
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            colliders.OrderBy((Collider2D c) => Vector3.Distance(cursorPos, c.gameObject.transform.position)).First().GetComponent<IInteractable>().interactionAction(this.gameObject);
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
