using MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Json response manager.
/// </summary>
public class APIManager
{
    /// <summary>
    /// ログインAPI
    /// </summary>
    /// <param name="sStrJson"></param>
    /// <returns></returns>
    public static LoginResponse LoginDeserializeFromJson(string sStrJson)
    {

        IDictionary json = (IDictionary)Json.Deserialize(sStrJson);

        LoginResponse res = new LoginResponse();

        if (json.Contains("user_id") && json["user_id"] != null)
        {
            res.user_id = json["user_id"].ToString();
        }
        if (json.Contains("tutorial") && json["tutorial"] != null)
        {
            res.tutorial = Convert.ToInt32(json["tutorial"]);
        }

        return res;
    }
    /// <summary>
    /// ユーザー作成
    /// </summary>
    /// <param name="sStrJson"></param>
    /// <returns></returns>
    public static CreateUserResponse CreateUserDeserializeFromJson(string sStrJson)
    {

        IDictionary json = (IDictionary)Json.Deserialize(sStrJson);

        CreateUserResponse res = new CreateUserResponse();

        if (json.Contains("success") && json["success"] != null)
        {
            res.success = (bool)json["success"];
        }
        if (json.Contains("message") && json["message"] != null)
        {
            res.message = json["message"].ToString();
        }

        return res;
    }
    /// <summary>
    /// セーブデータ送信API
    /// </summary>
    /// <param name="sStrJson"></param>
    /// <returns></returns>
    public static SaveDataResponse SendSaveDataDeserializeFromJson(string sStrJson)
    {

        IDictionary json = (IDictionary)Json.Deserialize(sStrJson);

        SaveDataResponse res = new SaveDataResponse();

        if (json.Contains("success") && json["success"] != null)
        {
            res.success = (bool)json["success"];
        }

        return res;
    }

    /// <summary>
    /// ランキング取得API
    /// </summary>
    /// <param name="sStrJson"></param>
    /// <returns></returns>
    public static List<RankData> GetRankDeserializeFromJson(string sStrJson)
    {

        var ret = new List<RankData>();

        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト      
        IList jsonList = (IList)Json.Deserialize(sStrJson);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        int count = 0;
        foreach (IDictionary jsonOne in jsonList)
        {
            count++;
            //新レコード解析開始
            var tmp = new RankData();
            tmp.rank = count;

            if (jsonOne.Contains("user_name"))
            {
                tmp.user_name = (string)jsonOne["user_name"];
            }

            if (jsonOne.Contains("clear_time"))
            {
                tmp.clear_time = (string)jsonOne["clear_time"];
            }

            ret.Add(tmp);
        }
        return ret;
    }
    /// <summary>
    /// セーブデータ取得
    /// </summary>
    /// <param name="sStrJson"></param>
    /// <returns></returns>
    public static SaveStatusResponse GetSaveDataDeserializeFromJson(string sStrJson)
    {

        IDictionary json = (IDictionary)Json.Deserialize(sStrJson);

        SaveStatusResponse res = new SaveStatusResponse();

        if (json.Contains("tutorial") && json["tutorial"] != null)
        {
            res.tutorial = Convert.ToInt32(json["tutorial"]);
        }
        if (json.Contains("stage1") && json["stage1"] != null)
        {
            res.stage1 = Convert.ToInt32(json["stage1"]);
        }
        if (json.Contains("stage2") && json["stage2"] != null)
        {
            res.stage2 = Convert.ToInt32(json["stage2"]);
        }
        if (json.Contains("stage3") && json["stage3"] != null)
        {
            res.stage3 = Convert.ToInt32(json["stage3"]);
        }

        return res;
    }
}