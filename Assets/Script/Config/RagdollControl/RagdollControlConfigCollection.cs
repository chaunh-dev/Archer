using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class RagdollControlConfigData
{
	public float bowControlBaseVelocity;
	public float bowControlAcceleration;
	public float bowControlDampening;
	public int fireForceMin;
	public int fireForceMax;
	public float arrowPullDuration;
	public float arrowDrag;

	#region ConfigSpecData
	#endregion
}

public class RagdollControlConfigCollection : ConfigBase<RagdollControlConfigData>
{
	public override List<RagdollControlConfigData> LoadDataFromText(string tx)
	{
		tx = tx.Replace("\r\n", "\n");
		string[] fLines = tx.Split('\n');
		if (fLines == null || fLines.Length < 3)
			return null;

		List<RagdollControlConfigData> dataList = new List<RagdollControlConfigData>();
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

				RagdollControlConfigData data = new RagdollControlConfigData();
				float.TryParse(values[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out data.bowControlBaseVelocity);
				float.TryParse(values[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out data.bowControlAcceleration);
				float.TryParse(values[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out data.bowControlDampening);
				int.TryParse(values[3].Trim(), out data.fireForceMin);
				int.TryParse(values[4].Trim(), out data.fireForceMax);
				float.TryParse(values[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out data.arrowPullDuration);
				float.TryParse(values[6].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out data.arrowDrag);
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
