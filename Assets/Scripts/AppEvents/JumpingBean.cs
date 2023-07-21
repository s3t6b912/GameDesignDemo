using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBean : MonoBehaviour
{
    private bool isGrounded = true;
    private void FixedUpdate()
    {
        int jump = Random.Range(0, 10);
        if (jump == 5 && isGrounded)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            float x = Random.Range(-4.0f, 4.0f);
            float y = Random.Range(1.0f, 3.0f);
            float z = Random.Range(-4.0f, 4.0f);

            Vector3 force = new Vector3(x, y, z);

            body.AddForce(force, ForceMode.Impulse);

            isGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "ground")
        {

            isGrounded = true;

        }
    }
}
