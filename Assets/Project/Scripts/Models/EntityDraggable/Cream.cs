using UnityEngine.EventSystems;

public class Cream : EntityDraggable, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsBlocked != true)
        {
            Hand.Catch(this);
            MoveStartPosition();
        }
    }
}
