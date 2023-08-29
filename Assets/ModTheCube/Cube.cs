using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private Material material;

    public int interpolationFramesCount = 1000; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;


    [Range(0.0f, 100.0f)]
    public float positionChangeMax = 15f;
    private float positionChangeMin = 0f;
    Vector3 finalPosition = new Vector3(0, 0, 0);
    Vector3 currentPosition = new Vector3(0, 0, 0);



    [Range(0.0f, 30.0f)]
    public float sizeChangeMax = 5f;
    private float sizeChangeMin = 1f;
    Vector3 finalSize = new Vector3(1, 1, 1);
    Vector3 currentSize = new Vector3(1, 1, 1);


    [Range(0.0f, 100.0f)]
    public float rotationSpeedMax = 30f;
    float rotationSpeedMin = 1f;
    float rotationSpeedX, rotationSpeedY, rotationSpeedZ;

    
    float currentRed = 0f, currentGreen = 0f, currentBlue = 0f, currentAlpha = 0f;
    float finalRed = 1f, finalGreen = 1f, finalBlue = 1f, finalAlpha = 1f;


    void Start()
    {
        material = Renderer.material;
    }

    void Update()
    {
        if (elapsedFrames == 0)
        {

            interpolationFramesCount = Random.Range(500, 2000);

            currentPosition = new Vector3(0, 0, 0);
            finalPosition = new Vector3(Random.Range(positionChangeMin, positionChangeMax), Random.Range(positionChangeMin, positionChangeMax), Random.Range(positionChangeMin, positionChangeMax));

            currentSize = finalSize;
            finalSize = new Vector3(Random.Range(sizeChangeMin, sizeChangeMax), Random.Range(sizeChangeMin, sizeChangeMax), Random.Range(sizeChangeMin, sizeChangeMax));

            rotationSpeedX = Random.Range(rotationSpeedMin, rotationSpeedMax);
            rotationSpeedY = Random.Range(rotationSpeedMin,rotationSpeedMax);
            rotationSpeedZ = Random.Range(rotationSpeedMin, rotationSpeedMax);

            currentRed = finalRed;
            currentGreen = finalGreen;
            currentBlue = finalBlue;
            currentAlpha = finalAlpha;

            finalRed = Random.Range(0f, 1f);
            finalGreen = Random.Range(0f, 1f);
            finalBlue = Random.Range(0f, 1f);
            finalAlpha = Random.Range(0.5f, 1f);

        }

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        Vector3 interpolatedPosition = Vector3.Lerp(currentPosition, finalPosition, interpolationRatio);
        transform.position = interpolatedPosition;


        Vector3 interpolatedSize = Vector3.Lerp(currentSize, finalSize, interpolationRatio);
        transform.localScale = interpolatedSize;


        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);


        float red = Mathf.Lerp(currentRed, finalRed, interpolationRatio),
            green = Mathf.Lerp(currentGreen, finalGreen, interpolationRatio),
            blue = Mathf.Lerp(currentBlue, finalBlue, interpolationRatio),
            alpha = Mathf.Lerp(currentAlpha, finalAlpha, interpolationRatio);

            Color materialColor = new(red, green, blue, alpha);

        material.color = materialColor;

    }
}