// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;


public class ConfigArguments 
{
    public String execPath { get; set; } 
    public String targetDir  { get; set; }
    public String user   { get; set; }
    public String password   { get; set; }

    public ConfigArguments()
    {
        
    }

    public ConfigArguments(String execPath, String targetDir, String user, String password)
    {
        this.execPath = execPath;
        this.targetDir = targetDir;
        this.user = user;
        this.password = password;
    }

    public void printOut()
    {
        Console.WriteLine("executable Path " + this.execPath);
        Console.WriteLine("targetDir " + this.targetDir);
        Console.WriteLine("user " + this.user);
        Console.WriteLine("password " + this.password);
    }

    public void checkExisting()
    {
        DirectoryInfo dir = new DirectoryInfo(this.targetDir);
        if (!dir.Exists)
        {
            dir.Create();
        }
    }

    public String createSub()
    {
        DateTime today = DateTime.Today;
        DirectoryInfo dir = new DirectoryInfo(this.targetDir);

        return dir.CreateSubdirectory("backup" + today).FullName;
    }


    public void executeCommand()
    {
        ProcessStartInfo ProcessInfo;
        Process Process;
        checkExisting();

        String fullTargetDir = createSub(); 
        String command = $"{execPath} --backup --target-dir \"{fullTargetDir}\" --user {user} --password {password}";
        Console.WriteLine("executing command " + command);
        ProcessInfo = new ProcessStartInfo(command);
//        Process = Process.Start(ProcessInfo);
    }
}

public class Program
{

    public static ConfigArguments readJSONfile(String pathToJSONfile)
    {

        ConfigArguments args = new ConfigArguments();

        String file = File.ReadAllText(pathToJSONfile);
        
        args = JsonSerializer.Deserialize<ConfigArguments>(file.ToString());
             
        return args;


    }

    public static void Main()
    {
        String runningDir = AppDomain.CurrentDomain.BaseDirectory;
        String pathToConfig =
            runningDir + "config.json";
        
       ConfigArguments args = readJSONfile(pathToConfig);
       args.printOut();
       args.executeCommand();

        
            
            
        


    }
    
}
