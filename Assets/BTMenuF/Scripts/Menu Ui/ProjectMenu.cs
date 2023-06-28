using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class ProjectMenu : MonoBehaviour
    {
        public ProjectsManager ProjectManager;
        public GameObject EditProjectNode { get; set; }
        [SerializeField] private GameObject projectContainer;
        [SerializeField] private GameObject ProjectNodePrefabReff;
        private List<SavedProject> projectNodes;

        void Start()
        {
            CreateProjectNodes(); 
        }

        private void CreateProjectNodes()
        {
            projectNodes = ProjectManager.DetectSavedProjectFiles();
            foreach (SavedProject projectNode in projectNodes)
            {
                GameObject TempProjectNode = GameObject.Instantiate(ProjectNodePrefabReff);
                TempProjectNode.GetComponent<ProjectNode>().OverrideProjectNode(this,projectNode);
                TempProjectNode.transform.SetParent(projectContainer.transform);
                TempProjectNode.transform.localScale = Vector3.one;
                TempProjectNode.transform.localPosition = new Vector3(TempProjectNode.transform.position.x, TempProjectNode.transform.position.y, 0);
            }
        }

        public void CreateNewProjectNode()
        {
            if (EditProjectNode == null)
            {
                EditProjectNode = GameObject.Instantiate(ProjectNodePrefabReff);
                EditProjectNode.GetComponent<ProjectNode>().InitializeProjectNode(this);
                EditProjectNode.transform.SetParent(projectContainer.transform);
                EditProjectNode.transform.localScale = Vector3.one;
                EditProjectNode.transform.localPosition = new Vector3(EditProjectNode.transform.position.x, EditProjectNode.transform.position.y, 0);
            }
        }

        public void ConfirmNewProjectNode(SavedProject project)
        {
            projectNodes.Add(project);
            _ = ProjectManager.CreateProjectFile(project);
            EditProjectNode = null;
        }

        public void CancelNewProjectNode()
        {
            Destroy(EditProjectNode);
        }

        public void CallEditProjectFile(SavedProject Project, string oldname)
        {
            _ = ProjectManager.EditProjectFile(Project, oldname);
        }

        public void SetOpenedProject(SavedProject project)
        {
            ProjectManager.OpenedProject = project;
        }

        public void CallDeleteProjectFile(ProjectNode project)
        {
            ProjectManager.DeleteProjectFile(project);
            projectNodes.Remove(project.Project);
            Destroy(project.gameObject);
        }
    }
}
