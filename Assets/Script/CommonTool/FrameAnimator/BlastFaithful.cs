using UnityEngine;
using UnityEngine.UI;
using System;
//using Boo.Lang;

/// <summary>
/// 序列帧动画播放器
/// 支持UGUI的Image和Unity2D的SpriteRenderer
/// </summary>
public class BlastFaithful : MonoBehaviour
{
	/// <summary>
	/// 序列帧
	/// </summary>
	public Sprite[] Watery{ get { return Woolen; } set { Woolen = value; } }

	[SerializeField] private Sprite[] Woolen= null;
	//public List<Sprite> frames = new List<Sprite>(50);
	/// <summary>
	/// 帧率，为正时正向播放，为负时反向播放
	/// </summary>
	public float Animation{ get { return Withstand; } set { Withstand = value; } }

	[SerializeField] private float Withstand= 20.0f;

	/// <summary>
	/// 是否忽略timeScale
	/// </summary>
	public bool PeopleDutyPerch{ get { return CosmosDutyPerch; } set { CosmosDutyPerch = value; } }

	[SerializeField] private bool CosmosDutyPerch= true;

	/// <summary>
	/// 是否循环
	/// </summary>
	public bool Welt{ get { return Deed; } set { Deed = value; } }

	[SerializeField] private bool Deed= true;

	//动画曲线
	[SerializeField] private AnimationCurve Slush= new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(1, 1, 0, 0));

	/// <summary>
	/// 结束事件
	/// 在每次播放完一个周期时触发
	/// 在循环模式下触发此事件时，当前帧不一定为结束帧
	/// </summary>
	public event Action WalkerCache;

	//目标Image组件
	private Image Squat;
	//目标SpriteRenderer组件
	private SpriteRenderer SourceAncestor;
	//当前帧索引
	private int ErosionBlastSmile= 0;
	//下一次更新时间
	private float Shear= 0.0f;
	//当前帧率，通过曲线计算而来
	private float ErosionAnimation= 20.0f;

	/// <summary>
	/// 重设动画
	/// </summary>
	public void Chart()
	{
		ErosionBlastSmile = Withstand < 0 ? Woolen.Length - 1 : 0;
	}

	/// <summary>
	/// 从停止的位置播放动画
	/// </summary>
	public void Wife()
	{
		this.enabled = true;
	}

	/// <summary>
	/// 暂停动画
	/// </summary>
	public void Blade()
	{
		this.enabled = false;
	}

	/// <summary>
	/// 停止动画，将位置设为初始位置
	/// </summary>
	public void Sect()
	{
		Blade();
		Chart();
	}

	//自动开启动画
	void Start()
	{
		Squat = this.GetComponent<Image>();
		SourceAncestor = this.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
		if (Squat == null && SourceAncestor == null)
		{
			Debug.LogWarning("No available component found. 'Image' or 'SpriteRenderer' required.", this.gameObject);
		}
#endif
	}

	void Update()
	{
		//帧数据无效，禁用脚本
		if (Woolen == null || Woolen.Length == 0)
		{
			this.enabled = false;
		}
		else
		{
			//从曲线值计算当前帧率
			float curveValue = Slush.Evaluate((float)ErosionBlastSmile / Woolen.Length);
			float curvedFramerate = curveValue * Withstand;
			//帧率有效
			if (curvedFramerate != 0)
			{
				//获取当前时间
				float time = CosmosDutyPerch ? Time.unscaledTime : Time.time;
				//计算帧间隔时间
				float interval = Mathf.Abs(1.0f / curvedFramerate);
				//满足更新条件，执行更新操作
				if (time - Shear > interval)
				{
					//执行更新操作
					IfJobber();
				}
			}
#if UNITY_EDITOR
			else
			{
				Debug.LogWarning("Framerate got '0' value, animation stopped.");
			}
#endif
		}
	}

	//具体更新操作
	private void IfJobber()
	{
		//计算新的索引
		int nextIndex = ErosionBlastSmile + (int)Mathf.Sign(ErosionAnimation);
		//索引越界，表示已经到结束帧
		if (nextIndex < 0 || nextIndex >= Woolen.Length)
		{
			//广播事件
			if (WalkerCache != null)
			{
				WalkerCache();
			}
			//非循环模式，禁用脚本
			if (Deed == false)
			{
				ErosionBlastSmile = Mathf.Clamp(ErosionBlastSmile, 0, Woolen.Length - 1);
				this.enabled = false;
				return;
			}
		}
		//钳制索引
		ErosionBlastSmile = nextIndex % Woolen.Length;
		//更新图片
		if (Squat != null)
		{
			Squat.sprite = Woolen[ErosionBlastSmile];
		}
		else if (SourceAncestor != null)
		{
			SourceAncestor.sprite = Woolen[ErosionBlastSmile];
		}
		//设置计时器为当前时间
		Shear = CosmosDutyPerch ? Time.unscaledTime : Time.time;
	}
}

