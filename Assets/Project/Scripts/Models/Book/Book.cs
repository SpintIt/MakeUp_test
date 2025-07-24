using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    private Hand _hand;

    [Header("Navigation")]
    [SerializeField] private Button _buttonPrev;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private List<Sheet> _lists;
    private int _numberOpenSheet = 0;

    [Header("Accessed")]
    [SerializeField] private Transform _accessedYes;
    [SerializeField] private Transform _accessedNo;
    public bool IsAccess { get; private set; }

    public Sheet OpenedSheet => _lists?[_numberOpenSheet];


    private void Awake()
    {
        OpenSheet();
    }

    public void Setup(Hand hand)
    {
        _hand = hand;
        SetAccess(false);
    }

    public void SetAccess(bool value)
    {
        IsAccess = value;

        if (value)
        {
            _accessedNo.Hide();
            _accessedYes.Show();
        }
        else
        {
            _accessedNo.Show();
            _accessedYes.Hide();
        }
    }

    private void OnEnable()
    {
        _buttonPrev.onClick.AddListener(OpenPrevSheet);
        _buttonNext.onClick.AddListener(OpenNextSheet);
    }

    private void OnDisable()
    {
        _buttonPrev.onClick.RemoveListener(OpenPrevSheet);
        _buttonNext.onClick.RemoveListener(OpenNextSheet);
    }

    private void OpenPrevSheet()
    {
        if (_numberOpenSheet > 0)
        {
            ResetBrush();
            _numberOpenSheet--;
            OpenSheet();
        }
    }

    private void OpenNextSheet()
    {
        if (_numberOpenSheet < _lists.Count - 1)
        {
            ResetBrush();
            _numberOpenSheet++;
            OpenSheet();
        }
    }

    private void OpenSheet()
    {
        _lists?.Hide();
        _lists?[_numberOpenSheet].Show();
        _buttonPrev.interactable = true;
        _buttonNext.interactable = true;


        if (_numberOpenSheet == 0)
            _buttonPrev.interactable = false;

        if (_numberOpenSheet >= _lists.Count - 1)
            _buttonNext.interactable = false;
    }

    private void ResetBrush()
    { 
        if (OpenedSheet.Brush != null && OpenedSheet.Brush.IsReady)
        {
            _hand.Hide();
            OpenedSheet.Brush.MoveBasePosition();
        }
    }
}
