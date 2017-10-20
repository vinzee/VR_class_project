using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create3DTex : MonoBehaviour
{
    public Texture3D volumeTex;
    public int size = 32;

	// Use this for initialization
	void Start ()
    {
        volumeTex = new Texture3D(size, size, size, TextureFormat.ARGB32, true);

        var colors = new Color[size * size * size];
        float mul = 1.0f / (size - 1);                      // I guess this is a scalar?
        int colorArrayIndex = 0;

        Color currentColor = Color.white;

        for (int z = 0; z < size; z++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    currentColor.r = x * mul;
                    currentColor.g = y * mul;
                    currentColor.b = z * mul;

                    colors[colorArrayIndex] = currentColor;
                    colorArrayIndex++;
                }
            }
        }

        volumeTex.SetPixels(colors);
        volumeTex.Apply();
        Renderer thisThing = GetComponent<Renderer>();
        thisThing.material.SetTexture("_Volume", volumeTex);
	}

}
