using UnityEngine;
using BehaviourTree;

public class IdleTask : Node
{
    private Agent self;
    private Vector3 idlePosition;
    private Vector3 originPosition;
    private float idleTime = 3f;
    private float idleCount = 0f;
    private bool idleDone = false; 

    // Constructor
    public IdleTask(Agent setSelf)
    {
        this.self = setSelf;
        this.idlePosition = this.self.transform.position;
        this.originPosition = this.self.transform.position;
    }

    // Evaluation
    public override NodeState Evaluate()
    {
        if (this.idleDone != true)
        {
            this.idleCount += Time.deltaTime;
            if (this.idleCount > this.idleTime)
            {
                this.idleDone = true;
                this.idleCount = 0f;
                this.idlePosition = this.originPosition + new Vector3(Random.Range(-5, 5) * 0.5f, Random.Range(-5, 5) * 0.5f, 0);
                this.self.SetTarget(this.idlePosition);
            }
        }
        else
        {
            if ((this.self.transform.position - this.idlePosition).magnitude < 0.01f)
            {
                this.idleDone = false;
            }
            else
            {
                this.self.MoveTo();
            }
        }

        return NodeState.FAILURE;
    }
}