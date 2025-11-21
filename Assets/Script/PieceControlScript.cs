using UnityEngine;

public class PieceControlScript : MonoBehaviour
{
    [Header("Settings")]
    public float dusunmeSuresi = 30.0f;
    public float dusunmeHizi = 0.1f;

    [Header("Çarpma Ayarları")]
    public LayerMask engelKatmani;
    private bool dusuyormu = false;
    private float sonDusmeZamani;
    GUIStyle yaziStili = new GUIStyle();

    void Start()
    {
        yaziStili.fontSize = 50;
        yaziStili.normal.textColor = Color.white;
    }

    void Update()
    {
        if (!dusuyormu)
        {
            dusunmeSuresi -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Gidebilirmi(Vector2.right))
                {
                    transform.position += Vector3.right;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(Gidebilirmi(Vector3.left))
                {
                    transform.position += Vector3.left;
                }
            }
            if(dusunmeSuresi <= 0 || Input.GetKeyDown(KeyCode.Space))
            {
                dusuyormu = true;
            }
        }
        else
        {
            if(Time.time - sonDusmeZamani > dusunmeHizi)
            {
                if (Gidebilirmi(Vector3.down))
                {
                    transform.position += Vector3.down;
                    sonDusmeZamani = Time.time;
                }
                else
                {
                    Debug.Log("Parça Yerleşti");
                    this.enabled = false;
                }
            }
        }
    }
    bool Gidebilirmi(Vector3 yon)
    {
        foreach(Transform cocukKup in transform)
        {
            Vector3 hedefYer = cocukKup.position + yon;
            if(Physics.CheckSphere(hedefYer, 0.4f, engelKatmani))
            {
                return false;
            }
        }
        return true;
    }
    void OnGUI()
    {
        if (!dusuyormu)
        {
            GUI.Label(new Rect(10, 10, 300, 100), "Düşme Süresi: " + Mathf.CeilToInt(dusunmeSuresi).ToString(), yaziStili);
        }
    }
}
