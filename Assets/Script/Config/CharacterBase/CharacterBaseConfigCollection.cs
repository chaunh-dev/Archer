using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class CharacterBaseConfigData
{
	public int health;

	#region ConfigSpecData
	#endregion
}

public class CharacterBaseConfigCollection : ConfigBase<CharacterBaseConfigData>
{
	public override List<CharacterBaseConfigData> LoadDataFromText(string tx)
	{
		tx = tx.Replace("\r\n", "\n");
		string[] fLines = tx.Split('\n');
		if (fLines == null || fLines.Length < 3)
			return null;

		List<CharacterBaseConfigData> dataList = new List<CharacterBaseConfigData>();
		List<string> lines = new List<string>();
		for (int i = 2; i < fLines.Length; i++)
		{
			lines.Add(fLines[i]);
		}

		try
		{
			for (int i = 0; i < lines.Count; i++)
			{
				if (string.IsNullOrEmpty(lines[i]))
					continue;

				string[] values = lines[i].Split(',');

				CharacterBaseConfigData data = new CharacterBaseConfigData();
				int.TryParse(values[0].Trim(), out data.health);
				dataList.Add(data);
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Shit!Parse error! " + e.Message);
			return null;
		}

		return dataList;
	}

	public override void SetDataFromText(string tx)
	{
		records = LoadDataFromText(tx);
	}


	#region ConfigSpecCollection
	#endregion
}
