using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float currentstamina;
    public float staminaDrain;
    public float loseSpeedAmount = 0.5f;
    public int foodUntilEncumbered = 1;

    public static int playerHealth;

    public Slider Staminabar;
    public GameObject Heart0, Heart1, Heart2, PlayerDeception;
    

    public bool hidden;
    public bool releasedStaminaKey;
    public bool inSafeRoom = false;

    private float resetSpeed = 0;

    PlayerDecption playerDeception;
<<<<<<< HEAD
    PlayerAudioHandler audioHandler;
=======
    AudioHandler audioHandler;

    private GameObject player;
>>>>>>> 45431ac78bcec9f53b5bcb3d6a613742b80c8b55
    private Inventory inventoryScriptObject;

    void Start()
    {
        playerHealth = 3;
        Heart0.gameObject.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);

        playerDeception = PlayerDeception.GetComponentInChildren<PlayerDecption>();
<<<<<<< HEAD
        inventoryScriptObject = LoseSpeed.GetComponent<Inventory>();
        audioHandler = GetComponent<PlayerAudioHandler>();
=======
        audioHandler = GetComponent<AudioHandler>();
        //player = GameObject.FindGameObjectWithTag("Player");
        inventoryScriptObject = GetComponent<Inventory>();
>>>>>>> 45431ac78bcec9f53b5bcb3d6a613742b80c8b55
    }

    void Update()
    {
        Health();
        GameOver();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") * 0.5f;
        Vector3 movement = new Vector3(x, y).normalized * Time.deltaTime * speed;
        transform.Translate(movement);
        HiddenAbility();
<<<<<<< HEAD
        LoseSpeedCarryingFood();
=======
      //  LoseSpeedCarryingFood();
>>>>>>> 45431ac78bcec9f53b5bcb3d6a613742b80c8b55
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

<<<<<<< HEAD
   private void LoseSpeedCarryingFood()
=======
    /*private void LoseSpeedCarryingFood()
>>>>>>> 45431ac78bcec9f53b5bcb3d6a613742b80c8b55
    {
       if (inventoryScriptObject.inventoryCount > foodUntilEncumbered)
        {
           speed -= (inventoryScriptObject.inventoryCount - foodUntilEncumbered) * loseSpeedAmount;
           
        }
<<<<<<< HEAD
    }

=======
    
    }
    */
>>>>>>> 45431ac78bcec9f53b5bcb3d6a613742b80c8b55
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Saferoom"))
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