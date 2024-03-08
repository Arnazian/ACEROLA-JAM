using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private Transform playerCameraFollowTarget;
    [SerializeField] private CinemachineImpulseSource enemyDeathImpulseSource;
    [SerializeField] private CinemachineImpulseSource bossDeathImpulseSource;

    public void SetCameraTarget(Transform newTarget)
    {
        camera.m_Follow = newTarget;
    }

    public void ResetCameraTarget()
    {
        camera.m_Follow = playerCameraFollowTarget;
    }
    public void EnemyDeathShake(float force)
    {
        DoCameraShake(enemyDeathImpulseSource, force);
    }
    public void BossDeathShake(float force)
    {
        DoCameraShake(bossDeathImpulseSource, force);
    }
    public void DoCameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        int choiceX = Random.Range(0, 2);
        int choiceY = Random.Range(0, 2);

        impulseSource.m_DefaultVelocity.x = (choiceX == 0) ? impulseSource.m_DefaultVelocity.x : -impulseSource.m_DefaultVelocity.x;
        impulseSource.m_DefaultVelocity.y = (choiceY == 0) ? impulseSource.m_DefaultVelocity.y : -impulseSource.m_DefaultVelocity.y;
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }

    #region Singleton Implementation
    public static CameraShake instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    #endregion
}
