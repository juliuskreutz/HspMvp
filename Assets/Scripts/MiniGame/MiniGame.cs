using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MiniGame
{
    public class MiniGame : MonoBehaviour
    {
        [SerializeField] private TextMeshPro infoText;
        [SerializeField] private TextMeshPro counterText;

        private double _counter;
        private bool _running;

        private void OnEnable()
        {
            // Bind space bar action
            InputManager.Instance.SpaceAction.performed += OnSpace;
        }

        private void OnDisable()
        {
            // Unbind space bar action
            InputManager.Instance.SpaceAction.performed -= OnSpace;
        }

        private void Update()
        {
            if (!_running) return;

            _counter += Time.deltaTime;

            counterText.text = _counter <= 3 ? Math.Round(_counter, 1).ToString("0.0") : "";
        }

        private void OnSpace(InputAction.CallbackContext _)
        {
            switch (_running)
            {
                // Crude way to check, if the player wants to start the counter :D
                case false when _counter == 0:
                    infoText.text = "";
                    _running = true;
                    return;
                case false:
                    SceneManager.LoadScene("BoardScene");
                    break;
            }

            // MiniGame is running and player pressed space => Stop MiniGame and show result
            _running = false;
            infoText.text = $"You pressed at {_counter}\n<space> to continue";
        }
    }
}