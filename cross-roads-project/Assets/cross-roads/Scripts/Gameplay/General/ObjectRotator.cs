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
        public Vector3 axis = Vector3.up;

        private Coroutine rotating;

        private void Start()
        {
            axis.x = (axis.x == 0) ? 0 : 1;
            axis.y = (axis.y == 0) ? 0 : 1;
            axis.z = (axis.z == 0) ? 0 : 1;
        }

        public void RotateObject(int direction)
        {
            //Sanity check ensuring it is either 1, 0, or -1
            direction = (direction > 0) ? 1 : direction;
            direction = (direction < 0) ? -1 : direction;

            Vector3 rot = this.transform.rotation.eulerAngles;
            Vector3 targetRotation = new Vector3 (axis.x * direction * (rot.x + rotateInterval),
                                                  axis.y * direction * (rot.y + rotateInterval),
                                                  axis.z * direction * (rot.y + rotateInterval));

            if (rotating == null)
                rotating = StartCoroutine(LerpRotation(targetRotation, speed));
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
