using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SelectMapGameManager : MonoBehaviour
{
    [Header("Stars")][SerializeField] private StarController[] _stars = default;
    private enum STG
    {
        Stage1 = 0,
        Stage2 = 1,
        Stage3 = 2,
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // セーブ状態を取得する
        StartCoroutine(
            CallGetSaveDataAPI(
                CallbackCallGetSaveDataAPISuccess
                , CallbackCallGetSaveDataAPIFailed
            )
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region GetSaveDataAPI
    /// <summary>セーブデータ取得API</summary>
    private IEnumerator CallGetSaveDataAPI(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // form にフィールドを追加する

        WWWForm form = new WWWForm();
        form.AddField("userId", PlayerPrefs.GetString(Const.playerPrefsUserId));

        // WebRequest.POST通信
        UnityWebRequest www = UnityWebRequest.Post(Const.strUrl + "getSaveData", form);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(www.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else
       if (www.isDone)
        {
            // リクエスト成功の場合
            Debug.Log(string.Format("Success:{0}", www.downloadHandler.text));
            if (null != cbkSuccess)
            {
                cbkSuccess(www.downloadHandler.text);
            }
        }
    }
    /// <summary>
    /// Callbacks the www success.
    /// APIコールが成功した際に呼ばれる関数
    /// </summary>
    /// <param name="response">Response.</param>
    private void CallbackCallGetSaveDataAPISuccess(string response)
    {
        // APIコールが成功したので、response にAPIからのレスポンス(JSON)が返ってきている。
        SaveStatusResponse result = APIManager.GetSaveDataDeserializeFromJson(response);
        PlayerPrefs.SetInt(Const.playerPrefsTutorial, result.tutorial);
        PlayerPrefs.SetInt(Const.playerPrefsStage1, result.stage1);
        PlayerPrefs.SetInt(Const.playerPrefsStage2, result.stage2);
        PlayerPrefs.SetInt(Const.playerPrefsStage3, result.stage3);

        // Setしたものから☆の状態をつける
        _stars[(int)STG.Stage1].SetWhite(result.stage1 == 0);
        _stars[(int)STG.Stage2].SetWhite(result.stage2 == 0);
        _stars[(int)STG.Stage3].SetWhite(result.stage3 == 0);
    }
    /// <summary>
    /// Callbacks the www failed.
    /// APIコールが失敗した際に呼ばれる関数
    /// </summary>
    private void CallbackCallGetSaveDataAPIFailed()
    {
        Debug.LogError("API Error");
    }

    #endregion
}
