using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset m_InputActionAsset;

    public event UnityAction<Vector2> DeformEvent;

    private InputAction m_DeformAction;

    private void OnEnable()
    {
        m_DeformAction = m_InputActionAsset.FindAction("Deform");

        m_DeformAction.started += OnClick;
        m_DeformAction.performed += OnClick;
        m_DeformAction.canceled += OnClick;

        m_DeformAction.Enable();
    }

    private void OnDisable()
    {
        m_DeformAction.started -= OnClick;
        m_DeformAction.performed -= OnClick;
        m_DeformAction.canceled -= OnClick;

        m_DeformAction.Disable();
    }
    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 MousePosition = Mouse.current.position.ReadValue();
            DeformEvent?.Invoke(MousePosition);
        }
    }
}
