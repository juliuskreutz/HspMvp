using System.Collections;
using TMPro;
using UnityEngine;

namespace Board
{
    // Make sure, that the dice has this component
    [RequireComponent(typeof(TextMeshPro))]
    public class Dice : MonoBehaviour
    {
        /// <value>Represents the dice roll interval in seconds.</value>
        [SerializeField] private float interval;

        private TextMeshPro _rollText;
        private bool _rolling;

        private void Awake()
        {
            _rollText = GetComponent<TextMeshPro>();
        }

        /// <summary>
        /// Start a die roll.
        /// </summary>
        /// <param name="callback">Gets called after the roll stops and calls the callback with the roll result.</param>
        public void StartRoll(System.Action<int> callback)
        {
            if (_rolling) return;

            _rolling = true;

            // https://docs.unity3d.com/6000.0/Documentation/Manual/coroutines.html
            StartCoroutine(DoRoll(callback));
        }

        public void StopRoll()
        {
            _rolling = false;
        }

        /// <summary>
        /// Rolls with the <see cref="interval"/> until <see cref="_rolling"/> is set to false.
        /// </summary>
        /// <param name="callback">Gets called after the roll stops and calls the callback with the roll result.</param>
        private IEnumerator DoRoll(System.Action<int> callback)
        {
            var i = 0;

            while (_rolling)
            {
                // i = [1, 3]
                i %= 3;
                i += 1;

                _rollText.text = i.ToString();

                yield return new WaitForSeconds(interval);
            }
            
            // Roll has stopped so we call the callback with the result
            callback(i);
        }
    }
}