using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System;
using System.Text.RegularExpressions;

public class DigitalRootCruelScript : MonoBehaviour {

    public KMAudio audio;
    public KMBombInfo bomb;

    public KMSelectable[] buttons;
    private KMSelectable yesbutton;
    private KMSelectable nobutton;
    public GameObject but1;
    public GameObject but2;

    public GameObject numDisplay1;
    public GameObject numDisplay2;

    public GameObject scrDisplay1;
    public GameObject scrDisplay2;
    public GameObject scrDisplay3;
    public GameObject scrDisplay4;
    public GameObject scrDisplay5;
    public GameObject scrDisplay6;

    public Color[] colors = new Color[4];

    private int topTotal;
    private int bottomTotal;

    private int ansTop;
    private int ansBottom;

    private bool digital;

    private bool fivepresent;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        foreach(KMSelectable obj in buttons){
            KMSelectable pressed = obj;
            pressed.OnInteract += delegate () { PressButton(pressed); return false; };
        }
    }

    void Start () {
        topTotal = 0;
        bottomTotal = 0;
        digital = false;
        fivepresent = false;
        generateNumsAndTotals();
        if (!unicorn())
        {
            ansTop = digitalRoot(topTotal);
            ansBottom = digitalRoot(bottomTotal);
            Debug.LogFormat("[Cruel Digital Root #{0}] The digital root of the top 6 numbers is {1}", moduleId, ansTop);
            Debug.LogFormat("[Cruel Digital Root #{0}] The digital root of the bottom 2 numbers is {1}", moduleId, ansBottom);
            if (ansTop == ansBottom)
            {
                digital = true;
                Debug.LogFormat("[Cruel Digital Root #{0}] The top and bottom digital roots are equal! The 'yes' button must be pressed!", moduleId);
            }
            else
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The top and bottom digital roots are not equal! The 'no' button must be pressed!", moduleId);
            }
        }
        else
        {
            Debug.LogFormat("[Cruel Digital Root #{0}] Lit SIG and no 5 is present in the set of top numbers! The 'yes' button must be pressed!", moduleId);
        }
        getCorrectButtons();
    }

    void PressButton(KMSelectable pressed)
    {
        if(moduleSolved != true)
        {
            pressed.AddInteractionPunch(0.25f);
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if(digital == true && pressed == yesbutton)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] Correct button pressed! Module disarmed!", moduleId);
                audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
            else if (digital == false && pressed == nobutton)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] Correct button pressed! Module disarmed!", moduleId);
                audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
            else
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] That button was incorrect! Resetting module...", moduleId);
                GetComponent<KMBombModule>().HandleStrike();
                Start();
            }
        }
    }

    private void generateNumsAndTotals()
    {
        string nums1 = "[Cruel Digital Root #{0}] Numbers from top 6 displays from left to right: ";
        string nums2 = "[Cruel Digital Root #{0}] Numbers from bottom 2 displays from left to right: ";
        for (int i = 0; i < 8; i++)
        {
            int dec = UnityEngine.Random.Range(0, 10);
            switch (i) {
                case 0: numDisplay1.GetComponent<TextMesh>().text = "" + dec; nums2 += (dec+" "); bottomTotal += dec; break;
                case 1: numDisplay2.GetComponent<TextMesh>().text = "" + dec; nums2 += (""+dec); bottomTotal += dec; break;
                case 2: scrDisplay1.GetComponent<TextMesh>().text = "" + dec; nums1 += (dec + " "); topTotal += dec; if (dec == 5) fivepresent = true; break;
                case 3: scrDisplay2.GetComponent<TextMesh>().text = "" + dec; nums1 += (dec + " "); topTotal += dec; if (dec == 5) fivepresent = true; break;
                case 4: scrDisplay3.GetComponent<TextMesh>().text = "" + dec; nums1 += (dec + " "); topTotal += dec; if (dec == 5) fivepresent = true; break;
                case 5: scrDisplay4.GetComponent<TextMesh>().text = "" + dec; nums1 += (dec + " "); topTotal += dec; if (dec == 5) fivepresent = true; break;
                case 6: scrDisplay5.GetComponent<TextMesh>().text = "" + dec; nums1 += (dec + " "); topTotal += dec; if (dec == 5) fivepresent = true; break;
                case 7: scrDisplay6.GetComponent<TextMesh>().text = "" + dec; nums1 += (""+dec); topTotal += dec; if (dec == 5) fivepresent = true; break;
            }   
        }
        Debug.LogFormat(nums1, moduleId);
        Debug.LogFormat(nums2, moduleId);
    }

    private void getCorrectButtons()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if(rand == 0)
        {
            yesbutton = buttons[0];
            nobutton = buttons[1];
            int yes = UnityEngine.Random.Range(0, 12);
            int no = UnityEngine.Random.Range(0, 12);
            if(yes == 0)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 1)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "N";
            }
            else if (yes == 2)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "YES";
            }
            else if (yes == 3)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "NO";
            }
            else if (yes == 4)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 5)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "N";
            }
            else if (yes == 6)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "NO";
            }
            else if (yes == 7)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "NAY";
            }
            else if (yes == 8)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "YES";
            }
            else if (yes == 9)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 10)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "Y";
            }
            else if (yes == 11)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "N";
            }
            if (no == 0)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "YES";
            }
            else if (no == 1)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "NO";
            }
            else if (no == 2)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "NAY";
            }
            else if (no == 3)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 4)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "NAY";
            }
            else if (no == 5)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 6)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "YES";
            }
            else if (no == 7)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "YEA";
            }
            else if (no == 8)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 9)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "N";
            }
            else if (no == 10)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "NO";
            }
            else if (no == 11)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "NAY";
            }
            //logging
            if(yes >= 0 && yes <= 1)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Green and says '{1}', making it the 'Yes' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (yes >= 2 && yes <= 5)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Dark Green and says '{1}', making it the 'Yes' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (yes >= 6 && yes <= 7)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Red and says '{1}', making it the 'Yes' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (yes >= 8 && yes <= 11)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Dark Red and says '{1}', making it the 'Yes' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            if (no >= 0 && no <= 3)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Green and says '{1}', making it the 'No' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (no >= 4 && no <= 5)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Dark Green and says '{1}', making it the 'No' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (no >= 6 && no <= 9)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Red and says '{1}', making it the 'No' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (no >= 10 && no <= 11)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Dark Red and says '{1}', making it the 'No' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
        }
        else
        {
            yesbutton = buttons[1];
            nobutton = buttons[0];
            int yes = UnityEngine.Random.Range(0, 12);
            int no = UnityEngine.Random.Range(0, 12);
            if (yes == 0)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 1)
            {
                but2.GetComponent<TextMesh>().color = colors[0];
                but2.GetComponent<TextMesh>().text = "N";
            }
            else if (yes == 2)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "YES";
            }
            else if (yes == 3)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "NO";
            }
            else if (yes == 4)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 5)
            {
                but2.GetComponent<TextMesh>().color = colors[1];
                but2.GetComponent<TextMesh>().text = "N";
            }
            else if (yes == 6)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "NO";
            }
            else if (yes == 7)
            {
                but2.GetComponent<TextMesh>().color = colors[2];
                but2.GetComponent<TextMesh>().text = "NAY";
            }
            else if (yes == 8)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "YES";
            }
            else if (yes == 9)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "YEA";
            }
            else if (yes == 10)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "Y";
            }
            else if (yes == 11)
            {
                but2.GetComponent<TextMesh>().color = colors[3];
                but2.GetComponent<TextMesh>().text = "N";
            }
            if (no == 0)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "YES";
            }
            else if (no == 1)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "NO";
            }
            else if (no == 2)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "NAY";
            }
            else if (no == 3)
            {
                but1.GetComponent<TextMesh>().color = colors[0];
                but1.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 4)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "NAY";
            }
            else if (no == 5)
            {
                but1.GetComponent<TextMesh>().color = colors[1];
                but1.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 6)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "YES";
            }
            else if (no == 7)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "YEA";
            }
            else if (no == 8)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "Y";
            }
            else if (no == 9)
            {
                but1.GetComponent<TextMesh>().color = colors[2];
                but1.GetComponent<TextMesh>().text = "N";
            }
            else if (no == 10)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "NO";
            }
            else if (no == 11)
            {
                but1.GetComponent<TextMesh>().color = colors[3];
                but1.GetComponent<TextMesh>().text = "NAY";
            }
            //logging
            if (no >= 0 && no <= 3)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Green and says '{1}', making it the 'No' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (no >= 4 && no <= 5)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Dark Green and says '{1}', making it the 'No' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (no >= 6 && no <= 9)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Red and says '{1}', making it the 'No' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            else if (no >= 10 && no <= 11)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The left button is Dark Red and says '{1}', making it the 'No' button", moduleId, but1.GetComponent<TextMesh>().text);
            }
            if (yes >= 0 && yes <= 1)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Green and says '{1}', making it the 'Yes' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (yes >= 2 && yes <= 5)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Dark Green and says '{1}', making it the 'Yes' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (yes >= 6 && yes <= 7)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Red and says '{1}', making it the 'Yes' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
            else if (yes >= 8 && yes <= 11)
            {
                Debug.LogFormat("[Cruel Digital Root #{0}] The right button is Dark Red and says '{1}', making it the 'Yes' button", moduleId, but2.GetComponent<TextMesh>().text);
            }
        }
    }

    private bool unicorn()
    {
        if(fivepresent == false && bomb.IsIndicatorOn("SIG"))
        {
            return true;
        }
        return false;
    }

    private int digitalRoot(int dig)
    {
        string combo = "" + dig;
        while (combo.Length > 1)
        {
            int total = 0;
            for (int i = 0; i < combo.Length; i++)
            {
                int temp = 0;
                int.TryParse(combo.Substring(i, 1), out temp);
                total += temp;
            }
            combo = total + "";
        }
        int temp2 = 0;
        int.TryParse(combo, out temp2);
        return temp2;
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} press left/l [Presses the left button] | !{0} press right/r [Presses the right button]";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {

            if (parameters.Length == 2)
            {
                if (parameters[1].EqualsIgnoreCase("left") || parameters[1].EqualsIgnoreCase("l"))
                {
                    yield return null;
                    buttons[0].OnInteract();
                }
                else if (parameters[1].EqualsIgnoreCase("right") || parameters[1].EqualsIgnoreCase("r"))
                {
                    yield return null;
                    buttons[1].OnInteract();
                }
            }
            yield break;
        }
    }
}
