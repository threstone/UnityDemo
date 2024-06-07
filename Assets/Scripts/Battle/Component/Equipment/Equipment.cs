public class Equipment
{
    readonly int equipmentId;
    public AttrObject Attr { get; set; }

    public Equipment(int equipmentId)
    {
        this.equipmentId = equipmentId;
        Attr = ConfigMgr.GetEquipmentAttr(equipmentId);
    }

    public void FixedUpdate(int curFrame)
    {
    }
}