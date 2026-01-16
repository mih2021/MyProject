using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class TitleGameManager : MonoBehaviour
{
    /// <summary>
    /// ボタン
    /// </summary>
    [SerializeField] Button[] _buttons;
    /// <summary>
    /// パネル
    /// </summary>
    [SerializeField] GameObject _panelLogin = default;
    /// <summary>
    /// 名前入力
    /// </summary>
    [SerializeField] TextMeshProUGUI _input = default;
    /// <summary>
    /// 名前表示
    /// </summary>
    [SerializeField] TextMeshProUGUI _username = default;
    /// <summary>
    /// メッセージ
    /// </summary>
    [SerializeField] TextMeshProUGUI _message = default;
    /// <summary>
    /// パネルメッセージ
    /// </summary>
    [SerializeField] TextMeshProUGUI _errorMessage = default;

    /// <summary>
    /// ボタンのEnum
    /// </summary>
    private enum BTN
    {
        Account = 0,
        NewGame,
        LoadGame,
        Ranking,
        Credit,
        Create,
        Login,
        Close,
        Delete
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set button action
        _buttons[(int)BTN.Account].onClick.AddListener(OnClickAccount);
        _buttons[(int)BTN.NewGame].onClick.AddListener(OnClickNewGame);
        _buttons[(int)BTN.LoadGame].onClick.AddListener(OnClickLoadGame);
        _buttons[(int)BTN.Ranking].onClick.AddListener(OnClickRanking);
        _buttons[(int)BTN.Credit].onClick.AddListener(OnClickCredit);
        _buttons[(int)BTN.Create].onClick.AddListener(OnClickCreate);
        _buttons[(int)BTN.Login].onClick.AddListener(OnClickLogin);
        _buttons[(int)BTN.Close].onClick.AddListener(OnClickClose);
        _buttons[(int)BTN.Delete].onClick.AddListener(OnClickDelete);

        _panelLogin.SetActive(false);

        // すでにPlayerPrefsが存在する場合
        if (PlayerPrefs.HasKey(Const.playerPrefsUserId))
        {
            _input.text = PlayerPrefs.GetString(Const.playerPrefsUserName);
            StartCoroutine(
                CallLoginAPI(
                    CallbackCallLoginAPISuccess,
                    CallbackCallLoginAPIFailed
                )
            );
        }
    }

    /// <summary>
    /// アカウントボタンのクリック
    /// </summary>
    private void OnClickAccount()
    {
        _panelLogin.SetActive(true);
    }
    /// <summary>
    /// アカウントボタンのクリック
    /// </summary>
    private void OnClickClose()
    {
        _panelLogin.SetActive(false);
    }
    /// <summary>
    /// ログイン
    /// </summary>
    private void OnClickLogin()
    {
        if (_input.text == "")
        {
            return;
        }
        StartCoroutine(
            CallLoginAPI(
                CallbackCallLoginAPISuccess,
                CallbackCallLoginAPIFailed
            )
        );

    }
    /// <summary>
    /// 登録
    /// </summary>
    private void OnClickCreate()
    {
        if (_input.text == "")
        {
            return;
        }
        StartCoroutine(
            CallCreateUserAPI(
                CallbackCallCreateUserAPISuccess
                , CallbackCallCreateUserAPIFailed
            )
        );
    }
    /// <summary>
    /// 削除
    /// </summary>
    private void OnClickDelete()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ニューゲームボタンのクリック
    /// </summary>
    private void OnClickNewGame()
    {
        if (!PlayerPrefs.HasKey(Const.playerPrefsUserId))
        {
            _message.text = "ログインしていません。";
            return;
        }
        if (PlayerPrefs.HasKey(Const.playerPrefsTutorial) && PlayerPrefs.GetInt(Const.playerPrefsTutorial) > 0)
        {
            _message.text = "データが存在します。ロードゲームからはじめてください。";
            return;
        }
        // チュートリアルシーンへ移行する
        SceneManager.LoadScene("Opening");
    }

    /// <summary>
    /// ロードゲームボタンのクリック
    /// </summary>
    private void OnClickLoadGame()
    {
        if (!PlayerPrefs.HasKey(Const.playerPrefsUserId))
        {
            _message.text = "ログインしていません。";
            return;
        }
        if (PlayerPrefs.HasKey(Const.playerPrefsTutorial) && PlayerPrefs.GetInt(Const.playerPrefsTutorial) == 0)
        {
            _message.text = "データが存在しません。ニューゲームからはじめてください。";
            return;
        }
        // マップ選択画面へ移動する
        SceneManager.LoadScene("SelectMap");
    }

    /// <summary>
    /// ランキングボタンのクリック
    /// </summary>
    private void OnClickRanking()
    {
        // ランキングシーンへ移動する
        SceneManager.LoadScene("Ranking");
    }
    /// <summary>
    /// クレジットボタンのクリック
    /// </summary>
    private void OnClickCredit()
    {
        // クレジットシーンへ移動する
        SceneManager.LoadScene("Credit");
    }

    #region loginAPI
    /// <summary>ログインAPI</summary>
    private IEnumerator CallLoginAPI(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // form にフィールドを追加する

        WWWForm form = new WWWForm();
        form.AddField("userName", _input.text);

        // WebRequest.POST通信
        UnityWebRequest www = UnityWebRequest.Post(Const.strUrl + "loginUser", form);
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
    private void CallbackCallLoginAPISuccess(string response)
    {
        // APIコールが成功したので、response にAPIからのレスポンス(JSON)が返ってきている。
        LoginResponse result = APIManager.LoginDeserializeFromJson(response);

        // user_idが存在しない場合
        string userId = result.user_id;

        if(userId != "")
        {
            PlayerPrefs.SetString(Const.playerPrefsUserId, userId);
            PlayerPrefs.SetString(Const.playerPrefsUserName, _input.text);
            PlayerPrefs.SetInt(Const.playerPrefsTutorial, result.tutorial);
            _username.text = PlayerPrefs.GetString(Const.playerPrefsUserName) + " (ログイン済)";
            _panelLogin.SetActive(false);

        } else
        {
            _errorMessage.text = "入力されたユーザーは存在しません。";
        }        
    }
    /// <summary>
    /// Callbacks the www failed.
    /// APIコールが失敗した際に呼ばれる関数
    /// </summary>
    private void CallbackCallLoginAPIFailed()
    {
        Debug.LogError("API Error");
    }

    #endregion
    #region createUserAPI
    /// <summary>ログインAPI</summary>
    private IEnumerator CallCreateUserAPI(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // form にフィールドを追加する

        WWWForm form = new WWWForm();
        Debug.Log(_input.text);
        form.AddField("userName", _input.text);

        // WebRequest.POST通信
        UnityWebRequest www = UnityWebRequest.Post(Const.strUrl + "createUser", form);
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
    private void CallbackCallCreateUserAPISuccess(string response)
    {
        // APIコールが成功したので、response にAPIからのレスポンス(JSON)が返ってきている。
        CreateUserResponse result = APIManager.CreateUserDeserializeFromJson(response);

        if (result.success)
        {
            // 成功したらそのままログインAPIを実行
            StartCoroutine(
            CallLoginAPI(
                CallbackCallLoginAPISuccess,
                CallbackCallLoginAPIFailed
            )
        );
        }
        else
        {
            _errorMessage.text = result.message;
        }
    }
    /// <summary>
    /// Callbacks the www failed.
    /// APIコールが失敗した際に呼ばれる関数
    /// </summary>
    private void CallbackCallCreateUserAPIFailed()
    {
        Debug.LogError("API Error");
    }
    #endregion
}
