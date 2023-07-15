using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
  public float maxSpeed = 10f;
  float speed;

  void Awake() {
    speed = maxSpeed;
  }

  public void SetSpeed(float percent) {
    speed = maxSpeed * percent;
  }

  void Update() {
    transform.position += transform.localRotation * Vector3.up * speed * Time.deltaTime;
  }
}
