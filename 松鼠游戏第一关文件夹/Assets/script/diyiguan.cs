using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diyiguan : MonoBehaviour {

    public void gotoone()
    {
        SceneManager.LoadScene("game");
    }
    public void gototwo()
    {
        SceneManager.LoadScene("game1");
    }
}
