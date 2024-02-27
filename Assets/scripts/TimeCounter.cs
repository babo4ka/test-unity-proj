using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CubeScript;

public class TimeCounter : MonoBehaviour
{

    [SerializeField]
    private TMP_Text highest;
    [SerializeField]
    private TMP_Text average;
    [SerializeField]
    private TMP_Text self;

    [SerializeField]
    private GameObject[] cubes;

    private float highestTime = 0.0f;
    private float averageTime = 0.0f;
    private List<float> times = new List<float>();

    private float startTime = 0.0f;

    void Start()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<CubeScript>().clicked += CubeClicked;
        }
    }

    


    private void CubeClicked()
    {
        float delta = Time.time - startTime;
        times.Add(delta);

        if (delta > highestTime)
        {
            highestTime = delta;
            highest.text = "Highest time: " + delta.ToString("0.00");
        }

        average.text = "Average time: " +
            times.Sum(s => Convert.ToDouble(s) / times.Count).ToString("0.00");

        startTime = Time.time;

    }


    void Update()
    {
        self.text = (Time.time - startTime).ToString("0.00");
    }
}
