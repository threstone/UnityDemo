﻿public class EquipmentComponent
{
    public Equipment[] Equipments { get; set; }
    public EquipmentComponent(Role role)
    {
        Equipments = new Equipment[6];
        for (int i = 0; i < role.equipmentIdList?.Count; i++)
        {
            Equipments[i] = new Equipment(role.equipmentIdList[i]);
        }
    }

    public void FixedUpdate(int curFrame) {
        // 装备逻辑
        for (int i = 0; i < Equipments.Length; i++)
        {
            Equipments[i]?.FixedUpdate(curFrame);
        }
    }
}