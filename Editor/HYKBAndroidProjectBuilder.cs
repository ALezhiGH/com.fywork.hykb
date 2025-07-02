using System;
using System.IO;
using UnityEditor.Android;
using UnityEngine;

public class HYKBAndroidProjectBuilder : IPostGenerateGradleAndroidProject
{
    public int callbackOrder => 100;

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        //Debug.Log(path);//..\SourceCode\Library\Bee\Android\Prj\IL2CPP\Gradle\unityLibrary
        
        //SchoolMaster\Library\Bee\Android\Prj\IL2CPP\Gradle\launcher\src\main\AndroidManifest.xml:35:18-78 Error:
        // Attribute application@networkSecurityConfig value=(@xml/network_security_config) from [:hykb-common:] AndroidManifest.xml:35:18-78
        //     is also present at [:unityLibrary:CSJ.androidlib] AndroidManifest.xml:33:9-60 value=(@xml/network_config).
        //     Suggestion: add 'tools:replace="android:networkSecurityConfig"' to <application> element at AndroidManifest.xml:3:3-83 to override.
        //【修改 launcher 的 AndroidManifest.xml】加入tools:replace="android:networkSecurityConfig"属性，不然hykb sdk会与csj sdk冲突
        var launcherManifest = Path.Combine(path.Replace("unityLibrary", "launcher"), "src", "main", "AndroidManifest.xml");
        var launcherManifestContent = File.ReadAllText(launcherManifest);
        var attribute = "android:networkSecurityConfig=\"@xml/network_config\"";//"android:allowBackup=\"true\"";
        if (!launcherManifestContent.Contains(attribute))
        {
            launcherManifestContent = launcherManifestContent.Replace("<application", "<application " + attribute);
        }

        attribute = "tools:replace=\"android:networkSecurityConfig\"";//"tools:replace=\"android:allowBackup\"";//
        if (!launcherManifestContent.Contains(attribute))
        {
            launcherManifestContent = launcherManifestContent.Replace("<application", "<application " + attribute);
        }
        File.WriteAllText(launcherManifest, launcherManifestContent);
        Debug.Log(launcherManifestContent);
        
#if HYKB
        //【修改 build.gradle 依赖库】
        var buildGradlePath = BuildGradlePath(path);
        var gradleContent = File.ReadAllText(buildGradlePath);
        //Debug.Log(gradleContent);
        gradleContent = AddDependencies(gradleContent, "implementation(name: 'hykb-common', ext:'aar')");
        gradleContent = AddDependencies(gradleContent, "implementation(name: 'hykb-login', ext:'aar')");
        gradleContent = AddDependencies(gradleContent, "implementation(name: 'hykb-anti', ext:'aar')");
        File.WriteAllText(buildGradlePath, gradleContent);
        //Debug.Log(gradleContent);
        
        // //【修改 AndroidManifest.xml】
        // var manifestPath = ManifestPath(path);
        // var manifestContent = File.ReadAllText(manifestPath);
        // // Debug.Log(manifestContent);
        // //添加权限
        // manifestContent = AddPermission(manifestContent, "INTERNET");//联⽹权限
        // manifestContent = AddPermission(manifestContent, "ACCESS_NETWORK_STATE");//检测当前⽹络状态是2G、3G、4G还是WiFi
        // manifestContent = AddPermission(manifestContent, "GET_TASKS");//允许应用程序获取有关当前或最近运行的任务的信息
        // manifestContent = AddPermission(manifestContent, "READ_PHONE_STATE");//获取设备标识IMEI，标识用户(4399分析Sdk、GDTSdk、KSSdk)
        // File.WriteAllText(manifestPath, manifestContent);
        // Debug.Log(manifestContent);

        //【加入 资源文件】
        CreateAssets("hykb_login.ini", path);
        CreateAssets("hykb_anti.ini", path);
        CopyFile("supplierconfig.json", path);
        
        //【确保签名证书文件CERT.RSA打入包体】
        var launcherBuildGradle = Path.Combine(path.Replace("unityLibrary", "launcher"), "build.gradle");
        var launcherBuildGradleContent = File.ReadAllText(launcherBuildGradle);
        launcherBuildGradleContent = AddSigningEnabled(launcherBuildGradleContent, "v2SigningEnabled");
        launcherBuildGradleContent = AddSigningEnabled(launcherBuildGradleContent, "v1SigningEnabled");
        File.WriteAllText(launcherBuildGradle, launcherBuildGradleContent);
        Debug.Log(launcherBuildGradleContent);
#endif
    }

    private static string AddSigningEnabled(string nowLauncherBuildGradle, string permission)
    {
        if (!nowLauncherBuildGradle.Contains(permission))
        {
            var endOfApplication = nowLauncherBuildGradle.IndexOf("keyPassword 'Fy880618'", StringComparison.OrdinalIgnoreCase) +
                                   "keyPassword 'Fy880618'".Length;

            nowLauncherBuildGradle = nowLauncherBuildGradle.Insert(endOfApplication, $"\n{permission} true");
        }

        return nowLauncherBuildGradle;
    }

    private static string BuildGradlePath(string rootPath)
    {
        return Path.Combine(rootPath, "build.gradle");
    }

    private static string ProGuardPath(string rootPath)
    {
        return Path.Combine(rootPath, "proguard-unity.txt");
    }

    private static string ManifestPath(string rootPath)
    {
        return Path.Combine(rootPath, "src", "main", "AndroidManifest.xml");
    }
    
    private static string AddDependencies(string buildGradle, string toAdd)
    {
        if (!buildGradle.Contains(toAdd))
        {
            var tag = "implementation fileTree(dir: 'libs', include: ['*.jar'])";
            var index = buildGradle.IndexOf(tag, StringComparison.OrdinalIgnoreCase) + tag.Length;
            buildGradle = buildGradle.Insert(index, $"\n{toAdd}");
        }

        return buildGradle;
    }

    private static string AddPermission(string nowManifestFile, string permission)
    {
        if (!nowManifestFile.Contains(permission))
        {
            var endOfApplication = nowManifestFile.IndexOf("</application>", StringComparison.OrdinalIgnoreCase) +
                                   "</application>".Length;

            nowManifestFile = nowManifestFile.Insert(endOfApplication,
                $"\n<uses-permission android:name=\"android.permission.{permission}\" />");
        }

        return nowManifestFile;
    }

    private static void CreateAssets(string thisAssetFile, string rootPath)
    {
        var targetPath = Path.Combine(rootPath, $"src/main/assets/{thisAssetFile}");
     
        //有可能第二次连续打包，已经在缓存里了，不要重复Copy
        if (!File.Exists(targetPath))
        {
            File.Create(targetPath);
        }
    }

    private static void CopyFile(string thisAssetFile, string rootPath)
    {
        string content = null;
        try
        {
            var text = Resources.Load<TextAsset>(thisAssetFile);
            if (text != null)
            {
                content = text.text;
            }
        }
        catch (Exception e)
        {
            content = null;
            Debug.LogError(e.Message);
            return;
        }

        if (content != null)
        {
            var targetPath = Path.Combine(rootPath, $"src/main/assets/{thisAssetFile}");
     
            //有可能第二次连续打包，已经在缓存里了，不要重复Copy
            if (!File.Exists(targetPath))
            {
                File.WriteAllText(targetPath, content);
            }
        }
        else
        {
            Debug.Log($"{thisAssetFile} is null");
        }
    }
}
