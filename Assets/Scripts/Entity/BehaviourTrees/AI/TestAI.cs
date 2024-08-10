using BehaviourTree;

public class TestBT : Tree
{
    public Agent self;
    public UnityEngine.GameObject targetObject;

    protected override Node SetupTree()
    {
        Node root = new Sequence
        (
            new Selector
            (
                new FindTargetTask(self, targetObject),
                new IdleTask(self)
            ),
            new BasicAttackTree(self, targetObject)
                // Basically acts as another Tree and executes the following
                //new MoveToTargetTask(self, targetObject)
                //new AttackTargetTask(self, targetObject)
                //new KiteTargetTask(self, targetObject)
        );
        return root;
    }
}
