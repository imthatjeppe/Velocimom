using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float currentstamina;

    public static int playerHealth;

    public Slider Staminabar;
    public GameObject Heart0, Heart1, Heart2;

    public bool hidden;

    private float resetSpeed = 0;
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
        Health();
        GameOver();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical") * 0.5f;
        Vector3 movement = new Vector3(x, y).normalized * Time.deltaTime * speed;
        transform.Translate(movement);
        SpaceAbility();
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
    private void SpaceAbility()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            speed = resetSpeed;
            
    }
        else{
            GainStamina(2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            LoseStamina(10);
            
            if (currentstamina >= 0)
            {
                hidden = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = maxSpeed;
            hidden = false;
            LoseStamina(200);
        }
    }
}