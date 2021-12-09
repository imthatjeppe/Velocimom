using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float currentstamina;
    public float loseSpeedAmount = 0.3f;
    public float staminaEncumberedDrain;
    public float staminaDrain;

    public int foodUntilEncumbered = 2;
    

    public static int playerHealth;

    public Slider Staminabar;
    public GameObject Heart0, Heart1, Heart2, PlayerDeception;

    public bool hidden;
    public bool releasedStaminaKey;
    public bool inSafeRoom = false;
    public bool isRunning = false;

    Vector3 movement = new Vector3();
    private float resetSpeed = 0;

    PlayerDecption playerDeception;
    PlayerAudioHandler audioHandler;
    private Inventory inventoryScriptObject;
    private Rigidbody2D rigidBody;
    private float speedMagnitude = 100;

    void Start()
    {
        playerHealth = 3;
        Heart0.gameObject.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);

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
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical") * 0.5f;
            speed = Mathf.Clamp(speed, maxSpeed/10, maxSpeed);

        if (!hidden)
        {
            movement = new Vector3(x, y).normalized * Time.deltaTime * speed;
        } else
        {
            movement = new Vector2(0, 0);
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        rigidBody.velocity = movement;
        Health();
        GameOver();
        HiddenAbility();
        LoseSpeedCarryingFood();
    }
    public void GameOver()
    {
        if (playerHealth <= 0)
            SceneManager.LoadScene("GameOver");
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

    private void LoseSpeedCarryingFood()
    {

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
    public void Health()
    {
        if (playerHealth > 3)
            playerHealth = 3;

        Heart0.gameObject.SetActive(false);
        Heart1.gameObject.SetActive(false);
        Heart2.gameObject.SetActive(false);

        if (playerHealth > 0)
            Heart0.gameObject.SetActive(true);
        if (playerHealth > 1)
            Heart1.gameObject.SetActive(true);
        if (playerHealth > 2)
            Heart2.gameObject.SetActive(true);
    }

    private void HiddenAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = resetSpeed;
            currentstamina -= 10;
            releasedStaminaKey = false;
            audioHandler.PlayHugoInhaleSFX();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            LoseStamina(staminaDrain);

            if (currentstamina >= 0)
            {
                hidden = true;
            }

            if(inventoryScriptObject.inventoryCount >= 3)
            {
                LoseStamina(staminaEncumberedDrain);
            }
        }
        else if (inSafeRoom)
        {
            GainStamina(20);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = maxSpeed;
            hidden = false;
            releasedStaminaKey = true;
            audioHandler.PlayHugoExhaleSFX();
        }

        if (currentstamina <= 0)
        {
            hidden = false;
        }
    }
     public Vector2 GetPlayerVelcoity()
    {
        return rigidBody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saferoom") && !playerDeception.enemyLure)
        {
            inSafeRoom = true;
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