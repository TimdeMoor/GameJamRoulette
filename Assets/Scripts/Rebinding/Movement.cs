using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private DefaultControls input;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    public float speed = 100f;

    private void Awake()
    {
        input = new DefaultControls();
        input.LoadBindingOverridesFromJson(PlayerPrefs.GetString("SavedControls"));
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.InGame.Move.performed += OnMovementPerformed;
        input.InGame.Move.canceled += OnMovementCanceled;

        input.InGame.Attack.performed += OnAttackPerformed;
        input.InGame.Interact.performed += OnInteractPerformed;
        input.InGame.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.InGame.Move.performed -= OnMovementPerformed;
        input.InGame.Move.canceled -= OnMovementCanceled;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        PopUpSpawner.instance.SpawnPopUp(transform.position, "Jump!");
    }
    
    private void OnAttackPerformed(InputAction.CallbackContext value)
    {
        PopUpSpawner.instance.SpawnPopUp(transform.position, "Attack!");
    }
    
    private void OnInteractPerformed(InputAction.CallbackContext value)
    {
        PopUpSpawner.instance.SpawnPopUp(transform.position, "Interact!");
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveDirection = Vector2.zero;
    }
}
