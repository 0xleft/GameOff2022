using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIDocument document;
    public bool started = false;
    private void Awake() {
        // root component
        var root = document.rootVisualElement;
        // working start button
        var startButton = root.Q<Button>("StartButton");
        startButton.clicked += () => {
            root.AddToClassList("hide");
        };
        // slider for volume
        var volumeSlider = root.Q<SliderInt>("VolumeSlider");
        volumeSlider.label = "Volume: "+volumeSlider.value;
        volumeSlider.RegisterValueChangedCallback(evt => {
            volumeSlider.label = "Volume: "+evt.newValue;
        });
        // hide and show options container
        var optionsButton = root.Q<Button>("OptionsButton");
        var optionsContainer = root.Q<VisualElement>("OptionsContainer");
        optionsContainer.AddToClassList("hide");
        optionsButton.clicked += () => optionsContainer.ToggleInClassList("hide");
    }
}