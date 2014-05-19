using UnityEngine;
using System.Collections;

public class MotherShipTakeOff : MonoBehaviour {

    public Collider2D cap;

    public Transform ExitPath;

    public bool waitingForShip = false;

	// Use this for initialization
	IEnumerator Start () {
        while (FindObjectsOfType<Abductable>().Length > 0)
            yield return new WaitForSeconds(1);

        Destroy(cap);

        waitingForShip = true;
	}

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (waitingForShip && col.GetComponent<ShipController>())
        {
            Destroy(col.GetComponent<ShipController>());
            var ship = col.transform.root;
            foreach (var h in ship.GetComponentsInChildren<Hull>())
                Destroy(h);
            yield return new WaitForSeconds(1);
            Destroy(ship.rigidbody2D);
            foreach (var c in ship.GetComponentsInChildren<Collider2D>())
                Destroy(c);
            ship.parent = transform;
            transform.root.collider2D.isTrigger = true;

            var hover = transform.root.GetComponent<MotherShipHover>();

            foreach (Transform t in ExitPath)
            {
                hover.target = t;
                while (Vector2.Distance(transform.root.position, t.position) > 1)
                {
                    yield return new WaitForFixedUpdate();
                }
            }

            //Destroy(transform.root.gameObject);
        }
    }
}
