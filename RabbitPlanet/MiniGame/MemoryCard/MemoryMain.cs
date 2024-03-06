using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryMain : MonoBehaviour
{
    public UIMemoryDirector director; //ĵ����

    public Sprite bgImage;

    [SerializeField]
    private Sprite[] puzzles; //���� �̹��� ���� �迭

    public List<Sprite> gamePuzzles = new List<Sprite>(); //���� ���� �̹��� ����Ʈ

    public List<Button> btns = new List<Button>(); //��ư ���� ����Ʈ

    private bool firstGuess, secondGuess; //ù ��°, �� ��° ���� ���� �Ǵ�

    private int countCorrectGuesses; //���� ���� �� ����
    private int gameGuesses; //���� Ƚ��

    private int firstGuessIndex, secondGuessIndex; //ù ��°, �� ��° ������ �ε��� ����

    private string firstGuessPuzzle, secondGuessPuzzle; //ù ��°, �� ��° ������ ���� �̸� ����
    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("MemorySprites");

        //���� ��� ������ �÷��� �ȵǰ� ���߱�
        Time.timeScale = 0;

    }
    private void Start()
    {
        AudioListener.volume = 5;

        this.GetButtons(); //��ư�� ��������
        this.AddListeners(); //������ �߰�
        this.AddGamePuzzles(); //���� ���� �߰�
        this.Shuffle(gamePuzzles); //���� ����
        gameGuesses = gamePuzzles.Count / 2; //���� ȹ�� ����
    }

    void GetButtons() //��ư �������� �޼���
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage; //��ư �̹����� ��� �̹����� ����
        }

    }

    void AddGamePuzzles() //���� ���� �߰� �޼���
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

    void AddListeners() //������ �߰� �޼���
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle() //���� ���� �޼���
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        int currentGuessIndex = int.Parse(name);
        if (firstGuess && currentGuessIndex == firstGuessIndex)
        {
            //���� ī�带 �� �� ������ ��� ó������ ����
            return;
        }

        if (!firstGuess) //ù ��° ����
        {
            //ȿ����
            SoundManager.PlaySFX("Pop");

            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess) //�� ��° ����
        {
            //ȿ����
            SoundManager.PlaySFX("Pop");

            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            StartCoroutine(CheckIfThePuzzleMatch()); //���� ��ġ Ȯ��
        }
    }

    IEnumerator CheckIfThePuzzleMatch() //���� ��ġ Ȯ�� �ڷ�ƾ
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

    void CheckIfTheGameIsFinished() //���� Ŭ���� Ȯ�� �޼���
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("���� ����");
            Debug.Log("������ �Ϸ��ϱ���� " + countCorrectGuesses + " ���� ������ �ɷȽ��ϴ�.");

            this.director.ShowUIClear(); //���� Ŭ���� UI �����ֱ�
        }
    }

    void Shuffle(List<Sprite> list) //���� �̹��� ����Ʈ ����
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
