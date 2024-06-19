using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Include the TextMeshPro namespace
using UnityEngine.UI;  

public class Gyro : MonoBehaviour
{
    float y = 0;
    static float speed = 1.0f;
    int pause = 0;
    public TextMeshProUGUI cheatMessage;  // Reference to the TextMeshProUGUI component for the cheat message
    public TextMeshProUGUI threshold; 
    public int max =100;

    void Start()
    {
        Input.gyro.enabled = true;
        cheatMessage.fontSize = 0;  
         threshold.text = "Threshold: " + pause.ToString();   
    }

    void Update()
    {
        if (Time.timeScale == 0){
            pause = 0;
            return;   
        }

        float old = y;
        float newY = Input.gyro.attitude.eulerAngles.y;

        speed = Mathf.Abs(old - newY);
        Debug.Log(speed);

        if (speed > 1)
        {
            pause++;
            threshold.text = "Threshold: " + pause.ToString();   
        }

        if (pause >= max)
        {
            PauseGameAndShowMessage();
        }
        
        y = newY;
    }

    void PauseGameAndShowMessage()
    {
        cheatMessage.gameObject.SetActive(true);   
        cheatMessage.fontSize = 40;  
        Time.timeScale = 0;   
        // Debug.Log("Keep Your Device on a Stable Surface");  // Log an error message

        cheatMessage.text = "Keep your device on a stable surface or check for earthquakes";
        cheatMessage.color = Color.red;  
    }

    public void ResetThreshold()
    {
        // pause = 0;  // Reset the threshold value
        threshold.text = "Threshold: " + pause.ToString(); 
        cheatMessage.fontSize = 0;  
        Time.timeScale = 1;   
    }
}
