using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIGaugeHandler : MonoBehaviour
{
    public float currentValue = 100, valueAfterUpdate = 100;
    [Space(10)]
    public int numberOfSteps = 30;
    private Slider slider;
    public UIShake uiShake;

    private void Start()
    {
        slider = GetComponent<Slider>();
        UpdateUISlider(100);
    }

    public void UpdateUISlider(float newCurrentHealth)
    {
        valueAfterUpdate = newCurrentHealth;
        StartCoroutine(UpdateUISliderCoroutine());
    }

    IEnumerator UpdateUISliderCoroutine()
    {
        float valueDifference = valueAfterUpdate - currentValue;
        int multiplier = 1;

        if (valueAfterUpdate > currentValue)
        {
            valueDifference = currentValue - valueAfterUpdate;
            multiplier = -1;
        }

        float numberOfStepsMultiplier = 1;
        if (valueDifference > 50) numberOfStepsMultiplier = 0.5f;
        else if (valueDifference < 10) numberOfStepsMultiplier = 2;

        float step = valueDifference / numberOfSteps * numberOfStepsMultiplier;

        if (valueDifference != 0) uiShake.ShakeUI();

        while ((currentValue >= valueAfterUpdate - step) | (currentValue <= valueAfterUpdate + step))
        {
            currentValue += step * multiplier;
            slider.value = currentValue;
            yield return new WaitForFixedUpdate();
        }

        currentValue = valueAfterUpdate;
        slider.value = currentValue;
    }
}
