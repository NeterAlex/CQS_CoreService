using System;
using System.Collections.Generic;
using CQS_CoreService.Core.Entity;

namespace CQS_CoreService.Core.Dto;

public class UserDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public string State { get; set; }
    public string IdNumber { get; set; }
    public string Username { get; set; }
    public List<UserRoleEntity> Roles { get; set; }
    public List<UserGroupEntity> UserGroups { get; set; }
}

public class UserSimpleDto
{
    public int Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public string State { get; set; }
    public string IdNumber { get; set; }
    public string Username { get; set; }
    public List<UserRoleEntity> Roles { get; set; }
    public List<UserGroupEntity> UserGroups { get; set; }
}