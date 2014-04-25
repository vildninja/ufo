using UnityEngine;
using System.Collections;

public class ImpactSparks : MonoBehaviour {

    public ParticleSystem particles;

    public float power
    {
        set {
            print(value);
            particles.Emit((int)(value * 2));
        }
    }

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
