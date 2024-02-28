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
    private GameObject [] fourCubes;
    [SerializeField]
    private GameObject[] nineCubes;

    private int right;

    private System.Random random = new System.Random();

    private int score = 0;
    private int best = 0;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text bestText;

    private enum CubesCount
    {
        FOUR, NINE
    }

    private CubesCount cubesCount = CubesCount.FOUR;

    [SerializeField]
    private GameObject fourSquaresWrapper;
    [SerializeField]
    private GameObject nineSquaresWrapper;


    private void Start()
    {
        SetColors(cubesCount);
        UpdateScore();
        for(int i=0; i< fourCubes.Length; i++)
        {
            fourCubes[i].GetComponent<CubeScript>().check += checkRight;
            fourCubes[i].GetComponent<CubeScript>().setCubeNum(i);
        }

        for(int i=0; i< nineCubes.Length; i++)
        {
            nineCubes[i].GetComponent<CubeScript>().check += checkRight;
            nineCubes[i].GetComponent<CubeScript>().setCubeNum(i);
        }
    }


    private void checkRight(int num)
    {
        if(right == num)
        {
            score++;

            if(score > 4)
            {
                cubesCount = CubesCount.NINE;

                fourSquaresWrapper.SetActive(false);
                nineSquaresWrapper.SetActive(true);
            }
        }
        else
        {
            if(score > best) {
                best = score;
                UpdateBest();
            }
            score = 0;
            cubesCount = CubesCount.FOUR;

            fourSquaresWrapper.SetActive(true);
            nineSquaresWrapper.SetActive(false);
        }

        SetColors(cubesCount);
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

 

    private void SetColors(CubesCount cc)
    {
        Color[] colors = generateColors();

        

        switch (cc)
        {
            case CubesCount.FOUR:
                right = random.Next(0, 3);
                for (int i = 0; i < fourCubes.Length; i++)
                {
                    fourCubes[i].GetComponent<SpriteRenderer>().color = i == right ? colors[1] : colors[0];
                }
            break;

            case CubesCount.NINE:
                right = random.Next(0, 8);
                for (int i = 0; i < nineCubes.Length; i++)
                {
                    nineCubes[i].GetComponent<SpriteRenderer>().color = i == right ? colors[1] : colors[0];
                }
            break;
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
