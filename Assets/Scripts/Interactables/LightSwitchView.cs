using System;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate(); // Signature
    public static event LightSwitchDelegate lightSwitch; // instance

    private void OnEnable() => lightSwitch += OnLightSwitchToggler;

    private void OnDisable() => lightSwitch -= OnLightSwitchToggler;

    private void Start() => currentState = SwitchState.Off;

    public void Interact() => lightSwitch?.Invoke();

    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void OnLightSwitchToggler()
    {
        // Custom Logic for Light Switch
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}