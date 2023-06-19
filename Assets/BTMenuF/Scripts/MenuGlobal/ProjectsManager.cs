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
        public SavedProject OpenedProject { get; set; }

        [HideInInspector] public string ProjectsFolder { get; set; } = "";

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            ProjectsFolder = Application.dataPath + "/Projects";
        }
        public void CheckProjectFolderExists()
        {
            if (!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
            }
        }

        public void DeleteProjectFile(ProjectNode ProjectNode)
        {
            CheckProjectFolderExists();
            SavedProject project = ProjectNode.Project;
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                if (FileUrl.Contains(project.ProjectName))
                {
                    File.Delete(FileUrl);
                }
            }
        }
        public async Task CreateProjectFile(SavedProject Project)
        {
            CheckProjectFolderExists();
            BinaryFormatter bf = new();
            await Task.Run(() => 
            {
                using (FileStream fs = new($"{ProjectsFolder}/{Project.ProjectName}.btsp", FileMode.Create))
                {
                    bf.Serialize(fs, Project);
                }
            });
        }
        
        public List<SavedProject> DetectSavedProjectFiles()
        {
            CheckProjectFolderExists();

            BinaryFormatter bf = new();
            List<SavedProject> projects = new();
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                using (FileStream fs = new(FileUrl, FileMode.Open))
                {
                    SavedProject LoadedProject = (SavedProject)bf.Deserialize(fs);
                    projects.Add(LoadedProject);
                }
            }
            return projects;
        }
        
        public async Task SaveOpenedProjectFile()
        {
           await CreateProjectFile(OpenedProject);
        }
        
        public async Task EditProjectFile(SavedProject Project,string oldname)
        {
            CheckProjectFolderExists();
            foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
            {
                if (FileUrl.Contains(oldname))
                {
                    File.Delete(FileUrl);
                }
            }
            await CreateProjectFile(Project);
        }
    }
}
