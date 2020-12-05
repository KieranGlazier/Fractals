using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePixels : MonoBehaviour
{
    private Color[] colorArray = new Color[16];
    private int imageHeight = 256;
    private int imageWidth = 256;
    private float minReal = -2.0f;
    private float maxReal = 1.0f;
    private float minIm = -1.2f;
    private float maxIm;

    // Start is called before the first frame update
    void Start()
    {
        maxIm = minIm + (maxReal - minReal) * imageHeight / imageWidth;
        float realFactor = (maxReal - minReal) / (imageWidth - 1);
        float imFactor = (maxIm - minIm) / (imageHeight - 1);

        Texture2D texture = new Texture2D(imageHeight, imageWidth);
        GetComponent<Renderer>().material.mainTexture = texture;


        colorArray[0] = new Color(0.25f, 0.12f, 0.06f);
        colorArray[1] = new Color(0.10f, 0.03f, 0.10f);
        colorArray[2] = new Color(0.04f, 0f, 0.18f);
        colorArray[3] = new Color(0.02f, 0.02f, 0.29f);
        colorArray[4] = new Color(0.00f, 0.03f, 0.39f);
        colorArray[5] = new Color(0.05f, 0.17f, 0.54f);
        colorArray[6] = new Color(0.09f, 0.32f, 0.69f);
        colorArray[7] = new Color(0.22f, 0.49f, 0.82f);
        colorArray[8] = new Color(0.52f, 0.71f, 0.89f);
        colorArray[9] = new Color(0.82f, 0.92f, 0.97f);
        colorArray[10] = new Color(0.94f, 0.91f, 0.75f);
        colorArray[11] = new Color(0.97f, 0.79f, 0.37f);
        colorArray[12] = new Color(1.00f, 0.66f, 0.00f);
        colorArray[13] = new Color(0.80f, 0.50f, 0.00f);
        colorArray[14] = new Color(0.60f, 0.34f, 0.00f);
        colorArray[15] = new Color(0.41f, 0.20f, 0.01f);

        /*
        for (int y  = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = ((x & y) != 0 ? Color.white : Color.gray);
                texture.SetPixel(x, y, color);
            }
        }
        */

        for (int y0 = 0; y0 < texture.height; y0++)
        {
            for (int x0 = 0; x0 < texture.width; x0++)
            {
                float x = 0;
                float y = 0;
                int iteration = 0;
                int max_iterations = 1000;

                float xC = minReal + x0 * realFactor;
                float yC = maxIm - y0 * imFactor;

                while (x * x + y * y <= 2 * 2 && iteration < max_iterations)
                {
                    float xTemp = x * x - y * y + xC;
                    y = 2 * x * y + yC;
                    x = xTemp;
                    iteration++;
                }
                if (iteration < max_iterations && iteration > 0)
                {
                    int i = iteration % 16;
                    texture.SetPixel(x0, y0, colorArray[i]);
                } else
                {
                    texture.SetPixel(x0, y0, Color.black);
                }
            }
        }
        texture.Apply();
        /*
        foreach (Color pixel in texture.GetPixels(0,0,128,128))
        {
            
        }
                */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
