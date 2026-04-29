using System;
using System.Collections.Generic;
using UnityEngine;

public enum FishSchoolEntryDirectionMode
{
    RandomLeftOrRight = 0,
    FixedFromLeftToRight = 1,
    FixedFromRightToLeft = 2,
}

public enum FishSchoolMirrorShapeXMode
{
    Off = 0,
    On = 1,
    AutoWhenSwimmingToRight = 2,
    AutoWhenSwimmingToLeft = 3,
}

/// <summary>编辑器与运行时共用的鱼种条目：格子里的值为 1..N 时对应 palette[N-1]。</summary>
[Serializable]
public class FishSchoolPaletteEntry
{
    public string label = "鱼种";
    public Color editorColor = new Color(0.25f, 0.65f, 0.95f, 1f);
    [Tooltip("该格鱼种实例化的预制体（需挂 UIFishEntity；美术默认鱼头朝左即可）")]
    public GameObject prefab;
    [Tooltip("该鱼种生成缩放（1=原始大小）")]
    [Min(0.01f)] public float scale = 1f;
}

/// <summary>
/// 鱼群形状：二维格子，每格可空 (0) 或一种鱼 (1..鱼种数)，对应 <see cref="fishTypes"/> 中的条目。
/// 由 <see cref="FishSchoolShapeEditorWindow"/> 编辑，运行时可按格子偏移实例化。
/// </summary>
[CreateAssetMenu(fileName = "FishSchoolShape", menuName = "FishingHook/鱼群形状", order = 620)]
public class FishSchoolShape : ScriptableObject
{
    [Min(1)] public int columns = 16;
    [Min(1)] public int rows = 12;
    [Header("入场方向")]
    [Tooltip("勾选后按固定方向入场；不勾选则每次随机左右入场。")]
    public bool useFixedEntryDirection;
    public FishSchoolEntryDirectionMode fixedEntryDirection = FishSchoolEntryDirectionMode.FixedFromLeftToRight;

    [Header("生成参数")]
    [Min(0f)] public float cellSpacingX = 72f;
    [Min(0f)] public float cellSpacingY = 72f;
    [Min(0f)] public float speed = 260f;
    public float centerY = 0f;
    [Tooltip("当未固定入场方向时，作为兜底方向（1=左->右，-1=右->左）。")]
    public int fallbackDir = 1;
    [Tooltip("为 true 时用 spawnBuffer 自动算中心 X，使整队从屏外切入")]
    public bool autoCenterX = true;
    [Tooltip("AutoCenterX 关闭时使用的形状中心 X（swimArea 本地坐标）")]
    public float manualCenterX = 0f;
    public FishSchoolMirrorShapeXMode mirrorMode = FishSchoolMirrorShapeXMode.AutoWhenSwimmingToRight;
    [Tooltip(">0：从屏外一侧按顺序逐条生成（间隔秒）；0：一帧内整队同时出现")]
    [Min(0f)] public float spawnStaggerSeconds = 0.07f;

    /// <summary>顺序即鱼种 id：第 1 种 = 格子里存 1，对应 fishTypes[0]。</summary>
    public List<FishSchoolPaletteEntry> fishTypes = new List<FishSchoolPaletteEntry>
    {
        new FishSchoolPaletteEntry { label = "鱼种 A", editorColor = new Color(0.25f, 0.65f, 0.95f, 1f) },
    };

    [SerializeField] private int[] cellFishTypeIds;

    /// <summary>旧版仅 bool，升级时一次性迁移到 <see cref="cellFishTypeIds"/>。</summary>
    [SerializeField, HideInInspector] private bool[] cells;

    public int CellCount => columns * rows;

    /// <summary>
    /// 计算本次鱼群入场方向：1=从左到右，-1=从右到左。
    /// 未固定时每次随机左右；固定模式下忽略 fallbackDir。
    /// </summary>
    public int ResolveSpawnDir(int fallbackDir = 1)
    {
        if (!useFixedEntryDirection)
        {
            return UnityEngine.Random.value < 0.5f ? 1 : -1;
        }

        if (fixedEntryDirection == FishSchoolEntryDirectionMode.FixedFromRightToLeft)
        {
            return -1;
        }

        if (fixedEntryDirection == FishSchoolEntryDirectionMode.FixedFromLeftToRight)
        {
            return 1;
        }

        return fallbackDir >= 0 ? 1 : -1;
    }

    public static bool ResolveMirrorShapeX(FishSchoolMirrorShapeXMode mode, int fishSchoolDir)
    {
        switch (mode)
        {
            case FishSchoolMirrorShapeXMode.Off:
                return false;
            case FishSchoolMirrorShapeXMode.On:
                return true;
            case FishSchoolMirrorShapeXMode.AutoWhenSwimmingToRight:
                return fishSchoolDir > 0;
            case FishSchoolMirrorShapeXMode.AutoWhenSwimmingToLeft:
                return fishSchoolDir < 0;
            default:
                return false;
        }
    }

    /// <summary>单格鱼种 id：0 空，1..fishTypes.Count 为具体种类。</summary>
    public int GetFishTypeId(int column, int row)
    {
        if (column < 0 || row < 0 || column >= columns || row >= rows)
        {
            return 0;
        }

        EnsureCells();
        return Mathf.Max(0, cellFishTypeIds[column + row * columns]);
    }

    public void SetFishTypeId(int column, int row, int fishTypeId)
    {
        if (column < 0 || row < 0 || column >= columns || row >= rows)
        {
            return;
        }

        EnsureCells();
        int maxId = Mathf.Max(1, fishTypes != null ? fishTypes.Count : 1);
        fishTypeId = Mathf.Clamp(fishTypeId, 0, maxId);
        cellFishTypeIds[column + row * columns] = fishTypeId;
    }

    /// <summary>兼容旧逻辑：是否有鱼（任意非空鱼种）。</summary>
    public bool GetCell(int column, int row) => GetFishTypeId(column, row) != 0;

    /// <summary>兼容旧逻辑：true 表示填入默认第 1 种鱼。</summary>
    public void SetCell(int column, int row, bool value) =>
        SetFishTypeId(column, row, value ? 1 : 0);

    private void OnValidate()
    {
        EnsurePalette();
        EnsureCells();
        ClampAllCellsToPalette();
    }

    public void EnsurePalette()
    {
        if (fishTypes == null)
        {
            fishTypes = new List<FishSchoolPaletteEntry>();
        }

        if (fishTypes.Count == 0)
        {
            fishTypes.Add(new FishSchoolPaletteEntry { label = "鱼种 A", editorColor = new Color(0.25f, 0.65f, 0.95f, 1f) });
        }
    }

    public void EnsureCells()
    {
        EnsurePalette();

        if (cellFishTypeIds != null && cellFishTypeIds.Length == CellCount)
        {
            return;
        }

        int[] next = new int[CellCount];
        if (cellFishTypeIds != null && cellFishTypeIds.Length > 0)
        {
            int min = Mathf.Min(cellFishTypeIds.Length, CellCount);
            for (int i = 0; i < min; i++)
            {
                next[i] = cellFishTypeIds[i];
            }
        }
        else if (cells != null && cells.Length == CellCount)
        {
            for (int i = 0; i < CellCount; i++)
            {
                next[i] = cells[i] ? 1 : 0;
            }

            cells = null;
        }

        cellFishTypeIds = next;
    }

    private void ClampAllCellsToPalette()
    {
        if (cellFishTypeIds == null || fishTypes == null)
        {
            return;
        }

        int maxId = fishTypes.Count;
        for (int i = 0; i < cellFishTypeIds.Length; i++)
        {
            if (cellFishTypeIds[i] > maxId)
            {
                cellFishTypeIds[i] = maxId;
            }

            if (cellFishTypeIds[i] < 0)
            {
                cellFishTypeIds[i] = 0;
            }
        }
    }

    public void ClearAll()
    {
        EnsureCells();
        for (int i = 0; i < cellFishTypeIds.Length; i++)
        {
            cellFishTypeIds[i] = 0;
        }
    }

    public void FillAll(int fishTypeId = 1)
    {
        EnsureCells();
        int id = Mathf.Clamp(fishTypeId, 1, Mathf.Max(1, fishTypes.Count));
        for (int i = 0; i < cellFishTypeIds.Length; i++)
        {
            cellFishTypeIds[i] = id;
        }
    }

    public void Resize(int newColumns, int newRows)
    {
        newColumns = Mathf.Max(1, newColumns);
        newRows = Mathf.Max(1, newRows);
        EnsureCells();
        int[] old = cellFishTypeIds;
        int oldColumns = columns;
        int oldRows = rows;
        columns = newColumns;
        rows = newRows;
        cellFishTypeIds = new int[CellCount];
        int copyCols = Mathf.Min(oldColumns, columns);
        int copyRows = Mathf.Min(oldRows, rows);
        for (int r = 0; r < copyRows; r++)
        {
            for (int c = 0; c < copyCols; c++)
            {
                cellFishTypeIds[c + r * columns] = old[c + r * oldColumns];
            }
        }
    }

    /// <summary>非空格子数量。</summary>
    public int CountFilledCells()
    {
        EnsureCells();
        int n = 0;
        for (int i = 0; i < cellFishTypeIds.Length; i++)
        {
            if (cellFishTypeIds[i] != 0)
            {
                n++;
            }
        }

        return n;
    }

    /// <summary>
    /// 以形状中心为原点，返回每个有鱼格子相对中心的偏移（列、行，浮点格单位）。
    /// 使用精确差值 (c-cx, r-cy)，不把空格子「挤掉」：字母间空列在乘 cellSpacing 后仍保留设计间距。
    /// </summary>
    public void GetFilledOffsets(List<Vector2> buffer)
    {
        buffer.Clear();
        EnsureCells();
        if (columns < 1 || rows < 1)
        {
            return;
        }

        float cx = (columns - 1) * 0.5f;
        float cy = (rows - 1) * 0.5f;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if (cellFishTypeIds[c + r * columns] == 0)
                {
                    continue;
                }

                buffer.Add(new Vector2(c - cx, r - cy));
            }
        }
    }

    /// <summary>
    /// 与 <see cref="GetFilledOffsets"/> 相同中心规则，并带上每格的 <see cref="GetFishTypeId"/>（1..N）。
    /// </summary>
    public void GetFilledOffsetsWithTypes(List<Vector2> offsetsBuffer, List<int> fishTypeIdsBuffer)
    {
        offsetsBuffer.Clear();
        fishTypeIdsBuffer.Clear();
        EnsureCells();
        if (columns < 1 || rows < 1)
        {
            return;
        }

        float cx = (columns - 1) * 0.5f;
        float cy = (rows - 1) * 0.5f;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                int id = cellFishTypeIds[c + r * columns];
                if (id == 0)
                {
                    continue;
                }

                offsetsBuffer.Add(new Vector2(c - cx, r - cy));
                fishTypeIdsBuffer.Add(id);
            }
        }
    }
}
