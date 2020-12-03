using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    int energyCount;
    int powerUp = 1;

    float speed = 10;

    public Animator PlayerAnim;
    public Text EnergyCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GMController.gmInstance.levelTime > 0)
        {
            // Player Move Forward
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                PlayerAnim.SetBool("isMoving", true);
            }
            // Player Move Backward
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                PlayerAnim.SetBool("isMoving", true);
            }
            // Player Move Right
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                PlayerAnim.SetBool("isMoving", true);
            }
            // Player Move Left
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                PlayerAnim.SetBool("isMoving", true);
            }
            // Player Idle State
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                PlayerAnim.SetBool("isMoving", false);
            }
            // Player Uses PowerUp
            if (Input.GetKeyDown(KeyCode.Space) && powerUp == 1)
            {
                GMController.gmInstance.DestoryMinusEnergy();
                powerUp = 0;
            }
        }
        
        // Win Scene
        if (energyCount > 50)
        {
            GMController.gmInstance.WinScene();
        }

        // Lose Scene
        if (energyCount < 0)
        {
            GMController.gmInstance.LoseScene();
        }
        else if (GMController.gmInstance.levelTime < 0)
        {
            GMController.gmInstance.LoseScene();
        }
        else if (transform.position.y < -5)
        {
            GMController.gmInstance.LoseScene();
        }

        // Energy Count UI
        EnergyCount.GetComponent<Text>().text = "Energy Count: " + energyCount;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // If Collision With AddEnergy
        if (collision.gameObject.CompareTag("AddEnergy"))
        {
            energyCount += 5;
            Destroy(collision.gameObject);
            GMController.gmInstance.AddTime();
        }
        // If Collision With MinusEnergy
        if (collision.gameObject.CompareTag("MinusEnergy"))
        {
            energyCount -= 25;
            Destroy(collision.gameObject);
        }
    }
}