using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float AbilityStamina = 100;
    public Slider Staminabar;
    public float currentstamina;
    public bool hidden;
    public float offset = 2;
    private float angle = 360;
    

    

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         float x = Input.GetAxisRaw("Horizontal");
         float y = Input.GetAxisRaw("Vertical");

         Vector3 movement = new Vector3(x, y).normalized * Time.deltaTime * speed;

        transform.Translate(movement); angle += offset;
         /*
          if (Input.GetKey(KeyCode.W))
          {
              transform.Translate(Vector3.up / offset * speed * Time.deltaTime);
          }

          if (Input.GetKey(KeyCode.A))
          {
              transform.Translate(Vector3.left / offset * speed * Time.deltaTime);
          }

          if (Input.GetKey(KeyCode.S))
          {
              transform.Translate(Vector3.down / offset * speed * Time.deltaTime);
          }

          if (Input.GetKey(KeyCode.D))
          {
              transform.Translate(Vector3.right / offset * speed * Time.deltaTime);
          }
          */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = 0;

        }

        else {
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
                speed = 25;
                hidden = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            speed = 25;
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
