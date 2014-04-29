using UnityEngine;
using System.Collections;

public class Flesh : MonoBehaviour {

    public float threshold;
    public float multiplier;

    void OnCollisionEnter2D(Collision2D col)
    {

        if (transform.root.GetComponent<Destructable>())
        {
            Vector2 normal = Vector2.zero;
            foreach (var c in col.contacts)
                normal += c.normal;

            float projected = Vector3.Project(col.relativeVelocity, normal).magnitude - threshold;

            if (projected > 0)
                transform.root.GetComponent<Destructable>().Damage(projected * multiplier);
        }
    }
}
