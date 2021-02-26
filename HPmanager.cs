using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    //基本設定
    public GameObject creature;//要跟隨的生物
    public Canvas HPcanvas;//要顯示在哪個畫布
    public RectTransform HPbarG;
    public RectTransform HPbarR;
    public RectTransform HPbarB;
    public float maxHP = 100;//最大生命值 預設為100
    float currentHP;
    Vector3 offset1;//基於world坐標系
    Vector3 offset2;//基於screen坐標系
    RectTransform G;
    RectTransform R;
    RectTransform B;
    public GameObject scence;

    
    void Start()
    {
        offset1 = new Vector3(0, transform.parent.GetComponent<Collider2D>().bounds.size.y / 2 + 0.3f, 0);
        offset2 = new Vector3(-maxHP / 2, 0, 0);
        //UI先產生的會在底下
        B = Instantiate(HPbarB, Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2, new Quaternion(0, 0, 0, 0));
        B.SetParent(HPcanvas.transform);
        B.sizeDelta = new Vector3(maxHP, 10, 0);
        R = Instantiate(HPbarR, Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2, new Quaternion(0, 0, 0, 0));
        R.SetParent(HPcanvas.transform);
        R.sizeDelta = new Vector3(maxHP, 10, 0);
        G = Instantiate(HPbarG, Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2, new Quaternion(0, 0, 0, 0));
        G.SetParent(HPcanvas.transform);
        G.sizeDelta = new Vector3(maxHP, 10, 0);

        currentHP = maxHP;
      

    }
    // Update is called once per frame
    void Update()
    {
        B.position = Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2;
        R.position = Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2;
        G.position = Camera.main.WorldToScreenPoint(creature.transform.position + offset1) + offset2;

        //讓紅色血條逐漸追上當前血量
        if (R.sizeDelta.x > G.sizeDelta.x)
        {
            R.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 35;
        }

        //血量小於0 撥放死亡動畫然後刪除所有物件
        if (currentHP <= 0)
        {
               
                Destroy(B.gameObject);
                Destroy(G.gameObject);
                Destroy(R.gameObject);
                Destroy(transform.parent.gameObject);
            if (transform.parent.gameObject.tag == "Green Tank")
                scence.GetComponent<manager>().GreenIsDead = 1;
            if (transform.parent.gameObject.tag == "Red Tank")
                scence.GetComponent<manager>().RedIsDead = 1;
                Destroy(this.gameObject);
         
        }
    }

    public void takeDamage(float damage)
    {
        currentHP = currentHP - damage;
        G.sizeDelta = new Vector2(currentHP, G.sizeDelta.y);
    }

}