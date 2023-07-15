using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
  public void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "Bullet") {
      Destroy(other.gameObject);
    }
  }
}
