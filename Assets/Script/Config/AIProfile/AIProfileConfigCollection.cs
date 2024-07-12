using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class AIProfileConfigData
{
    public string ID;
    public string difficulty;
    public float waitUntilShoot;
    public float accurateChance;
    public float headHitChance;
    public float bodyHitChance;
    public float legsHitChance;

    #region ConfigSpecData
    #endregion
}

public class AIProfileConfigCollection : ConfigBase<AIProfileConfigData>
{
    public override List<AIProfileConfigData> LoadDataFromText(string tx)
    {
        tx = tx.Replace("\r\n", "\n");
        string[] fLines = tx.Split('\n');
        if (fLines == null || fLines.Length < 3)
            return null;

        List<AIProfileConfigData> dataList = new List<AIProfileConfigData>();
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

                AIProfileConfigData data = new AIProfileConfigData();
                data.ID = values[0].Trim();
                data.difficulty = values[1].Trim();
                float.TryParse(values[2].Trim(), out data.waitUntilShoot);
                float.TryParse(values[3].Trim(), out data.accurateChance);
                float.TryParse(values[4].Trim(), out data.headHitChance);
                float.TryParse(values[5].Trim(), out data.bodyHitChance);
                float.TryParse(values[6].Trim(), out data.legsHitChance);

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
