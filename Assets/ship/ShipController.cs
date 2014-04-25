using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float rotation;
    public float power;

    public float maxHp;
    public float hp;

    Vector3 startPos;
    Quaternion startRot;

    public ImpactSparks impactSparks;

    public SpriteRenderer flash;

    [HideInInspector]
    public TractorBeam beam;

	// Use this for initialization
	void Start () {
        hp = maxHp;
        startPos = transform.position;
        startRot = transform.rotation;
	}

    void Update()
    {
        if (!rigidbody2D.isKinematic)
        {
            if (Input.GetButtonDown("Beam"))
            {
                beam.Toggle();
            }

            if (hp < 0)
            {
                StartCoroutine(Dead());
            }
        }
    }

    IEnumerator Dead()
    {
        hp = maxHp;
        rigidbody2D.isKinematic = true;

        beam.abducted = null;

        for (float t = 0; t < 1.4f; t += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = !renderer.enabled;
        }

        renderer.enabled = true;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0;
        transform.position = startPos;
        transform.rotation = startRot;
        rigidbody2D.isKinematic = false;
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
                hp -= 1;
            }
        }
	}

    IEnumerator Flash()
    {
        flash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        flash.enabled = false;
    }

    public void Impact(float damage, Vector2 contact)
    {
        print("DMG: " + damage);
        hp -= damage;

        StartCoroutine(Flash());
    }
}
