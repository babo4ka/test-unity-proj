using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject [] cubes;

    private int right;

    private System.Random random = new System.Random();

    private int score = 0;
    private int best = 0;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text bestText;



    private void Start()
    {
        SetColors();
        UpdateScore();
        for(int i=0; i<cubes.Length; i++)
        {
            cubes[i].GetComponent<CubeScript>().check += checkRight;
        }
    }

    private void checkRight(int num)
    {
        if(right == num)
        {
            score++;
        }
        else
        {
            if(score > best) {
                best = score;
                UpdateBest();
            }
            score = 0;
        }

        SetColors();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateBest()
    {
        bestText.text = "best: " + best.ToString();
    }

    private void SetColors()
    {
        Color[] colors = generateColors();

        right = random.Next(0, 3);
        
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<SpriteRenderer>().color = i == right ? colors[1] : colors[0];
            cubes[i].GetComponent<CubeScript>().setCubeNum(i);
        }
    }


    
    private Color[] generateColors()
    {
        float[] rgbs = generateRGB();


        int id = rgbs.ToList().IndexOf(rgbs.Max());

        float[] argbs = new float[3];

        for(int i=0; i<rgbs.Length;i++)
        {
            argbs[i] = i == id ? (
                rgbs[i] < 127 ? rgbs[i] + 45 : rgbs[i]-45
                ) : rgbs[i];
        }


        return new Color[]{ new Color(rgbs[0]/255, rgbs[1]/255, rgbs[2]/255, 1),
                new Color(argbs[0]/255, argbs[1]/255, argbs[2]/255, 1)};
    }



    private float[] generateRGB()
    {
        return new float[] { random.Next(0, 256), random.Next(0, 256), random.Next(0, 256) };
    }
}
