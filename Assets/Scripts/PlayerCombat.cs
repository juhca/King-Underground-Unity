using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    private MusicScript ms;
    private int health = 100;
    public Text countText;

    private List<GoblinCombat> enemies = new List<GoblinCombat>();
    private int st_sovraznikov = 0;
    private Animator anim;

    private float attackRange = 2.0f;
    private float attackAngle = 85f;

    private bool isDead = false;
    float timer = 5f;
    // tabela zvokov
    public AudioSource[] sounds;
    public AudioSource WilhelmScream;
    public AudioSource ManScream01;
    public AudioSource ManScream02;
    public AudioSource ManScream03;
    public AudioSource ManScream04;
    public AudioSource ManScream05;
    public AudioSource ManScream06;
    public AudioSource ManScream07;
    public AudioSource ManScream08;
    public AudioSource ManScream09;
    public AudioSource ManDeath1;
    public AudioSource Music0;
    public AudioSource Music1;
    void Start()
    {
        /*
        sounds = GetComponents<AudioSource>();
        WilhelmScream = sounds[0];
        ManScream01 = sounds[1];
        ManScream02 = sounds[2];
        ManScream03 = sounds[3];
        ManScream04 = sounds[4];
        ManScream05 = sounds[5];
        ManScream06 = sounds[6];
        ManScream07 = sounds[7];
        ManScream08 = sounds[8];
        ManScream09 = sounds[9];
        ManDeath1 = sounds[10];
        */
        anim = GetComponent<Animator>();
        SetCountText();
    }

    public void AddEnemy(GoblinCombat g)
    {
        if (st_sovraznikov == 0)
        {
            Music1.Play();
            StartCoroutine(MyMethod());
            Music0.Pause();
            /*
            while (Music0.volume != 0)
            {
                StartCoroutine(MyMethod());
                FadeMusic();
            }*/
            
        }
        st_sovraznikov = st_sovraznikov + 1;
        enemies.Add(g);
    }

    public void RemoveEnemy(GoblinCombat g)
    {
        st_sovraznikov = st_sovraznikov - 1;
        enemies.Remove(g);
        if (st_sovraznikov == 0)
        {
            Music0.Play();
            StartCoroutine(MyMethod());
            Music1.Stop();
        }
    }
    public void OnHit(int value)
    {
        if (isDead) return;

        anim.Play("HIT", -1, 0f);

        health -= value;
        SetCountText();

        if (health <= 0)
        {
            HandleDeath();
        }
        else
        {
            var val = Random.Range(0, 10);
            Debug.Log(val);
            if (val == 0)
            {
                WilhelmScream.Play();
            }
            else if (val == 1)
            {
                ManScream01.Play();
            }
            else if (val == 2)
            {
                ManScream02.Play();
            }
            else if (val == 3)
            {
                ManScream03.Play();
            }
            else if (val == 4)
            {
                ManScream04.Play();
            }
            else if (val == 5)
            {
                ManScream05.Play();
            }
            else if (val == 6)
            {
                ManScream06.Play();
            }
            else if (val == 7)
            {
                ManScream07.Play();
            }
            else if (val == 8)
            {
                ManScream08.Play();
            }
            else if (val >= 9)
            {
                ManScream09.Play();
            }

        }
    }

    public void HitTargets()
    {
        foreach (GoblinCombat g in enemies.ToArray())
        {
            if (Vector3.Distance(transform.position, g.transform.position) < attackRange && Vector3.Dot((g.transform.position - transform.position).normalized, transform.forward) > Mathf.Cos(attackAngle))
            {
                g.OnHit(15);
            }
        }
    }

    private void HandleDeath()
    {
        isDead = true;
        ManDeath1.Play();
        anim.Play("DIE", -1, 0f);
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void SetCountText()
    {
        countText.text = health.ToString();
    }

    public void increaseHealth()
    {
        if (health <= 90) health += 10;
        else health = 100;
        countText.text = health.ToString();
    }

    public int returnHealth()
    {
        return health;
    }

    public void DeadEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("lost");
    }

    void FadeMusic()
    {
        if (Music0.volume > 0)
        {
            Music0.volume -= 0.015f;
        }
    }

    /*
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }*/

    IEnumerator MyMethod()
    {
        yield return new WaitForSeconds(1);
    }
}
