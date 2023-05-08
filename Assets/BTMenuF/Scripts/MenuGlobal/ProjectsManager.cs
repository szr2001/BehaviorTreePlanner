using BehaviorTreePlanner.MenuUi;
using BehaviorTreePlanner.Nodes;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class ProjectsManager : MonoBehaviour
    {

        public MenuManager MenuManager;
        public static SavedProject OpenedProject { get; set; }
        public GameObject EditProjectNode { get; set; }

        [SerializeField] private GameObject projectContainer;
        [SerializeField] private GameObject ProjectNodePrefabReff;

        private string ProjectsFolder = "";

        private readonly List<SavedProject> projectNodes = new();

        void Start()
        {
            ProjectsFolder = Application.dataPath + "/Projects";
            if (!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
            }
            DetectProjects();
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
            WriteProjectFile(project);
            EditProjectNode = null;
        }
        public void CancelNewProjectNode()
        {
            Destroy(EditProjectNode);
        }



        public void DeleteProject(ProjectNode ProjectNode)
        {
            SavedProject project = ProjectNode.Project;
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                if (FileUrl.Contains(project.ProjectName))
                {
                    File.Delete(FileUrl);
                }
            }
            projectNodes.Remove(project);
            Destroy(ProjectNode.gameObject);
        }


        private void DetectProjects()
        {
            BinaryFormatter bf = new();
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                using (FileStream fs = new(FileUrl, FileMode.Open))
                {
                    SavedProject LoadedProject = (SavedProject)bf.Deserialize(fs);
                    GameObject TempProjectNode = GameObject.Instantiate(ProjectNodePrefabReff);
                    TempProjectNode.GetComponent<ProjectNode>().OverrideProjectNode(this, LoadedProject);
                    TempProjectNode.transform.SetParent(projectContainer.transform);
                    TempProjectNode.transform.localScale = Vector3.one;
                    TempProjectNode.transform.localPosition = new Vector3(TempProjectNode.transform.position.x, TempProjectNode.transform.position.y, 0);
                    projectNodes.Add(LoadedProject);
                }

            }
        }


        private void WriteProjectFile(SavedProject Project)
        {
            BinaryFormatter bf = new();
            using (FileStream fs = new($"{ProjectsFolder}/{Project.ProjectName}.btsp", FileMode.Create))
            {
                bf.Serialize(fs, Project);
            }
        }
        public void EditProjectFile(SavedProject Project,string oldname)
        {
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                if (FileUrl.Contains(oldname))
                {
                    File.Delete(FileUrl);
                    BinaryFormatter bf = new();
                    using (FileStream fs = new($"{ProjectsFolder}/{Project.ProjectName}.btsp", FileMode.Create))
                    {
                        bf.Serialize(fs, Project);
                    }
                }
            }
        }
    }
}
