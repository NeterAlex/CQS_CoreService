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