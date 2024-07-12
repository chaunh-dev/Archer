using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class CharacterStatsConfigData
{
    public string Stats;
    public int Default;
    public int StatIncrease;
    public string decriptision;

    #region ConfigSpecData
    #endregion
}

public class CharacterStatsConfigCollection : ConfigBase<CharacterStatsConfigData>
{
    public override List<CharacterStatsConfigData> LoadDataFromText(string tx)
    {
        tx = tx.Replace("\r\n", "\n");
        string[] fLines = tx.Split('\n');
        if (fLines == null || fLines.Length < 3)
            return null;

        List<CharacterStatsConfigData> dataList = new List<CharacterStatsConfigData>();
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

                CharacterStatsConfigData data = new CharacterStatsConfigData();
                data.Stats = values[0].Trim();
                int.TryParse(values[1].Trim(), out data.Default);
                int.TryParse(values[2].Trim(), out data.StatIncrease);
                data.decriptision = values[3].Trim();
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
