


using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Entity EntityInfo { get; set; }
    protected SpriteRenderer spriteRenderer;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Destroy();
}
