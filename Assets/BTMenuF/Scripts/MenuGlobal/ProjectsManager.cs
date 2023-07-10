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
        private BTLogger mLogger;
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
            mLogger = new(this.name, false);
        }
        public void CheckProjectFolderExists()
        {
            if (!Directory.Exists(ProjectsFolder))
            {
                Directory.CreateDirectory(ProjectsFolder);
                mLogger.Log("CheckProjectFolderExists", $"Does folder exists: {Directory.Exists(ProjectsFolder)}");
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
                        mLogger.Log("DeleteProjectFile",$"Deleted file {FileUrl}");
                        File.Delete(FileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.Log("DeleteProjectFile",$"EXCEPTION TRHOWN:  {ex.Message}");
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
                        mLogger.Log("CreateProjectFile", $"Writing Saved Project to File");
                        bf.Serialize(fs, Project);
                    }
                }
                catch(Exception ex) 
                {
                    mLogger.Log("CreateProjectFile", $"EXCEPTION TRHOWN:  {ex.Message}");
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
                        mLogger.Log("DetectSavedProjectFiles", $"Detect all project Files, Detected: {projects.Count} projects");
                    }
                }
            }
            catch(Exception ex)
            {
                mLogger.Log("DetectSavedProjectFiles", $"EXCEPTION TRHOWN:  {ex.Message}");
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
                foreach (string FileUrl in Directory.GetFiles(ProjectsFolder, "*.btsp"))
                {
                    if (FileUrl.Contains(oldname))
                    {
                        mLogger.Log("EditProjectFile", $"Deleted file {FileUrl} for recreation");
                        File.Delete(FileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.Log("EditProjectFile", $"EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex);
            }
            await CreateProjectFile(Project);
        }
    }
}
