using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Popups;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private DefaultControls input;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    public float walkSpeed = 100f;
    public float sprintSpeed = 250f;

    public bool isSprinting = false;

    [SerializeField] private float AttackCooldown = .3f;
    [SerializeField] private float comboCooldown = 3f;
    
    
    private float AttackCooldownTimer = 0f;
    private float comboCooldownTimer = 0f;

    private bool AttackReady => AttackCooldownTimer <= 0f;

    private string currentCombo = "";
    private bool comboMode = false;

    private Animator animator;
    private int attackCounter = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
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
        AttackCooldownTimer -= Time.deltaTime;
        comboCooldownTimer -= Time.deltaTime;

        AttackCooldownTimer = Mathf.Clamp(AttackCooldownTimer, 0f, AttackCooldown);
        comboCooldownTimer = Mathf.Clamp(comboCooldownTimer, 0f, comboCooldown);

        if (comboCooldownTimer <= 0f && comboMode)
        {
            ResetCombo();
        }
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
        AddCombo("J");
        CheckCombo();
    }
    
    private void OnLightAttackPerformed(InputAction.CallbackContext value)
    {
        if (!AttackReady) return;
        AttackCooldownTimer = AttackCooldown;

        attackCounter++;
        //animator.SetInteger("punch", attackCounter);
        
        AddCombo("L");
        CheckCombo();
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
        if (!AttackReady) return;
        AttackCooldownTimer = AttackCooldown;

        AddCombo("H");
        CheckCombo();
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

    private void CheckCombo()
    {
        if (currentCombo.Length > 3)
        {
            currentCombo = currentCombo.Substring(currentCombo.Length - 3);
        }
        switch (currentCombo)
        {
            case "LLH":
                CameraShaker.Instance.Shake();
                PopUpSpawner.instance.SpawnPopUp(transform.position, "QuickSmash!");
                ResetCombo();
                break;
            case "LLL":
                CameraShaker.Instance.Shake();
                PopUpSpawner.instance.SpawnPopUp(transform.position, "TripleStrike!");
                ResetCombo();
                break;
            case "JJL":
                CameraShaker.Instance.Shake();
                PopUpSpawner.instance.SpawnPopUp(transform.position, "JumpKick!");
                ResetCombo();
                break;
            default: 
                Debug.Log("No Combo");
                break;
        }
    }

    private void AddCombo(string comboType)
    {
        comboMode = true;
        currentCombo += comboType;
        comboCooldownTimer = comboCooldown;
    }
    private void ResetCombo()
    {
        comboMode = false;
        currentCombo = "";
        comboCooldownTimer = 0f;
        attackCounter = 0;
        Debug.Log("ComboReset");
    }
}
