using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleRender
{
    private readonly Dictionary<int, GameObject> entityMap = new();

    public static System.Random Random = new();

    private static GameObject canvas;
    public static GameObject Canvas
    {
        get
        {
            if (canvas == null) canvas = GameObject.Find("Canvas");
            return canvas;
        }
    }
    
    public BattleRender()
    {
    }

    public void FixedUpdate(Simulator simulator)
    {
        HashSet<int> set = new();
        simulator.EntityList.ForEach((entity) =>
        {
            if (!entityMap.ContainsKey(entity.Id))
            {
                AddEntity(entity);
            }

            set.Add(entity.Id);
        });

        for (int i = entityMap.Count - 1; i >= 0; i--)
        {
            var kvp = entityMap.ElementAt(i);
            if (set.Contains(kvp.Key) == false)
            {
                DelEntity(kvp.Value);
                entityMap.Remove(kvp.Key);
            }
        }
    }

    private void AddEntity(Entity entity)
    {

        if (entity is RoleEntity roleEntity) AddEntity(roleEntity);
        else if (entity is AttackProjectile atkProjectile) AddEntity(atkProjectile);
    }

    private void AddEntity(RoleEntity roleEntity)
    {
        var prefabName = roleEntity.AttrComponent.BaseAttr.PrefabName;
        GameObject prefab = Resources.Load<GameObject>("Role/Hero/" + prefabName + "/" + prefabName);// todo优化点 池化
        GameObject entityObject = Object.Instantiate(prefab, new Vector2(roleEntity.Position.X, roleEntity.Position.Y), Quaternion.identity);
        var controller = entityObject.GetComponent<RoleEntityController>();
        controller.EntityInfo = roleEntity;
        controller.UpdateGray();
        entityMap.Add(roleEntity.Id, entityObject);
    }

    private void AddEntity(AttackProjectile atkProjectile)
    {
        var prefabName = atkProjectile.Source.AttrComponent.BaseAttr.PrefabName;
        GameObject prefab = Resources.Load<GameObject>("Role/Hero/" + prefabName + "/attack");// todo优化点 池化
        GameObject atkProjectileObject = Object.Instantiate(prefab, new Vector2(atkProjectile.Position.X, atkProjectile.Position.Y), Quaternion.identity);
        var controller = atkProjectileObject.GetComponent<AtkProjectileController>();
        controller.EntityInfo = atkProjectile;
        controller.UpdateGray();
        entityMap.Add(atkProjectile.Id, atkProjectileObject);

    }

    private void DelEntity(GameObject gameObject)
    {
        var controller = gameObject.GetComponent<EntityController>();
        controller.Destroy();
    }
}