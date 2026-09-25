using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObject;
    private bool _endGame = false;

    public int currentLevel = 1;

    private void Awake()
    {
        endGameObject.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.transform.GetComponent<PlayerController>();

        if (!_endGame && player != null)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;
        foreach (var end in endGameObject)
        {
            end.SetActive(true);
            end.transform.DOScale(0, 0.2f).SetEase(Ease.OutBack).From();
        }
        
        //SaveManager.Instance.SaveLastLevel(currentLevel);
    }
}