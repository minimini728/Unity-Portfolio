using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvoidGameOver : MonoBehaviour
{
    public Button btnNewGame;
    void Start()
    {
        this.btnNewGame.onClick.AddListener(() =>
        {
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.NewGame);
        });
    }

}
