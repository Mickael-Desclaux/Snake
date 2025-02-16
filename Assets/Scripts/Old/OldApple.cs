using UnityEngine;
using Random = UnityEngine.Random;

namespace Old
{
    public class OldApple : MonoBehaviour
    {
        private int _positionXMax = 12;
        private int _positionYMax = 6;

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
}