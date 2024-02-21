using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    private int cubeNum;

    public delegate void CheckRight(int num);

    public CheckRight check;

    public void setCubeNum(int num)
    {
        cubeNum = num;
    }

    public void OnMouseUp()
    {
        check?.Invoke(cubeNum);
    }
}
