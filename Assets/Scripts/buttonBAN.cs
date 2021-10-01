using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBAN : MonoBehaviour
{
    public bool button_ban = false;

    public void StartCoroutine_Button_BAN_03sec()
    {
        StartCoroutine("Button_BAN_03sec");
    }

    IEnumerator Button_BAN_03sec() // баним нажатие на кнопку grip на 0.3 секунды, чтобы не нажималось много раз (костыль?, пока не пойму как правильно)
    {
        button_ban = true;
        yield return new WaitForSeconds(0.3f);
        button_ban = false;
    }
}
