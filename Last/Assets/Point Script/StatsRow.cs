using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsRow : MonoBehaviour
{
    //where the increase and decrease of stats process can happen
    public string statName;

    public TMP_Text valueText;

    public Button plusButton;
    public Button minusButton;

    public int value;

    public int minValue = -10;
    public int maxValue = 10;

    public System.Action<StatsRow> OnValueChanged;

    public Func<StatsRow, bool> CanIncrease;

    private void Start()
    {
        plusButton.onClick.AddListener(Increase);
        minusButton.onClick.AddListener(Decrease);

        Refresh();
    }
    //increase a stats value
    void Increase()
    {
        if (value >= maxValue)
            return;

        if (CanIncrease != null && !CanIncrease(this))
            return;

        value++;

        Refresh();
    }
    //decrease a stats value
    void Decrease()
    {
        if (value <= minValue)
            return;

        value--;

        Refresh();
    }
    //refresh the values
    void Refresh()
    {
        valueText.text = value.ToString();

        OnValueChanged?.Invoke(this);
    }

    public void SetValue(int newValue)
    {
        value = Mathf.Clamp(newValue, minValue, maxValue);
        Refresh();
    }
}
