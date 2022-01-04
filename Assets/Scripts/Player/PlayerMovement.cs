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
    public bool extraHidden; // hides player when empty inventory and standing still
    public bool releasedStaminaKey;
    public bool inSafeRoom = true;
    public bool isRunning;

    [Header("GameObjects")]
    public Slider Staminabar;
    public GameObject PlayerDeception;

    private float hiddenSpeed = 0;
    private float extraHiddenTimer = 1f;
    

    Vector3 movement = new Vector3();

    PlayerDecption playerDeception;
    PlayerAudioHandler audioHandler;
    VelocimomBehaviour velocimomBehaviour;
    Inventory inventoryScriptObject;
    Rigidbody2D rigidBody;
    PlayerInteractions playerInteractions;

    void Start()
    {
        velocimomBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<VelocimomBehaviour>();
        playerDeception = PlayerDeception.GetComponentInChildren<PlayerDecption>();
        inventoryScriptObject = GetComponent<Inventory>();
        audioHandler = GetComponentInChildren<PlayerAudioHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerInteractions = GetComponent<PlayerInteractions>();
    }

    void Update()
    {
        IsPlayerDetectable();
        HiddenAbility();
        Debug.Log("Speed: " + speed);
    }
    void FixedUpdate()
    {
        if(!playerInteractions.GetInteracting())
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

    private void IsPlayerDetectable()
    {

        if (inventoryScriptObject.inventoryCount <= 0 && rigidBody.velocity.sqrMagnitude == 0)
        {
            extraHiddenTimer -= Time.deltaTime;
            if (extraHiddenTimer <= 0)
            {
                extraHidden = true;
            }
        }
        else
        {
            extraHidden = false;
            extraHiddenTimer = 1f;
        }
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
        else
        {
            speed = maxSpeed;
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
        if (collision.CompareTag("Saferoom"))
        {
            inSafeRoom = true;
            velocimomBehaviour.ClearPlayerPathSpots();
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