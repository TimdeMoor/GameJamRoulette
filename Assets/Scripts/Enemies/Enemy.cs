using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State{
        Wander,
        Chase,
        Attack
    }

    [SerializeField][Range(0f, 10f)]private float wanderRadius = 1f;
    [SerializeField] private Transform target;
    
    
    private State state = State.Wander;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
