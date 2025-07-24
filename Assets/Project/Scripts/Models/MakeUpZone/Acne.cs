using UnityEngine;

public class Acne : MonoBehaviour, IMakedUpZone
{
    private Book _book;

    public void Setup(Book book)
    { 
        _book = book;
        Reset();
    }

    public bool TryAction(EntityDraggable entityDraggable)
    {
        if (entityDraggable as Cream)
        { 
            _book.SetAccess(true);
            transform.Hide();
            
            return true;
        }

        return false;
    }

    public void Reset()
        => transform.Show();
}
