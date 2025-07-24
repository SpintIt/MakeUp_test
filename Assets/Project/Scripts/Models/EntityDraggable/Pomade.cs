using UnityEngine;
using UnityEngine.EventSystems;

public class Pomade : EntityDraggable, IPointerClickHandler
{
    [SerializeField] private Person _person;
    [field: SerializeField] public ColorType ColorType { get; private set; }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_person.Book.IsAccess)
        {
            _person.Hand.Catch(this);
            MoveStartPosition();
        }
    }
}
