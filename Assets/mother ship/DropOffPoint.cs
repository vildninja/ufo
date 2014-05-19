using UnityEngine;
using System.Collections;

public class DropOffPoint : MonoBehaviour {

    public int score;
    public TextMesh text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Abductable>())
        {
            var a = col.GetComponent<Abductable>();
            foreach (var beam in FindObjectsOfType<TractorBeam>())
                if (beam.abducted == a.rigidbody2D)
                    beam.abducted = null;
            score += a.score;
            Destroy(a);

            if (text)
            {
                text.text = score.ToString();
            }

            yield return new WaitForSeconds(0.5f);
            Destroy(col.gameObject);
        }
    }
}
