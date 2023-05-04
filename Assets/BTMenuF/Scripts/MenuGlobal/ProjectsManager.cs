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
        [SerializeField] private GridLayoutGroup projectContainer;

        private string ProjectsFolder = "";

        private List<ProjectNode> projectNodes = new();
        private ProjectNode tempProjectNode;
        void Start()
        {
            ProjectsFolder = Application.dataPath + "/Projects";
            if(!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
            }
        }

        void Update()
        {
        
        }
        public void CreateNewProjectNode()
        {
            if(tempProjectNode == null)
            {

            }
        }
        public void ConfirmNewProjectNode()
        {
            projectNodes.Add(tempProjectNode);
            tempProjectNode = null;
        }
        public void CancelNewProjectNode()
        {
            tempProjectNode = null;
        }
        public void DeleteProject(ProjectNode prooject)
        {

        }

        private void DetectProjects()
        {

        }
    }
}
