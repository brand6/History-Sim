using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Roles
{
	public Role[] roles;

	public Roles(Role[] roles)
	{
		this.roles = roles;
	}

	public int  GetRoleNum()
	{
		return roles.Length;
	}

	public Role GetRole(int roleId)
	{
		for(int i = 0; i < roles.Length; i++)
		{
			if (roleId == roles[i].id)
			{
				return roles[i];
			}
		}
		return null;
	}
}

[System.Serializable]
public class Role
{
	public int id;
	public string name;
	public string familyName;
	public string nickName;

	public Role(int id, string name, string familyName, string nickName)
	{
		this.id = id;
		this.name = name;
		this.familyName = familyName;
		this.nickName = nickName;
	}

}
