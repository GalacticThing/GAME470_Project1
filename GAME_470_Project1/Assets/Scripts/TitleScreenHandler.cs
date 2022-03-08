using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenHandler : MonoBehaviour
{
    public Text countdownText;
    public AudioSource countdownAudio;
    public Button startButton;
  
    public void ChangeScene()
    {
        //Destroy(startButton);
        //Countdown();
        print("stuff");
        StartCoroutine("Countdown");
    }

    public IEnumerator Countdown()
    {
        countdownText.text = "3";
        countdownAudio.Play();

        yield return new WaitForSeconds(1.05f);

        countdownText.text = "2";

        yield return new WaitForSeconds(1.05f);

        countdownText.text = "1";

        yield return new WaitForSeconds(1.05f);

        countdownText.text = "GO!";

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("SampleScene");

        yield return null;
    }

    //public void Countdown()
    //{
    //    countdownText.text = "3";
    //    countdownAudio.Play();

    //    yield return new WaitForSeconds(1f);

    //    countdownText.text = "2";

    //    yield return new WaitForSeconds(1f);

    //    countdownText.text = "1";

    //    yield return new WaitForSeconds(1f);

    //    countdownText.text = "GO!";

    //    yield return new WaitForSeconds(0.5f);

    //    SceneManager.LoadScene("SampleScene");

    //}

}
