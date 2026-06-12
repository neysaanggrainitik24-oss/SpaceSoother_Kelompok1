using UnityEngine;

public class SistemBacksound : MonoBehaviour
{
    public static SistemBacksound instance;
    public AudioSource SuaraMusik;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}