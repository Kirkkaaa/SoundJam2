using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [Header("Power-up Settings")]
    public float rotationSpeed = 90f;          // py�rimisnopeus
    public float floatAmplitude = 0.25f;       // leijumisen korkeus
    public float floatFrequency = 2f;          // leijumisen nopeus
    public float speedMultiplier = 1.5f;       // 50 % nopeampi (1.5x)

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Py�rii
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Leijuu yl�s ja alas
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Haetaan pelaajan liikkumisskripti (Starter Assets ThirdPersonController)
        var controller = other.GetComponentInParent<StarterAssets.ThirdPersonController>();
        if (controller != null)
        {
            // Nostetaan pelaajan nopeutta pysyv�sti
            controller.MoveSpeed *= speedMultiplier;
        }

        // Piilotetaan ja poistetaan power-up
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 0.2f);
    }
}
