using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Blocs
{
	public Bloc[] blocs;

	public Blocs(Bloc[] blocs)
	{
		this.blocs = blocs;
	}

	public int GetBlocNum()
	{
		return blocs.Length;
	}

	public Bloc GetBloc(int blocId)
	{
		for (int i = 0;i< blocs.Length; i++)
		{
			if (blocId == blocs[i].id)
			{
				return blocs[i];
			}
		}
		return null;
	}
}


[System.Serializable]
public class Bloc
{
	public int id;
	public string title;
	public int lord;
	public int adviser;
	public int color;

	public Bloc(int id, string title, int lord, int adviser, int color)
	{
		this.id = id;
		this.title = title;
		this.lord = lord;
		this.adviser = adviser;
		this.color = color;
	}
}
