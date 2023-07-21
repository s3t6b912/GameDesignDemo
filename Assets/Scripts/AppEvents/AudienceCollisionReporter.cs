using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceCollisionReporter : MonoBehaviour
{
    public bool colliding = false;

    public void OnTriggerEnter(Collider c)
    {
        if (c.transform.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    public void OnTriggerExit(Collider c)
    {
        if (c.transform.gameObject.tag == "Player")
        {
            colliding = false;
        }
    }
}
