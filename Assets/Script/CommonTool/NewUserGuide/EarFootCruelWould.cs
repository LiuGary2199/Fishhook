using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarFootCruelWould : ShedUIHobby
{
    public static EarFootCruelWould instance;
[UnityEngine.Serialization.FormerlySerializedAs("Hand")]
    public GameObject Core;

    /// <summary>
    /// 高亮显示目标
    /// </summary>
    private GameObject Upland;
[UnityEngine.Serialization.FormerlySerializedAs("Text")]
    public Text Welt;
    /// <summary>
    /// 区域范围缓存
    /// </summary>
    private Vector3[] Support= new Vector3[4];
    /// <summary>
    /// 最终的偏移x
    /// </summary>
    private float UplandPatronX= 0;
    /// <summary>
    /// 最终的偏移y
    /// </summary>
    private float UplandPatronY= 0;
    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material Identity;
    /// <summary>
    /// 当前的偏移x
    /// </summary>
    private float ErosionPatronX= 0f;
    /// <summary>
    /// 当前的偏移y
    /// </summary>
    private float ErosionPatronY= 0f;
    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float OpposeDuty= 0.1f;
    /// <summary>
    /// 事件渗透组件
    /// </summary>
    private SeashoreCacheEstuarine NotchEstuarine;

    protected override void Awake()
    {
        base.Awake();

        instance = this;
    }

    

    /// <summary>
    /// 显示引导遮罩
    /// </summary>
    /// <param name="_target">要引导到的目标对象</param>
    /// <param name="text">引导说明文案</param>

    public void DaleCruel(GameObject _target, string text)
    {
        if (_target == null)
        {
            Core.SetActive(false);
            if (Identity == null)
            {
                Identity = GetComponent<Image>().material;
            }
            Identity.SetVector("_Center", new Vector4(0, 0, 0, 0));
            Identity.SetFloat("_SliderX", 0);
            Identity.SetFloat("_SliderY", 0);
            // 如果没有target，点击任意区域关闭引导
            GetComponent<Button>().onClick.AddListener(() =>
            {
                BloodUIJazz(GetType().Name);
            });
        }
        else
        {
            DOTween.Kill("NewUserHandAnimation");
            Cape(_target);
            GetComponent<Button>().onClick.RemoveAllListeners();
        }

        if (!string.IsNullOrEmpty(text))
        {
            Welt.text = text;
            Welt.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            Welt.transform.parent.gameObject.SetActive(false);
        }
    }

    private float UplandEnure= 1;
    private float UplandCanopy= 1;
    public void Cape(GameObject _target)
    {
        this.Upland = _target;

        NotchEstuarine = GetComponent<SeashoreCacheEstuarine>();
        if (NotchEstuarine != null)
        {
            NotchEstuarine.WhyLayoutTough(_target.GetComponent<Image>());
        }

        Canvas canvas = UIAwesome.AgeFletcher().ThaiNation.GetComponent<Canvas>();

        //获取高亮区域的四个顶点的世界坐标
        if (Upland.GetComponent<RectTransform>() != null)
        {
            Upland.GetComponent<RectTransform>().GetWorldCorners(Support);
        }
        else
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);
            pos = UIAwesome.AgeFletcher()._RutUIRumble.GetComponent<Camera>().ScreenToWorldPoint(pos);
            Support[0] = new Vector3(pos.x - UplandEnure, pos.y - UplandCanopy);
            Support[1] = new Vector3(pos.x - UplandEnure, pos.y + UplandCanopy);
            Support[2] = new Vector3(pos.x + UplandEnure, pos.y + UplandCanopy);
            Support[3] = new Vector3(pos.x + UplandEnure, pos.y - UplandCanopy);
        }
        //计算高亮显示区域在画布中的范围
        UplandPatronX = Vector2.Distance(CivicIDNationShe(canvas, Support[0]), CivicIDNationShe(canvas, Support[3])) / 2f;
        UplandPatronY = Vector2.Distance(CivicIDNationShe(canvas, Support[0]), CivicIDNationShe(canvas, Support[1])) / 2f;
        //计算高亮显示区域的中心
        float x = Support[0].x + ((Support[3].x - Support[0].x) / 2);
        float y= Support[0].y + ((Support[1].y - Support[0].y) / 2);
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 Juggle= CivicIDNationShe(canvas, centerWorld);
        //设置遮罩材质中的中心变量
        Vector4 centerMat = new Vector4(Juggle.x, Juggle.y, 0, 0);
        Identity = GetComponent<Image>().material;
        Identity.SetVector("_Center", centerMat);
        //计算当前高亮显示区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布区域的四个顶点
            canRectTransform.GetWorldCorners(Support);
            //计算偏移初始值
            for (int i = 0; i < Support.Length; i++)
            {
                if (i % 2 == 0)
                {
                    ErosionPatronX = Mathf.Max(Vector3.Distance(CivicIDNationShe(canvas, Support[i]), Juggle), ErosionPatronX);
                }
                else
                {
                    ErosionPatronY = Mathf.Max(Vector3.Distance(CivicIDNationShe(canvas, Support[i]), Juggle), ErosionPatronY);
                }
            }
        }
        //设置遮罩材质中当前偏移的变量
        Identity.SetFloat("_SliderX", ErosionPatronX);
        Identity.SetFloat("_SliderY", ErosionPatronY);
        Core.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(DaleCore(Juggle));
    }

    private IEnumerator DaleCore(Vector2 center)
    {
        Core.SetActive(false);
        yield return new WaitForSeconds(OpposeDuty);

        Core.transform.localPosition = center;
        CoreTradition();

        Core.SetActive(true);
    }
    /// <summary>
    /// 收缩速度
    /// </summary>
    private float OpposeEncircleX= 0f;
    private float OpposeEncircleY= 0f;
    private void Update()
    {
        if (Identity == null) return;

        ErosionPatronX = UplandPatronX;
        Identity.SetFloat("_SliderX", ErosionPatronX);
        ErosionPatronY = UplandPatronY;
        Identity.SetFloat("_SliderY", ErosionPatronY);
        //从当前偏移量到目标偏移量差值显示收缩动画
        //float valueX = Mathf.SmoothDamp(currentOffsetX, targetOffsetX, ref shrinkVelocityX, shrinkTime);
        //float valueY = Mathf.SmoothDamp(currentOffsetY, targetOffsetY, ref shrinkVelocityY, shrinkTime);
        //if (!Mathf.Approximately(valueX, currentOffsetX))
        //{
        //    currentOffsetX = valueX;
        //    material.SetFloat("_SliderX", currentOffsetX);
        //}
        //if (!Mathf.Approximately(valueY, currentOffsetY))
        //{
        //    currentOffsetY = valueY;
        //    material.SetFloat("_SliderY", currentOffsetY);
        //}


    }

    /// <summary>
    /// 世界坐标转换为画布坐标
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="world">世界坐标</param>
    /// <returns></returns>
    private Vector2 CivicIDNationShe(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }

    public void CoreTradition()
    {

        var s = DOTween.Sequence();
        s.Append(Core.transform.DOLocalMoveY(Core.transform.localPosition.y + 10f, 0.5f));
        s.Append(Core.transform.DOLocalMoveY(Core.transform.localPosition.y, 0.5f));
        s.Join(Core.transform.DOScaleY(1.1f, 0.125f));
        s.Join(Core.transform.DOScaleX(0.9f, 0.125f).OnComplete(() =>
        {
            Core.transform.DOScaleY(0.9f, 0.125f);
            Core.transform.DOScaleX(1.1f, 0.125f).OnComplete(() =>
            {
                Core.transform.DOScale(1f, 0.125f);
            });
        }));
        s.SetLoops(-1);
        s.SetId("NewUserHandAnimation");
    }

    public void OnDisable()
    {
        DOTween.Kill("NewUserHandAnimation");
    }
}
