using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorMakeUp : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color _color;
    [SerializeField] private ColorType _colorType;
    [SerializeField] private Person _person;

    private Brush Brush => _person.Book.OpenedSheet.Brush;
    private Hand Hand => _person.Hand;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_person.Book.IsAccess)
        {
            Hand.Hide();
            Brush.MoveToPosition(transform.position, () => {
                Hand.Catch(Brush);
                Brush.SetColor(_colorType)
                    .Tassel.SetColor(_color);
                Brush.MoveStartPosition();
            });
        }
    }
}
