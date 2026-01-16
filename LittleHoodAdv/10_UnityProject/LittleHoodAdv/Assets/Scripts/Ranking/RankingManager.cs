using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    // ボタン
    private enum BTN
    {
        Stage1 = 0,
        Stage2,
        Stage3,
    }
    [SerializeField] Button[] _buttons = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private GameObject _itemPrefab = default;

    private Color selectedColor = Color.red;
    private Color normalColor = Color.gray;

    // コンテント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // stage1のボタンを押した状態にする
        OnClickButtonStage1();

        // それぞれのボタンのアクション
        _buttons[(int)BTN.Stage1].onClick.AddListener(OnClickButtonStage1);
        _buttons[(int)BTN.Stage2].onClick.AddListener(OnClickButtonStage2);
        _buttons[(int)BTN.Stage3].onClick.AddListener(OnClickButtonStage3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickButtonStage1()
    {
        ColorInit();
        SetButtonColor(_buttons[(int)BTN.Stage1], selectedColor);
        OnClickButton("stage1");
    }
    private void OnClickButtonStage2()
    {
        ColorInit();
        SetButtonColor(_buttons[(int)BTN.Stage2], selectedColor);
        OnClickButton("stage2");
    }
    private void OnClickButtonStage3()
    {
        ColorInit();
        SetButtonColor(_buttons[(int)BTN.Stage3], selectedColor);
        OnClickButton("stage3");
    }
    void ColorInit()
    {
        // 全部グレーで初期化
        foreach (var btn in _buttons)
        {
            SetButtonColor(btn, normalColor);
        }
    }
    private void SetButtonColor(Button button, Color color)
    {
        // Imageの色を変更
        button.image.color = color;
    }
    private void OnClickButton(string stageType = "stage1")
    {
        StartCoroutine(
            CallGetRankAPI(
                CallbackCallGetRankAPISuccess,
                CallbackCallGetRankAPIFailed,
                stageType
            )
        );
    }

    private void MakeList(List<RankData> rankDatas)
    {
        ClearList();
        for (int index = 0; index < rankDatas.Count; index++)
        {
            RankData item = rankDatas[index];

            GameObject itemObject = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
            itemObject.transform.SetParent(_content.transform);

            foreach (Transform child in itemObject.transform)
            {
                if (child.name == "Rank")
                {
                    child.GetComponent<TextMeshProUGUI>().text = (index + 1).ToString();
                }
                if (child.name == "Name")
                {
                    child.GetComponent<TextMeshProUGUI>().text = item.user_name;
                }
                if (child.name == "ClearTime")
                {
                    child.GetComponent<TextMeshProUGUI>().text = item.clear_time;
                }
            }
        }
    }
    public void ClearList()
    {
        int count = 0;
        foreach (Transform child in _content.transform)
        {
            if (count == 0)
            {
                count++;
                continue;
            }
            Destroy(child.gameObject);
        }
    }

    #region getRankAPI
    /// <summary>セーブデータ送信API</summary>
    private IEnumerator CallGetRankAPI(Action<string> cbkSuccess = null, Action cbkFailed = null, string stageType = "stage1")
    {
        // form にフィールドを追加する

        WWWForm form = new WWWForm();
        form.AddField("stageType", stageType);


        // WebRequest.POST通信
        UnityWebRequest www = UnityWebRequest.Post(Const.strUrl + "getRank", form);
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
    private void CallbackCallGetRankAPISuccess(string response)
    {
        // APIコールが成功したので、response にAPIからのレスポンス(JSON)が返ってきている。
        List<RankData> result = APIManager.GetRankDeserializeFromJson(response);
        MakeList(result);
    }
    /// <summary>
    /// Callbacks the www failed.
    /// APIコールが失敗した際に呼ばれる関数
    /// </summary>
    private void CallbackCallGetRankAPIFailed()
    {
        Debug.LogError("API Error");
    }

    #endregion
}
