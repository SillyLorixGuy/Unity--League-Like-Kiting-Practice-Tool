using ImGuiNET;
using UImGui;
using UnityEngine;



public class StaticSample : MonoBehaviour
{

    private float _attackSpeedValue;
    private float _movementSpeedValue;


    private void Awake()
    {
        UImGuiUtility.Layout += OnLayout;
        UImGuiUtility.OnInitialize += OnInitialize;
        UImGuiUtility.OnDeinitialize += OnDeinitialize;
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        ImGui.Begin("Adjusting Tool");
        ImGui.Text("Move the sliders to adjusts the values");
        ImGui.SliderFloat("Attack Speed", ref _attackSpeedValue, 0.0f, 10.0f);
        ImGui.SliderFloat("Movement Speed", ref _movementSpeedValue, 10.0f, 60.0f);
        if (ImGui.Button("Save"))
        {
            Debug.Log("Save");
        }
    }

    private void OnInitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnEnable();
    }

    private void OnDeinitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnDisable();
    }

    private void OnDisable()
    {
        UImGuiUtility.Layout -= OnLayout;
        UImGuiUtility.OnInitialize -= OnInitialize;
        UImGuiUtility.OnDeinitialize -= OnDeinitialize;
    }
}