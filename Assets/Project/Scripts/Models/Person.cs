using System.IO;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Book _book;

    [SerializeField] private Cream _cream;

    [Header("Zones")]
    [SerializeField] private Acne _acneZone;
    [SerializeField] private Blush _blushZone;
    [SerializeField] private Blush _eyeShadowZone;
    [SerializeField] private Blush _pomadeZone;


    [Header("Brushes")]
    [SerializeField] private Brush _blushBrush;
    [SerializeField] private Brush _eyeShadowBrush;

    public Book Book => _book;

    [field: SerializeField] public Hand Hand { get; private set; }

    public void Setup(Book book)
    {
        _book = book;

        _acneZone.Setup(_book);
        _blushZone.Setup();
        _eyeShadowZone.Setup();
        _pomadeZone.Setup();
        _cream.Setup(Hand);
        _blushBrush.Setup(Hand);
        _eyeShadowBrush.Setup(Hand);
    }

    public void Reset()
    {
        _acneZone.Reset();
        _cream.Reset();
        _blushZone.Reset();
        _eyeShadowZone.Reset();
        _pomadeZone.Reset();
        _book.SetAccess(false);
    }
}
