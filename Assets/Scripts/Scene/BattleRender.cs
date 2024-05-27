using Assets.Scripts;
using System.Collections.Generic;
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
            if (entityMap.TryGetValue(entity.Id, out var gameObject))
            {
                UpdateEntity(gameObject, entity);
            }
            else
            {
                AddEntity(entity);
            }
            set.Add(entity.Id);
        });

        //foreach (var item in entityMap)
        //{
        //    item.
        //    set.Contains(item.getc);
        //}
    }

    private void UpdateEntity(GameObject gameObject, Entity entity)
    {
    }

    private void AddEntity(Entity entity)
    {
        if (entity is RoleEntity roleEntity)
        {
            GameObject prefab = Resources.Load<GameObject>("Role/Hero/" + roleEntity.RoleInfo.PrefabName + "/" + roleEntity.RoleInfo.PrefabName);
            GameObject entityObject = Object.Instantiate(prefab, new Vector2(roleEntity.Position.X, roleEntity.Position.Y), Quaternion.identity);
            var entityController = entityObject.GetComponent<EntityController>();
            entityController.RoleEntity = roleEntity;
            entityController.UpdateGray();
            //entityController.UpdateByEntityInfo();
            entityMap.Add(entity.Id, entityObject);
        }
    }
}