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

    void OnTriggerEnter2D(Collider2D col)
    {
        print(col);
        if (col.GetComponent<Abductable>())
        {
            var a = col.GetComponent<Abductable>();
            score += a.score;
            Destroy(a);

            if (text)
            {
                text.text = score.ToString();
            }
        }
    }
}
