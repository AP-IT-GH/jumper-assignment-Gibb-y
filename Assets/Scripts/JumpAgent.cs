using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class JumpAgent : Agent
{
    public float JumpForce;
    private Rigidbody rb;
    private bool isJumping = false;
    private bool grounded = true;

    public override void OnEpisodeBegin()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1 && grounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            grounded = false;
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 7)
        {
            grounded = true;
        }
    }
}
