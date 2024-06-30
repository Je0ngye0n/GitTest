using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportReticleHandler: MonoBehaviour
{
    public GameObject reticlePrefab;
    private GameObject reticleInstance;

    private void OnEnable()
    {
        TeleportationProvider teleportationProvider = GetComponent<TeleportationProvider>();
        teleportationProvider.beginLocomotion += OnBeginLocomotion;
    }

    private void OnDisable()
    {
        TeleportationProvider teleportationProvider = GetComponent<TeleportationProvider>();
        teleportationProvider.beginLocomotion -= OnBeginLocomotion;
    }

    private void OnBeginLocomotion(LocomotionSystem locomotionSystem)
    {
        if (reticleInstance == null)
        {
            reticleInstance = Instantiate(reticlePrefab);
            reticleInstance.AddComponent<RotateRaticle>(); // ȸ�� ��ũ��Ʈ�� �������� �߰�
        }
        reticleInstance.SetActive(true);
    }

    private void OnEndLocomotion(LocomotionSystem locomotionSystem)
    {
        if (reticleInstance != null)
        {
            reticleInstance.SetActive(false);
        }
    }
}
