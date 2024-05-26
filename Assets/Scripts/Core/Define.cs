using System.Collections.Generic;

public class Role
{
    public int RoleId { get; set; }
    public int UserId { get; set; }

    public List<int> equipmentIdList;

    public Role(int roleId, int userId)
    {
        RoleId = roleId;
        UserId = userId;
    }
}