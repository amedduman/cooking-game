using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void WinGame()
    {
        DisableInput();
        DG.Tweening.DOVirtual.DelayedCall(.8f, () => {
            ServiceLocator.Get<RectTransform>(SerLocID.winCanvas).gameObject.SetActive(true);
        });
    }

    public void LoseGame()
    {
        ServiceLocator.Get<RectTransform>(SerLocID.loseCanvas).gameObject.SetActive(true);
        DisableInput();
    }

    void DisableInput()
    {
        ServiceLocator.Get<InputHandler>().DisableInput();
    }

    public void OnClick_ReLoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
