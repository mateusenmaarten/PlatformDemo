﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumpScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float fallMultiplier = 1f;
    public float lowJumpMultiplier = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
