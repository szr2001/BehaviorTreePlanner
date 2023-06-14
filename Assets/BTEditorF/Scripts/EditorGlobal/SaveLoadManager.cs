using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {

        public EditorManager EditorManager { get; set; }

        private void Start()
        {
            //idea
            //get projectManager openedProject layer 0 , 
            //loop trough it and use spawnmanager spawn to spawn the nodes and lines and asign them in the spawnmanager lists
            //get the count lf layers and add buttons to Ui top left with the list index,
            //if the layerbutton is pressed, it cleares the screen and the spawnmanager lists 
            //and repeats what it done before with layer 0 but instead now use the button asign index

        }
        //test clear
        public void ClearScreen()//edit to use selected layer
        {
            for (int i = 0; i < EditorManager.SpawnManager.ActiveNodes.Count; i++)
            {
                try
                {
                    EditorManager.SpawnManager.ActiveNodes[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
            for (int i = 0; i < EditorManager.SpawnManager.ActiveLines.Count; i++)
            {
                try
                {
                    EditorManager.SpawnManager.ActiveLines[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
            //no need to clear the lists after because DestroyObject removes itself from lists.
        }

        public async Task UpdateProjectFile()//if this is an event leave void
        {
            //convert scene to savedproject
            //update the project file and replace the ProjectManager.OpenedProject using ProjectManager.EdidProjectFile
            
            //activate loading anim
            ProjectsManager.OpenedProject = await ConvertSceneToSavedProject();
            //stop loading anim
        }
        private async Task<SavedProject> ConvertSceneToSavedProject()
        {
            SavedProject SceneProject = null;
            await Task.CompletedTask;
            return SceneProject;
        }

        public async Task LoadLayer(int layerIndex)
        {
            await Task.CompletedTask;
        }

    }
}
