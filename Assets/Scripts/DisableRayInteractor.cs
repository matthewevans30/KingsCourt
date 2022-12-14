using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableRayInteractor : MonoBehaviour
{
    public InputActionReference toggleReference;

    [SerializeField]
    private XRRayInteractor interactor;
    [SerializeField]
    private LineRenderer lineRenderer;

    private void Start()
    {
        toggleReference.action.started += ToggleRayInteractor;
    }

    private void ToggleRayInteractor(InputAction.CallbackContext context)
    {
        bool isActive = !interactor.enabled;

        interactor.enabled = isActive;
        lineRenderer.enabled = isActive; 
    }
}
