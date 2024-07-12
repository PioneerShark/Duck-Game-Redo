using BehaviourTree;
using Unity.VisualScripting;
    
public class SpinnyBT : Tree
{
    public Agent agent;
    public UnityEngine.Transform target;
    // Start is called before the first frame update
    protected override Node SetupTree()
    {
        Node root = new TaskInitialize(agent, target);
        return root;
    }
}
