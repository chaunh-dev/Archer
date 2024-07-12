using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class BotConfigData
{
	public float shotInterval;
	public float aimDurationMin;
	public float aimDurationMax;
	public float aimErrorness;

	#region ConfigSpecData
	#endregion
}

public class BotConfigCollection : ConfigBase<BotConfigData>
{
	public override List<BotConfigData> LoadDataFromText(string tx)
	{
		tx = tx.Replace("\r\n", "\n");
		string[] fLines = tx.Split('\n');
		if (fLines == null || fLines.Length < 3)
			return null;

		List<BotConfigData> dataList = new List<BotConfigData>();
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

				BotConfigData data = new BotConfigData();
				float.TryParse(values[0].Trim(), out data.shotInterval);
				float.TryParse(values[1].Trim(), out data.aimDurationMin);
				float.TryParse(values[2].Trim(), out data.aimDurationMax);
				float.TryParse(values[3].Trim(), out data.aimErrorness);
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
