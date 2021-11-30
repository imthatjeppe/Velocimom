using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float newSpeed;
    public float AbilityStamina = 100;
    public float currentstamina;

    public static int playerHealth;

    public Slider Staminabar;
    public GameObject Heart0, Heart1, Heart2;

    public bool hidden;

    private float resetSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 3;
        Heart0.gameObject.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth > 3)
            playerHealth = 3;

        switch (playerHealth)
        {
            case 3:
                Heart0.gameObject.SetActive(true);
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                break;

            case 2:
                Heart0.gameObject.SetActive(true);
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(false);
                break;

            case 1:
                Heart0.gameObject.SetActive(true);
                Heart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(false);
                break;

            case 0:
                Heart0.gameObject.SetActive(false);
                Heart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(false);
                break;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") * 0.42f;

        Vector3 movement = new Vector3(x, y).normalized * Time.deltaTime * speed;

        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = resetSpeed;
        }
        else
        {
            if (currentstamina >= 100)
            {
                currentstamina = 100;
            }
            else
            {
                GainStamina(3);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (currentstamina >= 0)
            {
                LoseStamina(15);
                hidden = true;
            }
            else
            {
                LoseStamina(0);
                speed = newSpeed;
                hidden = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = newSpeed;
        }
    }

    public void SetMaxStamina(float stamina)
    {
        Staminabar.value = stamina;
    }
    public void SetStamina(float stamina)
    {
        Staminabar.value = stamina;
    }

    private void LoseStamina(float LoseStamina)
    {
        currentstamina -= LoseStamina * Time.deltaTime;

        SetStamina(currentstamina);
    }

    private void GainStamina(float GainStamina)
    {
        currentstamina += GainStamina * Time.deltaTime;

        SetStamina(currentstamina);
    }
}
