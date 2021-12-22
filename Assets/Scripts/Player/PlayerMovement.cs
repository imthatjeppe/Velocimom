using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Float Variables")]
    public float speed;
    public float maxSpeed;
    public float currentstamina;
    public float loseSpeedAmount;
    public float staminaEncumberedDrain;
    public float staminaDrain;

    [Header("Int Variables")]
    public int foodUntilEncumbered;

    [Header("Bools")]
    public bool hidden;
    public bool releasedStaminaKey;
    public bool inSafeRoom;
    public bool isRunning;

    [Header("GameObjects")]
    public Slider Staminabar;
    public GameObject PlayerDeception;

    private float hiddenSpeed = 0;

    Vector3 movement = new Vector3();

    PlayerDecption playerDeception;
    PlayerAudioHandler audioHandler;
    VelocimomBehaviour velocimomBehaviour;
    Inventory inventoryScriptObject;
    Rigidbody2D rigidBody;

    void Start()
    {
        velocimomBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        playerDeception = PlayerDeception.GetComponentInChildren<PlayerDecption>();
        inventoryScriptObject = GetComponent<Inventory>();
        audioHandler = GetComponentInChildren<PlayerAudioHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HiddenAbility();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") * 0.5f;
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        if (!hidden)
        {
            movement = new Vector3(x, y).normalized * Time.fixedDeltaTime * (speed - (loseSpeedAmount * (inventoryScriptObject.inventoryCount - foodUntilEncumbered)));
        }
        else
        {
            movement = new Vector3(0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !hidden)
        {
            movement *= 2;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        rigidBody.velocity = movement;
    }
    public Vector2 GetPlayerVelocity()
    {
        return rigidBody.velocity;
    }

    private void HiddenAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentstamina > 0)
        {
            speed = hiddenSpeed;
            currentstamina -= 10;
            releasedStaminaKey = false;
            audioHandler.PlayHugoInhaleSFX();
        }

        if (Input.GetKey(KeyCode.Space) && currentstamina > 0)
        {
            LoseStamina(staminaDrain);
            hidden = true;

            if (inventoryScriptObject.inventoryCount >= 3)
            {
                LoseStamina(staminaEncumberedDrain);
            }
        }
        else if (inSafeRoom)
        {
            GainStamina(20);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && currentstamina <= 0)
        {
            //TODO: add responsivnes when player stamina is empty
            Debug.Log("No Stamina");
        }

        if (Input.GetKeyUp(KeyCode.Space) && hidden || currentstamina == 0 && hidden)
        {
            speed = maxSpeed;
            hidden = false;
            releasedStaminaKey = true;
            audioHandler.PlayHugoExhaleSFX();
        }
    }

    private void LoseStamina(float LoseStamina)
    {
        currentstamina -= LoseStamina * Time.deltaTime;
        currentstamina = Mathf.Clamp(currentstamina, 0, 100);
        Staminabar.value = currentstamina;
    }

    private void GainStamina(float GainStamina)
    {
        LoseStamina(-GainStamina);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saferoom") && !playerDeception.enemyLure)
        {
            inSafeRoom = true;
            velocimomBehaviour.ClearPlayerPathSpots();
            //playerDeception.Resume();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Saferoom"))
        {
            inSafeRoom = false;
        }
    }

}