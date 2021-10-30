using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public delegate void CollideDelegate(Collider2D other);

    public CollideDelegate collideDelegate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        collideDelegate?.Invoke(other);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
