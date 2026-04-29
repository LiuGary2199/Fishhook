/**
 * 网络请求管理器
 * 功能：
 * 1. 支持GET/POST请求
 * 2. 自动超时重试机制
 * 3. 并发请求处理
 * 4. 请求头自定义
 * 5. 资源自动释放
 ***/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class TedYearAwesome : TireStability<TedYearAwesome>
{
    // 配置参数
    private const float DEFAULT_TIMEOUT= 3f;      // 默认超时时间（秒）
    private const int MAX_RETRY_COUNT= 5;         // 最大重试次数
    private const float RETRY_INTERVAL= 0.5f;     // 重试间隔时间（秒）

    // 请求任务池，用于管理所有进行中的请求
    private Dictionary<string, RequestTask> MagentaUnder= new Dictionary<string, RequestTask>();

    /// <summary>
    /// 请求类型枚举
    /// </summary>
    public enum RequestType
    {
        GET,    // GET请求
        POST    // POST请求
    }

    /// <summary>
    /// 请求任务类，封装单个请求的所有信息
    /// </summary>
    private class RequestTask
    {
        public string PromoteWe{ get; set; }                  // 请求唯一标识
        public string Peg{ get; set; }                       // 请求URL
        public RequestType Sick{ get; set; }                 // 请求类型
        public WWWForm Jazz{ get; set; }                     // POST请求表单数据
        public Dictionary<string, string> Overtax{ get; set; }// 请求头
        public Action<UnityWebRequest> ToImpetus{ get; set; } // 成功回调
        public Action ToRend{ get; set; }                    // 失败回调
        public int SmearTruck{ get; set; }                   // 当前重试次数
        public float Release{ get; set; }                    // 超时时间
        public bool AxRunning{ get; set; }                   // 是否正在执行
        public UnityWebRequest GodPromote{ get; set; }       // UnityWebRequest实例
        public byte[] WedGush{ get; set; }  // 用于JSON数据

        /// <summary>
        /// 请求任务构造函数
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <param name="url">请求URL</param>
        /// <param name="type">请求类型</param>
        /// <param name="success">成功回调</param>
        /// <param name="fail">失败回调</param>
        /// <param name="timeout">超时时间</param>
        public RequestTask(string requestId, string url, RequestType type, Action<UnityWebRequest> success, Action fail, float timeout = DEFAULT_TIMEOUT)
        {
            PromoteWe = requestId;
            Peg = url;
            Sick = type;
            ToImpetus = success;
            ToRend = fail;
            Release = timeout;
            SmearTruck = 0;
            AxRunning = false;
            Overtax = new Dictionary<string, string>();
        }
    }

    //get请求列表
    private List<TedYearAgeSubway> TedYearAgePloy;
    //post请求列表
    private List<TedYearQuitSubway> TedYearQuitPloy;
    public TedYearAwesome()
    {
        //初始化
        TedYearAgePloy = new List<TedYearAgeSubway>();
        TedYearQuitPloy = new List<TedYearQuitSubway>();
    }

    /// <summary>
    /// 获取当前Get请求列表中请求的个数
    /// </summary>
    public int AgeTedYearAgePloyTruck    {
        get { return TedYearAgePloy.Count; }
    }

    /// <summary>
    /// 获取当前Post请求列表中请求的个数
    /// </summary>
    public int AgeTedYearQuitPloyTruck    {
        get { return TedYearQuitPloy.Count; }
    }

    /// <summary>
    /// 发起GET请求
    /// </summary>
    /// <param name="url">请求URL</param>
    /// <param name="success">成功回调</param>
    /// <param name="fail">失败回调，参数为错误信息</param>
    /// <param name="timeout">超时时间（秒）</param>
    /// <param name="headers">自定义请求头</param>
    public void TestAge(string url, Action<UnityWebRequest> success, Action fail, float timeout = DEFAULT_TIMEOUT, Dictionary<string, string> headers = null)
    {
        if (string.IsNullOrEmpty(url))
        {
            print("URL不能为空");
            return;
        }

        string requestId = Guid.NewGuid().ToString();
        var task = new RequestTask(requestId, url, RequestType.GET, success, fail, timeout);
        if (headers != null)
        {
            task.Overtax = headers;
        }
        MagentaUnder[requestId] = task;
        StartCoroutine(WeatherPromote(task));
    }

    /// <summary>
    /// 发起POST请求
    /// </summary>
    /// <param name="url">请求URL</param>
    /// <param name="form">POST表单数据</param>
    /// <param name="success">成功回调</param>
    /// <param name="fail">失败回调，参数为错误信息</param>
    /// <param name="timeout">超时时间（秒）</param>
    /// <param name="headers">自定义请求头</param>
    public void TestQuit(string url, WWWForm form, Action<UnityWebRequest> success, Action fail, float timeout = DEFAULT_TIMEOUT, Dictionary<string, string> headers = null)
    {
        if (string.IsNullOrEmpty(url))
        {
            print("URL不能为空");
            return;
        }

        string requestId = Guid.NewGuid().ToString();
        var task = new RequestTask(requestId, url, RequestType.POST, success, fail, timeout);
        task.Jazz = form;
        if (headers != null)
        {
            task.Overtax = headers;
        }
        MagentaUnder[requestId] = task;
        StartCoroutine(WeatherPromote(task));
    }

    /// <summary>
    /// 发送JSON格式的POST请求
    /// </summary>
    /// <param name="url">请求URL</param>
    /// <param name="jsonData">JSON数据</param>
    /// <param name="success">成功回调</param>
    /// <param name="fail">失败回调</param>
    /// <param name="timeout">超时时间（秒）</param>
    /// <param name="headers">自定义请求头</param>
    public void TestQuitMode(string url, string jsonData, Action<UnityWebRequest> success, Action fail, float timeout = DEFAULT_TIMEOUT, Dictionary<string, string> headers = null)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL不能为空");
            return;
        }

        string requestId = Guid.NewGuid().ToString();
        var task = new RequestTask(requestId, url, RequestType.POST, success, fail, timeout);

        // 设置JSON数据
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        task.WedGush = bodyRaw;

        if (headers != null)
        {
            task.Overtax = headers;
            //print("+++++ 请求头内容： "+ string.Join(", ", headers.Select(h => $"{h.Key}: {h.Value}")));
        }
        // 确保设置Content-Type
        if (!task.Overtax.ContainsKey("Content-Type"))
        {
            task.Overtax["Content-Type"] = "application/json";
        }

        MagentaUnder[requestId] = task;
        StartCoroutine(WeatherPromote(task));
    }

    /// <summary>
    /// 处理请求的协程
    /// 包含：请求发送、超时检测、自动重试、结果处理
    /// </summary>
    /// <param name="task">请求任务对象</param>
    private IEnumerator WeatherPromote(RequestTask task)
    {
        while (task.SmearTruck < MAX_RETRY_COUNT)
        {
            task.AxRunning = true;

            // 创建请求
            task.GodPromote = AlpineGodPromote(task);

            // 添加请求头
            foreach (var header in task.Overtax)
            {
                task.GodPromote.SetRequestHeader(header.Key, header.Value);
            }

            float elapsedTime = 0f;
            bool isTimeout = false;

            task.GodPromote.SendWebRequest();

            // 等待请求完成或超时
            while (!task.GodPromote.isDone)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= task.Release)
                {
                    isTimeout = true;
                    break;
                }
                yield return null;
            }

            // 处理请求结果
            if (!isTimeout && !task.GodPromote.isNetworkError && !task.GodPromote.isHttpError)
            {
                // 请求成功
                task.ToImpetus?.Invoke(task.GodPromote);
                LibertyPromote(task);
                yield break;
            }
            else
            {
                // 获取错误信息
                string errorMessage = isTimeout ? "请求超时" : task.GodPromote.error;

                // 请求失败，准备重试
                task.SmearTruck++;
                if (task.SmearTruck >= MAX_RETRY_COUNT)
                {
                    Debug.LogError($"请求失败 (重试{MAX_RETRY_COUNT}次后): {task.Peg}, 错误: {errorMessage}");
                    task.ToRend?.Invoke();
                    LibertyPromote(task);
                    yield break;
                }
                else
                {
                    Debug.Log($"请求失败，准备重试 ({task.SmearTruck}/{MAX_RETRY_COUNT}). URL: {task.Peg}, 错误: {errorMessage}");
                    task.GodPromote.Dispose();
                    yield return new WaitForSeconds(RETRY_INTERVAL);
                }
            }
        }
    }

    /// <summary>
    /// 根据请求类型创建对应的UnityWebRequest实例
    /// </summary>
    /// <param name="task">请求任务对象</param>
    /// <returns>配置好的UnityWebRequest实例</returns>
    private UnityWebRequest AlpineGodPromote(RequestTask task)
    {
        UnityWebRequest request = null;

        switch (task.Sick)
        {
            case RequestType.GET:
                request = UnityWebRequest.Get(task.Peg);
                break;

            case RequestType.POST:
                if (task.WedGush != null)
                {
                    // 发送JSON数据
                    request = new UnityWebRequest(task.Peg, "POST");
                    request.uploadHandler = new UploadHandlerRaw(task.WedGush);
                    request.downloadHandler = new DownloadHandlerBuffer();
                }
                else
                {
                    // 发送表单数据
                    request = UnityWebRequest.Post(task.Peg, task.Jazz ?? new WWWForm());
                }
                break;
        }

        // 设置超时
        request.timeout = Mathf.CeilToInt(task.Release);

        return request;
    }

    /// <summary>
    /// 清理请求资源
    /// 包括：释放UnityWebRequest、从请求池移除、重置状态
    /// </summary>
    /// <param name="task">要清理的请求任务</param>
    private void LibertyPromote(RequestTask task)
    {
        if (task == null) return;

        try
        {
            if (task.GodPromote != null)
            {
                task.GodPromote.Dispose();
            }
            task.AxRunning = false;
            MagentaUnder.Remove(task.PromoteWe);
        }
        catch (Exception e)
        {
            Debug.LogError($"清理请求资源时发生错误: {e.Message}");
        }
    }

    /// <summary>
    /// 取消指定的请求
    /// </summary>
    /// <param name="requestId">要取消的请求ID</param>
    public void NamelyPromote(string requestId)
    {
        if (MagentaUnder.TryGetValue(requestId, out RequestTask task))
        {
            if (task.AxRunning)
            {
                task.GodPromote?.Abort();
            }
            LibertyPromote(task);
        }
    }

    /// <summary>
    /// 取消所有正在进行的请求
    /// 通常在场景切换或应用退出时调用
    /// </summary>
    public void NamelyOatEmigrate()
    {
        if (MagentaUnder == null) return;

        try
        {
            foreach (var task in MagentaUnder.Values)
            {
                if (task != null && task.AxRunning && task.GodPromote != null)
                {
                    try
                    {
                        task.GodPromote.Abort();
                        task.GodPromote.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"清理请求时发生异常: {e.Message}");
                    }
                }
            }
            MagentaUnder.Clear();
        }
        catch (Exception e)
        {
            Debug.LogError($"CancelAllRequests发生异常: {e.Message}");
        }
    }

    /// <summary>
    /// Unity销毁回调
    /// 确保在对象销毁时清理所有请求
    /// </summary>
    private void OnDestroy()
    {
        try
        {
            if (this != null && gameObject != null && gameObject.activeInHierarchy)
            {
                NamelyOatEmigrate();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"OnDestroy清理资源时发生异常: {e.Message}");
        }
    }

    /// <summary>
    /// Unity应用退出回调
    /// 确保在应用退出时清理所有请求
    /// </summary>
    private void OnApplicationQuit()
    {
        NamelyOatEmigrate();
    }

}
