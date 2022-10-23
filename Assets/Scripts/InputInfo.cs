using System;
using UnityEngine;

public class InputInfo : MonoBehaviour
{
    private Vector2 _firstMousePos;
    private Vector2 _diffMousePos;
    public Vector2 DiffMousePos
    {
        get
        {
            return _diffMousePos;
        }
        set
        {
            _diffMousePos = value;
        }
    }

    private bool _isPressed;
    public bool IsPressed
    {
        get
        {
            return _isPressed;
        }
        set
        {
            _isPressed = value;
        }
    }

    public static event Action onPressed;
    public static event Action onPressedUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _diffMousePos = Vector2.zero;
            _firstMousePos = Input.mousePosition;
            Press();
        }

        if (Input.GetMouseButton(0))
            _diffMousePos = (Vector2)Input.mousePosition - _firstMousePos;

        if (Input.GetMouseButtonUp(0))
            PressUp();
    }

    public float Longitudinal()
    {
        return Input.GetAxis("Vertical");
    }

    public float Lateral()
    {
        return Input.GetAxis("Horizontal");
    }

    public void Press()
    {
        _isPressed = true;
        if (onPressed != null)
            onPressed();
    }

    public void PressUp()
    {
        _isPressed = false;
        if (onPressedUp != null)
            onPressedUp();
    }
}