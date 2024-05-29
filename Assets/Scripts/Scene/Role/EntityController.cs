 


using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public Entity EntityInfo { get; set; }

    public abstract void Destroy();
}
