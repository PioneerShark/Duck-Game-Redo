using UnityEngine;

public class Entity2D : MonoBehaviour 
{
    public BoxCollider2D worldCollider;
    public EdgeCollider2D hitboxCollider;
    public Model2D entityModel;

    public virtual void Restore()
    {
        worldCollider = GetComponent<BoxCollider2D>();
        hitboxCollider = GetComponent<EdgeCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

