using UnityEngine;

public abstract class Panels : MonoBehaviour
{
    public void OpenPanel(Panels panel)
    {
        panel.gameObject.SetActive(true);
        gameObject.SetActive(false);
        UpdateInfo();
    }

    protected virtual void UpdateInfo() { }
}