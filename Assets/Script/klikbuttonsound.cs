using UnityEngine;

public class KlikSoundButton : MonoBehaviour
{
    public GameObject BtnOn;
    public GameObject BtnOff;

    private void OnMouseDown()
    {
        if (gameObject == BtnOn)
        {
            SistemBacksound.instance.SuaraMusik.Pause();
            BtnOff.SetActive(true);
            BtnOn.SetActive(false);
        }
        else if (gameObject == BtnOff)
        {
            SistemBacksound.instance.SuaraMusik.UnPause();
            BtnOff.SetActive(false);
            BtnOn.SetActive(true);
        }
    }
}