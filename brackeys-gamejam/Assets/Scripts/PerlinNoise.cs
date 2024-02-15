using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] internal int width = 256;
    [SerializeField] internal int height = 256;

    [SerializeField] internal float offsetX = 100f;
    [SerializeField] internal float offsetY = 100f;

    [SerializeField] internal float scale = 20f;


    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    internal Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    internal Color CalculateColor(int x, int y)
    {
        float xPerlinCoord = (float)x / width * scale + offsetX;
        float yPerlinCoord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xPerlinCoord, yPerlinCoord);
        return new Color(sample, sample, sample);
    }
}