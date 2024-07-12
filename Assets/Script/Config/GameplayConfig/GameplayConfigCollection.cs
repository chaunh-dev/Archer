using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class GameplayConfigData
{
	public int cameraSize;

	#region ConfigSpecData
	#endregion
}

public class GameplayConfigCollection : ConfigBase<GameplayConfigData>
{
	public override List<GameplayConfigData> LoadDataFromText(string tx)
	{
		tx = tx.Replace("\r\n", "\n");
		string[] fLines = tx.Split('\n');
		if (fLines == null || fLines.Length < 3)
			return null;

		List<GameplayConfigData> dataList = new List<GameplayConfigData>();
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

				GameplayConfigData data = new GameplayConfigData();
				int.TryParse(values[0].Trim(), out data.cameraSize);
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
