using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Very basic implementation of a singleton
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // Subscribable action
    public Action OnSpace;

    private InputAction _space;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        _space = InputSystem.actions.FindAction("Space");
    }

    private void Update()
    {
        if (_space.triggered) OnSpace.Invoke();
    }
}
