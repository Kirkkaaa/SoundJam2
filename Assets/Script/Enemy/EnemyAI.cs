using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] private float hoverHeight = 0.5f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float hoverSmoothSpeed = 5f;

    [Header("References (optional)")]
    [Tooltip("If null, the player will be found by the \"Player\" tag at Start")]
    [SerializeField] private Transform player;

    private NavMeshAgent agent;
    private bool isChasing;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Keep base offset zero because we manually control vertical hover
        agent.baseOffset = 0f;
        agent.stoppingDistance = attackRange;
    }

    private void Start()
    {
        if (player == null)
        {
            var pgo = GameObject.FindGameObjectWithTag("Player");
            if (pgo != null) player = pgo.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Use horizontal distance so vertical offset doesn't affect detection
        var myXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        var playerXZ = new Vector3(player.position.x, 0f, player.position.z);
        float horizontalDist = Vector3.Distance(myXZ, playerXZ);

        if (horizontalDist <= detectionRadius)
        {
            if (!isChasing) isChasing = true;
            agent.SetDestination(player.position);

            if (horizontalDist <= attackRange)
            {
                // Stop moving when attacking (optional)
                agent.ResetPath();
                isChasing = false;
                Attack();
            }
        }
        else if (isChasing)
        {
            isChasing = false;
            agent.ResetPath();
        }

        MaintainHover();
    }

    // Keeps the sphere hovering hoverHeight above the ground (raycast to ground).
    // This version sets the Y position immediately so the enemy is always exactly
    // hoverHeight above the ground hit.point.
    private void MaintainHover()
    {
        RaycastHit hit;
        // Cast from slightly above to ensure we hit ground collider
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        const float rayLength = 20f;

        float targetY;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength))
        {
            targetY = hit.point.y + hoverHeight;
        }
        else
        {
            // If no ground found, fallback to hoverHeight relative to world 0
            targetY = hoverHeight;
        }

        var pos = transform.position;
        // Immediately snap to the target Y so the enemy hovers exactly at hoverHeight above the ground.
        pos.y = targetY;
        transform.position = pos;
    }

    // Placeholder attack method — implement actual attack logic here.
    private void Attack()
    {
        // TODO: Implement attack behaviour (damage, animation, cooldown, etc.)
        // This method is intentionally left empty for now.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
