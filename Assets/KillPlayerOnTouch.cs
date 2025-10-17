using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    [Header("Settings")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // Etsit‰‰n Death-komponentti pelaajasta
        var death = other.GetComponentInParent<Death>();
        if (death != null)
        {
            Debug.Log("KillPlayerOnTouch: Activating player death sequence.");
            // Kutsutaan StartDeath-funktiota
            var method = typeof(Death).GetMethod("StartDeath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(death, null);
            }
        }
        else
        {
            Debug.LogWarning("KillPlayerOnTouch: No Death component found on player!");
        }
    }
}
