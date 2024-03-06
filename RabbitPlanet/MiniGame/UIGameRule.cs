using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameRule : MonoBehaviour
{
    public Button btnOkay;

    private void Start()
    {
        AudioListener.volume = 5;

        this.btnOkay.onClick.AddListener(() =>
        {
            //È¿°úÀ½
            SoundManager.PlaySFX("Selection");

            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
    }
}
