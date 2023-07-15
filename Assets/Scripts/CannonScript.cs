using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CannonScript : MonoBehaviour {
  public float range = 120;
  public int number;

  public BulletScript bulletPrefab;
  public float bulletCooldown = .25f;
  float bulletCooldownEnd = 0;

  public RayScript rayPrefab;
  public float maxRayAngle = 45;
  public float rayChargeRate = .1f;
  public float rayDischargeRate = .5f;
  public float rayChargeCooldown = .5f;
  float rayChargeAmount = 0;
  float rayChargeCooldownEnd = 0;
  RayScript ray = null;

  Transform gun;
  SpriteRenderer chargeLight;

  void Start() {
    GetComponent<PlayerInput>().onActionTriggered += OnAction;

    gun = transform.Find("Gun");
    chargeLight = transform.Find("Ray Charge").GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (!ray && Time.time >= rayChargeCooldownEnd && rayChargeAmount < 1) {
      rayChargeAmount = Mathf.Min(1, rayChargeAmount + rayChargeRate * Time.deltaTime);
      chargeLight.color = Color.Lerp(Color.black, Color.green, rayChargeAmount);
    }
    else if (ray) {
      rayChargeAmount = Mathf.Max(0, rayChargeAmount - rayDischargeRate * Time.deltaTime);
      chargeLight.color = Color.Lerp(Color.black, Color.green, rayChargeAmount);
      ray.SetAngle(maxRayAngle * rayChargeAmount);
    }
  }

  void OnAction(InputAction.CallbackContext ctx) {
    if (ctx.action.name == "C" + number) {
      RotateGun(ctx.ReadValue<float>());
    }
    else if (ctx.action.name == "PB" + number && ctx.action.phase == InputActionPhase.Performed) {
      FireBullet(ctx.ReadValue<float>());
    }
    else if (ctx.action.name == "PA" + number) {
      if (ctx.action.phase == InputActionPhase.Performed && !ray && rayChargeAmount > 0) {
        ray = Instantiate<RayScript>(rayPrefab, gun.position + Vector3.forward, gun.rotation, gun);
        ray.SetAngle(maxRayAngle * rayChargeAmount);
      }
      else if (ctx.action.phase == InputActionPhase.Canceled && ray) {
        Destroy(ray.gameObject);
        ray = null;
        rayChargeCooldownEnd = Time.time + rayChargeCooldown;
      }
    }
  }

  void RotateGun(float controlValue) {
    float angle = Mathf.LerpAngle(-range / 2, range / 2, controlValue);
    gun.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
  }

  void FireBullet(float controlValue) {
    if (Time.time >= bulletCooldownEnd) {
      BulletScript bullet = Instantiate<BulletScript>(bulletPrefab, transform.position, gun.rotation);
      bullet.SetSpeed(Mathf.Lerp(.5f, 1, controlValue));
      bulletCooldownEnd = Time.time + bulletCooldown;
    }
  }
}
