using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController1 : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public SpriteSwitcher backgroundController;
    public AudioController audioController;

    private State state = State.IDLE;
    private enum State 
    {
        IDLE, ANIMATE 
    }

    void Start()
    {
        StoryScene storyScene = currentScene as StoryScene;
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.background); // Set initial background
        // PlayAudio(storyScene.sentences[0]);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    PlayScene(currentScene.nextScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                    // PlayAudio((currentScene as StoryScene).sentence[bottomBar.GetSentenceIndex()]);
                }
            }
        }
    }

    private void PlayScene(StoryScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }
    private IEnumerator SwitchScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        backgroundController.SwitchImage(scene.background);
        // PlayAudio(storyScene.sentence[0]);
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        bottomBar.Show();
        yield return new WaitForSeconds(1f);
        bottomBar.PlayScene(scene);
        state = State.IDLE;
    }

    // private void PlayAudio(StoryScene.Sentence sentence)
    // {
    //     audioController.PlayAudio(sentence.music, sentence.sound);
    // }
}
