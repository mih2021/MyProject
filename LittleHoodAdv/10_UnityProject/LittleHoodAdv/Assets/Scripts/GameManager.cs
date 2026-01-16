using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Player player;

    /// <summary>
    /// Stage2の帰った時のクリアポイントをアクティブにする
    /// </summary>
    [SerializeField] GameObject _clearPoint = null;

    [SerializeField] GameObject _clearText;
    [SerializeField] string openingEventId = "";

    [SerializeField] string _stageType = null;

    [SerializeField] GameTimer m_gameTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (openingEventId != "")
        {
            callEvent(openingEventId);
        } else
        {
            m_gameTimer.OnStart();
        }

        if (_clearPoint != null)
        {
            _clearPoint.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callEvent(string eventId)
    {
        switch (eventId)
        {
            // openingの最初
            case "event00":
                player.setIsPlay(false);
                dialogueManager.StartDialogue(eventId, () =>
                {
                    player.setIsPlay(true);
                    m_gameTimer.OnStart();
                });

                break;
            // tutorialのクリア
            case "event01":
                player.setIsPlay(false);
                m_gameTimer.OnStop();
                dialogueManager.StartDialogue(eventId, () =>
                {
                    StartCoroutine(AfterDialogueEvent());
                });

                break;
            // stage1のクリア
            case "event02":
                player.setIsPlay(false);
                m_gameTimer.OnStop();
                dialogueManager.StartDialogue(eventId, () =>
                {
                    StartCoroutine(AfterDialogueEvent());
                });
                break;
            // オオカミにぶつかる→スタート地点へ戻る
            case "event03":
                player.setIsPlay(false);
                m_gameTimer.OnStop();
                dialogueManager.StartDialogue(eventId, () =>
                {
                    player.setIsPlay(true);
                    m_gameTimer.OnStart();
                    _clearPoint.SetActive(true);
                });
                break;
            // stage2のクリア
            case "event05":
                player.setIsPlay(false);
                m_gameTimer.OnStop();
                dialogueManager.StartDialogue(eventId, () =>
                {
                    StartCoroutine(AfterDialogueEvent());
                });
                break;
            // stage3のクリア
            case "event04":
                player.setIsPlay(false);
                m_gameTimer.OnStop();
                dialogueManager.StartDialogue(eventId, () =>
                {
                    StartCoroutine(AfterDialogueEvent());
                });
                break;
            default:
                break;

        }
    }

    IEnumerator AfterDialogueEvent()
    {
        _clearText.SetActive(true);
        player.setIsPlay(true);

        // ここでクリアのタイマーを送る
        StartCoroutine(
            CallSendSaveDataAPI(
                CallbackCallSendSaveDataAPISuccess,
                CallbackCallSendSaveDataAPIFailed
            )
        );

       yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SelectMap");
    }

    #region sendSaveDataAPI
    /// <summary>セーブデータ送信API</summary>
    private IEnumerator CallSendSaveDataAPI(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", PlayerPrefs.GetString(Const.playerPrefsUserId));
        form.AddField("stageType", _stageType);
        form.AddField("clearTimeOriginal", m_gameTimer.CurrentTime.ToString());
        form.AddField("clearTimeH", ((int)m_gameTimer.CurrentTime / 60).ToString());
        form.AddField("clearTimeM", ((int)m_gameTimer.CurrentTime % 60).ToString());
        form.AddField("clearTimeS", ((int)(m_gameTimer.CurrentTime * 100) % 60).ToString());

        // WebRequest.POST通信
        UnityWebRequest www = UnityWebRequest.Post(Const.strUrl + "sendSaveData", form);
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
    private void CallbackCallSendSaveDataAPISuccess(string response)
    {
        // APIコールが成功したので、response にAPIからのレスポンス(JSON)が返ってきている。
        SaveDataResponse result = APIManager.SendSaveDataDeserializeFromJson(response);

        if (result.success)
        {
            Debug.Log("送信成功");
        }
        else
        {
            Debug.Log("送信失敗");
        }
    }
    /// <summary>
    /// Callbacks the www failed.
    /// APIコールが失敗した際に呼ばれる関数
    /// </summary>
    private void CallbackCallSendSaveDataAPIFailed()
    {
        Debug.LogError("API Error");
    }

    #endregion
}
