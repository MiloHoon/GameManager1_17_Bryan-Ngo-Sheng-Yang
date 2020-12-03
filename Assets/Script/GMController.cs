using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class GMController : MonoBehaviour
{
    public static GMController gmInstance;

    //public GameObject addEnergyPrefab;
    //public GameObject minusEnergyPrefab;

    GameObject[] MinusEnergyCount;
    public GameObject[] EnergyCubePrefab;
    public Text Timer;

    public int numberOfSpawn;
    public float levelTime;

    int RandomPos;

    // Start is called before the first frame update
    void Start()
    {
        if (gmInstance == null)
        {
            gmInstance = this;
        }
        /*
        // Instantate addEnergyPrefab At Random Position //
        for (int i = 0; i < numberOfSpawn; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));

            if (Random.Range(0, 2) < 1)
            {
                Instantiate(addEnergyPrefab, randomPos, Quaternion.identity);
            }
            else
            {
                Instantiate(minusEnergyPrefab, randomPos, Quaternion.identity);
            }
        }
        */
        int Size = 10;
        while (Size > 0)
        {
            RandomPos = Random.Range(0, EnergyCubePrefab.Length);

            // Total Random size
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));

            // Create Object
            Instantiate(EnergyCubePrefab[RandomPos], randomPos, Quaternion.identity);

            Size--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check If There Is Still Time Left //
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            //print("LevelTime: " + levelTime);
            //print("LevelTime: " + FormatTime(levelTime));
            Timer.GetComponent<Text>().text = "Time Left: " + FormatTime(levelTime);
        }
        else
        {
            levelTime = 0;
            //print("Times Up!");
            Timer.GetComponent<Text>().text = "Time Left: " + "Times Up!";
        }

        // Count Total MinusEnergy
        MinusEnergyCount = GameObject.FindGameObjectsWithTag("MinusEnergy");
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void AddTime()
    {
        levelTime += 5;
        int spawn = 4;
        while (spawn > 0)
        {
            RandomPos = Random.Range(0, EnergyCubePrefab.Length);

            //Total Random size
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));

            //Create Object
            Instantiate(EnergyCubePrefab[RandomPos], randomPos, Quaternion.identity);

            spawn--;
        }
    }

    public void DestoryMinusEnergy()
    {
        for (int i = 0; i < MinusEnergyCount.Length; i++)
        {
            Destroy(MinusEnergyCount[i]);
        }
    }

    public void WinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }
}