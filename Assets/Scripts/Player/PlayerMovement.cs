using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Float Variables")]
    public float speed;
    public float maxSpeed;
    public float currentstamina;
    public float loseSpeedAmount = 0.3f;
    public float staminaEncumberedDrain;
    public float staminaDrain;

    [Header("Int Variables")]
    public int foodUntilEncumbered = 2;

    [Header("Bools")]
    public bool hidden;
    public bool releasedStaminaKey;
    public bool inSafeRoom;
    public bool isRunning;

    [Header("GameObjects")]
    public Slider Staminabar;
    public GameObject PlayerDeception;

    private float speedMagnitude = 100;
    private float resetSpeed = 0;

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

        speed *= speedMagnitude;
        maxSpeed *= speedMagnitude;
        loseSpeedAmount *= speedMagnitude;
    }

    void Update()
    {
        Move();
        HiddenAbility();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") * 0.5f;
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        if (!hidden)
        {
            movement = new Vector3(x, y).normalized * Time.deltaTime * speed;
        }
        else
        {
            movement = new Vector2(0, 0);
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

        //Following code makes player lose speed depending on inventory count
        if (inventoryScriptObject.inventoryCount >= 3)
        {
            if (inventoryScriptObject.inventoryCount > foodUntilEncumbered)
            {
                speed -= (inventoryScriptObject.inventoryCount - foodUntilEncumbered) * loseSpeedAmount;
                foodUntilEncumbered += 1;
            }
            else if (inventoryScriptObject.inventoryCount < foodUntilEncumbered)
            {
                foodUntilEncumbered -= 1;
                speed += loseSpeedAmount;
            }

        }
    }
    public Vector2 GetPlayerVelocity()
    {
        return rigidBody.velocity;
    }

    private void HiddenAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentstamina > 0)
        {
            speed = resetSpeed;
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
            velocimomBehaviour.clearPlayerPathSpots();
            playerDeception.Resume();
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