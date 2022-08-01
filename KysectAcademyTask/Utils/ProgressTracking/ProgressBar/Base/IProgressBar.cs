namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

public interface IProgressBar
{
    void Update(int workToDo, int workDone, int percentage);
}