using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DigManager : MonoBehaviour
{
    [SerializeField] private TresureLocation _tresureLocation = default;


    public Otemo _otemo;
    public BoxScript _box;
    public TextMeshProUGUI _gpsText;

    public int _locationCount = 0;
    protected bool _isDig = true;


    //[SerializeField] TextAsset _textFile;
    //[SerializeField] public TextMeshProUGUI _mainTextObject;
    protected IEnumerator _gps;

    // Start is called before the first frame update
    void Start()
    {
        //this._gpsText.text = "Hello World!!!!";

        Debug.Log("DigManager::Start() : enter *******");


        Debug.Log("DATA Lat: " + _tresureLocation.latitude);
        Debug.Log("DATA Lon: " + _tresureLocation.longitude);

        this._locationCount = 0;
        this._isDig = true;


        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location Disabled.....");
            //return false;
        }

        Input.location.Start();
        Debug.Log(Input.location.status);

        _gps = this.getLocation();

        this.StartCoroutine(_gps);

    }


    // Update is called once per frame
    void Update()
    {
        //

    }

    public IEnumerator getLocation()
    {
        while (this._isDig)
        {

            Debug.Log("DigManager::getLocation() : " + this._locationCount);
            string location = _locationCount + "\n";


            if (Input.location.status == LocationServiceStatus.Running)
            {

                this._locationCount++;

                float distance = this.getDistance(Input.location.lastData.latitude, Input.location.lastData.longitude);


                location += "Location: \n"
                    + "  *distance* : " + distance + "\n"
                    + "  latitude:" + Input.location.lastData.latitude + "\n"
                    + "  longitude:" + Input.location.lastData.longitude + "\n"
                    + "  altitude:" + Input.location.lastData.altitude + "\n"
                    + "  horizontalAccuracy:" + Input.location.lastData.horizontalAccuracy + "\n"
                    + "  timestamp:" + Input.location.lastData.timestamp;

                this._gpsText.text = location;
                Debug.Log(location);

                if (this._locationCount >= 3)
                {
                    //if (Input.location.lastData.horizontalAccuracy <= 20.0f)
                    {
                        if (distance <= 15.0f)
                        {
                            this._isDig = false;
                            this._box.onShow();
                            this._otemo.onHit();
                            break;

                        }

                    }

                }
                if (this._locationCount >= 5)
                {
                    Debug.Log("FAILED!!!");
                    this._isDig = false;
                    this._otemo.onFailed();
                    break;
                }


            }
            else
            {
                this._gpsText.text = location + "disabled...";


                ////////////////////////
                //// TEST
                //this._locationCount++;
                //if (_locationCount >= 10)
                //{
                //    this._isDig = false;
                //    this._box.onShow();
                //    this._otemo.onHit();
                //    break;
                //}
                //if (this._locationCount >= 2)
                //{
                //    this._isDig = false;
                //    this._otemo.onFailed();
                //    break;
                //}
                ////////////////////////
            }



            yield return new WaitForSeconds(1.0f);
        }

    }


    protected float getDistance(float latitude, float longitude)
    {
        const float laToM = 0.000008983148616f;
        const float loToM = 0.000010966382364f;


        float diffLa = (latitude - this._tresureLocation.latitude) / laToM;
        float diffLo = (longitude - this._tresureLocation.longitude) / loToM;


        float distance = Mathf.Sqrt(Mathf.Pow(diffLa, 2) + Mathf.Pow(diffLo, 2));

        return distance;
    }

}
