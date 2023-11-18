using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    public int sum=0;
    public TextMeshProUGUI text;
    void Start()
    {
        // Start invoking the GenerateRandomNumber method every 5 seconds.
        InvokeRepeating("GenerateRandomNumber", 0f, 5f);
    }

    void GenerateRandomNumber()
    {
        // Generate a random number between 1 and 10 (inclusive).
        int randomNumber = Random.Range(1, 11);
        sum = sum + randomNumber;
        text.text = sum.ToString() + " items";
    }
}
