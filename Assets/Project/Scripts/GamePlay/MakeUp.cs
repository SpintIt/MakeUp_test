using UnityEngine;

public class MakeUp : MonoBehaviour
{
    [SerializeField] private Person _person;
    [SerializeField] private Book _book;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        _book.Setup(_person.Hand);
        _person.Setup(_book);
    }
}
