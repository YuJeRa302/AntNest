using UnityEngine;

public class DialogPanel : MonoBehaviour
{
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}