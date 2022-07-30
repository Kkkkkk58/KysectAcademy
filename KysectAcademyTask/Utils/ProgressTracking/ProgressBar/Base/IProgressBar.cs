namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

internal interface IProgressBar
{
    void Update(int workToDo, int workDone, int percentage);
}