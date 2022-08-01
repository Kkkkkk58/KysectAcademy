namespace KysectAcademyTask.Utils;

public class ExtensionApplier
{
    public string GetFileNameWithDesiredExtension(string fileName, string extensionWithDot)
    {
        ValidateExtension(extensionWithDot);

        if (!Path.HasExtension(fileName) || Path.GetExtension(fileName) != extensionWithDot)
        {
            return Path.ChangeExtension(fileName, extensionWithDot);
        }

        return fileName;
    }

    private void ValidateExtension(string extensionWithDot)
    {
        if (!extensionWithDot.Contains('.'))
        {
            throw new ArgumentException("Passed extension without a dot");
        }
    }
}