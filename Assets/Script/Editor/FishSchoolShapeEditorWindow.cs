using UnityEditor;
using UnityEngine;

/// <summary>
/// 独立窗口：在格子画布上画鱼群形状；每格一条鱼，可选多种鱼种（见资源上的鱼种列表）。
/// </summary>
public class FishSchoolShapeEditorWindow : EditorWindow
{
    private const float MinCellPixels = 8f;
    private const float MaxCellPixels = 32f;

    private FishSchoolShape m_Target;
    private SerializedObject m_TargetSo;
    private Vector2 m_Scroll;
    private float m_CellPixels = 18f;
    private bool m_PaintErase;
    /// <summary>当前笔刷：0..fishTypes.Count-1，写入格子的 id 为 索引+1。</summary>
    private int m_PaintFishTypeIndex;

    [MenuItem("Window/FishingHook/鱼群形状编辑器")]
    public static void Open()
    {
        FishSchoolShapeEditorWindow win = GetWindow<FishSchoolShapeEditorWindow>();
        win.titleContent = new GUIContent("鱼群形状");
        win.minSize = new Vector2(420f, 360f);
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(4f);
        EditorGUILayout.LabelField("鱼群形状编辑器", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "每格一条鱼。先在「鱼种列表」里配置多种鱼（名称与编辑器颜色）；再用「当前笔刷」左键绘制该种鱼，右键擦除。旧资源会从 bool 自动迁移。创建资源：Create → FishingHook → 鱼群形状。",
            MessageType.Info);

        EditorGUI.BeginChangeCheck();
        m_Target = (FishSchoolShape)EditorGUILayout.ObjectField("编辑资源", m_Target, typeof(FishSchoolShape), false);
        if (EditorGUI.EndChangeCheck())
        {
            m_TargetSo = m_Target != null ? new SerializedObject(m_Target) : null;
            if (m_Target != null)
            {
                m_Target.EnsurePalette();
                m_Target.EnsureCells();
                ClampBrushIndex();
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("新建资源…", GUILayout.Width(100f)))
        {
            CreateNewAsset();
        }

        if (GUILayout.Button("全部清除", GUILayout.Width(80f)) && m_Target != null)
        {
            Undo.RecordObject(m_Target, "Clear fish school shape");
            m_Target.ClearAll();
            EditorUtility.SetDirty(m_Target);
        }

        if (GUILayout.Button("全部填满", GUILayout.Width(80f)) && m_Target != null)
        {
            Undo.RecordObject(m_Target, "Fill fish school shape");
            m_Target.FillAll(m_PaintFishTypeIndex + 1);
            EditorUtility.SetDirty(m_Target);
        }

        EditorGUILayout.EndHorizontal();

        if (m_Target == null)
        {
            EditorGUILayout.HelpBox("请先指定或新建一个「鱼群形状」资源。", MessageType.Warning);
            return;
        }

        m_Target.EnsureCells();

        if (m_TargetSo == null || m_TargetSo.targetObject != m_Target)
        {
            m_TargetSo = new SerializedObject(m_Target);
        }

        m_TargetSo.Update();
        SerializedProperty fishTypesProp = m_TargetSo.FindProperty("fishTypes");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(fishTypesProp, new GUIContent("鱼种列表"), true);
        if (EditorGUI.EndChangeCheck())
        {
            m_TargetSo.ApplyModifiedProperties();
            m_Target.EnsurePalette();
            ClampBrushIndex();
            EditorUtility.SetDirty(m_Target);
        }
        else
        {
            m_TargetSo.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();
        bool useFixedDir = EditorGUILayout.ToggleLeft("固定入场方向（不勾选=随机左右）", m_Target.useFixedEntryDirection);
        FishSchoolEntryDirectionMode fixedDir = m_Target.fixedEntryDirection;
        if (useFixedDir)
        {
            fixedDir = (FishSchoolEntryDirectionMode)EditorGUILayout.EnumPopup("固定方向", fixedDir);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_Target, "Change fish school entry direction");
            m_Target.useFixedEntryDirection = useFixedDir;
            m_Target.fixedEntryDirection = fixedDir;
            EditorUtility.SetDirty(m_Target);
        }

        m_Target.EnsurePalette();
        ClampBrushIndex();
        string[] brushLabels = GetBrushPopupLabels();
        m_PaintFishTypeIndex = EditorGUILayout.Popup("当前笔刷", m_PaintFishTypeIndex, brushLabels);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("网格大小", GUILayout.Width(60f));
        int newCols = EditorGUILayout.IntField(m_Target.columns, GUILayout.Width(48f));
        EditorGUILayout.LabelField("×", GUILayout.Width(14f));
        int newRows = EditorGUILayout.IntField(m_Target.rows, GUILayout.Width(48f));
        if (newCols != m_Target.columns || newRows != m_Target.rows)
        {
            if (GUILayout.Button("应用", GUILayout.Width(48f)))
            {
                Undo.RecordObject(m_Target, "Resize fish school grid");
                m_Target.Resize(newCols, newRows);
                EditorUtility.SetDirty(m_Target);
            }
        }

        EditorGUILayout.EndHorizontal();

        m_CellPixels = EditorGUILayout.Slider("格子大小", m_CellPixels, MinCellPixels, MaxCellPixels);
        m_PaintErase = EditorGUILayout.ToggleLeft("仅擦除模式（左键也当橡皮）", m_PaintErase);

        int filled = m_Target.CountFilledCells();
        EditorGUILayout.LabelField($"已点亮格子（鱼数）: {filled}  /  {m_Target.CellCount}");

        EditorGUILayout.Space(6f);
        m_Scroll = EditorGUILayout.BeginScrollView(m_Scroll, GUI.skin.box);
        DrawPaintableGrid();
        EditorGUILayout.EndScrollView();
    }

    private void CreateNewAsset()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "新建鱼群形状",
            "FishSchoolShape",
            "asset",
            "选择保存路径");
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        FishSchoolShape asset = CreateInstance<FishSchoolShape>();
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        m_Target = asset;
        Selection.activeObject = asset;
        EditorGUIUtility.PingObject(asset);
    }

    private void DrawPaintableGrid()
    {
        int cols = m_Target.columns;
        int rows = m_Target.rows;
        float cell = m_CellPixels;
        float gridW = cols * cell;
        float gridH = rows * cell;

        Rect outer = GUILayoutUtility.GetRect(gridW + 4f, gridH + 4f, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
        outer.width = gridW + 4f;
        outer.height = gridH + 4f;

        Rect gridRect = new Rect(outer.x + 2f, outer.y + 2f, gridW, gridH);
        Color empty = new Color(0.22f, 0.22f, 0.24f, 1f);
        Color line = new Color(0.5f, 0.5f, 0.52f, 0.35f);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int typeId = m_Target.GetFishTypeId(c, r);
                Rect cellRect = new Rect(gridRect.x + c * cell, gridRect.y + (rows - 1 - r) * cell, cell - 1f, cell - 1f);
                Color cellColor = empty;
                if (typeId > 0 && m_Target.fishTypes != null && typeId <= m_Target.fishTypes.Count)
                {
                    cellColor = m_Target.fishTypes[typeId - 1].editorColor;
                }
                else if (typeId > 0)
                {
                    cellColor = new Color(0.9f, 0.35f, 0.35f, 1f);
                }

                EditorGUI.DrawRect(cellRect, cellColor);
            }
        }

        for (int i = 0; i <= cols; i++)
        {
            float x = gridRect.x + i * cell;
            EditorGUI.DrawRect(new Rect(x, gridRect.y, 1f, gridRect.height), line);
        }

        for (int j = 0; j <= rows; j++)
        {
            float y = gridRect.y + j * cell;
            EditorGUI.DrawRect(new Rect(gridRect.x, y, gridRect.width, 1f), line);
        }

        HandlePaintEvents(gridRect, cell, cols, rows);
    }

    private void HandlePaintEvents(Rect gridRect, float cell, int cols, int rows)
    {
        Event e = Event.current;
        if (e.type != EventType.MouseDown && e.type != EventType.MouseDrag)
        {
            return;
        }

        if (!gridRect.Contains(e.mousePosition))
        {
            return;
        }

        int button = e.button;
        if (button != 0 && button != 1)
        {
            return;
        }

        Vector2 local = e.mousePosition - gridRect.position;
        int cx = Mathf.FloorToInt(local.x / cell);
        int cy = rows - 1 - Mathf.FloorToInt(local.y / cell);
        if (cx < 0 || cy < 0 || cx >= cols || cy >= rows)
        {
            return;
        }

        bool erase = m_PaintErase || button == 1;
        int paintId = erase ? 0 : m_PaintFishTypeIndex + 1;

        if (m_Target.GetFishTypeId(cx, cy) == paintId && e.type == EventType.MouseDrag)
        {
            return;
        }

        Undo.RecordObject(m_Target, "Paint fish school cell");
        m_Target.SetFishTypeId(cx, cy, paintId);
        EditorUtility.SetDirty(m_Target);
        e.Use();
        Repaint();
    }

    private void ClampBrushIndex()
    {
        if (m_Target == null || m_Target.fishTypes == null || m_Target.fishTypes.Count == 0)
        {
            m_PaintFishTypeIndex = 0;
            return;
        }

        m_PaintFishTypeIndex = Mathf.Clamp(m_PaintFishTypeIndex, 0, m_Target.fishTypes.Count - 1);
    }

    private string[] GetBrushPopupLabels()
    {
        if (m_Target == null || m_Target.fishTypes == null || m_Target.fishTypes.Count == 0)
        {
            return new[] { "（无鱼种）" };
        }

        string[] labels = new string[m_Target.fishTypes.Count];
        for (int i = 0; i < m_Target.fishTypes.Count; i++)
        {
            string name = m_Target.fishTypes[i].label;
            labels[i] = string.IsNullOrEmpty(name) ? $"鱼种 {i + 1}" : $"{i + 1}. {name}";
        }

        return labels;
    }
}
