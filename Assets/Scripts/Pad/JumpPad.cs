using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float padPower;
    void OnCollisionEnter(Collision collision)
    {
        // Player의 layer 번호는 7
        if (collision.gameObject.layer == 7) 
        {
            Rigidbody rigidbody = collision.rigidbody;
            rigidbody.AddForce(Vector2.up * padPower, ForceMode.Impulse);
        }
    }
}
