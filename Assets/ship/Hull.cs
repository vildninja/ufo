using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour {

    public float threshold;
    public float multiplier;

    Vector2 velocity;

    void FixedUpdate()
    {
        velocity = transform.root.rigidbody2D.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = (velocity - transform.root.rigidbody2D.velocity).magnitude - threshold;
        if (impact > 0)
        {
            transform.root.GetComponent<Destructable>().Damage(impact * multiplier);
        }
    }
}
