using System;
using UnityEngine;

[CreateAssetMenu]
public class TresureLocation : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField] public float initialLatitute;
    [SerializeField] public float initialLongitude;
    [SerializeField] public string initialDetails;


    [NonSerialized] public float latitude;
    [NonSerialized] public float longitude;
    [NonSerialized] public string details;



    public void OnAfterDeserialize()
    {
        latitude = initialLatitute;
        longitude = initialLongitude;
        details = initialDetails;

    }

    public void OnBeforeSerialize() { }
}

