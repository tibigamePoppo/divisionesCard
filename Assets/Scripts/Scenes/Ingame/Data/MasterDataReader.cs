using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDataReader : MonoBehaviour
{
    [SerializeField,TextArea] private string _masterData;
    public static MasterDataReader Instance;
    private Master _master;
    public Master Master {  get { return _master; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(this);
        }
    }
    private void Init()
    {
        _master = JsonUtility.FromJson<Master>(_masterData);
    }
}
