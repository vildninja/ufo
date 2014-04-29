using UnityEngine;
using System.Collections;

public class RepairDock : MonoBehaviour {

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.GetComponent<Destructable>())
        {
            col.collider.GetComponent<Destructable>().Heal(7 * Time.fixedDeltaTime);
        }
    }
}
