using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Very basic implementation of a singleton
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public InputAction SpaceAction  { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        SpaceAction = InputSystem.actions.FindAction("Space");
    }
}
