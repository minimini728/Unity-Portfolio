using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryMain : MonoBehaviour
{
    public UIMemoryDirector director; //캔버스

    public Sprite bgImage;

    [SerializeField]
    private Sprite[] puzzles; //퍼즐 이미지 저장 배열

    public List<Sprite> gamePuzzles = new List<Sprite>(); //게임 퍼즐 이미지 리스트

    public List<Button> btns = new List<Button>(); //버튼 저장 리스트

    private bool firstGuess, secondGuess; //첫 번째, 두 번째 추측 여부 판단

    private int countCorrectGuesses; //정답 추측 수 저장
    private int gameGuesses; //게임 횟수

    private int firstGuessIndex, secondGuessIndex; //첫 번째, 두 번째 추측한 인덱스 저장

    private string firstGuessPuzzle, secondGuessPuzzle; //첫 번째, 두 번째 추측한 퍼즐 이름 저장
    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("MemorySprites");

        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;

    }
    private void Start()
    {
        AudioListener.volume = 5;

        this.GetButtons(); //버튼들 가져오기
        this.AddListeners(); //리스너 추가
        this.AddGamePuzzles(); //게임 퍼즐 추가
        this.Shuffle(gamePuzzles); //퍼즐 섞기
        gameGuesses = gamePuzzles.Count / 2; //게임 획수 설정
    }

    void GetButtons() //버튼 가져오는 메서드
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage; //버튼 이미지를 배경 이미지로 설정
        }

    }

    void AddGamePuzzles() //게임 퍼즐 추가 메서드
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);

            index++;
        }
    }

    void AddListeners() //리스너 추가 메서드
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle() //퍼즐 선택 메서드
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        int currentGuessIndex = int.Parse(name);
        if (firstGuess && currentGuessIndex == firstGuessIndex)
        {
            //같은 카드를 두 번 선택한 경우 처리하지 않음
            return;
        }

        if (!firstGuess) //첫 번째 선택
        {
            //효과음
            SoundManager.PlaySFX("Pop");

            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess) //두 번째 선택
        {
            //효과음
            SoundManager.PlaySFX("Pop");

            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            StartCoroutine(CheckIfThePuzzleMatch()); //퍼즐 일치 확인
        }
    }

    IEnumerator CheckIfThePuzzleMatch() //퍼즐 일치 확인 코루틴
    {
        yield return new WaitForSeconds(1f);

        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished() //게임 클리어 확인 메서드
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("게임 종료");
            Debug.Log("게임을 완료하기까지 " + countCorrectGuesses + " 번의 추측이 걸렸습니다.");

            this.director.ShowUIClear(); //게임 클리어 UI 보여주기
        }
    }

    void Shuffle(List<Sprite> list) //퍼즐 이미지 리스트 섞기
    {
        for(int i = 0; i<list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
