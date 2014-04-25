using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GroundCreator : MonoBehaviour {

    public bool updateMesh;

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
        if (updateMesh)
        {
            UpdateMesh();
            updateMesh = false;
        }
	}

    public void UpdateMesh()
    {
        var poly = GetComponent<PolygonCollider2D>();
        print(poly.pathCount + " " + poly.shapeCount + " " + poly.points);
        var tris = new Triangulator(poly.points);
        var indices = tris.Triangulate();


        Mesh mesh = new Mesh();
        Vector3[] points3 = new Vector3[poly.points.Length];
        for (int i = 0; i < poly.points.Length; i++)
            points3[i] = poly.points[i];

        Vector2[] uv = new Vector2[poly.points.Length];
        for (int i = 0; i < poly.points.Length; i++)
            uv[i] = transform.TransformPoint(poly.points[i]);
        mesh.vertices = points3;
        mesh.uv = uv;
        mesh.triangles = indices;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
