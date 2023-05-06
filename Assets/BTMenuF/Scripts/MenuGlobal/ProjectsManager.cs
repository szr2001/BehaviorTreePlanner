using BehaviorTreePlanner.MenuUi;
using BehaviorTreePlanner.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class ProjectsManager : MonoBehaviour
    {
        public MenuManager MenuManager;
        [SerializeField] private GameObject projectContainer;
        [SerializeField] private GameObject ProjectNodePrefabReff;

        private string ProjectsFolder = "";

        private List<SavedProject> projectNodes = new();

        public GameObject EditProjectNode { get; set; }

        void Start()
        {
            ProjectsFolder = Application.dataPath + "/Projects";
            if(!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
            }
        }

        public void CreateNewProjectNode() 
        {
            if(EditProjectNode == null)
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
            EditProjectNode = null;
        }
        public void CancelNewProjectNode()
        {
            Destroy(EditProjectNode);
        }



        public void DeleteProject(SavedProject project)
        {
            projectNodes.Remove(project);
        }
        public void EditProject(SavedProject project)
        {

        }


        private void DetectProjects()
        {

        }
        private void RefreashVisibleProjects()
        {

        }
    }
}
