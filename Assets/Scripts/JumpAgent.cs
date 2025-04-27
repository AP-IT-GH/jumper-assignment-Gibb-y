using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class JumpAgent : Agent
{
    public float JumpForce;
    public ObstacleSpawner spawner;
    private Rigidbody rb;
    private bool isJumping = false;
    private bool grounded = true;
    private float rewardTimer = 0f;

    public override void OnEpisodeBegin()
    {
        rb = GetComponent<Rigidbody>();
        spawner.amount = 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
            AddReward(-0.01f);

        if (actions.DiscreteActions[0] == 1 && grounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(isJumping);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;

        if (isJumping)
        {
            discreteActions[0] = 1;
            isJumping = false;
        }
        else
            discreteActions[0] = 0;

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isJumping = true;

        LayerMask layerMask = LayerMask.GetMask("Obstacle");

        RaycastHit hit;
        rewardTimer += Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask) && rewardTimer >= 0.5)
        {
            AddReward(5.0f);
            rewardTimer = 0f;
        }

        if (spawner.amount == 21)
        {
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 7)
        {
            grounded = true;
        }

        if (collision.collider.gameObject.layer == 6)
        {
            AddReward(-5f);
            Destroy(collision.collider.gameObject);
        }
    }
}
