package heroes.battle;

import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.net.URLClassLoader;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.jar.JarEntry;
import java.util.jar.JarFile;

public class Mods {
    private final static File folderWithJars;

    static {
        folderWithJars = new File(System.getProperty("user.dir") + "/bin");
    }

    private static ArrayList<File> getJarsFromDir() {
        ArrayList<File> jars = new ArrayList<>();

        if (Files.exists(folderWithJars.toPath())) {
            for (File file : folderWithJars.listFiles()) {
                if (file.isFile() && file.getName().endsWith(".jar")) {
                    jars.add(file);
                }
            }
        }

        return jars;
    }

    private static ArrayList<String> getNameOfModulesFromJar(File file) throws IOException {
        ArrayList<String> classes = new ArrayList<>();
        Enumeration enums = new JarFile(file).entries();

        while (enums.hasMoreElements()) {
            JarEntry entry = (JarEntry) enums.nextElement();
            String name = entry.getName();
            if (name.startsWith("heroes") && name.endsWith(".class")) {
                classes.add(name.replace('/', '.').replaceAll("[.]class$", ""));
            }
        }

        return classes;
    }

    public static ArrayList<Object> getMods(Class classForCompare) {
        ArrayList<Object> mods = new ArrayList<>();

        try {
            for (File file : getJarsFromDir()) {
                URLClassLoader child = new URLClassLoader(
                        new URL[] {new URL("jar:" + file.toURI().toURL() + "!/")}
                );

                for (String moduleName : getNameOfModulesFromJar(file)) {
                    Class modClass = Class.forName(moduleName, true, child);
                    if (modClass.getSuperclass().equals(classForCompare)) {
                        mods.add(modClass.getDeclaredConstructor().newInstance());
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return mods;
    }
}
