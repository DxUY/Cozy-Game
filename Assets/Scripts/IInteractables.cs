using UnityEngine;

public interface IInteractables
{
    Vector3Int tilePosition { get; set; } // Vị trí của đối tượng tương tác
    void Interact(); // Phương thức tương tác,Laurak
}
