using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Web : MonoBehaviour
{
    [SerializeField] private TresureLocation _tresureLocation = default;
    [SerializeField] public TextMeshProUGUI _gpsTitle;
    [SerializeField] public TextMeshProUGUI _gpsText;



    [Serializable]
    private sealed class TresurePoint
    {
        //public int id = 0;
        public string title;
        public string details;
        public float latitude;
        public float longitude;
    }


    private IEnumerator Start()
    {
        // sato : 35.409526440038384, 139.58180702923255
        this._tresureLocation.latitude = 35.409526440038384f;
        this._tresureLocation.longitude = 139.58180702923255f;

        // base : 35.40959143413617, 139.58512153244988
        //this._tresureLocation.latitude = 35.40959143413617f;
        //this._tresureLocation.longitude = 139.58512153244988f;


        var request = UnityWebRequest.Get("https://locator.kysaeed.com/api/point/1");

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                Debug.Log(request.downloadHandler.text);
                this.parseResponse(request.downloadHandler.text);
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log
                (
                    @"サーバとの通信に失敗。
リクエストが接続できなかった、
セキュリティで保護されたチャネルを確立できなかったなど。"
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log
                (
                    @"サーバがエラー応答を返した。
サーバとの通信には成功したが、
接続プロトコルで定義されているエラーを受け取った。"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log
                (
                    @"データの処理中にエラーが発生。
リクエストはサーバとの通信に成功したが、
受信したデータの処理中にエラーが発生。
データが破損しているか、正しい形式ではないなど。"
                );
                break;

            default: throw new ArgumentOutOfRangeException();
        }
    }

    protected void parseResponse(string responseText)
    {
        TresurePoint point = JsonUtility.FromJson<TresurePoint>(responseText);

        Debug.Log(String.Format("get: {0} {1} ,{2}", point.title, point.latitude, point.longitude));
        Debug.Log(point.details);

        this._tresureLocation.latitude = point.latitude;
        this._tresureLocation.longitude = point.longitude;

        this._gpsTitle.text = point.title;
        this._gpsText.text = point.details;

    }
}
