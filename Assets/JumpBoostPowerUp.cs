using UnityEngine;

public class JumpBoostPowerUp : MonoBehaviour
{
    [Header("Power-up Settings")]
    public float rotationSpeed = 90f;          // kuinka nopeasti py�rii
    public float floatAmplitude = 0.25f;       // kuinka paljon leijuu
    public float floatFrequency = 2f;          // kuinka nopeasti leijuu
    public float jumpMultiplier = 1.5f;        // 50 % korkeampi hyppy

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Py�rii ymp�ri
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Leijuu yl�s ja alas
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Etsit��n ThirdPersonController
        var controller = other.GetComponentInParent<StarterAssets.ThirdPersonController>();
        if (controller != null)
        {
            // Nostetaan hyppykorkeutta pysyv�sti
            controller.JumpHeight *= jumpMultiplier;
        }

        // Piilotetaan ja poistetaan power-up
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 0.2f);
    }
}
