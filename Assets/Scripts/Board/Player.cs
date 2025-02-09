using UnityEngine;

namespace Board
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Dice dice;
        [SerializeField] private float speed;

        public void StartRoll(System.Action<int> callback)
        {
            dice.StartRoll(callback);
        }

        public void StopRoll()
        {
            dice.StopRoll();
        }

        /// <summary>
        /// Move player towards the destination.
        /// Speed is based on <see cref="speed"/> and <see cref="Time.deltaTime"/>
        /// </summary>
        /// <param name="dest">Destination</param>
        /// <returns><c>true</c> if <see cref="Player"/> arrived at the Destination</returns>
        public bool Move(Vector3 dest)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);

            if (!(Vector3.Distance(transform.position, dest) < 0.001)) return false;

            transform.position = dest;

            return true;
        }
    }
}