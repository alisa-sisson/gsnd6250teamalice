using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //public Text crouchText;
    //public Text jumpText;
    public Text promptText;
    public Text centerText;
    public Animator itemAnimator;

    private int currentLevel = 1;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }

    public int finalDoorsOpened = 0;

    public void DisplayItemAnim()
    {
        itemAnimator.gameObject.SetActive(true);

    }

    public void HideItemAnim()
    {
        itemAnimator.gameObject.SetActive(false);

    }

    public void DisplayCenterText(string text, float duration = 2f)
    {
        StartCoroutine(DisplayCenterTextCoroutine(text, duration));

    }

    public void DisplayPrompt(string text, float duration = 3f)
    {
        StartCoroutine(DisplayPromptCoroutine(text, duration));
    }

    public void DisplayCenterImage(Sprite sprite, float duration = 3f)
    {
        StartCoroutine(DisplayCenterImageCoroutine(sprite, duration));
    }

    public void DisplayCrouchPrompt()
    {
        StartCoroutine(DisplayCrouchPromptCoroutine());

    }

    public void DisplayJumpPrompt()
    {
        StartCoroutine(DisplayJumpPromptCoroutine());

    }

    public void DisplayPickupPrompt()
    {
        StartCoroutine(DisplayPickupPromptCoroutine());

    }

    IEnumerator DisplayCenterTextCoroutine(string text, float duration)
    {
        centerText.text = text;
        centerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        centerText.gameObject.SetActive(false);
    }

    IEnumerator DisplayPromptCoroutine(string text, float duration)
    {
        promptText.text = text;
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        promptText.gameObject.SetActive(false);
    }

    public Image centerImage;
    IEnumerator DisplayCenterImageCoroutine(Sprite sprite, float duration)
    {
        centerImage.sprite = sprite;
        centerImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        centerImage.gameObject.SetActive(false);
        centerImage.sprite = null;
    }


    IEnumerator DisplayCrouchPromptCoroutine()
    {
        promptText.text = Constants.Prompts.Crouch;
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        promptText.gameObject.SetActive(false);
    }

    IEnumerator DisplayJumpPromptCoroutine()
    {
        promptText.text = Constants.Prompts.Jump;
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        promptText.gameObject.SetActive(false);
    }

    IEnumerator DisplayPickupPromptCoroutine()
    {
        promptText.text = Constants.Prompts.Pickup;
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        promptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (finalDoorsOpened >= 3)
        {
            // Win the game!
            DisplayCenterText("You beat the game! \nAnd now you could exit.", 60f);
        }
    }
}
