using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class ArrowStatsConfigData
{
	public string ID;
	public int damageToHead;
	public int damageToNeck;
	public int damageToArm;
	public int damageToLeg;
	public int damageToBody;
	public int forceToHead;
	public int forceToNeck;
	public int forceToArm;
	public int forceToLeg;
	public int forceToBody;

	#region ConfigSpecData
	#endregion
}

public class ArrowStatsConfigCollection : ConfigBase<ArrowStatsConfigData>
{
	public override List<ArrowStatsConfigData> LoadDataFromText(string tx)
	{
		tx = tx.Replace("\r\n", "\n");
		string[] fLines = tx.Split('\n');
		if (fLines == null || fLines.Length < 3)
			return null;

		List<ArrowStatsConfigData> dataList = new List<ArrowStatsConfigData>();
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

				ArrowStatsConfigData data = new ArrowStatsConfigData();
				data.ID = values[0].Trim();
				int.TryParse(values[1].Trim(), out data.damageToHead);
				int.TryParse(values[2].Trim(), out data.damageToNeck);
				int.TryParse(values[3].Trim(), out data.damageToArm);
				int.TryParse(values[4].Trim(), out data.damageToLeg);
				int.TryParse(values[5].Trim(), out data.damageToBody);
				int.TryParse(values[6].Trim(), out data.forceToHead);
				int.TryParse(values[7].Trim(), out data.forceToNeck);
				int.TryParse(values[8].Trim(), out data.forceToArm);
				int.TryParse(values[9].Trim(), out data.forceToLeg);
				int.TryParse(values[10].Trim(), out data.forceToBody);
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
