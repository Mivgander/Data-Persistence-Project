using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;

    private Renderer tintRenderer;

    // Start is called before the first frame update
    void Start()
    {
        tintRenderer = GetComponent<Renderer>();

        if (SettingsManager.Instance != null)
        {
            SetColor(SettingsManager.Instance.paddleColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }

    private void SetColor(Color color)
    {
        tintRenderer.material.color = color;
    }
}
