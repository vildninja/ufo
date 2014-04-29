using UnityEngine;
using System.Collections;

public class ParralaxedBG : MonoBehaviour {

    public Transform follow;
    public float multiplier = 0.7f;
    Vector3 baseOffset;
    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        baseOffset = transform.position - follow.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = follow.position + baseOffset;
        transform.position = startPosition + (pos - startPosition) * multiplier;
	}
}
