namespace KysectAcademyTask.Utils.ProgressTracking;

internal interface IProgressBar
{
    void Update(int workToDo, int workDone, int percentage);
}