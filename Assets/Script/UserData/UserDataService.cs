using System.Collections;
using System.Collections.Generic;
using GFramework.GameData;
using UnityEngine;
using System.Runtime.InteropServices;
using GFramework.Utils;
using System;

public class UserDataService : UserDataServiceBase<UserDataService, UserData>
{
    private int userPlayedCount = -1;
    public int UserPlayedCount
    {
        get
        {
            if (userPlayedCount < 0)
            {
                userPlayedCount = PlayerPrefsHelper.GetInt("USER_PLAYED_COUNT", 0);
            }
            return userPlayedCount;
        }
        set
        {
            userPlayedCount = value;
            PlayerPrefsHelper.SetInt("USER_PLAYED_COUNT", value);
            PlayerPrefsHelper.Save();
        }
    }

    private DateTime? dateUserLogined;
    public double DayUserPlayed
    {
        get
        {
            double d = 0;

            if (!PlayerPrefsHelper.HasKey("DAY_USER_PLAYED"))
            {
                dateUserLogined = DateTime.UtcNow;
                PlayerPrefsHelper.SetString("DAY_USER_PLAYED", dateUserLogined.Value.ToShortDateString());
                PlayerPrefsHelper.Save();
            }

            if (dateUserLogined == null)
                dateUserLogined = DateTime.Parse(PlayerPrefsHelper.GetString("DAY_USER_PLAYED", DateTime.UtcNow.ToShortDateString()));

            d = DateTime.UtcNow.Subtract(dateUserLogined.Value).TotalDays;
            Debug.Log("====> DayUserPlayed:: " + d);
            return d;
        }
    }

    public override IEnumerator Init()
    {
        if (Gameplay.Instance.IsWebGLFlow)
        {
            yield return StartCoroutine(WebCommunication.instance.LoadData());

            if (string.IsNullOrEmpty(WebCommunication.instance.GetUserData()) == false)
            {
                userData = StorageDataBase.FromJSonString<UserData>(WebCommunication.instance.GetUserData());
            }

            if (userData == null)
            {
                CreateNewUserData();
                userData.SetDefault();
            }

            yield return new WaitForEndOfFrame();

            IsInited = true;
        }
        else
        {
            yield return StartCoroutine(base.Init());
        }
    }

    public override void Save(bool force = false)
    {
        if (Gameplay.Instance.IsWebGLFlow)
        {
            if (userData == null) return;
            if (isDirty == false && force == false) return;

            isDirty = false;

            var da = userData.ToJSonString<UserData>();
            WebCommunication.instance.SetUserData(da);
            WebCommunication.instance.SaveAllData();

            Debug.Log("-----> save! " + da);
        }
        else
        {
            base.Save(force);
        }
    }
}
