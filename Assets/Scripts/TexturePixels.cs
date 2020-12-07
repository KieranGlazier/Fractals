using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePixels : MonoBehaviour
{
    private Color[] colorArray = new Color[16];
    public int imageHeight = 256;
    public int imageWidth = 256;
    private float minReal = -2.0f;
    private float maxReal = 1.0f;
    private float minIm = -1.2f;
    private float maxIm;

    public int maxIterations = 1000;
    private int maxColor = 255 * 255 * 255;

    // Start is called before the first frame update
    void Start()
    {
        maxIm = minIm + (maxReal - minReal) * imageHeight / imageWidth;
        float realFactor = (maxReal - minReal) / (imageWidth - 1);
        float imFactor = (maxIm - minIm) / (imageHeight - 1);

        Texture2D texture = new Texture2D(imageHeight, imageWidth, TextureFormat.RGBA32,false);
        GetComponent<Renderer>().material.mainTexture = texture;

        int colorDivisions = maxColor / maxIterations;

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


        for (int y0 = 0; y0 < texture.height; y0++)
        {
            for (int x0 = 0; x0 < texture.width; x0++)
            {
                float x = 0;
                float y = 0;
                int iteration = 0;

                float xC = minReal + x0 * realFactor;
                float yC = maxIm - y0 * imFactor;

                float y2 = 0;
                float x2 = 0;
                                
                while ((x2 + y2) <= 4 && iteration < maxIterations)
                {
                    y = 2 * x * y + yC;
                    x = x2 - y2 + xC;
                    x2 = x * x;
                    y2 = y * y;
                    iteration++;
                }
                
                
                if (iteration < maxIterations && iteration > 0)
                {
                    int colorInt = iteration * colorDivisions;
                    Color32 color = new Color32
                    {
                        b = (byte)((colorInt & 0xFF)),
                        g = (byte)((colorInt >> 8 & 0xFF)),
                        r = (byte)((colorInt >> 16 & 0xFF)),
                        a = 1//(byte)((colorInt >> 24 & 0xFF))
                    };
                    int i = iteration % 16;
                    texture.SetPixel(x0, y0, colorArray[i]);
                    //texture.SetPixel(x0, y0, color);
                    //Debug.Log(color.ToString());
                } else
                {
                    texture.SetPixel(x0, y0, Color.black);
                }
            }
        }
        texture.Apply();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
