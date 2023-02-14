using UnityEngine;

public class PauseSwitch : MonoBehaviour
{
    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            AudioListener.pause = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            AudioListener.pause = true;
            Time.timeScale = 0.0f;
        }
    }
}
