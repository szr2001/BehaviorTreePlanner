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
        //try make it a static class maybe?
        public MenuManager MenuManager;
        public SavedProject OpenedProject { get; set; }

        [HideInInspector] public string ProjectsFolder { get; set; } = "";

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            ProjectsFolder = Application.dataPath + "/Projects";
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

        public void CheckProjectFolderExists()
        {
            if (!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
            }
        }

        public void WriteProjectFile(SavedProject Project)
        {
            CheckProjectFolderExists();
            BinaryFormatter bf = new();
            using (FileStream fs = new($"{ProjectsFolder}/{Project.ProjectName}.btsp", FileMode.Create))
            {
                bf.Serialize(fs, Project);
            }
        }
        public void WriteOpenedProjectFile()
        {
            WriteProjectFile(OpenedProject);
        }
        public void EditProjectFile(SavedProject Project,string oldname)
        {
            CheckProjectFolderExists();
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
                    return;
                }
            }
            //if the code continues, the file is  missing, recreate it
            WriteProjectFile(Project);
        }
    }
}
