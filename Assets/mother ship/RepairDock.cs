using UnityEngine;
using System.Collections;

public class RepairDock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.GetComponent<ShipController>())
        {
            var ship = col.collider.GetComponent<ShipController>();
            ship.Heal(7 * Time.fixedDeltaTime);
        }
    }
}
