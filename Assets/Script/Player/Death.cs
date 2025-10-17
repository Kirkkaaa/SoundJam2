using System.Collections;
using UnityEngine;
using StarterAssets;

public class Death : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";

    [Header("Audio (assign in Inspector)")]
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource playerDeath;
    [SerializeField] private AudioSource deathAmbient;

    [Header("UI")]
    [SerializeField] private GameObject deathScreenCanvas;

    private bool isDead;

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag(enemyTag)) StartDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        if (other.gameObject.CompareTag(enemyTag)) StartDeath();
    }

    private void StartDeath()
    {
        isDead = true;

        // Disable ThirdPersonController on same GameObject (if present)
        var tpc = GetComponent<ThirdPersonController>();
        if (tpc != null) tpc.enabled = false;

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        if (playerHit != null)
        {
            playerHit.Play();
            yield return new WaitWhile(() => playerHit.isPlaying);
        }

        if (playerDeath != null)
        {
            playerDeath.Play();
            yield return new WaitWhile(() => playerDeath.isPlaying);
        }

        if (deathAmbient != null)
        {
            deathAmbient.Play();
            if (deathScreenCanvas != null)
            {
                deathScreenCanvas.SetActive(true);
            }
        }
    }
}
