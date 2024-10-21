using TMPro;
using UnityEngine;

public class MessengePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const string TAKE_ITEM = "����� F";
    private const string PUT_ITEM = "�������� G";

    public void Take()
    {
        gameObject.SetActive(true);
        _text.text = TAKE_ITEM;
    }

    public void Put()
    {
        gameObject.SetActive(true);
        _text.text = PUT_ITEM;
    }

    public void HideMessengePanel()
    {
        gameObject.SetActive(false);
    }
}
