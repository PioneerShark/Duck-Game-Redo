using UnityEngine;
using BehaviourTree;

public class BasicAttackTree : Node
{
    private Agent self;
    private GameObject target;

    // Constructor
    public BasicAttackTree(Agent setSelf, GameObject setTarget)
    {
        this.self = setSelf;
        this.target = setTarget;
    }

    // Evaluation
    public override NodeState Evaluate()
    {
        Node root = new Sequence
        (
            new MoveToTargetTask(self, target),
            new AttackTargetTask(self, target),
            new KiteTargetTask(self, target)
        );
        return root.Evaluate();
    }
}