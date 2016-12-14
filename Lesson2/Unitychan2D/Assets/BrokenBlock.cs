using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBlock : MonoBehaviour {

    public Rigidbody2D[] blocks;

    public float force = 300f;

    void Start()
    {
        blocks[0].AddForce(Vector2.up * force);
        blocks[1].AddForce(Vector2.down * force);
        blocks[2].AddForce(Vector2.left * force);
        blocks[3].AddForce(Vector2.right * force);
    }
}
