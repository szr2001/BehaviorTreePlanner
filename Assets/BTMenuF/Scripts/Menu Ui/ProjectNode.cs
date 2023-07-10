using BehaviorTreePlanner.Global;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace BehaviorTreePlanner.MenuUi
{
    public class ProjectNode : MonoBehaviour
    {
        private ProjectMenu projectMenu;
        public SavedProject Project { get; private set; }
        private string ProjectName = "";
        private bool IsEditing = true;
        [SerializeField] private InputField Name;
        [SerializeField] private Button Confirm;
        [SerializeField] private Text ConfirmT;
        [SerializeField] private Button Cancel;
        [SerializeField] private Text CancelT;
        [SerializeField] private GameObject EditToggle;
        [SerializeField] private Text NodeNumber;
        [SerializeField] private Text LineNumber;
        [SerializeField] private Text LayerNumber;
        [SerializeField] private Text Date;

        public void InitializeProjectNode(ProjectMenu projectmenu) // initialize and override?
        {
            projectMenu = projectmenu;
            Confirm.onClick.AddListener(ConfirmProjectNode);
            Cancel.onClick.AddListener(CancelProjectNode);
        }

        public void OverrideProjectNode(ProjectMenu projectmenu, SavedProject project)
        {
            Project = project;
            projectMenu = projectmenu;
            ToggleEditMode();
            UpdateDisplayInfo();
        }
        public void ConfirmProjectNode()
        {
            if (Name.text == "")
                return;


            ProjectName = Name.text;

            List<SavedNodeBase> nodes = new();
            List<SavedLinePoint> lines = new();
            SavedNodeBase blackboard = new(-1, -1, -1);
            SavedProjectLayer baseLayer = new(nodes, lines, blackboard);
            baseLayer.LayerName = "Base Layer";
            Project = new SavedProject(baseLayer, 1, ProjectName, DateTime.UtcNow);

            projectMenu.ConfirmNewProjectNode(Project);

            UpdateDisplayInfo();

            ToggleEditMode();
        }
        public void CancelProjectNode()
        {
            projectMenu.CancelNewProjectNode();
        }


        public void ConfirmEditProjectNode()
        {
            Project.Date = DateTime.UtcNow;
            string OldName = ProjectName;
            ProjectName = Name.text;
            Project.ProjectName = ProjectName;
            projectMenu.CallEditProjectFile(Project, OldName);
            ToggleEditMode();
            UpdateDisplayInfo();
        }
        public void CancelEditProjectNode()
        {
            Name.text = ProjectName;
            ToggleEditMode();
        }


        public void OpenProjectNode()
        {
            projectMenu.SetOpenedProject(Project);
            SceneManager.LoadScene("BTEditor");
        }
        public void DeleteProjectNode()
        {
            projectMenu.CallDeleteProjectFile(this);
        }


        public void ToggleEditMode()
        {
            if (IsEditing)
            {
                Name.interactable = false;

                ConfirmT.text = "Open";
                Confirm.onClick.RemoveAllListeners();
                Confirm.onClick.AddListener(OpenProjectNode);

                CancelT.text = "Delete";
                Cancel.onClick.RemoveAllListeners();
                Cancel.onClick.AddListener(DeleteProjectNode);
                EditToggle.SetActive(true);
                IsEditing = false;
                projectMenu.EditProjectNode = null;// null reff error
            }
            else
            {
                if (projectMenu.EditProjectNode == null)
                {
                    Name.interactable = true;

                    ConfirmT.text = "Confirm";
                    Confirm.onClick.RemoveAllListeners();
                    Confirm.onClick.AddListener(ConfirmEditProjectNode);

                    CancelT.text = "Cancel";
                    Cancel.onClick.RemoveAllListeners();
                    Cancel.onClick.AddListener(CancelEditProjectNode);
                    EditToggle.SetActive(false);
                    IsEditing = true;
                    projectMenu.EditProjectNode = this.gameObject;
                }
            }
        }
        private void UpdateDisplayInfo()
        {
            Date.text = Project.Date.Date.ToString();
            ProjectName = Project.ProjectName;
            Name.text = ProjectName;
            LayerNumber.text = Project.Layers.Count.ToString();
            int Nodes = 0;
            int Lines = 0;
            foreach (var layer in Project.Layers)
            {
                Nodes += layer.SavedNodes.Count;
                Lines += layer.SavedLinePoints.Count;
            }
            NodeNumber.text = Nodes.ToString();
            LineNumber.text = Lines.ToString();
        }
    }
}
