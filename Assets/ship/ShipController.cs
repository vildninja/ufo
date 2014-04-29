using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float rotation;
    public float power;

    [HideInInspector]
    public TractorBeam beam;

    Destructable hp;

    void Awake()
    {
        hp = GetComponent<Destructable>();
    }

    void Update()
    {
        if (!rigidbody2D.isKinematic)
        {
            if (Input.GetButtonDown("Beam"))
            {
                beam.Toggle();
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!rigidbody2D.isKinematic)
        {
            rigidbody2D.AddTorque(Input.GetAxis("Horizontal") * -rotation * Time.fixedDeltaTime);
            if (Input.GetButton("Power"))
            {
                rigidbody2D.AddForce(transform.up * power);
            }

            if (rigidbody2D.velocity.sqrMagnitude < 0.01f && Vector2.Angle(Vector2.up, transform.up) > 100)
            {
                hp.Damage(10 * Time.deltaTime, false);
            }
        }
	}
}
