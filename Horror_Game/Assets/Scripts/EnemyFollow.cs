using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;                      // Reference to the player's transform
    private NavMeshAgent agent;                   // Reference to the NavMesh Agent
    private Animator animator;                    // Reference to the Animator component
    public float accelerationDistance = 10f;      // Distance within which the agent starts accelerating
    public float stopDistance = 2f;               // Distance at which the agent stops following the player
    public float maxSpeed = 6f;                   // Maximum speed of the agent
    public float minSpeed = 2f;                   // Minimum speed of the agent

    public RuntimeAnimatorController farAnimator; // Animator Controller for far distance
    public RuntimeAnimatorController closeAnimator; // Animator Controller for close distance
    public RuntimeAnimatorController accelerationAnimator; // Animator Controller for acceleration

    public RuntimeAnimatorController stopFollowAnimator; // Animator Controller for when too close
    public float switchDistance = 5f;             // Distance threshold to switch Animator Controller

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null && agent.isOnNavMesh)
        {
            // Calculate distance to the player
            float distance = Vector3.Distance(transform.position, player.position);

            // Stop following if too close and switch to the "stopFollowAnimator"
            if (distance <= stopDistance)
            {
                agent.isStopped = true; // Stop the agent
                agent.speed = 0;        // Set speed to 0
                animator.SetFloat("Speed", 0); // Trigger idle animation
                SwitchAnimatorController(stopFollowAnimator); // Switch to "stopFollow" animator
            }
            else
            {
                agent.isStopped = false; // Resume following
                agent.SetDestination(player.position);

                // Adjust speed based on proximity and switch to the appropriate animator
                if (distance <= accelerationDistance)
                {
                    agent.speed = Mathf.Lerp(maxSpeed, minSpeed, distance / accelerationDistance);
                    SwitchAnimatorController(accelerationAnimator); // Switch to "acceleration" animator
                }
                else
                {
                    agent.speed = minSpeed;
                    SwitchAnimatorController(farAnimator); // Switch back to "far" animator
                }

                // Update animation based on speed
                float normalizedSpeed = (agent.speed - minSpeed) / (maxSpeed - minSpeed);
                animator.SetFloat("Speed", normalizedSpeed);
            }
        }
    }

    void SwitchAnimatorController(RuntimeAnimatorController newAnimator)
    {
        if (animator.runtimeAnimatorController != newAnimator)
        {
            animator.runtimeAnimatorController = newAnimator;
        }
    }
}
