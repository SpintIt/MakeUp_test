using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IEndDragHandler, IDragHandler
{
    private EntityDraggable _entityDraggable;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private SpriteRenderer _image;
    [SerializeField, Range(.1f, 3f)] private float _speedFade = .5f;

    private void OnEnable() 
        => Hide();

    public void Hide() 
        => _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);

    public void OnDrag(PointerEventData eventData) 
        => _rectTransform.anchoredPosition += eventData.delta;

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = Camera.main.WorldToScreenPoint(_entityDraggable.MakeUpPosition.position)
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerData, results);
        bool hasResult = false;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent<IMakedUpZone>(out var makeUpZone))
            {
                if (makeUpZone.TryAction(_entityDraggable))
                {
                    hasResult = true;
                    Hide();
                    _entityDraggable.FinalAnimation()
                        .SetBlocked();
                    break;
                }
            }
        }

        if (hasResult == false)
        {
            Hide();
            _entityDraggable.OnStart += MoveToEntity;
            _entityDraggable.MoveStartPosition();
        }
    }

    public void Catch(EntityDraggable entityDraggable)
    {
        Hide();
        
        if (_entityDraggable != null)
        {
            _entityDraggable.OnStart -= MoveToEntity;
            _entityDraggable.MoveBasePosition();
        }
        
        _entityDraggable = entityDraggable;
        _entityDraggable.OnStart += MoveToEntity;
    }

    public void MoveToEntity()
    {
        _entityDraggable.OnStart -= MoveToEntity;
        _image.DOFade(1, _speedFade);
        transform.position = _entityDraggable.CatchPosition.position;
        _entityDraggable.transform
            .With(item => item.SetParent(transform));
    }
}
