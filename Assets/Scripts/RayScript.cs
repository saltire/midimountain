using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RayScript : MonoBehaviour {
  MeshFilter meshFilter;
  PolygonCollider2D polyCollider;

  float rayLength = 100;

  void Awake() {
    meshFilter = GetComponent<MeshFilter>();
    polyCollider = GetComponent<PolygonCollider2D>();

    meshFilter.mesh = new Mesh();
  }

  public void SetAngle(float angle) {
    Vector3[] vertices = {
      Vector3.zero,
      Quaternion.AngleAxis(-angle / 2, Vector3.back) * Vector3.up * rayLength,
      Quaternion.AngleAxis(angle / 2, Vector3.back) * Vector3.up * rayLength,
    };
    int[] triangles = { 0, 1, 2 };

    meshFilter.mesh.vertices = vertices;
    meshFilter.mesh.triangles = triangles;

    Vector2[] points = {
      Vector2.zero,
      Quaternion.AngleAxis(-angle / 2, Vector3.back) * Vector2.up * rayLength,
      Quaternion.AngleAxis(angle / 2, Vector3.back) * Vector2.up * rayLength,
    };
    polyCollider.points = points;
  }
}
