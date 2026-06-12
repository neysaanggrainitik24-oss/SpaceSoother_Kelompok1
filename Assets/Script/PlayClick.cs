using UnityEngine;

public class PlayClick : MonoBehaviour
{
    public UIManager uiManager;

    private void OnMouseDown()
    {
        uiManager.PlayGame();
    }
}