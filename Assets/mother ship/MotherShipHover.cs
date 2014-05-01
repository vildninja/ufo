using UnityEngine;
using System.Collections;

public class MotherShipHover : MonoBehaviour {

    public Transform target;
    public float force;
    public float torque;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        rigidbody2D.AddForce(direction * force);

        float angle = target.eulerAngles.z - transform.eulerAngles.z;
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;

        //print(transform.eulerAngles.z + " " + target.eulerAngles.z + " " + angle);
        rigidbody2D.AddTorque(angle * torque);
    }
}
