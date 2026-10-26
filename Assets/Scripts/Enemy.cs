using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public EnemyType type = EnemyType.Normal;
    public float damageFlashTime = 0.25f;
    private bool active = false;
    private SpriteRenderer[] renderers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            return;
        }

        Vector3 dist = UnityEngine.Camera.main.WorldToViewportPoint(transform.position);
        //0,0 is bot left, 1,1 is top right
        if(dist.x > 0f && dist.x < 1f)
        {
            active = true;
        }
    }

    public void Die()
    {
        Debug.Log("killing enemy");
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FlashRed());

    }

    IEnumerator FlashRed()
    {
        foreach(SpriteRenderer r in renderers)
        {
            r.color = Color.red;
        }

        yield return new WaitForSeconds(damageFlashTime);

        Destroy(gameObject);
    }
}
