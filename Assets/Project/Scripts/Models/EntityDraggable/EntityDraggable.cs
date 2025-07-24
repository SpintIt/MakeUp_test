using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityDraggable : MonoBehaviour
{
    private Tweener _tweener;

    [field: SerializeField] public Transform CatchPosition { get; private set; }
    [field: SerializeField] public Transform MakeUpPosition { get; private set; }
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private Transform _basePosition;
    [SerializeField] private Transform _startMovePosition;
    [SerializeField, Range(.1f, 3f)] private float _speedMove = .5f;

    [Header("Final Animation")]
    [SerializeField] private bool _isFinalAnimate;
    [SerializeField] private Transform _startAnimatePosition;
    [SerializeField] private Transform _finishAnimatePosition;
    [SerializeField, Range(1, 5)] private int _countAnimate;
    [SerializeField, Range(.1f, 1)] private float _speedAnimate;

    public bool IsBlocked { get; private set; }
    public bool IsReady { get; private set; }
    public Hand Hand { get; private set; }

    public event UnityAction OnStart;

    private void OnDisable()
        => _tweener?.Kill();

    public void Setup(Hand hand)
        => Hand = hand;

    public void SetBlocked()
        => IsBlocked = true;

    public EntityDraggable MoveStartPosition()
    {
        IsBlocked = true;
        transform.SetParent(_parentTransform);
        _tweener?.Kill();
        _tweener = transform.DOMove(_startMovePosition.position, _speedMove).OnComplete(() => {
            IsBlocked = false;
            IsReady = true;
            OnStart?.Invoke();
        });

        return this;
    }

    public EntityDraggable FinalAnimation()
    {
        if (_isFinalAnimate)
        {
            _tweener?.Kill();
            var animatePath = new[] { _startAnimatePosition.position, _finishAnimatePosition.position };
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(_startAnimatePosition.position, _speedAnimate / 2))
              .Append(transform.DOPath(animatePath, _speedAnimate)
                .SetEase(Ease.Linear)
                .SetLoops(_countAnimate, LoopType.Yoyo)
                .OnComplete(() => MoveBasePosition()));
        }
        else
        {
            MoveBasePosition();
        }

        return this;
    }

    public EntityDraggable MoveBasePosition()
    {
        IsReady = false;
        transform.SetParent(_parentTransform);
        MoveToPosition(_basePosition.position);

        return this;
    }

    public EntityDraggable MoveToPosition(Vector3 newPosition)
    {
        _tweener?.Kill();
        _tweener = transform.DOMove(newPosition, _speedMove);

        return this;
    }

    public EntityDraggable MoveToPosition(Vector3 newPosition, TweenCallback action)
    {
        _tweener?.Kill();
        _tweener = transform.DOMove(newPosition, _speedMove).OnComplete(action);

        return this;
    }

    public void Reset()
    {
        IsReady = false;
        IsBlocked = false;
    }
}
