using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour {

    public float threshold;
    public float multiplier;

    Vector2 velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        velocity = transform.root.rigidbody2D.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float impact = (velocity - transform.root.rigidbody2D.velocity).magnitude - threshold;
        if (impact > 0)
            transform.root.GetComponent<ShipController>().Impact(impact * multiplier, col.contacts[0].point);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        OnCollisionEnter2D(col);
    }
}
