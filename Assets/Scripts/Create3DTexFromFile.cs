using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create3DTexFromFile : MonoBehaviour
{
    public Texture3D volumeTex;
    public int xsize = 32, ysize = 32, zsize = 32;
    public string filePath;

    // Use this for initialization
    void Start()
    {
        volumeTex = new Texture3D(xsize, xsize, xsize, TextureFormat.ARGB32, false);

        byte[] byteArray = System.IO.File.ReadAllBytes(filePath);


        if (byteArray.Length != 0)
            Debug.Log("Byte data read in successfully. Bytes read: " + byteArray.Length);

        var colors = new Color[xsize * xsize * xsize];
        float xmul = 1.0f / (xsize - 1);
        float ymul = 1.0f / (ysize - 1);
        float zmul = 1.0f / (zsize - 1);

        int colorArrayIndex = 0;

        Color currentColor = Color.white;

        for (int z = 0; z < zsize; z++)
        {
            for (int y = 0; y < ysize; y++)
            {
                for (int x = 0; x < xsize; x++)
                {
                    currentColor.a = (byteArray[colorArrayIndex] / 255.0f);

                    currentColor.r = xmul * x * currentColor.a;
                    currentColor.g = ymul * y * currentColor.a;
                    currentColor.b = zmul * z * currentColor.a;

                    //currentColor.a = .5f;
                    //currentColor.a *= 0.5f;

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
