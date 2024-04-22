using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveLoadRebinds : MonoBehaviour
{
    [SerializeField] public InputActionAsset _inputActions;

    public void OnEnable()
    {
        _inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("SavedControls"));
    }

    public void OnDisable()
    {
        PlayerPrefs.SetString("SavedControls", _inputActions.SaveBindingOverridesAsJson());
    }
}
