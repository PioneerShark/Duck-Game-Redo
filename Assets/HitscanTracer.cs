using UnityEngine;

public class HitscanTracer : MonoBehaviour
{
    private float trailDuration, duration;
    private Vector2 force, endPoint;

    private Rigidbody2D rb;
    private TrailRenderer trail;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    public void SetVariables(float _speed, Vector2 _startPoint, Vector2 _endPoint, float _trailDuration, float _scale)
    {
        transform.position = _startPoint;
        transform.localScale = new Vector3(_scale, _scale, _scale);
        trail.widthMultiplier = _scale;
        duration = Vector2.Distance(_endPoint, _startPoint)/_speed;
        force = (_endPoint - _startPoint).normalized * _speed;
        endPoint = _endPoint;
        trailDuration = _trailDuration;
        trail.Clear();
    }
    public void OnEnable()
    {
        rb.simulated = true;
        trail.time = trailDuration;
        rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log(duration);
        Invoke("DeactivatePrep", duration);
    }

    private void DeactivatePrep()
    {
        rb.simulated = false;
        transform.position = endPoint;
        Invoke("Deactivate", trail.time);

    }

    void Deactivate()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);
    }
}
