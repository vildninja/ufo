using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {

    public float maxHp = 10;
    public float hp;

    public Renderer impactGraphics;
    public Renderer flashOnDeath;
    public float flashTime = 1.5f;
    public SpriteRenderer color;
    public Gradient colorFromHp;

    public bool Alive { get { return hp > 0; } }
    public float Normalized
    {
        get { return hp / maxHp; }
        set { hp = maxHp * value; }
    }

    public enum ActionOnDeath
    {
        Respawn,
        Destroy,
        Revive,
        None,
    }

    public ActionOnDeath actionOnDeath = ActionOnDeath.Destroy;

    Vector3 spawnPos;
    Quaternion spawnRot;

    void Awake()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        hp = maxHp;
    }

    IEnumerator ShowImpact()
    {
        if (impactGraphics != null)
        {
            impactGraphics.enabled = true;
            yield return new WaitForSeconds(0.05f);
            impactGraphics.enabled = false;
        }
    }

    IEnumerator Die()
    {
        rigidbody2D.isKinematic = true;
        for (float t = 0; t <= flashTime || (actionOnDeath == ActionOnDeath.None && !Alive); t += 0.1f)
        {
            if (flashOnDeath)
                flashOnDeath.enabled = !flashOnDeath.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        switch (actionOnDeath)
        {
            case ActionOnDeath.Destroy:
                Destroy(gameObject);
                break;
            case ActionOnDeath.Revive:
                Revive();
                break;
            case ActionOnDeath.Respawn:
                transform.position = spawnPos;
                transform.rotation = spawnRot;
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.angularVelocity = 0;
                Revive();
                break;
        }

    }

    public void Damage(float damage, bool impactAnim = true)
    {
        if (Alive)
        {
            //print(damage + " " + impactAnim);
            hp -= damage;
            if (impactAnim)
                StartCoroutine(ShowImpact());
            if (color)
                color.color = colorFromHp.Evaluate(Normalized);

            if (hp <= 0)
                StartCoroutine(Die());
        }
    }

    public void Heal(float amount)
    {
        if (Alive)
        {
            hp = Mathf.Clamp(hp + amount, 0, maxHp);
        }
        if (color)
            color.color = colorFromHp.Evaluate(Normalized);
    }

    public void Revive(float normalizedHp = 1)
    {
        Normalized = normalizedHp;
        if (flashOnDeath)
            flashOnDeath.enabled = true;
        rigidbody2D.isKinematic = false;
        if (color)
            color.color = colorFromHp.Evaluate(Normalized);
    }
}
