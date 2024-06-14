public class Equipment
{
    public EquipmentConfig Config { get; set; }

    public Equipment(int equipmentId)
    {
        Config = ConfigMgr.GetEquipmentAttr(equipmentId);
    }

    public void FixedUpdate(int curFrame)
    {
    }
}