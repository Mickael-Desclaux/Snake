using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Apple : MonoBehaviour
{
    private int _positionXMax = 10;
    private int _positionYMax = 10;

    private void Start()
    {
        int randomX = Random.Range(-_positionXMax, _positionXMax);
        int randomY = Random.Range(-_positionYMax, _positionYMax);

        transform.position = new Vector3(randomX, randomY, 0);
    }

    public void UpdatePosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3 lastPosition = currentPosition;

        while (lastPosition == currentPosition)
        {
            int randomX = Random.Range(-_positionXMax, _positionXMax);
            int randomY = Random.Range(-_positionYMax, _positionYMax);

            currentPosition = new Vector3(randomX, randomY, 0);
        }

        transform.position = currentPosition;
    }
}