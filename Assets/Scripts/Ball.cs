using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float normalModeVelocityMultiplier = 0.01f;
    public float hardModeVelocityMultiplier = 0.1f;
    public float normalModeMaxVelocity = 3f;
    public float hardModeMaxVelocity = 6f;

    private Renderer tintRenderer;
    private Rigidbody m_Rigidbody;
    private float currentVelocityMultiplier;
    private float currentMaxVelocity;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        tintRenderer = GetComponent<Renderer>();

        if (SettingsManager.Instance != null)
        {
            SetColor(SettingsManager.Instance.ballColor);
            if (SettingsManager.Instance.hardMode)
            {
                currentVelocityMultiplier = hardModeVelocityMultiplier;
                currentMaxVelocity = hardModeMaxVelocity;
            }
            else
            {
                currentVelocityMultiplier = normalModeVelocityMultiplier;
                currentMaxVelocity = normalModeMaxVelocity;
            }
        }
        else
        {
            currentVelocityMultiplier = normalModeVelocityMultiplier;
            currentMaxVelocity = normalModeMaxVelocity;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * currentVelocityMultiplier;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.2f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > currentMaxVelocity)
        {
            velocity = velocity.normalized * currentMaxVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }

    private void SetColor(Color color)
    {
        tintRenderer.material.color = color;
    }
}
