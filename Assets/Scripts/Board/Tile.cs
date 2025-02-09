using UnityEngine;

namespace Board
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] public Tile next;

        // Safe to ignore. Draws the lines in the inspector
        private void OnDrawGizmos()
        {
            if (!next) return;
            
            const float arrowHeadAngle = 20.0f;
            const float arrowHeadLength = 0.25f;

            Gizmos.color = Color.red;

            var direction = (next.transform.position - gameObject.transform.position);
            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) *
                        new Vector3(0, 0, 1);
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) *
                       new Vector3(0, 0, 1);

            Gizmos.DrawRay(gameObject.transform.position, direction);
            Gizmos.DrawRay(next.transform.position, right * arrowHeadLength);
            Gizmos.DrawRay(next.transform.position, left * arrowHeadLength);
        }
    }
}