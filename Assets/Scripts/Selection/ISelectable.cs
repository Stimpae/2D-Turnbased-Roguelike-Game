using System;

///<summary>
/// ISelectable
///</summary>
public interface ISelectable{
    void OnSelect();
    void OnDeselect();
    void OnHover();
    void OnUnhover();
}
