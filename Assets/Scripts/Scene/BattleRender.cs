using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleRender
{
    private Dictionary<int, GameObject> entityMap = new Dictionary<int, GameObject>();
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
        if (entity is RoleEntity roleEntity)
        {
            GameObject prefab = Resources.Load<GameObject>("Role/Hero/" + roleEntity.AttrComponent.BaseAttr.PrefabName + "/" + roleEntity.AttrComponent.BaseAttr.PrefabName);
            GameObject entityObject = Object.Instantiate(prefab, new Vector2(roleEntity.Position.X, roleEntity.Position.Y), Quaternion.identity);
            var entityController = entityObject.GetComponent<RoleEntityController>();
            entityController.EntityInfo = roleEntity;
            entityController.UpdateGray();
            entityMap.Add(entity.Id, entityObject);
        }
    }

    private void DelEntity(GameObject gameObject)
    {
        var controller = gameObject.GetComponent<EntityController>();
        controller.Destroy();
    }
}