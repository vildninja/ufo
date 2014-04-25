using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour {

    public bool activated;

    public float power;

    public AnimationCurve force;

    public Transform anchor;

    public Rigidbody2D abducted;

    LineRenderer line;
    float timeOfAbduction;
    public AnimationCurve lineWidth;

	// Use this for initialization
	void Start () {
        transform.parent = null;
        power = 0;
        anchor.root.GetComponent<ShipController>().beam = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;

        if (activated)
            power = Mathf.Clamp01(power + Time.deltaTime);
        else
            power = Mathf.Clamp01(power - 2 * Time.deltaTime);

        if (line.enabled)
        {
            Vector3 target = new Vector2(0, -2);
            float t = (Time.time - timeOfAbduction) * 1.5f;
            if (abducted)
            {
                target = Vector3.Lerp(new Vector2(0, -1.5f), transform.InverseTransformPoint(abducted.transform.position), t);
            }
            line.SetWidth(0, lineWidth.Evaluate(t));
            line.SetPosition(1, target);
            if (!abducted && t > 1)
                line.enabled = false;
        }
	}

    public void Toggle()
    {
        if (abducted)
        {
            abducted = null;
            line.enabled = false;
        }
        else
        {
            line.enabled = true;
            timeOfAbduction = Time.time;

            Abductable nearest = null;
            float distance = float.MaxValue;
            foreach (var abduct in FindObjectsOfType<Abductable>())
            {
                if (collider2D.OverlapPoint(abduct.transform.position))
                {
                    float d = Vector2.Distance(transform.position, abduct.transform.position);
                    if (d < distance)
                    {
                        distance = d;
                        nearest = abduct;
                    }
                }
            }
            if (nearest)
            {
                print(nearest.name);
                abducted = nearest.rigidbody2D;
                line.enabled = true;
                timeOfAbduction = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        if (abducted)
        {
            if (!collider2D.OverlapPoint(abducted.transform.position) || !abducted.GetComponent<Abductable>())
            {
                abducted = null;
                line.enabled = false;
            }
            else
            {
                Vector2 direction = anchor.position - abducted.transform.position;
                float pull = force.Evaluate(direction.magnitude);
                direction.Normalize();

                anchor.root.rigidbody2D.AddForceAtPosition(direction * -pull, abducted.transform.position);
                abducted.AddForce(direction * pull);
            }
        }
    }
}
