using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoTelekinesis : MonoBehaviour
{
    public float detectionRadius = 4f;
    public LayerMask telekinesisLayer;
    public float moveSpeed = 2f;
    public Camera mainCamera;

    private bool isInTelekinesisMode = false;
    private Transform targetObject;
    private Rigidbody targetRb;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private Vector3 referenceAcceleration;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (isInTelekinesisMode && targetObject != null)
        {
            Vector3 currentAcc = Input.acceleration;
            Vector3 delta = currentAcc - referenceAcceleration;

            // Solo eje X
            Vector3 moveDir = new Vector3(delta.x, 0, 0);
            targetRb.MovePosition(targetRb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    public void ToggleTelekinesis()
    {
        if (!isInTelekinesisMode)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, telekinesisLayer);
            if (colliders.Length > 0)
            {
                targetObject = GetClosestObject(colliders).transform;
                targetRb = targetObject.GetComponent<Rigidbody>();
                if (targetRb != null)
                {
                    EnterTelekinesisMode();
                }
            }
        }
        else
        {
            ExitTelekinesisMode();
        }
    }

    Transform GetClosestObject(Collider[] colliders)
    {
        Transform closest = colliders[0].transform;
        float minDist = Vector3.Distance(transform.position, closest.position);

        foreach (var col in colliders)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = col.transform;
            }
        }

        return closest;
    }

    void EnterTelekinesisMode()
    {
        isInTelekinesisMode = true;

        // Guardar acelerómetro inicial
        referenceAcceleration = Input.acceleration;

        // Guardar cámara
        originalCameraPosition = mainCamera.transform.position;
        originalCameraRotation = mainCamera.transform.rotation;

        // Asegurar física estable
        targetRb.useGravity = false;
        targetRb.velocity = Vector3.zero;
        targetRb.angularVelocity = Vector3.zero;

        StartCoroutine(LockCameraToObject());
    }

    void ExitTelekinesisMode()
    {
        isInTelekinesisMode = false;

        if (targetRb != null)
            targetRb.useGravity = true;

        targetObject = null;
        targetRb = null;

        // Restaurar cámara
        mainCamera.transform.position = originalCameraPosition;
        mainCamera.transform.rotation = originalCameraRotation;
    }

    IEnumerator LockCameraToObject()
    {
        while (isInTelekinesisMode && targetObject != null)
        {
            Vector3 offset = new Vector3(0, 2f, -4f); // Ajustable
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetObject.position + offset, Time.deltaTime * 5);
            mainCamera.transform.LookAt(targetObject);
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
