using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private float timeLeft;
    private float timeLeftMax;
    private float initialAlpha;

    private SpriteRenderer sprite;
    private Color spriteColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        spriteColor = sprite.color;
        
    }

    public void UpdateAfterImage(float _imageTime, float _initialAlpha, GameObject original)
    {
        timeLeft = _imageTime;
        timeLeftMax = _imageTime;
        initialAlpha = _initialAlpha;
        spriteColor.a = 1;
        sprite.color = spriteColor;
        SpriteRenderer originalSprite = original.GetComponent<SpriteRenderer>();
        Material material = originalSprite.material;
        sprite.material = material;
        sprite.sprite = originalSprite.sprite;
        transform.localScale = original.transform.lossyScale;
        transform.localRotation = original.transform.localRotation;
        transform.position = original.transform.position;
        sprite.sortingOrder = originalSprite.sortingOrder-1;

        this.gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        spriteColor.a = timeLeft / timeLeftMax;
        sprite.color = spriteColor;

        if (timeLeft < 0) this.gameObject.SetActive(false);
    }
}
