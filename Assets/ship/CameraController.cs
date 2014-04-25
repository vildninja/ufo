using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public AnimationCurve xCurve = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0));
    public AnimationCurve yCurve = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0));

    public Transform target;
    public Vector2 extends = new Vector2(4, 3);

    bool isRunning = false;
    Vector3 center;

    void OnDrawGizmos()
    {
        if (!isRunning)
            center = transform.position;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(center, extends * 2);
    }

    void Awake()
    {
        isRunning = true;
        center = transform.position;
    }

	// Update is called once per frame
	void Update () {
        Vector2 pos = target.position - center;
        pos += extends;

        float x = xCurve.Evaluate(pos.x / (2 * extends.x)) * 2 - 1;
        float y = yCurve.Evaluate(pos.y / (2 * extends.y)) * 2 - 1;


        Vector2 dif = new Vector2(extends.x - camera.orthographicSize * camera.aspect, extends.y - camera.orthographicSize);
        transform.position = center + new Vector3(x * dif.x, y * dif.y);

        //Vector3 camPos = new Vector3(x.Evaluate(pos.x / (2 * extends.x)), y.Evaluate(pos.y / (2 * extends.y)));
        //camPos = Vector3.Scale(camPos, dif);
        //camPos += new Vector3(camera.orthographicSize * camera.aspect, camera.orthographicSize);
        //camPos.z = transform.position.z;
        //transform.position = camPos;
	}
}
