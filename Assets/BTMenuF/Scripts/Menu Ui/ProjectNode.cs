using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace BehaviorTreePlanner
{
    public class ProjectNode : MonoBehaviour
    {
        private ProjectsManager projectManager;
        private SavedProject Project;
        private string ProjectName = "";
        private bool IsEditing = true;
        [SerializeField] private InputField Name;
        [SerializeField] private Button Confirm;
        [SerializeField] private Text ConfirmT;
        [SerializeField] private Button Cancel;
        [SerializeField] private Text CancelT;
        [SerializeField] private GameObject EditToggle;

        public void InitializeProjectNode(ProjectsManager projectmanager)
        {
            projectManager = projectmanager;
            Confirm.onClick.AddListener(ConfirmProjectNode);
            Cancel.onClick.AddListener(CancelProjectNode);
        }


        public void ConfirmProjectNode()
        {
            if(Name.text == "")
            {
                return;
            }

            List<SavedNodeBase> nodes = new List<SavedNodeBase>();
            List<SavedLinePoint> lines = new List<SavedLinePoint>();
            SavedProjectLayer test = new(nodes, lines);
            Project = new SavedProject(test, 1, name);
            ProjectName = Name.text;
            projectManager.ConfirmNewProjectNode(Project);

            ToggleEditMode();
        }
        public void CancelProjectNode() 
        {
            projectManager.CancelNewProjectNode();
        }


        public void ConfirmEditProjectNode()
        {
            ProjectName = Name.text;
            ToggleEditMode();
        }
        public void CancelEditProjectNode()
        {
            Name.text = ProjectName;
            ToggleEditMode();
        }


        public void OpenProjectNode()
        {

        }
        public void DeleteProjectNode()
        {
            projectManager.DeleteProject(Project);
            Destroy(this.gameObject);
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
    }
}
