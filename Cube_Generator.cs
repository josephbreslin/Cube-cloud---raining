using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Generator : MonoBehaviour
{
    GameObject[,,] cubeMatrix;
    Renderer[,,] cubeMatrixRenderer;
    int[,] rainAltitude;
    bool isRain = false;
    float timer = 0;

    public Color clear, white;
    public float scale = .4f;
    public int size;  
    public Material mat;
    
    void Start()
    {
        Generate3DMatrix(size, size, size);
    }

    void Update()
    {
        Rain();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRain = !isRain;
        }
    }

    void Generate3DMatrix(int height, int width, int length)
    {
        cubeMatrix = new GameObject[height, width, length];
        cubeMatrixRenderer = new Renderer[height, width, length];
        rainAltitude = new int[width, length];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < length; k++)
                {
                    cubeMatrix[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeMatrix[i, j, k].transform.position = new Vector3(i, j, k);
                    cubeMatrix[i, j, k].transform.localScale = Vector3.one * scale;
                    cubeMatrixRenderer[i, j, k] = cubeMatrix[i, j, k].GetComponent<Renderer>();
                    cubeMatrixRenderer[i, j, k].material = mat;
                    rainAltitude[i, k] = Mathf.FloorToInt(Random.Range(0f, (size + .99f)));
                }
            }
        }
    }

    void Rain()
    {
        timer += Time.deltaTime;
        if (timer > .1f)
        {
            for (int x = 0; x < size; x++)
            {
                for (int z = 0; z < size; z++)
                {
                    int y = rainAltitude[x, z];
                    if (y < 0)
                    {
                        rainAltitude[x, z] = size;
                    }
                    else
                    {
                        rainAltitude[x, z] = y - 1;
                        y = rainAltitude[x, z];
                    }      
                    for(int i = 0; i <size; i++)
                    {
                        if(i != y)
                        {
                            cubeMatrixRenderer[x, i, z].material.color = isRain == true? clear: white;

                        }
                        else
                        {
                            cubeMatrixRenderer[x, i, z].material.color = white;
                        }
                    }
                }
            }
            timer = 0;
        }
    }
}

