using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class ProjectsManager : MonoBehaviour
    {
        public static ProjectsManager Instance;
        public SavedProject OpenedProject { get; set; }

        [HideInInspector] public string ProjectsFolder { get; set; } = "";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
            ProjectsFolder = $@"{Application.dataPath}/Projects";
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
            try
            {
                foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
                {
                    if (FileUrl.Contains(project.ProjectName))
                    {
                        File.Delete(FileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        public async Task CreateProjectFile(SavedProject Project)
        {
            CheckProjectFolderExists();
            BinaryFormatter bf = new();
            await Task.Run(() => 
            {
                try
                {

                    using (FileStream fs = new($"{ProjectsFolder}/{Project.ProjectName}.btsp", FileMode.Create))
                    {
                        bf.Serialize(fs, Project);
                    }
                }
                catch(Exception ex) 
                {
                    Debug.LogException(ex);
                }
            });
        }
        
        public List<SavedProject> DetectSavedProjectFiles()
        {
            CheckProjectFolderExists();

            BinaryFormatter bf = new();
            List<SavedProject> projects = new();
            try
            {
                foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
                {
                    using (FileStream fs = new(FileUrl, FileMode.Open))
                    {
                        SavedProject LoadedProject = (SavedProject)bf.Deserialize(fs);
                        projects.Add(LoadedProject);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
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
            try
            {
                foreach (var FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
                {
                    if (FileUrl.Contains(oldname))
                    {
                        File.Delete(FileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            await CreateProjectFile(Project);
        }
    }
}
