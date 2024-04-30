using System;
using System.Collections;
using System.Collections.Generic;
using Popups;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private DefaultControls input;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    public float walkSpeed = 100f;
    public float sprintSpeed = 250f;

    public bool isSprinting = false;

    [SerializeField] private float lightAttackCooldown = .3f;
    [SerializeField] private float heavyAttackCooldown = 1f;

    private float lightAttackCooldownTimer = 0f;
    private float heavyAttackCooldownTimer = 0f;
    
    private bool lightAttackReady => lightAttackCooldownTimer <= 0f;
    private bool heavyAttackReady => heavyAttackCooldownTimer <= 0f;
    
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

        input.InGame.LightAttack.performed += OnLightAttackPerformed;
        input.InGame.Interact.performed += OnInteractPerformed;
        input.InGame.Jump.performed += OnJumpPerformed;
        input.InGame.HeavyAttack.performed += OnHeavyAttackPerformed;
        
        input.InGame.Sprint.performed += OnSprintPerformed;
        input.InGame.Sprint.canceled += OnSprintCanceled;
        
        input.InGame.Pause.performed += OnPausePerformed;
    }

    private void Update()
    {
        lightAttackCooldownTimer -= Time.deltaTime;
        heavyAttackCooldownTimer -= Time.deltaTime;

        lightAttackCooldownTimer = Mathf.Clamp(lightAttackCooldownTimer, 0f, lightAttackCooldown);
        heavyAttackCooldownTimer = Mathf.Clamp(heavyAttackCooldownTimer, 0f, heavyAttackCooldown);
    }
    
    private void OnDisable()
    {
        input.Disable();
        input.InGame.Move.performed -= OnMovementPerformed;
        input.InGame.Move.canceled -= OnMovementCanceled;
    }

    private void FixedUpdate()
    {
        rb.velocity = isSprinting ? moveDirection * sprintSpeed : moveDirection * walkSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }
    
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        Debug.Log("OnJumpPerformed");
    }
    
    private void OnLightAttackPerformed(InputAction.CallbackContext value)
    {
        if (!lightAttackReady) return;
        lightAttackCooldownTimer = lightAttackCooldown;
        
        Debug.Log("OnLightAttackPerformed");
        CameraShaker.Instance.Shake(.2f, 10f);
    }
    
    private void OnInteractPerformed(InputAction.CallbackContext value)
    {
        Debug.Log("OnInteractPerformed");
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveDirection = Vector2.zero;
    }

    private void OnHeavyAttackPerformed(InputAction.CallbackContext value)
    {
        if (!heavyAttackReady) return;
        heavyAttackCooldownTimer = heavyAttackCooldown;
        
        Debug.Log("OnHeavyAttackPerformed");
        CameraShaker.Instance.Shake(.3f, 20f);

    }

    private void OnPausePerformed(InputAction.CallbackContext value)
    {
        Debug.Log("OnPausePerformed");
    }
    
    private void OnSprintPerformed(InputAction.CallbackContext value)
    {
        isSprinting = true;
    }
    
    private void OnSprintCanceled(InputAction.CallbackContext value)
    {
        isSprinting = false;
    }
}
