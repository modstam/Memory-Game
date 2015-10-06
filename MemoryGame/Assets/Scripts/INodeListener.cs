using UnityEngine;
using System.Collections;

public interface INodeListener{
    void onNodeChanged(Node node);
    void setNode(Node node);
}
