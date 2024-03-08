using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class BasicShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private CinemachineImpulseSource shootImpulseSource;
    [SerializeField] private float cameraShakeForce;

    [Header("Variables")]
    [Range(0, 5)]
    [SerializeField] private float shootPointSpread = 0.1f;
    [Range(0, 50)]
    [SerializeField] private float shootDirectionSpread = 0.1f;

    [Header("Rapid Fire")]
    [SerializeField] private bool enableRapidFire = false;
    [Range(0, 1.5f)]
    [SerializeField] private float rapidFireCoolDownMin = 0.05f, rapidFireCoolDownMax = 0.1f;
    private float rapidFireCoolDownCur;

    private bool isShooting = false;
    private bool canShoot = true;
    private bool queueShooting = false;

    private void Update()
    {
        DoRapidFire();
    }

    void DoRapidFire()
    {
        if(!enableRapidFire) { return; }
        if(!isShooting) { return; }

        if (rapidFireCoolDownCur <= 0)
        {
            CameraShake.instance.DoCameraShake(shootImpulseSource, cameraShakeForce);
            GameObject newBullet = Instantiate(bulletPrefab, GetShootPointSpread(), shootPoint.rotation);
            AdjustObjectsAngle(newBullet.transform);
            float rapidFireCoolDown = Random.Range(rapidFireCoolDownMin, rapidFireCoolDownMax);
            rapidFireCoolDownCur = rapidFireCoolDown;
        }
        else
        {
            rapidFireCoolDownCur -= Time.deltaTime;
        }
    }    
    public void OnFirePressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            queueShooting = true;

            if (!canShoot)
                return;
            StartShooting();
        }        
        else if(context.canceled)
        {
            queueShooting = false;
            if (!canShoot)
                return;
            StopShooting();          
        }
    }

    void StartShooting()
    {
        isShooting = true;
        PlayerStateController.instance.EnableShootState();
        if (!enableRapidFire)
        {
            GameObject newBullet = Instantiate(bulletPrefab, GetShootPointSpread(), shootPoint.rotation);
            AdjustObjectsAngle(newBullet.transform);
        }
    }

    void StopShooting()
    {
        isShooting = false;
        PlayerStateController.instance.EnableMoveState();
        rapidFireCoolDownCur = 0;
    }
    public void EnableShooting()
    {
        canShoot = true;
        if (queueShooting)
            StartShooting();
    }

    public void DisableShooting()
    {
        canShoot = false;
        isShooting = false;
        GetComponent<CocosTopDownController>().SetSpeedToMoveState();
    }
    Vector3 GetShootPointSpread()
    {
        return new Vector3(shootPoint.position.x + Random.Range(-shootPointSpread, shootPointSpread),
                shootPoint.position.y + Random.Range(-shootPointSpread, shootPointSpread));
    }

    void AdjustObjectsAngle(Transform objectToRotate)
    {
        float angleAdjustment = Random.Range(-shootDirectionSpread, shootDirectionSpread);
        Vector3 objectsEulerAngles = objectToRotate.rotation.eulerAngles;
        objectsEulerAngles.z += angleAdjustment;
        Quaternion newRotation = Quaternion.Euler(objectsEulerAngles);
        objectToRotate.rotation = newRotation;
    }
}
