using System.Collections.Generic;

using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class ConversationUI : MonoBehaviour
{
    private SignalBus signalBus;
    private Queue<string> textToDisplay = new();
    private string currentText;
    private int currentPosition;

    public GameObject Dialog;
    public TextMeshProUGUI Text;
    public Image Portrait;


    [Inject]
    public void Contruct(SignalBus signalBus)
    {
        this.signalBus = signalBus;

        signalBus.Subscribe<UISignal>(this.OnUIStateChange);
    }

    private void OnUIStateChange(UISignal signal)
    {
        if (Dialog.activeSelf && signal.IsOpen)
        {
            this.CloseWindow();
        }
    }

    public void DisplayText()
    {
        if (this.IsShowingText())
        {
            currentPosition = currentText.Length - 1;
            return;
        }

        if (this.textToDisplay.Count == 0)
        {
            this.CloseWindow();
        }
        else
        {
            currentText = this.textToDisplay.Dequeue();
            currentPosition = 0;
        }
    }

    private void FixedUpdate()
    {
        if (this.IsShowingText())
        {
            this.Text.text = currentText.Substring(0, currentPosition);
            currentPosition++;

            if (currentPosition >= currentText.Length)
            {
                currentText = string.Empty;
            }
        }
    }

    private bool IsShowingText()
    {
        return !string.IsNullOrWhiteSpace(currentText);
    }

    public void CloseWindow()
    {
        Dialog.SetActive(false);
        signalBus.Fire(UISignal.Closed());
    }
    public void OpenWindow(Sprite portrait, List<string> texts)
    {
        // UI do not open if no text to show
        if (texts == null || texts.Count == 0)
        {
            return;
        }

        Portrait.sprite = portrait;

        signalBus.Fire(UISignal.Opened());
        Dialog.SetActive(true);

        this.textToDisplay.Clear();
        foreach (var text in texts)
        {
            this.textToDisplay.Enqueue(text);
        }

        this.DisplayText();
    }
}
