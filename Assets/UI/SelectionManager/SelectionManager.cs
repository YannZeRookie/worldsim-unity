using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SelectionIcon;

    CellDisplayer Selected;
    CellDisplayer Hovered;

    CellInfosDisplayer CellInfosDisplayer;

    private void Start()
    {
        CellInfosDisplayer = FindObjectOfType<UIManager>().CellInfosDisplayer;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            UnSelect();
    }

    public void Select(CellDisplayer cell)
    {
        Selected = cell;
        CellInfosDisplayer.SetCell(cell.Cell);
        UpdateDisplay();
    }

    public void UnSelect()
    {
        Selected = null;
        CellInfosDisplayer.SetCell(null);
        UpdateDisplay();
    }

    public void Hover(CellDisplayer cell)
    {
        if (Selected == null)
            CellInfosDisplayer.SetCell(cell.Cell);

        Hovered = cell;
    }

    public void HoverStop(CellDisplayer cell)
    {
        if (Selected == null && Hovered == cell)
            CellInfosDisplayer.SetCell(null);
        Hovered = cell;
    }

    private void UpdateDisplay()
    {
        if (Selected != null)
        {
            SelectionIcon.transform.position = Selected.transform.position;
            SelectionIcon.enabled = true;
        }
        else
            SelectionIcon.enabled = false;
    }
}
