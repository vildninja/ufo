using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObstacles : MonoBehaviour {

    public List<Transform> waypoints;

    int next = 0;

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (next >= waypoints.Count)
        {
            transform.position = waypoints[0].position;
            next = 1;
        }

        Vector2 direction = waypoints[next].position - transform.position;
        if (direction.sqrMagnitude < speed * speed * Time.deltaTime)
        {
            next++;
        }
        direction.Normalize();

        transform.Translate(direction * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<ShipController>())
        {
            col.GetComponent<Destructable>().Damage(30);
        }

        if (col.rigidbody2D)
        {
            print(name + " hit " + col);
            if (FindObjectOfType<TractorBeam>().abducted == col.rigidbody2D)
            {
                FindObjectOfType<TractorBeam>().abducted = null;
            }
        }
    }
}
