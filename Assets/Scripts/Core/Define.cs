using System.Collections.Generic;

public class Role
{
    public int RoleId { get; set; }
    public int PlayerId { get; set; }

    public int Level { get; set; }

    public List<int> equipmentIdList;

    public Role(int roleId, int level, int userId)
    {
        RoleId = roleId;
        Level = level;
        PlayerId = userId;
    }
}