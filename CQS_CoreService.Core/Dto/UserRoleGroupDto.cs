using System.Collections.Generic;

namespace CQS_CoreService.Core.Dto;

public class UserRoleDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
}

public class UserGroupDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public List<int> Admin { get; set; }
}

public class UserRoleListItem
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
}

public class UserRoleListDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public List<UserRoleListItem> Users { get; set; }
}