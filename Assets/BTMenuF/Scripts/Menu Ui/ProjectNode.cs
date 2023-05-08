using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using Button = UnityEngine.UI.Button;

namespace BehaviorTreePlanner
{
    public class ProjectNode : MonoBehaviour
    {
        private ProjectsManager projectManager;
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

        public void InitializeProjectNode(ProjectsManager projectmanager)
        {
            projectManager = projectmanager;
            Confirm.onClick.AddListener(ConfirmProjectNode);
            Cancel.onClick.AddListener(CancelProjectNode);
        }

        public void OverrideProjectNode(ProjectsManager projectmanager,SavedProject project)
        {
            projectManager = projectmanager;
            Project = project;
            ToggleEditMode();
            UpdateDisplayInfo();
        }
        public void ConfirmProjectNode()
        {
            if(Name.text == "")
                return;


            ProjectName = Name.text;

            List<SavedNodeBase> nodes = new();
            List<SavedLinePoint> lines = new();
            SavedProjectLayer baseLayer = new(nodes, lines);
            Project = new SavedProject(baseLayer, 1, ProjectName,DateTime.UtcNow);

            projectManager.ConfirmNewProjectNode(Project);

            UpdateDisplayInfo();

            ToggleEditMode();
        }
        public void CancelProjectNode() 
        {
            projectManager.CancelNewProjectNode();
        }


        public void ConfirmEditProjectNode()
        {
            Project.Date = DateTime.UtcNow;
            string OldName = ProjectName;
            ProjectName = Name.text;
            Project.ProjectName = ProjectName;
            projectManager.EditProjectFile(Project, OldName);
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
            ProjectsManager.OpenedProject = Project;
            SceneManager.LoadScene("BTEditor");
        }
        public void DeleteProjectNode()
        {
            projectManager.DeleteProject(this);
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
                projectManager.EditProjectNode = null;
            }
            else
            {
                if(projectManager.EditProjectNode == null)
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
                    projectManager.EditProjectNode = this.gameObject;
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
