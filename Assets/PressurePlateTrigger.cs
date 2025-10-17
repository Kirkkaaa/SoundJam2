using UnityEngine;
using System.Collections;

public class PressurePlateTrigger : MonoBehaviour
{
    [SerializeField] private GameObject movableObject;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float delay = 0.5f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        originalPosition = movableObject.transform.position;
        targetPosition = originalPosition + new Vector3(0, 0, moveDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(MoveObjectWithDelay());
        }
    }

    private IEnumerator MoveObjectWithDelay()
    {
        isMoving = true;

        // Odotetaan ennen liikkeen aloitusta
        yield return new WaitForSeconds(delay);

        // Liikuta kohteeseen
        yield return StartCoroutine(MoveToPosition(movableObject, targetPosition));

        // Odotetaan paikallaan
        yield return new WaitForSeconds(delay);

        // Liikuta takaisin alkuperäiseen paikkaan
        yield return StartCoroutine(MoveToPosition(movableObject, originalPosition));

        isMoving = false;
    }

    private IEnumerator MoveToPosition(GameObject obj, Vector3 destination)
    {
        while (Vector3.Distance(obj.transform.position, destination) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(
                obj.transform.position,
                destination,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Varmistetaan tarkka lopullinen sijainti
        obj.transform.position = destination;
    }
}
