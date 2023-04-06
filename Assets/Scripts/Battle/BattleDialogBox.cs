using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond; // hur m�nga bokst�ver som ska printas per sekund
    [SerializeField] Color highlightedColor; // gjord f�r ending the game

    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector; // kallar p� action selector
    [SerializeField] GameObject moveSelector; // kallar p� move selector
    [SerializeField] GameObject moveDetails; // kallar p� move details

    [SerializeField] List<Text> actionTexts; // lista f�r alla actions tex run och fight
    [SerializeField] List<Text> moveTexts; // lista d�r alla moves l�ggs in i

    [SerializeField] Text ppText; // d�r du l�gger till hur m�nga g�nger du kan anv�nda movet(inte implementerat vad som h�nder vid 0 �nnu)
    [SerializeField] Text typeText; // d�r du l�gger till texten f�r vilken type movet har

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog) // g�r s� att texten prinar med delay bokstav f�r bokstav
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled) // toggles moves
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; ++i)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;
            else
                actionTexts[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }

        ppText.text = $"PP {move.PP}/{move.Base.PP}";
        typeText.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.Name;
            else
                moveTexts[i].text = "-";
        }
    }
}
