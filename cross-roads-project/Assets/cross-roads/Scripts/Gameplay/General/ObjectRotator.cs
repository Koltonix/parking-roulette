using System.Collections;
using UnityEngine;

namespace Roads.Utilities
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.25f;
        [SerializeField]
        private float rotateInterval = 45.0f;
        private Vector3 currentRotation = Vector3.zero;
        public Vector3 axis = Vector3.up;

        private Coroutine rotating;

        private void Start()
        {
            currentRotation = this.transform.rotation.eulerAngles;

            axis.x = (axis.x == 0) ? 0 : 1;
            axis.y = (axis.y == 0) ? 0 : 1;
            axis.z = (axis.z == 0) ? 0 : 1;
        }

        public void RotateObject(int direction)
        {
            if (rotating != null)
                return;

            //Sanity check ensuring it is either 1, 0, or -1
            direction = (direction > 0) ? 1 : direction;
            direction = (direction < 0) ? -1 : direction;

            Vector3 rot = currentRotation;
            currentRotation = new Vector3 (axis.x * direction * (rot.x + rotateInterval),
                                           axis.y * direction * (rot.y + rotateInterval),
                                           axis.z * direction * (rot.y + rotateInterval));
            ClampRotation(direction);
            rotating = StartCoroutine(LerpRotation(currentRotation, speed));
        }

        private void ClampRotation(int direction)
        {
            Debug.Log(currentRotation.y);
            if (direction < 0.0f && currentRotation.y == -45)
            {
                currentRotation.y = 315;
            }

            else if (direction > 0.0f)
            {
                Debug.Log("forwards");
                //Positive - above 360 -> 0
                currentRotation.x = (currentRotation.x > 360) ? 0 + (currentRotation.x - 360) : currentRotation.x;
                currentRotation.y = (currentRotation.y > 360) ? 0 + (currentRotation.y - 360) : currentRotation.y;
                currentRotation.z = (currentRotation.z > 360) ? 0 + (currentRotation.z - 360) : currentRotation.z;
            }
            
            else if (direction < 0.0f)
            {
                Debug.Log("backwards");
                //Negative - below 0 -> 360
                //currentRotation.x = (currentRotation.x < 0) ? 360 - 45: currentRotation.x;
                currentRotation.y = (currentRotation.y < 0) ? 360 : currentRotation.y;
                //currentRotation.z = (currentRotation.z < 0) ? 360 - 45: currentRotation.z;
            }
        }

        private IEnumerator LerpRotation(Vector3 targetRotation, float speed)
        {
            Quaternion originalRotation = this.transform.rotation;

            float t = 0.0f;
            while (t <= 1.0f)
            {
                t += Time.deltaTime * speed;
                this.transform.rotation = Quaternion.Lerp(originalRotation, Quaternion.Euler(targetRotation), t);

                yield return new WaitForEndOfFrame();
            }

            rotating = null;
        }
    }
}
