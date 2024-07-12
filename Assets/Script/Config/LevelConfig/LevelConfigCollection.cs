using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region ConfigUsingAndStarting
#endregion

[System.Serializable]
public class LevelConfigData
{
    public int level;
    public int singleBot, doubleBot, trippleBot, verticalMovingBot, horizontalMovingBot, blinkBot;
    public int botHealth, bossHealth;
    public string botAIProfile, bossAIProfile;
    public bool bossSkin, botHeadArmor, botChestArmor, botLegArmor;
    public float time;
    #region ConfigSpecData
    #endregion
}

public class LevelConfigCollection : ConfigBase<LevelConfigData>
{
    public override List<LevelConfigData> LoadDataFromText(string textAsset)
    {
        textAsset = textAsset.Replace("\r\n", "\n");
        string[] fLines = textAsset.Split('\n');
        if (fLines == null || fLines.Length < 3)
            return null;

        List<LevelConfigData> dataList = new List<LevelConfigData>();
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

                LevelConfigData data = new LevelConfigData();
                int.TryParse(values[0].Trim(), out data.level);
                int.TryParse(values[1].Trim(), out data.singleBot);
                int.TryParse(values[2].Trim(), out data.doubleBot);
                int.TryParse(values[3].Trim(), out data.trippleBot);
                int.TryParse(values[4].Trim(), out data.verticalMovingBot);
                int.TryParse(values[5].Trim(), out data.horizontalMovingBot);
                int.TryParse(values[6].Trim(), out data.blinkBot);
                int.TryParse(values[7].Trim(), out data.botHealth);
                int.TryParse(values[8].Trim(), out data.bossHealth);
                data.bossSkin = int.Parse(values[9].Trim()) == 1;
                data.botAIProfile = values[10].Trim();
                data.bossAIProfile = values[11].Trim();
                data.botHeadArmor = int.Parse(values[12].Trim()) == 1;
                data.botChestArmor = int.Parse(values[13].Trim()) == 1;
                data.botLegArmor = int.Parse(values[14].Trim()) == 1;
                float.TryParse(values[15].Trim(), out data.time);
                dataList.Add(data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Parse error! " + e.Message);
            return null;
        }

        return dataList;
    }

    public override void SetDataFromText(string textAsset)
    {
        records = LoadDataFromText(textAsset);
    }


    #region ConfigSpecCollection
    #endregion
}